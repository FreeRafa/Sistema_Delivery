using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDelivery.Modelo.Entidades;
using SistemaDelivery.Modelo.Enums;

////Esta classe foi criada para rastrear a configuração do restaurante no banco de dados
//pois a categoria do restaurante foi grava como NVARCHAR com valores em português, e com fotmatacao especial, 
//e o Entity Framework não consegue mapear corretamente para a classe Restaurante, que tem a propriedade Categoria como enum.
//Por isso vamos usar um HasConversion<string> para mapear.


namespace SistemaDelivery.Infrastructure.Data
{
    public class RestauranteConfiguration : IEntityTypeConfiguration<Restaurante>
    {
        public void Configure(EntityTypeBuilder<Restaurante> builder)
        {
            // Configuração da propriedade Categoria para usar HasConversion<string>
            builder.ToTable("Restaurantes");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Nome)
                .HasColumnName("Nome")
                .IsRequired();

            builder.HasIndex(r => r.Nome)
                .IsUnique();

            builder.Property(r => r.Nipc)
                .HasColumnName("NIPC")
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
                .HasConversion(
                    categoria => CategoriaParaString(categoria),
                    valor => StringParaCategoria(valor))
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

        private static string CategoriaParaString(CategoriaRestaurante categoria) => categoria switch
        {
            CategoriaRestaurante.FastFood => "Fast Food",
            CategoriaRestaurante.Casual => "Casual",
            CategoriaRestaurante.AltaGastronomia => "Alta Gastronomia",
            CategoriaRestaurante.Tematicos => "Temáticos",
            CategoriaRestaurante.Regional => "Regional",
            _ => throw new ArgumentOutOfRangeException(nameof(categoria), categoria, null)
        };

        private static CategoriaRestaurante StringParaCategoria(string valor) => valor switch
        {
            "Fast Food" => CategoriaRestaurante.FastFood,
            "Casual" => CategoriaRestaurante.Casual,
            "Alta Gastronomia" => CategoriaRestaurante.AltaGastronomia,
            "Temáticos" => CategoriaRestaurante.Tematicos,
            "Regional" => CategoriaRestaurante.Regional,
            _ => throw new ArgumentOutOfRangeException(nameof(valor), valor, null)
        };
    }
}
