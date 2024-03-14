using AppUnipsico.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppUnipsico.Data.ConfigurationModels
{
    public class ConsultaConfiguration : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder
                .HasKey(c => c.ConsultaId);

            builder
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Consultas)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
