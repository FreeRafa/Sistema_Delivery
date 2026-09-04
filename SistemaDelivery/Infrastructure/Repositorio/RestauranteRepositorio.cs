using SistemaDelivery.Modelo.Interfaces;
using SistemaDelivery.Infrastructure.Data;
using SistemaDelivery.Modelo.Entidades;
using Microsoft.EntityFrameworkCore;

namespace SistemaDelivery.Infrastructure.Repositorio
{
    internal class RestauranteRepositorio : IRestauranteRepositorio
    {
        private readonly SistemaDeliveryContext _context;

        public RestauranteRepositorio(SistemaDeliveryContext context)
        {
            _context = context;
        }

        public async Task<Restaurante?> ObterPorIdAsync(int id)
        {
            return await _context.Restaurante.FindAsync(id);
        }

        public async Task<Restaurante> AdicionarRestauranteAsync(Restaurante entity)
        {
            await _context.Restaurante.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Restaurante> AtualizarRestauranteAsync(Restaurante entity)
        {
            _context.Restaurante.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Restaurante?> RemoverRestauranteAsync(int id)
        {
            var restaurante = await _context.Restaurante.FindAsync(id);

            if (restaurante != null)
            {
                _context.Restaurante.Remove(restaurante);
                await _context.SaveChangesAsync();
            }

            return restaurante;
        }

        public async Task<List<Restaurante>> ObterTodosRestauranteAsync()
        {
            return await _context.Restaurante.ToListAsync();
        }
    }
}
