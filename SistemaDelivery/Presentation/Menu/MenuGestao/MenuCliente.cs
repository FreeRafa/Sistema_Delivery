using SistemaDelivery.Modelo.Entidades;
using SistemaDelivery.Modelo.Interfaces;
using SistemaDelivery.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDelivery.Presentation.Menu.MenuGestao
{
    public class MenuCliente
    {
        private readonly ClienteServico _clienteServico;

        public MenuCliente(ClienteServico clienteServico)
        {
            _clienteServico = clienteServico;
        }

        public async Task ExibirMenuClienteAsync()
        {
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("Menu Cliente");
                Console.WriteLine("1. Adicionar um cliente");
                Console.WriteLine("2. Atualizar cliente");
                Console.WriteLine("3. Remover cliente");
                Console.WriteLine("4. Listar todos os clientes");
                Console.WriteLine("5. Sair");
                Console.Write("Escolha uma opção: ");

                var opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        await AdicionarClienteAsync();
                        break;

                    case "2":
                        await AtualizarClienteAsync();
                        break;

                    case "3":
                        await RemoverClienteAsync();
                        break;

                    case "4":
                        await ListarClientesAsync();
                        break;

                    case "5":
                        continuar = false;
                        break;
                    default:

                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        private async Task AdicionarClienteAsync()
        {
            Console.Write("Digite o nome do cliente: ");
            var nome = Console.ReadLine();

            Console.Write("Digite o NIF do cliente: ");
            var nif = Console.ReadLine();
            
            Console.Write("Digite o número de telemóvel: ");
            var telemovel = Console.ReadLine();

            Console.Write("Digite o email do cliente: ");
            var email = Console.ReadLine();

            var cliente = new Cliente { Nome = nome, Nif = nif, Telemovel = telemovel, Email = email };
            try
            {
                await _clienteServico.AdicionarClienteAsync(cliente);
                Console.WriteLine("Cliente adicionado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar cliente: {ex.Message}");
            }

        }
    }
}
