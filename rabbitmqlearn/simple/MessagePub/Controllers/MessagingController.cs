using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Model;

namespace MessagePub.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MessagingController : ControllerBase
    {

        private readonly ILogger<MessagingController> _logger;

        private readonly IBus _busService;

        public MessagingController(ILogger<MessagingController> logger, IBus busService)
        {
            _logger = logger;
            _busService = busService;
        }

        
        [HttpGet]
        public async Task<string> PageStatics()
        {
            var news = new WebVisitMsg
            {
                content = "Web visit message: " + "@" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            Uri uri = new Uri("rabbitmq://localhost:8672/newsqueue");
            var endPoint = await _busService.GetSendEndpoint(uri);
            await endPoint.Send(news);
            _logger.LogInformation($"Publisher Message Send {news.content}");
            return "ok";
        }

        [HttpGet]
        public async Task<string> RedisSync()
        {
            var redisSyncMsg = new RedisSyncMsg
            {
                content = "Redis sync message: " + "@" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            Uri uri = new Uri("rabbitmq://localhost:8672/redissync");
            var endPoint = await _busService.GetSendEndpoint(uri);
            await endPoint.Send(redisSyncMsg);
            _logger.LogInformation($"Publisher Message Send {redisSyncMsg.content}");
            return "ok";
        }

    }
}