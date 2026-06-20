using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConexaoSolidaria.Models;

public class GrupoApoio
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o nome do grupo.")]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a descrição.")]
    [StringLength(200)]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "Selecione o tipo de atividade.")]
    [StringLength(20)]
    [Display(Name = "Tipo de Atividade")]
    public string TipoAtividade { get; set; } = "Misto";

    [StringLength(2000)]
    public string? DescricaoDetalhada { get; set; }


    [Required(ErrorMessage = "Informe a cidade.")]
    [StringLength(100)]
    public string? Cidade { get; set; }

    [StringLength(2)]
    public string? Estado { get; set; }

   public string? FotoUrl { get; set; }

    public bool Publico { get; set; } = true;

    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    [Required]
    public string CriadorId { get; set; } = string.Empty;

    [ForeignKey("CriadorId")]
    public virtual Usuario? Criador { get; set; }
}
