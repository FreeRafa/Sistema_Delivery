using SistemaDelivery.Modelo.Interfaces;
using SistemaDelivery.Infrastructure.Data;
using SistemaDelivery.Modelo.Entidades;
using Microsoft.EntityFrameworkCore;

namespace SistemaDelivery.Infrastructure.Repositorio
{
    internal class PedidoRepositorio : IPedidoRepositorio
    {
        private readonly SistemaDeliveryContext _context;

        public PedidoRepositorio(SistemaDeliveryContext context)
        {
            _context = context;
        }

        public async Task<Pedido?> ObterPorIdAsync(int id)
        {
            return await _context.Pedidos.FindAsync(id);
        }

        public async Task<Pedido> AdicionarPedidoAsync(Pedido entity)
        {
            await _context.Pedidos.AddAsync(entity);
            return entity;
        }

        public async Task<Pedido> AtualizarPedidoAsync(Pedido entity)
        {
            _context.Pedidos.Update(entity);
            return entity;
        }

        public async Task<Pedido?> RemoverPedidoAsync(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }
            return pedido;
        }

        public async Task<List<Pedido>> ObterTodosPedidosAsync()
        {
            return await _context.Pedidos.ToListAsync();
        }
    }
}
