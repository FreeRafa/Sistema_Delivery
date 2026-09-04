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
            return await _context.Pedido.FindAsync(id);
        }

        public async Task<Pedido> AdicionarPedidoAsync(Pedido entity)
        {
            await _context.Pedido.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Pedido> AtualizarPedidoAsync(Pedido entity)
        {
            _context.Pedido.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Pedido?> RemoverPedidoAsync(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);

            if (pedido != null)
            {
                _context.Pedido.Remove(pedido);
                await _context.SaveChangesAsync();
            }

            return pedido;
        }

        public async Task<List<Pedido>> ObterTodosPedidosAsync()
        {
            return await _context.Pedido.ToListAsync();
        }
    }
}
