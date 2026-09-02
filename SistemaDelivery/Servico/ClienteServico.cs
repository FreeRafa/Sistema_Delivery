using SistemaDelivery.Modelo.Entidades;
using SistemaDelivery.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDelivery.Servico
{
    public class ClienteServico
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteServico(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public async Task<Cliente> ObterPorIdAsync(int id)
        {
            var cliente = await _clienteRepositorio.ObterPorIdAsync(id);

            if (cliente == null)
                throw new KeyNotFoundException($"Cliente com id {id} não encontrado.");

            return cliente;
        }

        public async Task<List<Cliente>> ObterTodosClienteAsync()
        {
            return await _clienteRepositorio.ObterTodosClienteAsync();
        }

        public async Task<Cliente> AdicionarClienteAsync(Cliente cliente)
        {
            await ValidarNifUnicoAsync(cliente.Nif);

            return await _clienteRepositorio.AdicionarClienteAsync(cliente);
        }

        public async Task<Cliente> AtualizarClienteAsync(Cliente cliente)
        {
            var existente = await _clienteRepositorio.ObterPorIdAsync(cliente.Id);

            if (existente == null)
                throw new KeyNotFoundException($"Cliente com id {cliente.Id} não encontrado.");

            await ValidarNifUnicoAsync(cliente.Nif, ignorarId: cliente.Id);

            return await _clienteRepositorio.AtualizarClienteAsync(cliente);
        }

        public async Task<Cliente> RemoverClienteAsync(int id)
        {
            var existente = await _clienteRepositorio.ObterPorIdAsync(id);

            if (existente == null)
                throw new KeyNotFoundException($"Cliente com id {id} não encontrado.");

            return await _clienteRepositorio.RemoverClienteAsync(id);
        }

        private async Task ValidarNifUnicoAsync(string nif, int? ignorarId = null)
        {
            var clientes = await _clienteRepositorio.ObterTodosClienteAsync();

            bool existeDuplicado = clientes.Any(c =>
                c.Nif == nif && (ignorarId == null || c.Id != ignorarId));

            if (existeDuplicado)
                throw new InvalidOperationException("Já existe um cliente registado com este NIF.");
        }
    }
}