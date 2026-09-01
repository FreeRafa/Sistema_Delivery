using SistemaDelivery.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDelivery.Modelo.Interfaces
{
    public interface IPratoRepositorio
    {
        Task<Prato> ObterPorIdAsync(int id);
        Task<Prato> AdicionarPratoAsync(Prato prato);
        Task<Prato> AtualizarPratoAsync(Prato prato);
        Task<Prato> RemoverPratoAsync(int id);
        Task<List<Prato>> ObterTodosPratosAsync();
    }
}
