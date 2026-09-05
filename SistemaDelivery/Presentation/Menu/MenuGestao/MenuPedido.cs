using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaDelivery.Modelo.Entidades;
using SistemaDelivery.Servico;

namespace SistemaDelivery.Presentation.Menu.MenuFluxo
{
    public class MenuPedido
    {
        private readonly ClienteServico _clienteServico;
        private readonly RestauranteServico _restauranteServico;
        private readonly PratoServico _pratoServico;
        private readonly PedidoServico _pedidoServico;

        public MenuPedido(
            ClienteServico clienteServico,
            RestauranteServico restauranteServico,
            PratoServico pratoServico,
            PedidoServico pedidoServico)
        {
            _clienteServico = clienteServico;
            _restauranteServico = restauranteServico;
            _pratoServico = pratoServico;
            _pedidoServico = pedidoServico;
        }

        public async Task ExibirMenuPedidoAsync()
        {
            try
            {
                var cliente = await EscolherClienteAsync();
                if (cliente == null) return;

                var restaurante = await EscolherRestauranteAsync();
                if (restaurante == null) return;

                var pratosDisponiveis = await _pratoServico.ObterPratosDisponiveisPorRestauranteAsync(restaurante.Id);

                if (pratosDisponiveis.Count == 0)
                {
                    Console.WriteLine("Este restaurante não tem pratos disponíveis de momento.");
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    return;
                }

                var carrinho = MontarCarrinho(pratosDisponiveis);

                if (carrinho.Count == 0)
                {
                    Console.WriteLine("Nenhum item adicionado. Pedido cancelado.");
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    return;
                }

                await ConfirmarEFinalizarPedidoAsync(cliente, restaurante, pratosDisponiveis, carrinho);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar o pedido: {ex.Message}");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        private async Task<Cliente> EscolherClienteAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Identificação do Cliente ===");

            var clientes = await _clienteServico.ObterTodosClienteAsync();

            if (clientes.Count == 0)
            {
                Console.WriteLine("Não existem clientes registados. Registe um cliente primeiro.");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return null;
            }

            foreach (var c in clientes)
                Console.WriteLine($"Id: {c.Id}, Nome: {c.Nome}, NIF: {c.Nif}");

            Console.Write("\nIndique o Id do cliente (ou 0 para cancelar): ");
            if (!int.TryParse(Console.ReadLine(), out int clienteId) || clienteId == 0)
                return null;

            try
            {
                return await _clienteServico.ObterPorIdAsync(clienteId);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return null;
            }
        }

        private async Task<Restaurante> EscolherRestauranteAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Escolha o Restaurante ===");

            var restaurantes = (await _restauranteServico.ObterTodosRestaurantesAsync())
                .Where(r => r.Ativo)
                .ToList();

            if (restaurantes.Count == 0)
            {
                Console.WriteLine("Não existem restaurantes ativos de momento.");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return null;
            }

            foreach (var r in restaurantes)
                Console.WriteLine($"Id: {r.Id}, Nome: {r.Nome}, Categoria: {r.Categoria}");

            Console.Write("\nIndique o Id do restaurante (ou 0 para cancelar): ");
            if (!int.TryParse(Console.ReadLine(), out int restauranteId) || restauranteId == 0)
                return null;

            try
            {
                return await _restauranteServico.ObterPorIdAsync(restauranteId);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return null;
            }
        }

        private List<(int PratoId, int Quantidade)> MontarCarrinho(List<Prato> pratosDisponiveis)
        {
            var carrinho = new List<(int PratoId, int Quantidade)>();
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== Menu do Restaurante ===");

                foreach (var prato in pratosDisponiveis)
                    Console.WriteLine($"Id: {prato.Id}, Nome: {prato.Nome}, Preço: {prato.Preco:C}");

                if (carrinho.Count > 0)
                {
                    Console.WriteLine("\n--- Carrinho Atual ---");
                    foreach (var item in carrinho)
                    {
                        var prato = pratosDisponiveis.First(p => p.Id == item.PratoId);
                        Console.WriteLine($"{prato.Nome} x{item.Quantidade} = {(prato.Preco * item.Quantidade):C}");
                    }
                }

                Console.Write("\nIndique o Id do prato a adicionar (ou 0 para finalizar): ");
                if (!int.TryParse(Console.ReadLine(), out int pratoId) || pratoId == 0)
                {
                    continuar = false;
                    continue;
                }

                var pratoEscolhido = pratosDisponiveis.FirstOrDefault(p => p.Id == pratoId);
                if (pratoEscolhido == null)
                {
                    Console.WriteLine("Prato inválido. Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }

                Console.Write("Quantidade: ");
                if (!int.TryParse(Console.ReadLine(), out int quantidade) || quantidade <= 0)
                {
                    Console.WriteLine("Quantidade inválida. Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }

                var indiceExistente = carrinho.FindIndex(i => i.PratoId == pratoId);
                if (indiceExistente >= 0)
                {
                    var atual = carrinho[indiceExistente];
                    carrinho[indiceExistente] = (atual.PratoId, atual.Quantidade + quantidade);
                }
                else
                {
                    carrinho.Add((pratoId, quantidade));
                }
            }

            return carrinho;
        }

        private async Task ConfirmarEFinalizarPedidoAsync(
            Cliente cliente,
            Restaurante restaurante,
            List<Prato> pratosDisponiveis,
            List<(int PratoId, int Quantidade)> carrinho)
        {
            Console.Clear();
            Console.WriteLine("=== Resumo do Pedido ===");
            Console.WriteLine($"Cliente: {cliente.Nome}");
            Console.WriteLine($"Restaurante: {restaurante.Nome}\n");

            decimal total = 0;
            foreach (var item in carrinho)
            {
                var prato = pratosDisponiveis.First(p => p.Id == item.PratoId);
                var subtotal = prato.Preco * item.Quantidade;
                total += subtotal;
                Console.WriteLine($"{prato.Nome} x{item.Quantidade} = {subtotal:C}");
            }

            Console.WriteLine($"\nTotal: {total:C}");
            Console.Write("\nConfirmar pedido? (S/N): ");

            if (!string.Equals(Console.ReadLine(), "S", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Pedido cancelado.");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            var pedido = await _pedidoServico.CriarPedidoAsync(cliente.Id, restaurante.Id, carrinho);

            Console.WriteLine($"\nPedido criado com sucesso! Id: {pedido.Id}, Total: {pedido.Total:C}");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}