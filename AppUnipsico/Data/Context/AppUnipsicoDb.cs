using AppUnipsico.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppUnipsico.Data.Context
{
    public class AppUnipsicoDb : IdentityDbContext<IdentityUser>
    {
        public AppUnipsicoDb(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
