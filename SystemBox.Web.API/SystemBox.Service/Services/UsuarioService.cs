using SystemBox.Data.Repositories;
using SystemBox.Domain.Consts;
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

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<Usuario> GetByIdAsync(string id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id) ?? throw new UsuarioNaoExiste(UsuariosConsts.MsgUsuarioNaoExiste);
            
            return usuario;
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(email);

            return usuario;
        }

        public async Task<Usuario?> GetByNomeUsuarioAsync(string nomeUsuario)
        {
            var usuario = await _usuarioRepository.GetByNomeUsuarioAsync(nomeUsuario);

            return usuario;
        }

        public async Task<Usuario> PostAsync(Usuario usuario)
        {
            var usuarioExiste = await _usuarioRepository.GetByNomeUsuarioAsync(usuario.UserName);

            if (usuarioExiste != null)
            {
                throw new UsuarioJaCadastradoException($"{UsuariosConsts.MsgUsuarioJaCadastrado(usuario.UserName)}");
            }

            var usuarioCriado = await _usuarioRepository.PostAsync(usuario);

            return usuarioCriado;
        }

        public async Task UpdateAsync(string id, Usuario usuarioInput)
        {
            await _usuarioRepository.UpdateAsync(id, usuarioInput);
        }

        public async Task DeleteAsync(string id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id) ?? throw new UsuarioNaoExiste(UsuariosConsts.MsgUsuarioNaoExiste);
            await _usuarioRepository.DeleteAsync(usuario.Id);
        }

        public async Task<bool> CheckPasswordSignInAsync(Usuario usuario, string password)
        {
            return await _usuarioRepository.CheckPasswordSignInAsync(usuario, password);
        }
    }
}
