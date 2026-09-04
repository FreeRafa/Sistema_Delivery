using SistemaDelivery.Modelo.Entidades;
using SistemaDelivery.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDelivery.Servico
{
    public class RestauranteServico
    {
        private readonly IRestauranteRepositorio _restauranteRepositorio;

        public RestauranteServico(IRestauranteRepositorio restauranteRepositorio)
        {
            _restauranteRepositorio = restauranteRepositorio;
        }

        public async Task<Restaurante> ObterPorIdAsync(int id)
        {
            var restaurante = await _restauranteRepositorio.ObterPorIdAsync(id);
            if (restaurante == null)
                throw new KeyNotFoundException($"Restaurante com id {id} não encontrado.");
            return restaurante;
        }

        public async Task<List<Restaurante>> ObterTodosRestaurantesAsync()
        {
            return await _restauranteRepositorio.ObterTodosRestauranteAsync();
        }

        public async Task<Restaurante> AdicionarRestauranteAsync(Restaurante restaurante)
        {
            return await _restauranteRepositorio.AdicionarRestauranteAsync(restaurante);

        }

        public async Task<Restaurante> AtualizarRestauranteAsync(Restaurante restaurante)
        {
            var existente = await _restauranteRepositorio.ObterPorIdAsync(restaurante.Id);

            if (existente == null)
                throw new KeyNotFoundException($"Restaurante com id {restaurante.Id} não encontrado.");

            return await _restauranteRepositorio.AtualizarRestauranteAsync(restaurante);

        }

        public async Task<Restaurante?> RemoverRestauranteAsync(int id)
        {
            var existente = await _restauranteRepositorio.ObterPorIdAsync(id);

            if (existente == null)
                throw new KeyNotFoundException($"Restaurante com id {id} não encontrado.");

            return await _restauranteRepositorio.RemoverRestauranteAsync(id);
        }
    }
}
