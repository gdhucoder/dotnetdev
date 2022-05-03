using MassTransit;
using Shared.Model;

namespace MessageConsumer
{
    public class PageVisitConsumer : IConsumer<WebVisitMsg>
    {
        /// <summary>
        /// define consume logic
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<WebVisitMsg> context)
        {
            var obj = context.Message;
            Console.WriteLine($"WebVisitStatics Message Received:{obj.content} \n at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }
    }
}
