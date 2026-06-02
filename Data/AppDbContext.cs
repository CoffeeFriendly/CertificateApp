using CertificatesApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel.Resolution;
using System.Security.Cryptography.X509Certificates;

namespace CertificatesApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Models.CertificateRequest> CertificateRequests { get; set; } = null!;
        public DbSet<RequestHistory> RequestHistory { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RequestHistory>()
                .HasOne<Models.CertificateRequest>()
                .WithMany()
                .HasForeignKey(h => h.RequestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Models.CertificateRequest>()
                .HasIndex(r => new { r.EmployeeId, r.Type })
                .HasDatabaseName("Index_CertificateRequest_EmployeeId_Type");

            base.OnModelCreating(modelBuilder);
        }
    }
}
