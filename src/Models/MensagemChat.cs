using System.ComponentModel.DataAnnotations;

namespace ConexaoSolidaria.Models
{
    public class MensagemChat
    {
        public int Id { get; set; }

        public int ChatApoioId { get; set; }
        public virtual ChatApoio? ChatApoio { get; set; }

        [Required]
        public string RemetenteId { get; set; } = string.Empty;
        public virtual Usuario? Remetente { get; set; }

        [Required]
        public string Conteudo { get; set; } = string.Empty;

        public DateTime EnviadaEm { get; set; } = DateTime.Now;
    }
}