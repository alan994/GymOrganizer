using Microsoft.AspNetCore.Mvc;
using Utils.Queue;

namespace Web.Controllers
{
    [Route("api/mq")]
    public class RabbitMQController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {            
            return Ok();   
        }
        
    }
}
