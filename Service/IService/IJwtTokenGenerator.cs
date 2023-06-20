using SuPlaza.Compras.Pedidos.AuthAPI.Models;

namespace SuPlaza.Compras.Pedidos.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
