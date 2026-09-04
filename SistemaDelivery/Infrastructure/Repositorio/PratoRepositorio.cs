using SistemaDelivery.Modelo.Interfaces;
using SistemaDelivery.Infrastructure.Data;
using SistemaDelivery.Modelo.Entidades;
using Microsoft.EntityFrameworkCore;

namespace SistemaDelivery.Infrastructure.Repositorio
{
    public class PratoRepositorio : IPratoRepositorio
    {
        private readonly SistemaDeliveryContext _context;

        public PratoRepositorio(SistemaDeliveryContext context)
        {
            _context = context;
        }

        public async Task<Prato?> ObterPorIdAsync(int id)
        {
            return await _context.Prato.FindAsync(id);
        }

        public async Task<Prato> AdicionarPratoAsync(Prato entity)
        {
            await _context.Prato.AddAsync(entity);
            return entity;
        }

        public async Task<Prato> AtualizarPratoAsync(Prato entity)
        {
            _context.Prato.Update(entity);
            return entity;
        }

        public async Task<Prato?> RemoverPratoAsync(int id)
        {
            var prato = await _context.Prato.FindAsync(id);
            if (prato != null)
            {
                _context.Prato.Remove(prato);
            }
            return prato;
        }

        public async Task<List<Prato>> ObterTodosPratosAsync()
        {
            return await _context.Prato.ToListAsync();
        }

        public async Task<List<Prato>> ObterPratosPorRestauranteAsync(int restauranteId)
        {
            return await _context.Prato
                .Where(p => p.RestauranteId == restauranteId)
                .ToListAsync();
        }
    }
}
