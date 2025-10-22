// Data/AppDbContext.cs
using deBurgas.Entities;
using Microsoft.EntityFrameworkCore;
using deBurgas.Models.Entities; // Asegúrate de incluir tu namespace

namespace deBurgas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // --- Tablas del Catálogo (Menú) ---
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<Modifier> Modifiers { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<CatalogItemModifier> CatalogItemModifiers { get; set; } // La tabla de unión

        // --- Tablas de Órdenes (las que ya tenías) ---
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Configurar la clave primaria compuesta para la tabla M:N
            modelBuilder.Entity<CatalogItemModifier>()
                .HasKey(cim => new { cim.ProductId, cim.ModifierId });

            // 2. Definir la relación de CatalogItem -> CatalogItemModifier
            modelBuilder.Entity<CatalogItemModifier>()
                .HasOne(cim => cim.CatalogItem)
                .WithMany(ci => ci.ItemModifiers)
                .HasForeignKey(cim => cim.ProductId);

            // 3. Definir la relación de Modifier -> CatalogItemModifier
            modelBuilder.Entity<CatalogItemModifier>()
                .HasOne(cim => cim.Modifier)
                .WithMany() // <-- Modifiers no necesita una colección directa de unión
                .HasForeignKey(cim => cim.ModifierId);

            base.OnModelCreating(modelBuilder);
        }
    }
}