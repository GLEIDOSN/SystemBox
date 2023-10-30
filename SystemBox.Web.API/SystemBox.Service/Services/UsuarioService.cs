using SystemBox.Data.Repositories;
using SystemBox.Domain.Models;
using static System.CustomExceptions.UsuariosCustomExceptions;

namespace SystemBox.Service.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id) ?? throw new UsuarioNaoExiste("Usuário não existe no banco de dados.");
            
            return usuario;
        }

        public async Task<Usuario> PostAsync(Usuario usuario)
        {
            var usuarioExiste = await _usuarioRepository.GetByNomeUsuarioAsync(usuario.NomeUsuario);

            if (usuarioExiste != null)
            {
                throw new UsuarioJaCadastradoException($"Nome de Usuário já cadastrado. [{usuario.NomeUsuario}]");
            }

            var usuarioCriado = await _usuarioRepository.PostAsync(usuario);

            return usuarioCriado;
        }

        public async Task UpdateAsync(int id, Usuario usuarioInput)
        {
            await _usuarioRepository.UpdateAsync(id, usuarioInput);
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id) ?? throw new UsuarioNaoExiste("Usuário não existe no banco de dados.");
            await _usuarioRepository.DeleteAsync(usuario.Id);
        }
    }
}
