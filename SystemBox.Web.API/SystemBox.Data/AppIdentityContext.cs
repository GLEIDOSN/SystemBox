using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SystemBox.Domain.Models;

namespace SystemBox.Data
{
    public class AppIdentityContext : IdentityDbContext<Usuario>
    {
        public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
