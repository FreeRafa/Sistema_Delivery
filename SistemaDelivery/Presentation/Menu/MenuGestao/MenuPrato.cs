using SistemaDelivery.Modelo.Entidades;
using SistemaDelivery.Modelo.Interfaces;
using SistemaDelivery.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace SistemaDelivery.Presentation.Menu.MenuGestao
{
    public class MenuPrato
    {
        private readonly PratoServico _pratoServico;
        private readonly RestauranteServico _restauranteServico;

        public MenuPrato(PratoServico pratoServico, RestauranteServico restauranteServico)
        {
            _pratoServico = pratoServico;
            _restauranteServico = restauranteServico;
        }

        public async Task ExibirMenuPratoAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Menu de Gestão de Pratos ===");
            Console.WriteLine("1. Adicionar Prato");
            Console.WriteLine("2. Atualizar Prato");
            Console.WriteLine("3. Remover Prato");
            Console.WriteLine("4. Listar Pratos");
            Console.WriteLine("0. Voltar ao Menu Principal");
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
                case "0":
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

            try
            {
                var restaurantes = await _restauranteServico.ObterTodosRestaurantesAsync();

                if (restaurantes.Count == 0)
                {
                    Console.WriteLine("Não existem restaurantes registados.");
                    Console.WriteLine("Registe um restaurante antes de adicionar um prato.");
                    Console.ReadKey(true);
                    return;
                }

                Console.WriteLine("\n=== Restaurantes disponíveis ===");

                foreach (var restaurante in restaurantes)
                {
                    Console.WriteLine(
                        $"ID: {restaurante.Id} | " +
                        $"Nome: {restaurante.Nome} | " +
                        $"Categoria: {restaurante.Categoria}");
                }

                Console.Write("\nNome do prato: ");
                var nome = Console.ReadLine() ?? string.Empty;

                Console.Write("Preço: ");
                var textoPreco = Console.ReadLine();

                if (!decimal.TryParse(
                        textoPreco,
                        NumberStyles.Number,
                        CultureInfo.InvariantCulture,
                        out decimal preco))
                {
                    Console.WriteLine("Preço inválido. Exemplo válido: 7.50");
                    Console.ReadKey(true);
                    return;
                }

                Console.Write("ID do restaurante: ");
                if (!int.TryParse(Console.ReadLine(), out int restauranteId))
                {
                    Console.WriteLine("ID do restaurante inválido.");
                    Console.ReadKey(true);
                    return;
                }

                Console.Write("Disponível? (s/n): ");
                var disponivel = Console.ReadLine()?.Trim().ToLower() == "s";

                var prato = new Prato
                {
                    Nome = nome,
                    Preco = preco,
                    RestauranteId = restauranteId,
                    Disponivel = disponivel
                };

                await _pratoServico.AdicionarPratoAsync(prato);
                Console.WriteLine("Prato adicionado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar prato: {ex.Message}");
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey(true);
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
                    Console.WriteLine($"Id: {prato.Id}, " +
                        $"Nome: {prato.Nome}, " +
                        $"Preço: {prato.Preco}, " +
                        $"Restaurante Nome: {prato.Restaurante?.Nome}, " +
                        $"Disponível: {prato.Disponivel}");
                }
            }
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
