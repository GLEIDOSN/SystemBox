using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using SystemBox.Domain.Models;

namespace SystemBox.Data
{
    public class SystemDbContext : IdentityDbContext<Usuario>
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
