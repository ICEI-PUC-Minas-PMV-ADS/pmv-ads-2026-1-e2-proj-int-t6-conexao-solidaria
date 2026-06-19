using System.ComponentModel.DataAnnotations;

namespace ConexaoSolidaria.Models;

public class Grupo
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Informe o nome do grupo.")]
    [StringLength(50)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a descrição")]
    [StringLength(60)]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "Selecione o tipo de atividade.")]
    [StringLength(10)]
    [Display(Name = "Tipo de Atividade")]
    public string TipoAtiv { get; set; } = "voluntariado";
    
    [StringLength(2000)]
    public string? DescricaoDetalhada { get; set; }

    [StringLength(20)]
    public string TipoAtividade { get; set; } = "Misto";

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
