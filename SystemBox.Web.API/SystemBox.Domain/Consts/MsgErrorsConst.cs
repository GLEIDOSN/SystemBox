namespace SystemBox.Domain.Consts
{
    public static class MsgErrorsConst
    {
        public static string ErroInternoServidor(string msg) => $"Ocorreu um erro interno no servidor. [{msg}]";
    }
}
