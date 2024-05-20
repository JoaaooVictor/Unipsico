using AppUnipsico.Data.ConfigurationModels;
using AppUnipsico.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AppUnipsico.Data.Context
{

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<AppUnipsicoDb>
    {
        public AppUnipsicoDb CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppUnipsicoDb>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=UNIPSICO;Integrated Security=true;TrustServerCertificate=True;");

            return new AppUnipsicoDb(optionsBuilder.Options);
        }
    }

    public class AppUnipsicoDb : IdentityDbContext<Usuario>
    {
        public AppUnipsicoDb(DbContextOptions options) : base(options) { }

        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Datas> Datas { get; set; }
        public DbSet<Estagio> Estagios { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ConsultaConfiguration());
            builder.ApplyConfiguration(new UsuarioConfiguration());
            builder.ApplyConfiguration(new EstagioConfiguration());
            builder.ApplyConfiguration(new InstituicaoConfiguration());
        }
    }
}
