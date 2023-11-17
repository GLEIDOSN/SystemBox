using SystemBox.Data.Identity;
using SystemBox.Domain.Models;

namespace SystemBox.Service.Services
{
    public interface ITokenService
    {
        public string CreateToken(Usuario user);
    }
}
