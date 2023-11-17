namespace SystemBox.Domain.Consts
{
    public static class UsuariosConsts
    {
        public const string MsgUsuarioNaoExiste = "Usuário não existe no banco de dados.";
        public static string MsgUsuarioJaCadastrado(string msg) => $"Nome de Usuário já cadastrado. [{msg}]";
        public static string MsgUsuarioSenhaInvalida = "Senha do Usuário inválida.";
    }
}
