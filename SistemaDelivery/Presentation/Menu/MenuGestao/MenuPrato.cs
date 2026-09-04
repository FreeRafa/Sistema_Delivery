using SistemaDelivery.Modelo.Entidades;
using SistemaDelivery.Modelo.Interfaces;
using SistemaDelivery.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDelivery.Presentation.Menu.MenuGestao
{
    public class MenuPrato
    {
        private readonly PratoServico _pratoServico;

        public MenuPrato(PratoServico pratoServico)
        {
            _pratoServico = pratoServico;
        }

        public async Task ExibirMenuPratoAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Menu de Gestão de Pratos ===");
            Console.WriteLine("1. Adicionar Prato");
            Console.WriteLine("2. Atualizar Prato");
            Console.WriteLine("3. Remover Prato");
            Console.WriteLine("4. Listar Pratos");
            Console.WriteLine("5. Voltar ao Menu Principal");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();
            switch (opcao)
            {
                case "1":
                    await AdicionarPratoAsync();
                    break;
                case "2":
                    await AtualizarPratoAsync();
                    break;
                case "3":
                    await RemoverPratoAsync();
                    break;
                case "4":
                    await ListarPratosAsync();
                    break;
                case "5":
                    return; // Voltar ao menu principal
                default:
                    Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
            await ExibirMenuPratoAsync(); // Exibe o menu novamente após a ação
        }

        private async Task AdicionarPratoAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Adicionar Prato ===");

            Console.Write("Nome do Prato: ");
            var nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Preço: ");
            decimal.TryParse(Console.ReadLine(), out decimal preco);

            Console.Write("Id do Restaurante: ");
            int.TryParse(Console.ReadLine(), out int restauranteId);

            Console.Write("Disponível? (s/n): ");
            var disponivel = Console.ReadLine()?.Trim().ToLower() == "s";

            var prato = new Prato
            {
                Nome = nome,
                Preco = preco,
                RestauranteId = restauranteId,
                Disponivel = disponivel
            };

            try
            {
                await _pratoServico.AdicionarPratoAsync(prato);
                Console.WriteLine("Prato adicionado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar prato: {ex.Message}");
            }
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private async Task AtualizarPratoAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Atualizar Pratos ===");

            var pratos = await _pratoServico.ObterTodosPratosAsync();

            if (pratos.Count == 0)
            {
                Console.WriteLine("Não existem pratos cadastrados.");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Pratos cadastrados:");
            foreach (var p in pratos)
            {
                Console.WriteLine($"Id: {p.Id} - Nome: {p.Nome} (Preço: {p.Preco})");
            }

            Console.Write("\nDigite o Id do Prato a atualizar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido. Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Prato prato;
            try
            {
                prato = await _pratoServico.ObterPorIdAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }
        }

        private async Task RemoverPratoAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Remover Prato ===");
            Console.Write("Id do Prato: ");
            int.TryParse(Console.ReadLine(), out int id);
            try
            {
                await _pratoServico.RemoverPratoAsync(id);
                Console.WriteLine("Prato removido com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao remover prato: {ex.Message}");
            }
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private async Task ListarPratosAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Pratos ===");
            var pratos = await _pratoServico.ObterTodosPratosAsync();
            if (pratos.Count == 0)
            {
                Console.WriteLine("Nenhum prato encontrado.");
            }
            else
            {
                foreach (var prato in pratos)
                {
                    Console.WriteLine($"Id: {prato.Id}, Nome: {prato.Nome}, Preço: {prato.Preco}, RestauranteId: {prato.RestauranteId}, Disponível: {prato.Disponivel}");
                }
            }
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
