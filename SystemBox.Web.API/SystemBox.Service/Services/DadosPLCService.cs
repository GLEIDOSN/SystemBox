using EasyModbus;
using Microsoft.Extensions.Configuration;

namespace SystemBox.Service.Services
{
    public class DadosPLCService : IDadosPLCService
    {
        private string _enderecoIP;
        private int _porta;

        private readonly ModbusClient _modbusClient;
        private readonly IConfiguration _configuration;

        public DadosPLCService(ModbusClient modbusClient, IConfiguration configuration)
        {
            _modbusClient = modbusClient;
            _configuration = configuration;

            _enderecoIP = _configuration["ClpConnection:ModbusIPAddress"];
            _porta = _configuration.GetValue<int>("ClpConnection:ModbusPort");
        }

        public void ConectarPLC(string enderecoIP, int porta = 502)
        {
            try
            {
                _modbusClient.Connect(enderecoIP, porta); // Conecta-se ao dispositivo Modbus TCP

                if (_modbusClient.Connected)
                {
                    Console.WriteLine("Conectado!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao tentar conectar.", ex.Message);
                throw;
            }
        }

        public int LerRegistroDeEntrada(int endereco)
        {

            ConectarPLC(_enderecoIP, _porta);
            if (_modbusClient.Connected)
            {
                return _modbusClient.ReadInputRegisters(endereco, 1)[0];
            }
            else
            {
                throw new Exception("Erro ao tentar conectar.");
            }
        }

        public void Dispose()
        {
            _modbusClient.Disconnect(); // Desconectar ao ser descartado
        }
    }
}
