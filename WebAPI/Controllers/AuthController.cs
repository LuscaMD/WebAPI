using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.Services;
using WebAPI.Domain.Model;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if(username == "lucas" && password == "123456")
            {
                var token = TokenService.GenerateToken(new Employee("lucas", 20, ""));
                return Ok(token);
            }

            return BadRequest("Username or password invalid");
        }
    }
}
