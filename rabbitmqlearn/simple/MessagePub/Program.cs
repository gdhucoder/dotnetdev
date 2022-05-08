using MassTransit;
using Polly;
using Polly.Bulkhead;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(new Uri("rabbitmq://localhost:8672"), h =>
        {
            h.Username("webapp");
            h.Password("123456");
            h.PublisherConfirmation = false;
        });
        cfg.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter("gdhu", true));
        // cfg.ConfigureEndpoints(ctx);

    });
    //x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    //{
    //    config.Host(new Uri("rabbitmq://localhost:8672"), h =>
    //    {
    //        h.Username("guest");
    //        h.Password("guest");
    //    });
    //}));
});



var bulk = Policy.BulkheadAsync<HttpResponseMessage>(
    maxParallelization:5,
    maxQueuingActions:1,
    onBulkheadRejectedAsync: context => Task.CompletedTask
    );
var message = new HttpResponseMessage()
{
    Content = new StringContent("{}"),
};

var fallback = Policy<HttpResponseMessage>.Handle<BulkheadRejectedException>().FallbackAsync(message);
var fallbackbulk = Policy.WrapAsync(fallback, bulk);

builder.Services.AddHttpClient("httpv4").AddPolicyHandler(fallbackbulk);

// builder.Services.AddMassTransitHostedService();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
