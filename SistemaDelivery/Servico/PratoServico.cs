using SistemaDelivery.Modelo.Entidades;
using SistemaDelivery.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDelivery.Servico
{
    public class PratoServico
    {
        private readonly IPratoRepositorio _pratoRepositorio;

        public PratoServico(IPratoRepositorio pratoRepositorio)
        {
            _pratoRepositorio = pratoRepositorio;
        }

        public async Task<Prato> ObterPorIdAsync(int id)
        {
            var prato = await _pratoRepositorio.ObterPorIdAsync(id);
            if (prato == null)
                throw new KeyNotFoundException($"Prato com id {id} não encontrado.");
            return prato;
        }

        public async Task<List<Prato>> ObterTodosPratosAsync()
        {
            return await _pratoRepositorio.ObterTodosPratosAsync();
        }

        public async Task<Prato> AdicionarPratoAsync(Prato prato)
        {
            return await _pratoRepositorio.AdicionarPratoAsync(prato);
        }

        public async Task<Prato> AtualizarPratoAsync(Prato prato)
        {
            var existente = await _pratoRepositorio.ObterPorIdAsync(prato.Id);
            if (existente == null)
                throw new KeyNotFoundException($"Prato com id {prato.Id} não encontrado.");
            return await _pratoRepositorio.AtualizarPratoAsync(prato);
        }

        public async Task<Prato> RemoverPratoAsync(int id)
        {
            var existente = await _pratoRepositorio.ObterPorIdAsync(id);
            if (existente == null)
                throw new KeyNotFoundException($"Prato com id {id} não encontrado.");
            return await _pratoRepositorio.RemoverPratoAsync(id);
        }
    }
}
