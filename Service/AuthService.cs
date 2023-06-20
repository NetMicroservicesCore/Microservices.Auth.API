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
        private IJwtTokenGenerator _jwtTokenGenerator;


        public AuthService(AppDbContext dbContext, UserManager<ApplicationUser> userManaManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _dbContext = dbContext;
            _userManaManager = userManaManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null) {
                if(_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManaManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;

        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            bool isValid = await _userManaManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (user == null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }
            //password encontrado
            var token = _jwtTokenGenerator.GenerateToken(user);

            UserDto userDto = new()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };
            return loginResponseDto;


        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
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
                        Id = userDtoReturn.Id,
                        Name = userDtoReturn.Name,
                        PhoneNumber = userDtoReturn.PhoneNumber
                    };
                    return string.Empty;
                }
                else {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {

               string message = ex.Message;
            }
            return "Ocurrio un error al registrar los datos.";
        }
    }
}
