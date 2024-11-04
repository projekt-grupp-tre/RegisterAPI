using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace RikaRegisterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController(UserRepository userRepository) : ControllerBase
    {
        private readonly UserRepository _userRepository = userRepository;

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] SignUpModel model) 
        {
            if (model != null)
            {
                StatusModel result = new StatusModel();

                try
                {
                    result = await _userRepository.AddUserToDatabaseAsync(model);

                    if (result.StatusCode == 201)
                    {
                        using var http = new HttpClient();
                        var content = new StringContent(JsonConvert.SerializeObject(new { Email = model.Email }), Encoding.UTF8, "application/json");
                        var response = await http.PostAsync("https://verificationprovider.azurewebsites.net/api/GenerateVerificationCodeHttp?code=fpLuXfujgTLKWY17RYGFEkxcKFALp4JhmAfmsf91ZFnqAzFuA7oNhg%3D%3D", content);

                        var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new { Email = model.Email })));

                        var queueClient = new QueueClient("Endpoint=sb://sb-emailprovider.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=QjQqQwMnKcdPepvC5Xh5Kc7ohPzEF/7hx+ASbAbHsVo=", "verification_request");
                        await queueClient.SendAsync(message);

                        return Created(result.Message, result.StatusCode);
                    }
                    else if (result.StatusCode == 409)
                    {
                        return Conflict(result.Message);
                    }
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); }
                
                return new ObjectResult(result);
            }

            return BadRequest();
        }
    }
}
