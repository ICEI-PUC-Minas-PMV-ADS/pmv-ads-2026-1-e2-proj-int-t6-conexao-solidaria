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

    public DbSet<OfertaAjuda> OfertasAjuda => Set<OfertaAjuda>();

    public DbSet<Avaliacao> Avaliacoes { get; set; }

    public DbSet<ChatApoio> Chats { get; set; }

    public DbSet<MensagemChat> MensagensChat { get; set; }

    public DbSet<GrupoApoio> Grupos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Regra para evitar o erro de múltiplos caminhos em cascata
        builder.Entity<OfertaAjuda>()
            .HasOne(o => o.Solicitacao)
            .WithMany()
            .HasForeignKey(o => o.SolicitacaoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuração da tabela Solicitacao
        builder.Entity<Solicitacao>()
            .HasIndex(s => new { s.Status, s.Urgencia, s.CriadoEm });

        builder.Entity<Solicitacao>()
            .HasIndex(s => s.Cidade);

        // Configuração para a tabela de Avaliacoes
        builder.Entity<Avaliacao>()
            .HasOne(a => a.Avaliado)
            .WithMany()
            .HasForeignKey(a => a.AvaliadoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Avaliacao>()
            .HasOne(a => a.Avaliador)
            .WithMany()
            .HasForeignKey(a => a.AvaliadorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuração para ChatApoio
        builder.Entity<ChatApoio>()
            .HasOne(c => c.OfertaAjuda)
            .WithMany()
            .HasForeignKey(c => c.OfertaAjudaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuração para MensagemChat
        builder.Entity<MensagemChat>()
            .HasOne(m => m.ChatApoio)
            .WithMany(c => c.Mensagens)
            .HasForeignKey(m => m.ChatApoioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<MensagemChat>()
            .HasOne(m => m.Remetente)
            .WithMany()
            .HasForeignKey(m => m.RemetenteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}