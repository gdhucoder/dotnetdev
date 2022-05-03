using Microsoft.AspNetCore.Mvc;

namespace MessageConsumer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DummyController : ControllerBase
    {
       
        private readonly ILogger<DummyController> _logger;

        public DummyController(ILogger<DummyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string test()
        {
            return "ok";
        }
    }
}