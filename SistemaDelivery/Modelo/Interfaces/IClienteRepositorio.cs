using SistemaDelivery.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDelivery.Modelo.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<Cliente?> ObterPorIdAsync(int id);
        Task<Cliente> AdicionarClienteAsync(Cliente cliente);
        Task<Cliente> AtualizarClienteAsync(Cliente cliente);
        Task<Cliente?> RemoverClienteAsync (int id);
        Task<List<Cliente>> ObterTodosClienteAsync();
    }
}
