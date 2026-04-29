using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ConexaoSolidaria.Models;

/// <summary>
/// Representa um usuário do Conexão Solidária.
/// Estende IdentityUser para reaproveitar autenticação, hash de senha
/// e gerenciamento de sessão do ASP.NET Core Identity (atende RF01, RF02, RF13).
/// </summary>
public class Usuario : IdentityUser
{
    [Required]
    [StringLength(120)]
    public string NomeCompleto { get; set; } = string.Empty;

    [StringLength(20)]
    public string? Telefone { get; set; }

    [StringLength(100)]
    public string? Cidade { get; set; }

    [StringLength(2)]
    public string? Estado { get; set; }

    /// <summary>URL pública da foto de perfil no Azure Blob Storage.</summary>
    [StringLength(500)]
    public string? FotoUrl { get; set; }

    /// <summary>Tipo de perfil. Para a POC mantemos como string simples.</summary>
    [StringLength(20)]
    public string TipoPerfil { get; set; } = "beneficiario";

    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    // Relacionamento: um usuário pode ter várias solicitações
    public ICollection<Solicitacao> Solicitacoes { get; set; } = new List<Solicitacao>();
}
