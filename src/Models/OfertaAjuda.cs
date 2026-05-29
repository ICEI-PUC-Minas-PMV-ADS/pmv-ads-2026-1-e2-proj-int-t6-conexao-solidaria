using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConexaoSolidaria.Models;

/// <summary>
/// Representa uma oferta de ajuda de um voluntário a uma solicitação específica.
/// Cobre o ciclo: oferta → confirmação → entrega. Atende RF07, RF09 e RF10.
///
/// Estados possíveis (campo Status):
///   - confirmada : oferta foi feita, aguardando entrega
///   - concluida  : entrega foi confirmada pelo voluntário (Tela 17)
///   - cancelada  : oferta foi cancelada por uma das partes
/// </summary>
public class OfertaAjuda
{
    public int Id { get; set; }

    // Solicitação atendida -------------------------------------------------------
    [Required]
    public int SolicitacaoId { get; set; }

    [ForeignKey(nameof(SolicitacaoId))]
    public Solicitacao? Solicitacao { get; set; }

    // Voluntário que ofereceu a ajuda --------------------------------------------
    [Required]
    [StringLength(450)]
    public string VoluntarioId { get; set; } = string.Empty;

    [ForeignKey(nameof(VoluntarioId))]
    public Usuario? Voluntario { get; set; }

    // Modalidade da ajuda (Tela 10 — Como Ajudar) --------------------------------
    [Required(ErrorMessage = "Selecione a modalidade de ajuda.")]
    [StringLength(20)]
    [Display(Name = "Modalidade")]
    public string Modalidade { get; set; } = "doacao";
    // Valores aceitos: doacao | voluntariado | transporte

    [StringLength(500)]
    [Display(Name = "Mensagem para o solicitante")]
    public string? Mensagem { get; set; }

    // Estado da oferta -----------------------------------------------------------
    [StringLength(15)]
    public string Status { get; set; } = "confirmada";

    public DateTime CriadaEm { get; set; } = DateTime.UtcNow;

    // Dados de conclusão/entrega (preenchidos na Tela 17) ------------------------
    [StringLength(500)]
    public string? FotoEntregaUrl { get; set; }

    [StringLength(500)]
    [Display(Name = "Observações da entrega")]
    public string? ObservacoesEntrega { get; set; }

    public DateTime? ConcluidaEm { get; set; }
}
