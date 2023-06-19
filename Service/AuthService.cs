using Microsoft.AspNetCore.Identity;
using SuPlaza.Compras.Pedidos.AuthAPI.Data;
using SuPlaza.Compras.Pedidos.AuthAPI.Models;
using SuPlaza.Compras.Pedidos.AuthAPI.Models.Dto;
using SuPlaza.Compras.Pedidos.AuthAPI.Service.IService;

namespace SuPlaza.Compras.Pedidos.AuthAPI.Service
{
    public class AuthService : IAuthService
    {

        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManaManager;


        public AuthService(AppDbContext dbContext, UserManager<ApplicationUser> userManaManager)
        {
            _dbContext = dbContext;
            _userManaManager = userManaManager;
        }

        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> Register(RegistrationRequestDto registrationRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
