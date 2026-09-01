using SistemaDelivery.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDelivery.Modelo.Interfaces
{
    public interface IPedido
    {
        Task<Pedido> ObterPorIdAsync(int id);
        Task<Pedido> AdicionarPedidoAsync(Pedido pedido);
        Task<Pedido> AtualizarPedidoAsync(Pedido pedido);
        Task<Pedido> RemoverPedidoAsync(int id);
        Task<List<Pedido>> ObterTodosPedidosAsync();
    }
}
