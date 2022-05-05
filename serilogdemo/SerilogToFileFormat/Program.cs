using Serilog;
using Serilog.Events;
using System;

namespace SerilogToFileFormat
{
    class Program
    {
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                // 自定义日志格式
                .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(WebVisitMessage)
                .WriteTo.File("visitmessages.txt", LogEventLevel.Verbose, "{Timestamp:yyyy-MM-dd HH:mm:ss.fff},{Message:lj}{NewLine}"))
                .WriteTo.File("applog.txt")
                .CreateLogger();

            Log.Information("Hello, world!");

            Log.Information("go");
            Log.ForContext("data", "12345").Information("Small packet");
            Log.ForContext("data", "1234567890987654321").Information("Big packet");

            var msg = "01023234234234234,userABCD,adsfasfdsadf";
            // 记录日志
            Log.ForContext("visit", msg).Information(msg);

            int a = 10, b = 0;
            try
            {
                Log.Debug("Dividing {A} by {B}", a, b);
                Console.WriteLine(a / b);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Something went wrong");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static bool WebVisitMessage(LogEvent le)
        {
            return le.Properties.ContainsKey("visit");
        }
    }

}
