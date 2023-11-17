namespace SystemBox.Service.Services
{
    public interface IDadosPLCService
    {
        public void ConectarPLC(string enderecoIP, int porta = 502);
        public int LerRegistroDeEntrada(int endereco);
    }
}
