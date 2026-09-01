using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDelivery.Modelo.Entidades;

namespace SistemaDelivery.Infrastructure.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedido");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.DataPedido)
                .HasColumnName("DataPedido")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.Property(p => p.Total)
                .HasColumnName("Total")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(p => p.StatusPedido)
                .HasColumnName("StatusPedido")
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(p => p.Restaurante)
                .WithMany(r => r.Pedido)
                .HasForeignKey(p => p.RestauranteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
