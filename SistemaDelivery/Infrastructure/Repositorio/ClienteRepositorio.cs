using SistemaDelivery.Modelo.Interfaces;
using SistemaDelivery.Infrastructure.Data;
using SistemaDelivery.Modelo.Entidades;
using Microsoft.EntityFrameworkCore;

namespace SistemaDelivery.Infrastructure.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly SistemaDeliveryContext _context;

        public ClienteRepositorio(SistemaDeliveryContext context)
        {
            _context = context;
        }

        public async Task<Cliente?> ObterPorIdAsync(int id)
        {
             return await _context.Cliente.FindAsync(id);
        }

        public async Task<Cliente> AdicionarClienteAsync(Cliente entity)
        {
            await _context.Cliente.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Cliente> AtualizarClienteAsync(Cliente entity)
        {
            _context.Cliente.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Cliente?> RemoverClienteAsync(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente != null)
            {
                _context.Cliente.Remove(cliente);
                await _context.SaveChangesAsync();
            }

            return cliente;
        }

        public async Task<List<Cliente>> ObterTodosClienteAsync()
        {
            return await _context.Cliente.ToListAsync();
        }
    }
}
