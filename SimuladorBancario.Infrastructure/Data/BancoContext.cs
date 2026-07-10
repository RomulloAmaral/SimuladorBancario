using Microsoft.EntityFrameworkCore;
using SimuladorBancario.Domain.Entities;

namespace SimuladorBancario.Infrastructure.Data;

public class BancoContext : DbContext
{
    public BancoContext(DbContextOptions<BancoContext> options)
        : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Conta> Contas { get; set; }
    public DbSet<Movimentacao> Movimentacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.CpfCnpj).IsRequired().HasMaxLength(14);
            e.HasIndex(c => c.CpfCnpj).IsUnique();
            e.ToTable("Clientes");
        });

        modelBuilder.Entity<Conta>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Saldo).HasPrecision(18, 2);
            e.Property(c => c.ClienteId);
            e.HasMany(c => c.Movimentacoes)
             .WithOne()
             .HasForeignKey(m => m.ContaId);
            e.ToTable("Contas");
        });

        modelBuilder.Entity<Movimentacao>(e =>
        {
            e.HasKey(m => m.Id);
            e.Property(m => m.Valor).HasPrecision(18, 2);
            e.ToTable("Movimentacoes");
        });
    }
}