using AppUnipsico.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppUnipsico.Data.ConfigurationModels
{
    public class InstituicaoConfiguration : IEntityTypeConfiguration<Instituicao>
    {
        public void Configure(EntityTypeBuilder<Instituicao> builder)
        {
            builder
                 .HasKey(i => i.InstituicaoId);

            builder
                .HasMany(i => i.Estagios)
                .WithOne(e => e.Instituicao)
                .HasForeignKey(i => i.EstagioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
