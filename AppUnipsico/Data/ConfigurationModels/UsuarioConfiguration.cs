using AppUnipsico.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppUnipsico.Data.ConfigurationModels
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder
                .HasMany(u => u.Consultas)
                .WithOne(u => u.Usuario)
                .HasForeignKey(u => u.UsuarioId);

            builder
                .HasIndex(u => new { u.RA, u.TipoUsuario })
                .IsUnique()
                .HasFilter("TipoUsuario = 2");

            builder
                .Property(u => u.RA)
                .HasColumnType("varchar(10)");
        }
    }
}
