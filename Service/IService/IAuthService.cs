using SuPlaza.Compras.Pedidos.AuthAPI.Models.Dto;

namespace SuPlaza.Compras.Pedidos.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
