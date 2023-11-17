namespace SystemBox.API.Dtos
{
    public sealed class UsuarioDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public int Numero { get; set; }
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Token {  get; set; } = string.Empty;
        public string? Email {  get; set; } = string.Empty;
    }
}
