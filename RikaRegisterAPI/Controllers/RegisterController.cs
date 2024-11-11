using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace RikaRegisterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController(UserRepository userRepository, IConfiguration configuration) : ControllerBase
    {
        private readonly UserRepository _userRepository = userRepository;
        private readonly IConfiguration _configuration = configuration;

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
                        var response = await http.PostAsync("https://verification-rika.azurewebsites.net/api/GenerateVerificationCodeHttp?code=BItffmctv-BTFfGNf9NM61EP5Mz1AVueZv_l54lDiED8AzFuZoyorg%3D%3D", content);

                        var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new { Email = model.Email })));

                        var queueClient = new QueueClient("Endpoint=sb://sb-emailprovider-v2.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=sBIcy5Zaw85JUZSKVh07clpqUEGvjXDoh+ASbFhN8eg=", "verification_request");
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
