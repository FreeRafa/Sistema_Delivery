using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDelivery.Modelo.Entidades;

namespace SistemaDelivery.Infrastructure.Configurations
{
    public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            builder.ToTable("ItemPedido");
            builder.HasKey(ip => ip.Id);

            builder.Property(ip => ip.Quantidade)
                .HasColumnName("Quantidade")
                .IsRequired();

            builder.Property(ip => ip.PrecoUnitario)
                .HasColumnName("PrecoUnitario")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.HasOne(ip => ip.Pedido)
                .WithMany(p => p.Itens)
                .HasForeignKey(ip => ip.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ip => ip.Prato)
                .WithMany()
                .HasForeignKey(ip => ip.PratoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}