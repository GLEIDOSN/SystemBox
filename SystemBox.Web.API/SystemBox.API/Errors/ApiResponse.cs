namespace SystemBox.API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }

        public string Message { get; set; }

        public string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Sua solicitação contém erros e não pode ser processada no momento.",
                401 => "Você não possui autorização para acessar este recurso. Por favor, faça login ou forneça credenciais válidas.",
                404 => "O recurso que você procura não pôde ser encontrado no servidor.",
                500 => "Ocorreu um erro interno no servidor. Entre em contato com o administrador do sistema para resolver o problema.",
                _ => null
            };
        }
    }
}
