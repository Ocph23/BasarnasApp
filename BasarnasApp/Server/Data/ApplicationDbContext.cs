using BasarnasApp.Server.Models;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OcphApiAuth;

namespace BasarnasApp.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Instansi> Instansi { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<JenisKejadian> JenisKejadian { get; set; }
        public DbSet<PihakTerkait> PihakTerkait { get; set; }
        public DbSet<Kejadian> Kejadian { get; set; }
        public DbSet<Pelapor> Pelapor { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}