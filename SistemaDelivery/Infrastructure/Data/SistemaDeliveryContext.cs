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

        public DbSet<Cliente> Cliente => Set<Cliente>();
        public DbSet<Restaurante> Restaurante => Set<Restaurante>();
        public DbSet<Prato> Prato => Set<Prato>();
        public DbSet<Pedido> Pedido => Set<Pedido>();
        public DbSet<ItemPedido> ItensPedido => Set<ItemPedido>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(SistemaDeliveryContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
