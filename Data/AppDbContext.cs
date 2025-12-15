using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OtomasyonApi.Models;

namespace OtomasyonApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }  // sende zaten vardı
        public DbSet<Cari> Cariler { get; set; }
        public DbSet<CariHareket> CariHareketler { get; set; }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Fatura> Faturalar { get; set; }
        public DbSet<FaturaKalemi> FaturaKalemleri { get; set; }
        public DbSet<Kasa> Kasalar { get; set; }
        public DbSet<Banka> Bankalar { get; set; }

        public override int SaveChanges()
        {
            ConvertDatesToUtc();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ConvertDatesToUtc();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ConvertDatesToUtc()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                foreach (var property in entry.Properties)
                {
                    if (property.Metadata.ClrType == typeof(DateTime) && property.CurrentValue != null)
                    {
                        var dateTime = (DateTime)property.CurrentValue;
                        if (dateTime.Kind != DateTimeKind.Utc)
                        {
                            property.CurrentValue = dateTime.Kind == DateTimeKind.Unspecified
                                ? DateTime.SpecifyKind(dateTime, DateTimeKind.Utc)
                                : dateTime.ToUniversalTime();
                        }
                    }
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cari - CariHareket ilişkisi
            modelBuilder.Entity<Cari>()
                .HasMany(c => c.Hareketler)
                .WithOne(h => h.Cari)
                .HasForeignKey(h => h.CariId);

            // Fatura - FaturaKalemi ilişkisi
            modelBuilder.Entity<Fatura>()
                .HasMany(f => f.Kalemler)
                .WithOne(k => k.Fatura)
                .HasForeignKey(k => k.FaturaId);

            // Fatura - Cari ilişkisi
            modelBuilder.Entity<Fatura>()
                .HasOne(f => f.Cari)
                .WithMany()
                .HasForeignKey(f => f.CariId)
                .OnDelete(DeleteBehavior.SetNull);

            // FaturaKalemi - Urun ilişkisi
            modelBuilder.Entity<FaturaKalemi>()
                .HasOne(k => k.Urun)
                .WithMany()
                .HasForeignKey(k => k.UrunId)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }
    }
}
