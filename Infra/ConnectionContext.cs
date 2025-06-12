using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Infra
{
    public class ConnectionContext : DbContext
    {
        // Construtor correto para injeção de dependência
        public ConnectionContext(DbContextOptions<ConnectionContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(
                "Server=localhost;" +
                "Port=5432;Database=order_db;" +
                "User Id=postgres;" +
                "Password=root");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica todas as configurações numa única chamada
            modelBuilder.Entity<Order>(entity =>
            {
                // Configuração do ID como UUID
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .ValueGeneratedOnAdd();

                // Configuração do Status (assumindo que OrderStatus é um enum)
                entity.Property(e => e.Status)
                    .HasConversion<string>()
                    .HasDefaultValue(OrderStatus.Pendente)
                    .HasMaxLength(20);

                // Configuração do CreatedAt
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("NOW()")
                    .ValueGeneratedOnAdd();

                // Configurações adicionais para outros campos se necessário
                entity.Property(e => e.Client).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Product).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Value).HasColumnType("decimal(18,2)");
            });
        }
    }
}