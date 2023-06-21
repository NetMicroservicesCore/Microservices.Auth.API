using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuPlaza.Compras.Pedidos.AuthAPI.Models.Dto;
using SuPlaza.Compras.Pedidos.AuthAPI.Service.IService;

namespace SuPlaza.Compras.Pedidos.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {

        private readonly IAuthService _authService;
        protected ResponseDto _response;

        public AuthApiController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }


        [HttpPost("register")]
        public async Task<IActionResult> Registro([FromBody] RegistrationRequestDto model)
        {
            var message = await _authService.Register(model);
            if (!string.IsNullOrEmpty(message))
            {
                _response.IsSuccess = false;
                _response.Message = message;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "El nombre de usuario o el password es incorrecto";
                return BadRequest(_response);
            }
            _response.Data = loginResponse;
            return Ok(_response);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var rolResponse = await _authService.AssignRole(model.Email,model.Role.ToUpper());
            if (!rolResponse)
            {
                _response.IsSuccess = false;
                _response.Message = "La asignación del rol fallo";
                return BadRequest(_response);
            }
            
            return Ok(_response);
        }

    }
}
