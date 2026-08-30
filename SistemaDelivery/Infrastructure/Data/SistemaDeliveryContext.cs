using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDelivery.Infrastructure.Data
{
    public class SistemaDeliveryContext : DbContext
    {
        public SistemaDeliveryContext(DbContextOptions<SistemaDeliveryContext> options) : base (options)
        {
        }

        public DbSet<Cliente> Clientes => Set<Clientes>();
        public DbSet<Restaurante> Restaurantes => Set<Restaurantes>();
        public DbSet<Prato> Pratos => Set<Pratos>();
        public DbSet<Pedido> Pedidos => Set<Pedido>();
        public DbSet<ItemPedido> ItemsPedidos => Set<ItemPedido>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SistemaDeliveryContext).Assembly);
        }
    }
}
