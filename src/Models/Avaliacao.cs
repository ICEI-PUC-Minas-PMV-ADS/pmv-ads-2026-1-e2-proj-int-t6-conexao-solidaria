using System.ComponentModel.DataAnnotations;

namespace ConexaoSolidaria.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }

        [Required]
        public int DoacaoId { get; set; }
        public virtual Doacao? Doacao { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "A nota deve ser entre 1 e 5.")]
        public int Nota { get; set; } // Estrelas

        [StringLength(500)]
        public string? Comentario { get; set; }

        public DateTime DataAvaliacao { get; set; } = DateTime.Now;

        // Quem está sendo avaliado (o Doador)
        [Required]
        public string AvaliadoId { get; set; } = string.Empty;
        public virtual Usuario? Avaliado { get; set; }

        // Quem está avaliando (o Beneficiário)
        [Required]
        public string AvaliadorId { get; set; } = string.Empty;
        public virtual Usuario? Avaliador { get; set; }
    }
}