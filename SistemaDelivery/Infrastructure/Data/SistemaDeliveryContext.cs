using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SistemaDelivery.Modelo.Entidades;

namespace SistemaDelivery.Infrastructure.Data
{
    public class SistemaDeliveryContext : DbContext
    {
        public SistemaDeliveryContext(DbContextOptions<SistemaDeliveryContext> options) : base (options)
        {
        }

        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Restaurante> Restaurantes => Set<Restaurante>();
        public DbSet<Prato> Pratos => Set<Prato>();
        public DbSet<Pedido> Pedidos => Set<Pedido>();
        public DbSet<ItemPedido> ItemsPedidos => Set<ItemPedido>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SistemaDeliveryContext).Assembly);
        }
    }
}
