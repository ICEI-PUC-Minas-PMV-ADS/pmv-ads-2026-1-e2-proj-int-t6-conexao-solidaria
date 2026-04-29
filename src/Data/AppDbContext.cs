using ConexaoSolidaria.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConexaoSolidaria.Data;

/// <summary>
/// Contexto do Entity Framework Core. Estende IdentityDbContext
/// para que as tabelas de Identity sejam criadas no Azure SQL.
/// </summary>
public class AppDbContext : IdentityDbContext<Usuario>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Solicitacao> Solicitacoes => Set<Solicitacao>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Indices uteis para a tela de listagem (RF06)
        builder.Entity<Solicitacao>()
            .HasIndex(s => new { s.Status, s.Urgencia, s.CriadoEm });

        builder.Entity<Solicitacao>()
            .HasIndex(s => s.Cidade);
    }
}
