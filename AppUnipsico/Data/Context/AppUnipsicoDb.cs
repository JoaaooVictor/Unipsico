using AppUnipsico.Data.ConfigurationModels;
using AppUnipsico.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppUnipsico.Data.Context
{
    public class AppUnipsicoDb : IdentityDbContext<IdentityUser>
    {
        public AppUnipsicoDb(DbContextOptions options) : base(options) { }

        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<DataDisponivel> DatasDisponiveis { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ConsultaConfiguration());
            builder.ApplyConfiguration(new UsuarioConfiguration());
        }
    }
}
