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
                Console.Clear();
                Console.WriteLine("Menu Cliente");
                Console.WriteLine("1. Adicionar um cliente");
                Console.WriteLine("2. Atualizar cliente");
                Console.WriteLine("3. Remover cliente");
                Console.WriteLine("4. Listar todos os clientes");
                Console.WriteLine("0. Voltar ao Menu Principal");
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

                    case "0":
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
            Console.Clear();

            Console.Write("Digite o nome do cliente: ");
            var nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite o NIF do cliente: ");
            var nif = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite o número de telemóvel: ");
            var telemovel = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite o email do cliente: ");
            var email = Console.ReadLine() ?? string.Empty;

            try
            {
                var cliente = new Cliente
                {
                    Nome = nome,
                    Nif = nif,
                    Telemovel = telemovel,
                    Email = email
                };

                await _clienteServico.AdicionarClienteAsync(cliente);
                Console.WriteLine("Cliente adicionado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar cliente: {ex.Message}");
            }

            AguardarContinuacao();
        }

        private async Task AtualizarClienteAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Atualizar Restaurante ===");

            var clientes = await _clienteServico.ObterTodosClienteAsync();

            if (clientes.Count == 0)
            {
                Console.WriteLine("Não existem clientes cadastrados.");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Clientes cadastrados:");
            foreach (var c in clientes)
            {
                Console.WriteLine($"Id: {c.Id} - Nome: {c.Nome} (NIF: {c.Nif})");
            }

            Console.Write("\nDigite o Id do Cliente a atualizar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido. Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Cliente cliente;
            try
            {
                cliente = await _clienteServico.ObterPorIdAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }
        }

        private async Task RemoverClienteAsync()
        {
            Console.Clear();
            Console.Write("Digite o ID do cliente a remover: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                AguardarContinuacao();
                return;
            }

            try
            {
                await _clienteServico.RemoverClienteAsync(id);
                Console.WriteLine("Cliente removido com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao remover cliente: {ex.Message}");
            }

            AguardarContinuacao();
        }

        private async Task ListarClientesAsync()
        {
            Console.Clear();

            try
            {
                var clientes = await _clienteServico.ObterTodosClienteAsync();

                Console.WriteLine("=== Lista de Clientes ===");

                if (clientes.Count == 0)
                {
                    Console.WriteLine("Não existem clientes registados.");
                }
                else
                {
                    foreach (var cliente in clientes)
                    {
                        Console.WriteLine(
                            $"ID: {cliente.Id}, Nome: {cliente.Nome}, " +
                            $"NIF: {cliente.Nif}, Telemóvel: {cliente.Telemovel}, " +
                            $"Email: {cliente.Email}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar clientes: {ex.Message}");
            }

            AguardarContinuacao();
        }

        private static void AguardarContinuacao()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey(true);
        }
    }
}
