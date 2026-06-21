using System.ComponentModel.DataAnnotations;

namespace ConexaoSolidaria.Models;

public class Grupo
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a descrição")]
    [StringLength(200)]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "Selecione o tipo de atividade.")]
    [StringLength(10)]
    [Display(Name = "Tipo de Atividade")]
    public string TipoAtiv { get; set; } = "voluntariado";
    
    [StringLength(2000)]
    public string? DescricaoDetalhada { get; set; }

    [StringLength(20)]
    public string TipoAtividade { get; set; } = "Ambos";

    [Required(ErrorMessage = "Informe a cidade.")]
    [StringLength(100)]
    public string? Cidade { get; set; }
    
    public string? FotoUrl { get; set; }
    
    [StringLength(2)]
    public string? Estado { get; set; }

    public bool Publico { get; set; } = true;

    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    [Required]
    public string CriadorId { get; set; } = string.Empty;
    public virtual Usuario? Criador { get; set; }
}
