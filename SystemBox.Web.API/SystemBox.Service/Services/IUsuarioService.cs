using SystemBox.Domain.Models;

namespace SystemBox.Service.Services
{
    public interface IUsuarioService
    {
        public Task<IEnumerable<Usuario>> GetAllAsync();
        public Task<Usuario> GetByIdAsync(int id);
        public Task<Usuario> PostAsync(Usuario usuario);
        public Task UpdateAsync(int id, Usuario usuarioInput);
        public Task DeleteAsync(int id);
    }
}
