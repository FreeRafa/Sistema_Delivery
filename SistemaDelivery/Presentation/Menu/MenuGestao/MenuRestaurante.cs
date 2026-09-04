using SistemaDelivery.Modelo.Entidades;
using SistemaDelivery.Modelo.Interfaces;
using SistemaDelivery.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDelivery.Presentation.Menu.MenuGestao
{
    public class MenuRestaurante
    {
        private readonly RestauranteServico _restauranteServico;

        public MenuRestaurante(RestauranteServico restauranteServico)
        {
            _restauranteServico = restauranteServico;
        }

        public async Task ExibirMenuRestauranteAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Menu de Gestão de Restaurantes ===");
            Console.WriteLine("1. Adicionar Restaurantes");
            Console.WriteLine("2. Atualizar Restaurante");
            Console.WriteLine("3. Remover Restaurante");
            Console.WriteLine("4. Listar Restaurantes");
            Console.WriteLine("5. Voltar ao Menu Principal");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();
            switch (opcao)
            {
                case "1":
                    await AdicionarRestauranteAsync();
                    break;

                case "2":
                    await AtualizarRestauranteAsync();
                    break;

                case "3":
                    await RemoverRestauranteAsync();
                    break;

                case "4":
                    await ListarRestaurantesAsync();
                    break;

                case "5":
                    return; // Voltar ao menu principal
                default:
                    Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
            await ExibirMenuRestauranteAsync(); // Exibe o menu novamente após a ação
        }

        private async Task AdicionarRestauranteAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Adicionar Restaurante ===");
            Console.Write("Nome do Restaurante: ");
            var nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite o NIPC do Restaurante: ");
            var nipc = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite o número de telemovel do Restaurante: ");
            var telemovel = Console.ReadLine() ?? string.Empty;

            CategoriaRestaurante categoria;
            while (true)
            {
                Console.Write("Digite a categoria do Restaurante (0 - FastFood, 1 - Casual, 2 - AltaGastronomia, 3 - Tematicos, 4 - Regional): ");
                var categoriaInput = Console.ReadLine();

                if (Enum.TryParse(categoriaInput, out categoria))
                    break;

                Console.WriteLine("Categoria inválida. Tente novamente.");
            }

            var restaurante = new Restaurante
            {
                Nome = nome,
                Nipc = nipc,
                Telemovel = telemovel,
                Categoria = categoria
            };

            await _restauranteServico.AdicionarRestauranteAsync(restaurante);
            Console.WriteLine("Restaurante adicionado com sucesso! Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private async Task AtualizarRestauranteAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Atualizar Restaurante ===");

            var restaurantes = await _restauranteServico.ObterTodosRestaurantesAsync();

            if (restaurantes.Count == 0)
            {
                Console.WriteLine("Não existem restaurantes cadastrados.");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Restaurantes cadastrados:");
            foreach (var r in restaurantes)
            {
                Console.WriteLine($"{r.Id} - {r.Nome} ({r.Categoria})");
            }

            Console.Write("\nDigite o Id do Restaurante a atualizar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido. Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Restaurante restaurante;
            try
            {
                restaurante = await _restauranteServico.ObterPorIdAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.Write($"Nome ({restaurante.Nome}): ");
            var nome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nome))
                restaurante.Nome = nome;

            Console.Write($"NIPC ({restaurante.Nipc}): ");
            var nipc = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nipc))
                restaurante.Nipc = nipc;

            Console.Write($"Telemóvel ({restaurante.Telemovel}): ");
            var telemovel = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(telemovel))
                restaurante.Telemovel = telemovel;

            Console.Write($"Categoria ({restaurante.Categoria}) — deixe em branco para manter, ou 0-FastFood, 1-Casual, 2-AltaGastronomia, 3-Tematicos, 4-Regional: ");
            var categoriaInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(categoriaInput))
            {
                if (Enum.TryParse<CategoriaRestaurante>(categoriaInput, out var categoria))
                    restaurante.Categoria = categoria;
                else
                    Console.WriteLine("Categoria inválida, mantendo o valor anterior.");
            }

            await _restauranteServico.AtualizarRestauranteAsync(restaurante);
            Console.WriteLine("Restaurante atualizado com sucesso! Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private async Task RemoverRestauranteAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Remover Restaurante ===");
            var restaurantes = await _restauranteServico.ObterTodosRestaurantesAsync();
            if (restaurantes.Count == 0)
            {
                Console.WriteLine("Não existem restaurantes cadastrados.");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Restaurantes cadastrados:");
            foreach (var r in restaurantes)
            {
                Console.WriteLine($"{r.Id} - {r.Nome} ({r.Categoria})");
            }
            Console.Write("\nDigite o Id do Restaurante a remover: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido. Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }
            try
            {
                await _restauranteServico.RemoverRestauranteAsync(id);
                Console.WriteLine("Restaurante removido com sucesso! Pressione qualquer tecla para continuar...");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Pressione qualquer tecla para continuar...");
            }
            Console.ReadKey();
        }

        private async Task ListarRestaurantesAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Restaurantes ===");
            var restaurantes = await _restauranteServico.ObterTodosRestaurantesAsync();
            if (restaurantes.Count == 0)
            {
                Console.WriteLine("Não existem restaurantes cadastrados.");
            }
            else
            {
                foreach (var r in restaurantes)
                {
                    Console.WriteLine($"Id: {r.Id}, Nome: {r.Nome}, NIPC: {r.Nipc}, Telemóvel: {r.Telemovel}, Categoria: {r.Categoria}");
                }
            }
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}

   
    

