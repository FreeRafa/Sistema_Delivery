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
            var nome = Console.ReadLine() ?? string.Empty; 

            Console.Write("Digite o NIF do cliente: ");
            var nif = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite o número de telemóvel: ");
            var telemovel = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite o email do cliente: ");
            var email = Console.ReadLine() ?? string.Empty;

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

        private async Task AtualizarClienteAsync()
        {
            Console.Write("Digite o ID do cliente a atualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var clienteExistente = await _clienteServico.ObterPorIdAsync(id);
                if (clienteExistente != null)
                {
                    Console.Write("Digite o novo nome do cliente: ");
                    clienteExistente.Nome = Console.ReadLine() ?? clienteExistente.Nome;

                    Console.Write("Digite o novo NIF do cliente: ");
                    clienteExistente.Nif = Console.ReadLine() ?? clienteExistente.Nif;

                    Console.Write("Digite o novo número de telemóvel: ");
                    clienteExistente.Telemovel = Console.ReadLine() ?? clienteExistente.Telemovel;

                    Console.Write("Digite o novo email do cliente: ");
                    clienteExistente.Email = Console.ReadLine() ?? clienteExistente.Email;
                    try
                    {
                        await _clienteServico.AtualizarClienteAsync(clienteExistente);
                        Console.WriteLine("Cliente atualizado com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao atualizar cliente: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Cliente não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        private async Task RemoverClienteAsync()
        {
            Console.Write("Digite o ID do cliente a remover: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    await _clienteServico.RemoverClienteAsync(id);
                    Console.WriteLine("Cliente removido com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao remover cliente: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        private async Task ListarClientesAsync()
        {
            try
            {
                var clientes = await _clienteServico.ObterTodosClienteAsync();
                Console.WriteLine("Lista de Clientes:");
                foreach (var cliente in clientes)
                {
                    Console.WriteLine($"ID: {cliente.Id}, Nome: {cliente.Nome}, NIF: {cliente.Nif}, Telemóvel: {cliente.Telemovel}, Email: {cliente.Email}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar clientes: {ex.Message}");
            }
        }
    }
}
