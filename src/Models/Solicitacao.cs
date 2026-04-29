using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConexaoSolidaria.Models;

/// <summary>
/// Representa uma solicitação de ajuda registrada por um beneficiário.
/// Atende aos requisitos RF05, RF06 e RF08.
/// </summary>
public class Solicitacao
{
    public int Id { get; set; }

    [Required]
    [StringLength(450)]
    public string UsuarioId { get; set; } = string.Empty;

    [ForeignKey(nameof(UsuarioId))]
    public Usuario? Usuario { get; set; }

    [Required(ErrorMessage = "Informe o tipo de necessidade.")]
    [StringLength(30)]
    [Display(Name = "Tipo de necessidade")]
    public string TipoNecessidade { get; set; } = string.Empty;
    // Valores aceitos: alimentos | medicamentos | abrigo | vestuario | outros

    [Required(ErrorMessage = "Informe um título para a solicitação.")]
    [StringLength(120)]
    [Display(Name = "Título")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descreva sua solicitação.")]
    [StringLength(1000)]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Selecione a urgência.")]
    [StringLength(10)]
    [Display(Name = "Urgência")]
    public string Urgencia { get; set; } = "media";
    // Valores aceitos: alta | media | baixa  (RF08)

    [Required(ErrorMessage = "Informe a cidade.")]
    [StringLength(100)]
    public string Cidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o estado.")]
    [StringLength(2)]
    public string Estado { get; set; } = string.Empty;

    /// <summary>URL pública do anexo (foto da situação) no Azure Blob Storage.</summary>
    [StringLength(500)]
    public string? AnexoUrl { get; set; }

    [StringLength(15)]
    public string Status { get; set; } = "ativa";
    // Valores aceitos: ativa | atendida | cancelada

    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}
