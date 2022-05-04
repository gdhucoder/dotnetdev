using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(new Uri("rabbitmq://localhost:8672"), h =>
        {
            h.Username("guest");
            h.Password("guest");
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
