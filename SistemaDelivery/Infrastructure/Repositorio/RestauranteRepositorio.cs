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
            return await _context.Restaurantes.FindAsync(id);
        }

        public async Task<Restaurante> AdicionarRestauranteAsync(Restaurante entity)
        {
            await _context.Restaurantes.AddAsync(entity);
            return entity;
        }

        public async Task<Restaurante> AtualizarRestauranteAsync(Restaurante entity)
        {
            _context.Restaurantes.Update(entity);
            return entity;
        }

        public async Task<Restaurante?> RemoverRestauranteAsync(int id)
        {
            var restaurante = await _context.Restaurantes.FindAsync(id);
            if (restaurante != null)
            {
                _context.Restaurantes.Remove(restaurante);
            }
            return restaurante;
        }

        public async Task<List<Restaurante>> ObterTodosRestauranteAsync()
        {
            return await _context.Restaurantes.ToListAsync();
        }
    }
}
