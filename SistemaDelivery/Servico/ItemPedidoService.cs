using SistemaDelivery.Modelo.Entidades;
using SistemaDelivery.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SistemaDelivery.Servico
{
    public class ItemPedidoService
    {
        private readonly IItemPedidoRepositorio _itemPedidoRepositorio;

        public ItemPedidoService(IItemPedidoRepositorio itemPedidoRepositorio)
        {
            _itemPedidoRepositorio = itemPedidoRepositorio;
        }

        public async Task<ItemPedido> ObterPorIdAsync(int id)
        {
            var itemPedido = await _itemPedidoRepositorio.ObterPorIdAsync(id);
            if (itemPedido == null)
                throw new KeyNotFoundException($"ItemPedido com id {id} não encontrado.");
            return itemPedido;
        }

        public async Task<List<ItemPedido>> ObterTodosItensPedidosAsync()
        {
            return await _itemPedidoRepositorio.ObterTodosItensPedidosAsync();
        }

        public async Task<ItemPedido> AdicionarItemPedidoAsync(ItemPedido itemPedido)
        {
            return await _itemPedidoRepositorio.AdicionarItemPedidoAsync(itemPedido);
        }

        public async Task<ItemPedido> AtualizarItemPedidoAsync(ItemPedido itemPedido)
        {
            var existente = await _itemPedidoRepositorio.ObterPorIdAsync(itemPedido.Id);
            if (existente == null)
                throw new KeyNotFoundException($"ItemPedido com id {itemPedido.Id} não encontrado.");
            return await _itemPedidoRepositorio.AtualizarItemPedidoAsync(itemPedido);
        }

        public async Task<ItemPedido> RemoverItemPedidoAsync(int id)
        {
            var existente = await _itemPedidoRepositorio.ObterPorIdAsync(id);
            if (existente == null)
                throw new KeyNotFoundException($"ItemPedido com id {id} não encontrado.");
            return await _itemPedidoRepositorio.RemoverItemPedidoAsync(id);
        }
    }
}
