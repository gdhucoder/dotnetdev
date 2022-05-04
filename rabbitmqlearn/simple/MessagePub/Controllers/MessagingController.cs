using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Model;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace MessagePub.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MessagingController : ControllerBase
    {

        private readonly ILogger<MessagingController> _logger;

        private readonly IBus _busService;

        private readonly IPublishEndpoint _publishEndpoint;

        private static ConcurrentQueue<WebVisitMsg> conque = new ConcurrentQueue<WebVisitMsg>();
        private static List<WebVisitMsg> list = new List<WebVisitMsg>();

        public MessagingController(ILogger<MessagingController> logger, IBus busService, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _busService = busService;
            _publishEndpoint = publishEndpoint;
            for (int i = 0; i < 10; i++)
            {
                var news = new WebVisitMsg
                {
                    content = "Web visit message: " + "@" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                list.Add(news);
            }
        }

        [HttpGet]
        public async Task<string> EnqueWebVisist()
        {

            var news = new WebVisitMsg
            {
                content = "Web visit message: " + "@" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            conque.Enqueue(news);
            if (conque.Count > 1000)
            {
                _logger.LogInformation($"concurrentqueue is purging.... {conque.Count}");
                await PurgeQueue(_publishEndpoint);
            }

            return "ok";
        }

        [HttpGet]
        public async Task<string> PublishWebVisitList()
        {
            
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 10; i++)
            {
                await _publishEndpoint.Publish(list[i]);
            }
            _logger.LogInformation($"time is {sw.Elapsed.Milliseconds}");
            return "ok";
        }


        [HttpGet]
        public async Task<string> WebVisitStaticsEndpoint()
        {
            var news = new WebVisitMsg
            {
                content = "Web visit message: " + "@" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            await _publishEndpoint.Publish(news);
            // _logger.LogInformation($"Publisher Message Send {news.content}");
            return "ok";
        }

        [HttpGet]
        public async Task<string> WebVisitStaticsEndpoint2()
        {
            var news = new WebVisitMsg
            {
                content = "Web visit message: " + "@" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            for (int i = 0; i < 10000; i++)
            {
                await _publishEndpoint.Publish(news);
            }
            
            // _logger.LogInformation($"Publisher Message Send {news.content}");
            return "ok";
        }


        /// <summary>
        /// publish single msg
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost]
        public async Task<string> WebVisitStatics(WebVisitMsg msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException(nameof(msg));
            }

            await _publishEndpoint.Publish(msg);

            _logger.LogInformation($"Publisher Message Send {msg.content}");

            return "ok";
        }

        /// <summary>
        /// publish WebVisitMsg list
        /// </summary>
        /// <param name="visitMsgs"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        [HttpPost]
        public async Task<string> WebVisitStaticsMsgs(List<WebVisitMsg> visitMsgs)
        {
            if(visitMsgs == null)
            {
                throw new ArgumentNullException(nameof(visitMsgs));
            }

            if(visitMsgs.Count == 0)
            {
                throw new ArgumentException("list is empty!");
            }

            foreach (var msg in visitMsgs)
            {
                await _publishEndpoint.Publish(msg);
            }

            _logger.LogInformation($"Publisher Message Send WebVisitMsgs total: {visitMsgs.Count} ");

            return "ok";
        }

        /// <summary>
        /// publish single msg
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost]
        public async Task<string> RedisSync(RedisSyncMsg msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException(nameof(msg));
            }

            var redisSyncMsg = new RedisSyncMsg
            {
                baseId = "",
                type = "Redis sync message: " + "@" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            await _publishEndpoint.Publish(msg);

            _logger.LogInformation($"Publisher Message Send {redisSyncMsg.type}, {redisSyncMsg.baseId}");

            return "ok";
        }

        /// <summary>
        /// publish list msgs
        /// </summary>
        /// <param name="msgs"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        [HttpPost]
        public async Task<string> RedisSyncMsgs(List<RedisSyncMsg> msgs)
        {
            if (msgs == null)
            {
                throw new ArgumentNullException(nameof(msgs));
            }

            if (msgs.Count == 0)
            {
                throw new ArgumentException("list is empty!");
            }

            foreach (var msg in msgs)
            {
                await _publishEndpoint.Publish(msg);
            }

            _logger.LogInformation($"Publisher Message Send RedisSyncMsgs total: {msgs.Count} ");

            return "ok";
        }


        [HttpGet]
        public async Task<string> Test()
        {
            return "ok";
        }

        private async Task PurgeQueue(IPublishEndpoint _publishEndpoint)
        {
            int count = 1000;
            while (count > 0)
            {
                WebVisitMsg msg;
                while (conque.TryDequeue(out msg))
                {
                    await _publishEndpoint.Publish(msg);
                    count--;
                }
            }
        }

    }
}