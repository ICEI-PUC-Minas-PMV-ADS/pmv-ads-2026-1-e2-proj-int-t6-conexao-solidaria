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

    [Required(ErrorMessage = "Forneça uma descrição.")]
    [StringLength(500)]
    public string Descricao { get; set; } = string.Empty;
}
