using Microsoft.EntityFrameworkCore;
using SystemBox.Domain.Consts;
using SystemBox.Domain.Models;

namespace SystemBox.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SystemContext _context;

        public UsuarioRepository(SystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Usuario?> GetByNomeUsuarioAsync(string nomeUsuario)
        {
            return await _context.Usuarios.SingleOrDefaultAsync(x => x.NomeUsuario.ToLower() == nomeUsuario.ToLower());
        }

        public async Task<Usuario> PostAsync(Usuario usuario)
        {
            try
            {
                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception(MsgErrorsCrudConst.MsgErrorPost("Usuário", ex.Message));
            }
        }

        public async Task UpdateAsync(int id, Usuario usuarioInput)
        {
            try
            {
                var usuario = await _context.Usuarios.SingleOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Usuário não existe no banco de dados.");

                usuario.Nome = usuarioInput.Nome;
                usuario.NomeUsuario = usuarioInput.NomeUsuario;
                usuario.Senha = usuarioInput.Senha;

                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(MsgErrorsCrudConst.MsgErrorUpdate("Usuário", ex.Message));
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.SingleOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Usuário não existe no banco de dados.");

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(MsgErrorsCrudConst.MsgErrorDelete("Usuário", ex.Message));
            }
        }
    }
}
