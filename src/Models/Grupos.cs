using System.ComponentModel.DataAnnotations;

namespace ConexaoSolidaria.Models;

public class Grupo
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [StringLength(200)]
    public string? Descricao { get; set; }

    [StringLength(20)]
    public string TipoAtividade { get; set; } = "Misto";

    [StringLength(100)]
    public string? Cidade { get; set; }

    [StringLength(2)]
    public string? Estado { get; set; }

    public bool Publico { get; set; } = true;

    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    [Required]
    public string CriadorId { get; set; } = string.Empty;
    public virtual Usuario? Criador { get; set; }
}
