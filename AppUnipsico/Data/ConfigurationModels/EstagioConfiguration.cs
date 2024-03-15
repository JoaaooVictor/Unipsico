using AppUnipsico.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppUnipsico.Data.ConfigurationModels
{
    public class EstagioConfiguration : IEntityTypeConfiguration<Estagio>
    {
        public void Configure(EntityTypeBuilder<Estagio> builder)
        {
            builder
                .HasKey(e => e.EstagioId);

            builder
                .HasOne(e => e.Aluno)
                .WithMany(a => a.Estagios)
                .HasForeignKey(e => e.AlunoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
