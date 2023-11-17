namespace SystemBox.API.Dtos
{
    public sealed class LoginDto
    {
        public string NomeUsuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
