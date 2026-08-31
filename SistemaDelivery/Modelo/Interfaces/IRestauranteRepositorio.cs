using SistemaDelivery.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDelivery.Modelo.Interfaces
{
    public interface IRestauranteRepositorio
    {
        Task<Restaurante> ObterPorIdAsync(int id);
        Task<Restaurante> AdicionarRestauranteAsync(Restaurante restaurante);
        Task<Restaurante> AtualizarRestauranteAsync(Restaurante restaurante);
        Task<Restaurante> RemoverRestauranteAsync(int id);
        Task<List<Restaurante>> ObterTodosRestauranteAsync();
    }
}
