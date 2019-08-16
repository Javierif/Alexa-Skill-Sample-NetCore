using System.IO;
using System.Threading.Tasks;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Alexa.Skill.MySkill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlexaController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                string json = reader.ReadToEnd();
                var skillRequest = JsonConvert.DeserializeObject<SkillRequest>(json);

                var requestType = skillRequest.GetRequestType();

                SkillResponse response = null;

                if (requestType == typeof(LaunchRequest))
                {
                    response = ResponseBuilder.Tell("Hola soy la aplicación MySkill");
                    response.Response.ShouldEndSession = false;
                }
                else if (requestType == typeof(IntentRequest))
                {
                    var intentRequest = skillRequest.Request as IntentRequest;

                    if (intentRequest.Intent.Name == "Saludo")
                    {
                        response = ResponseBuilder.Tell("Hola a ti también");
                    }
                }

                return new OkObjectResult(response);
            }
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<string> root()
        {
            return "im alive";
        }

    }
}
