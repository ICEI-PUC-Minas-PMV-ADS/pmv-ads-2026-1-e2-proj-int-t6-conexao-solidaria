using ConexaoSolidaria.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConexaoSolidaria.Data;

/// <summary>
/// Contexto do Entity Framework Core. Estende IdentityDbContext
/// para que as tabelas de Identity sejam criadas no Azure SQL.
/// DI - Injestão de Dependência - Classe Startup
/// </summary>
public class AppDbContext : IdentityDbContext<Usuario>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Solicitacao> Solicitacoes => Set<Solicitacao>();
    public DbSet<Doacao> Doacoes => Set<Doacao>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configuração da tabela Solicitacao
        builder.Entity<Solicitacao>()
            .HasIndex(s => new { s.Status, s.Urgencia, s.CriadoEm });

        builder.Entity<Solicitacao>()
            .HasIndex(s => s.Cidade);

        // Configuração para a tabela de Doacoes
        builder.Entity<Doacao>()
            .HasOne(d => d.Doador)
            .WithMany()
            .HasForeignKey(d => d.DoadorId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder.Entity<Doacao>()
            .HasOne(d => d.PedidoAjuda)
            .WithMany()
            .HasForeignKey(d => d.PedidoAjudaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
}
