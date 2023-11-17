using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using SystemBox.Domain.Consts;
using SystemBox.Domain.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SystemBox.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public UsuarioRepository(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<Usuario?> GetByIdAsync(string id)
        {
            return await _userManager.Users.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<Usuario?> GetByNomeUsuarioAsync(string nomeUsuario)
        {
            return await _userManager.Users.SingleOrDefaultAsync(x => x.UserName.ToLower() == nomeUsuario.ToLower());
        }

        public async Task<Usuario> PostAsync(Usuario usuario)
        {
            usuario.EmailConfirmed = true;
            try
            {
                var results = await _userManager.CreateAsync(usuario, usuario.PasswordHash);
                StringBuilder dadosConcatenados = new StringBuilder();

                if (!results.Succeeded)
                {
                    foreach (IdentityError erro in results.Errors)
                    {
                        dadosConcatenados.Append($"{erro.Description};");
                    }

                    if (dadosConcatenados.Length > 0)
                    {
                        dadosConcatenados.Length--; // Remove o último caractere
                    }

                    string resultadoFinal = dadosConcatenados.ToString();

                    throw new Exception(resultadoFinal);
                }

                await _userManager.SetLockoutEnabledAsync(usuario, false);

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception(MsgErrorsCrudConst.MsgErrorPost("Usuário", ex.Message));
            }
        }

        public async Task UpdateAsync(string id, Usuario usuarioInput)
        {
            try
            {
                var usuario = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Usuário não existe no banco de dados.");

                usuario.Nome = usuarioInput.Nome;
                usuario.UserName = usuarioInput.UserName;
                usuario.PasswordHash = usuarioInput.PasswordHash;

                var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                var result = await _userManager.ResetPasswordAsync(usuario, token, usuarioInput.PasswordHash);

                if (result.Succeeded)
                {
                    // Se a senha foi atualizada com sucesso, você pode optar por fazer login novamente se desejar
                    await _signInManager.SignInAsync(usuario, isPersistent: false);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MsgErrorsCrudConst.MsgErrorUpdate("Usuário", ex.Message));
            }
        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                var usuario = await _userManager.FindByIdAsync(id) ?? throw new Exception("Usuário não existe no banco de dados.");

                var result = await _userManager.DeleteAsync(usuario);
            }
            catch(Exception ex)
            {
                throw new Exception(MsgErrorsCrudConst.MsgErrorDelete("Usuário", ex.Message));
            }
        }

        public async Task<bool> CheckPasswordSignInAsync(Usuario usuario, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(usuario, password, false);          

            return result.Succeeded;
        }
    }
}
