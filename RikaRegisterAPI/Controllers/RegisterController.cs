using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
