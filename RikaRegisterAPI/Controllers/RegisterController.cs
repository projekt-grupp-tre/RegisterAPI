using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

                        var response2 = await http.PostAsJsonAsync("https://verificationprovider.azurewebsites.net/api/GenerateVerificationCodeHttp?code=fpLuXfujgTLKWY17RYGFEkxcKFALp4JhmAfmsf91ZFnqAzFuA7oNhg%3D%3D", new { Email = model.Email });
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
