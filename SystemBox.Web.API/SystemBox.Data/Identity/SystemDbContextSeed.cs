using Microsoft.AspNetCore.Identity;
using SystemBox.Domain.Models;

namespace SystemBox.Data.Identity
{
    public class SystemDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<Usuario> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new Usuario
                {
                    Nome = "Gleidson",
                    Endereco = "Rua tal",
                    Numero = 30,
                    Complemento = "Próximo",
                    Bairro = "Centro",
                    Cidade = "Caucaia",
                    Estado = "Ceará",
                    CEP = "61610000",
                    Telefone = "8533421155",
                    Celular = "85986868686",
                    UserName = "Gleidson",
                    PasswordHash = "Pa$$w0rd",
                    Email = "gledison@hotmail.com"
                };

                await userManager.CreateAsync(user, user.PasswordHash);
            }
        }
    }
}
