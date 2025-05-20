using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MessageSender.Controllers
{
    [ApiController]
    [Route("/")]
    public class SendController : ControllerBase
    {
        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] string msg)
        {
            using var client = new HttpClient();

            // Kubernetes-Service-Name des Empfänger
            var response = await client.PostAsync(
                "http://message-receiver-service/message",
                new StringContent($"\"{msg}\"", Encoding.UTF8, "application/json")
            );

            var result = await response.Content.ReadAsStringAsync();
            return Ok(result);
        }
    }
}
