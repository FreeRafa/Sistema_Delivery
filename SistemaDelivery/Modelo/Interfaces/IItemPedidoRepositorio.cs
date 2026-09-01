using SistemaDelivery.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDelivery.Modelo.Interfaces
{
    public interface IItemPedidoRepositorio
    {
        Task<ItemPedido> ObterPorIdAsync(int id);
        Task<ItemPedido> AdicionarItemPedidoAsync(ItemPedido itemPedido);
        Task<ItemPedido> AtualizarItemPedidoAsync(ItemPedido itemPedido);
        Task<ItemPedido> RemoverItemPedidoAsync(int id);
        Task<List<ItemPedido>> ObterTodosItensPedidosAsync();

        //traz todos os iten do pedido junto com o pedido
        Task<List<ItemPedido>> ObterItensPedidosPorPedidoIdAsync(int pedidoId);
    }
}
