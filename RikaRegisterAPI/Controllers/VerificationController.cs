using Data.Models;
using Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace RikaRegisterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController(VerificationServices verificationServices) : Controller
    {
        private readonly VerificationServices _verificationServices = verificationServices;

        [HttpPost]
        public async Task<IActionResult> VerifyCode([FromBody] VerificationEmailModel verificationEmailModel)
        {
            
            if (ModelState.IsValid)
            {
                var result = await _verificationServices.VerifyCode(verificationEmailModel.Email);
                if (result)
                {
                    return Ok();
                }     
            }

            return BadRequest();
        }   
    }   
}
