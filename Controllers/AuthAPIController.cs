using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuPlaza.Compras.Pedidos.AuthAPI.Controllers
{
    [Route("api/[auth]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {


        [HttpPost("registro")]
        public async Task<IActionResult> Registro()
        {
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }


    }
}
