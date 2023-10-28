using SystemBox.Domain.Models;

namespace SystemBox.Data.Repositories
{
    public interface IUsuarioRepository
    {
        public Task<IEnumerable<Usuario>> GetAllAsync();
        public Task<Usuario?> GetByIdAsync(int id);
        public Task<Usuario?> GetByNomeUsuarioAsync(string nomeUsuario);
        public Task<Usuario> PostAsync(Usuario usuario);
        public Task UpdateAsync(int id, Usuario usuarioInput);
        public Task DeleteAsync(int id);
    }
}
