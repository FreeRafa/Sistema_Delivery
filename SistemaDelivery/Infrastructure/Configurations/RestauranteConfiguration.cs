using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDelivery.Modelo.Entidades;


// Por padrão, o EF Core grava enums como int (valor numérico).
// Como a coluna Categoria no banco é NVARCHAR com CHECK aceitando os nomes do enum
// (ex.: "FastFood", "Casual"...), usamos HasConversion<string>() para gravar e ler
// o enum pelo nome, em vez do valor numérico.

namespace SistemaDelivery.Infrastructure.Configurations
{
    public class RestauranteConfiguration : IEntityTypeConfiguration<Restaurante>
    {
        public void Configure(EntityTypeBuilder<Restaurante> builder)
        {
            // Configuração da propriedade Categoria para usar HasConversion<string>
            builder.ToTable("Restaurante");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(250)
                .IsRequired();

            builder.HasIndex(r => r.Nome)
                .IsUnique();

            builder.Property(r => r.Nipc)
                .HasColumnName("Nipc")
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(r => r.Nipc)
                .IsUnique();

            builder.Property(r => r.Telemovel)
                .HasColumnName("Telemovel")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(r => r.Categoria)
                .HasColumnName("Categoria")
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(r => r.Ativo)
                .HasColumnName("Ativo")
                .HasDefaultValue(true)
                .IsRequired();

            builder.HasMany(r => r.Prato)
                .WithOne(p => p.Restaurante)
                .HasForeignKey(p => p.RestauranteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(r => r.Pedido)
                .WithOne(p => p.Restaurante)
                .HasForeignKey(p => p.RestauranteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
