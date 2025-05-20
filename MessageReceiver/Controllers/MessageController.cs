using Microsoft.AspNetCore.Mvc;

namespace MessageReceiver.Controllers
{
    [ApiController]
    [Route("/")]
    public class MessageController : ControllerBase
    {
        private static string _message = "Noch keine Nachricht";

        [HttpPost("message")]
        public IActionResult SetMessage([FromBody] string message)
        {
            _message = message;
            return Ok("Nachricht gespeichert");
        }

        [HttpGet("message")]
        public IActionResult GetMessage()
        {
            return Ok(_message);
        }
    }
}