using SistemaDelivery.Modelo.Interfaces;
using SistemaDelivery.Infrastructure.Data;
using SistemaDelivery.Modelo.Entidades;
using Microsoft.EntityFrameworkCore;


namespace SistemaDelivery.Infrastructure.Repositorio
{
    public class ItemPedidoRepositorio : IItemPedidoRepositorio
    {
        private readonly SistemaDeliveryContext _context;
        public ItemPedidoRepositorio(SistemaDeliveryContext context)
        {
            _context = context;
        }
        public async Task<ItemPedido?> ObterPorIdAsync(int id)
        {
            return await _context.ItensPedido.FindAsync(id);
        }
        public async Task<ItemPedido> AdicionarItemPedidoAsync(ItemPedido entity)
        {
            await _context.ItensPedido.AddAsync(entity);
            return entity;
        }
        public async Task<ItemPedido> AtualizarItemPedidoAsync(ItemPedido entity)
        {
            _context.ItensPedido.Update(entity);
            return entity;
        }
        public async Task<ItemPedido?> RemoverItemPedidoAsync(int id)
        {
            var itemPedido = await _context.ItensPedido.FindAsync(id);
            if (itemPedido != null)
            {
                _context.ItensPedido.Remove(itemPedido);
            }
            return itemPedido;
        }
        public async Task<List<ItemPedido>> ObterTodosItensPedidosAsync()
        {
            return await _context.ItensPedido.ToListAsync();
        }
        public async Task<List<ItemPedido>> ObterItensPedidosPorPedidoIdAsync(int pedidoId)
        {
            return await _context.ItensPedido
                .Where(ip => ip.PedidoId == pedidoId)
                .ToListAsync();
        }
    }
}
