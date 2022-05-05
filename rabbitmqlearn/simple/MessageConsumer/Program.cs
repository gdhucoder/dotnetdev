using MassTransit;
using MessageConsumer;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PageVisitConsumer>();
    x.AddConsumer<RedisSyncConsumer>();
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(new Uri("rabbitmq://localhost:8672"), h =>
        {
            h.Username("webapp");
            h.Password("123456");
        });
        cfg.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter("gdhu", true));
        // cfg.ConfigureEndpoints(ctx);

    });
    //x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cur =>
    //{
    //    cur.Host(new Uri("rabbitmq://localhost:8672"), h =>
    //    {
    //        h.Username("guest");
    //        h.Password("guest");
    //    });
    //    cfg.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter("ttt", true));
    //    //cur.ReceiveEndpoint("newsqueue", oq =>
    //    //{
    //    //    oq.PrefetchCount = 6;
    //    //    oq.UseMessageRetry(r => r.Interval(2, 100));
    //    //    oq.ConfigureConsumer<PageVisitConsumer>(provider);
    //    //});
    //    //cur.ReceiveEndpoint("redissync", oq =>
    //    //{
    //    //    oq.PrefetchCount = 6;
    //    //    oq.UseMessageRetry(r => r.Interval(2, 100));
    //    //    oq.ConfigureConsumer<RedisSyncConsumer>(provider);
    //    //});
    //}));
});
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
