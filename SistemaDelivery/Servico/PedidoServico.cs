using SistemaDelivery.Modelo.Entidades;
using SistemaDelivery.Modelo.Enums;
using SistemaDelivery.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDelivery.Servico
{
    public class PedidoServico
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IRestauranteRepositorio _restauranteRepositorio;
        private readonly IPratoRepositorio _pratoRepositorio;

        public PedidoServico(
            IPedidoRepositorio pedidoRepositorio,
            IClienteRepositorio clienteRepositorio,
            IRestauranteRepositorio restauranteRepositorio,
            IPratoRepositorio pratoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _restauranteRepositorio = restauranteRepositorio;
            _pratoRepositorio = pratoRepositorio;
        }

        public async Task<Pedido> ObterPorIdAsync(int id)
        {
            var pedido = await _pedidoRepositorio.ObterPorIdAsync(id);
            if (pedido == null)
                throw new KeyNotFoundException($"Pedido com id {id} não encontrado.");
            return pedido;
        }
        public async Task<List<Pedido>> ObterTodosPedidosAsync()
        {
            return await _pedidoRepositorio.ObterTodosPedidosAsync();
        }

        public async Task<Pedido> AdicionarPedidoAsync(Pedido pedido)
        {
            return await _pedidoRepositorio.AdicionarPedidoAsync(pedido);
        }

        public async Task<Pedido> AtualizarPedidoAsync(Pedido pedido)
        {
            var existente = await _pedidoRepositorio.ObterPorIdAsync(pedido.Id);
            if (existente == null)
                throw new KeyNotFoundException($"Pedido com id {pedido.Id} não encontrado.");
            return await _pedidoRepositorio.AtualizarPedidoAsync(pedido);
        }

        public async Task<Pedido?> RemoverPedidoAsync(int id)
        {
            var existente = await _pedidoRepositorio.ObterPorIdAsync(id);
            if (existente == null)
                throw new KeyNotFoundException($"Pedido com id {id} não encontrado.");
            return await _pedidoRepositorio.RemoverPedidoAsync(id);
        }

        public async Task<Pedido> CriarPedidoAsync(int clienteId, int restauranteId, List<(int PratoId, int Quantidade)> itensSolicitados)
        {
            if (itensSolicitados == null || itensSolicitados.Count == 0)
                throw new InvalidOperationException("O pedido tem de ter pelo menos um item.");

            var cliente = await _clienteRepositorio.ObterPorIdAsync(clienteId);
            if (cliente == null)
                throw new KeyNotFoundException($"Cliente com id {clienteId} não encontrado.");

            var restaurante = await _restauranteRepositorio.ObterPorIdAsync(restauranteId);
            if (restaurante == null)
                throw new KeyNotFoundException($"Restaurante com id {restauranteId} não encontrado.");

            if (!restaurante.Ativo)
                throw new InvalidOperationException("O restaurante selecionado não está ativo.");

            var itensPedido = new List<ItemPedido>();

            foreach (var item in itensSolicitados)
            {
                if (item.Quantidade <= 0)
                    throw new InvalidOperationException("A quantidade de cada item tem de ser maior que zero.");

                var prato = await _pratoRepositorio.ObterPorIdAsync(item.PratoId);

                if (prato == null)
                    throw new KeyNotFoundException($"Prato com id {item.PratoId} não encontrado.");

                if (prato.RestauranteId != restauranteId)
                    throw new InvalidOperationException($"O prato '{prato.Nome}' não pertence ao restaurante selecionado.");

                if (!prato.Disponivel)
                    throw new InvalidOperationException($"O prato '{prato.Nome}' não está disponível.");

                itensPedido.Add(new ItemPedido
                {
                    PratoId = prato.Id,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = prato.Preco
                });
            }

            var total = itensPedido.Sum(i => i.Quantidade * i.PrecoUnitario);

            var pedido = new Pedido
            {
                ClienteId = clienteId,
                RestauranteId = restauranteId,
                StatusPedido = StatusPedido.Preparado,
                DataPedido = DateTime.Now,
                Total = total,
                Itens = itensPedido
            };

            return await _pedidoRepositorio.AdicionarPedidoAsync(pedido);
        }
    }
}