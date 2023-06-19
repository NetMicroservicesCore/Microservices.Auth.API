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
        private readonly RoleManager<IdentityRole> _roleManager;



        public AuthService(AppDbContext dbContext, UserManager<ApplicationUser> userManaManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManaManager = userManaManager;
            _roleManager = roleManager;

        }

        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail =registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber
            };
            try
            {
                var result = await _userManaManager.CreateAsync(user,registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userDtoReturn = _dbContext.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);

                    UserDto userDto = new()
                    {
                        Email = userDtoReturn.Email,
                        Id= userDtoReturn.Id,
                        Name = userDtoReturn.Name,
                        PhoneNumber = userDtoReturn.PhoneNumber
                    };
                    return userDto;
                }
            }
            catch (Exception ex)
            {

               string message = ex.Message;
            }
            return new UserDto();
        }
    }
}
