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
        public async Task<ItemPedido> ObterPorIdAsync(int id)
        {
            return await _context.ItemPedido.FindAsync(id);
        }
        public async Task<ItemPedido> AdicionarItemPedidoAsync(ItemPedido entity)
        {
            await _context.ItemsPedidos.AddAsync(entity);
            return entity;
        }
        public async Task<ItemPedido> AtualizarItemPedidoAsync(ItemPedido entity)
        {
            _context.ItemsPedidos.Update(entity);
            return entity;
        }
        public async Task<ItemPedido> RemoverItemPedidoAsync(int id)
        {
            var itemPedido = await _context.ItemsPedidos.FindAsync(id);
            if (itemPedido != null)
            {
                _context.ItemsPedidos.Remove(itemPedido);
            }
            return itemPedido;
        }
        public async Task<List<ItemPedido>> ObterTodosItensPedidosAsync()
        {
            return await _context.ItemsPedidos.ToListAsync();
        }
        public async Task<List<ItemPedido>> ObterItensPedidosPorPedidoIdAsync(int pedidoId)
        {
            return await _context.ItemsPedidos
                .Where(ip => ip.PedidoId == pedidoId)
                .ToListAsync();
        }
    }
}
