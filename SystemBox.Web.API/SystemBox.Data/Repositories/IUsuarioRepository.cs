using SystemBox.Domain.Models;

namespace SystemBox.Data.Repositories
{
    public interface IUsuarioRepository
    {
        public Task<List<Usuario>> GetAllAsync();
        public Task<Usuario?> GetByIdAsync(string id);
        public Task<Usuario?> GetByNomeUsuarioAsync(string nomeUsuario);
        public Task<Usuario?> GetByEmailAsync(string email);
        public Task<Usuario> PostAsync(Usuario usuario);
        public Task UpdateAsync(string id, Usuario usuarioInput);
        public Task DeleteAsync(string id);
        public Task<bool> CheckPasswordSignInAsync(Usuario usuario, string password);
    }
}
