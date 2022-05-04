using MassTransit;
using Shared.Model;

namespace MessageConsumer
{
    public class RedisSyncConsumer : IConsumer<RedisSyncMsg>
    {
        public async Task Consume(ConsumeContext<RedisSyncMsg> context)
        {
            var obj = context.Message;
            Console.WriteLine($"RedisSyncMsg Message Received:{obj.baseId},{obj.type} \n at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }
    }
}
