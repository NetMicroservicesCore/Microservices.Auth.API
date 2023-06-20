﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuPlaza.Compras.Pedidos.AuthAPI.Models.Dto;
using SuPlaza.Compras.Pedidos.AuthAPI.Service.IService;

namespace SuPlaza.Compras.Pedidos.AuthAPI.Controllers
{
    [Route("api/[auth]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {

        private readonly IAuthService _authService;
        protected ResponseDto _response;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }


        [HttpPost("registro")]
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


    }
}
