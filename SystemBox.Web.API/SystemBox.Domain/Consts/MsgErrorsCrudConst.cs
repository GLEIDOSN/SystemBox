namespace SystemBox.Domain.Consts
{
    public static class MsgErrorsCrudConst
    {
        public static string MsgErrorPost(string NomeRegistro, string msg) => $"Erro ao tentar criar {NomeRegistro}: {msg}";
        public static string MsgErrorUpdate(string NomeRegistro, string msg) => $"Erro ao tentar atualizar {NomeRegistro}: {msg}";
        public static string MsgErrorDelete(string NomeRegistro, string msg) => $"Erro ao tentar deletar {NomeRegistro}: {msg}";
    }
}
