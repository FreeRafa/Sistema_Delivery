using SistemaDelivery.Modelo.Entidades;
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

        public PedidoServico(IPedidoRepositorio pedidoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
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

        public async Task<Pedido> RemoverPedidoAsync(int id)
        {
            var existente = await _pedidoRepositorio.ObterPorIdAsync(id);
            if (existente == null)
                throw new KeyNotFoundException($"Pedido com id {id} não encontrado.");
            return await _pedidoRepositorio.RemoverPedidoAsync(id);
        }
    }
}
