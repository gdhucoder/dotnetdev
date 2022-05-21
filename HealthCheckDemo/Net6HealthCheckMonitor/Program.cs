using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks()
    .AddCheck("DataBase", ()=>
        HealthCheckResult.Healthy("data base good"), new[] { "service" }
    )
    .AddDiskStorageHealthCheck(s => s.AddDrive("C:\\", 1024)) // 1024 MB (1 GB) free minimum
    .AddProcessAllocatedMemoryHealthCheck(512)
    .AddProcessHealthCheck("ProcessName", p => p.Length > 0)
    .AddUrlGroup(new Uri("https://localhost:7134/weatherforecast"), "Example endpoint")
    .AddUrlGroup(new Uri("https://www.baidu.com"), "首页", tags: new string[] { "remote" })
    .AddUrlGroup(new Uri("https://www.baidu.com1"), "百度1", tags: new string[] { "remote" })
    ;
;

/// <summary>
/// partation
/// 磁盘
/// https://github.com/dotnet/runtime/issues/26081分区
/// </summary>
var free = new DriveInfo("D:\\myarticles\\myknowledgebase").AvailableFreeSpace;
Console.WriteLine(free/1024/1024);

builder.Services.AddHealthChecksUI().AddInMemoryStorage();

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

app.MapHealthChecksUI(setup =>

{
    setup.UIPath = "/health-ui"; // this is ui path in your browser
    setup.AddCustomStylesheet("dotnet.css");

});
app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});



app.Run();
