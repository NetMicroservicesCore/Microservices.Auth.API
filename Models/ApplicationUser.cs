using Microsoft.AspNetCore.Identity;

namespace SuPlaza.Compras.Pedidos.AuthAPI.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
    }
}
