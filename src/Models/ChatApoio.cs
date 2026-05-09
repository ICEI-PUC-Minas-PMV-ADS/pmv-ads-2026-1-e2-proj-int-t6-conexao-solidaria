namespace ConexaoSolidaria.Models
{
    public class ChatApoio
    {
        public int Id { get; set; }

        // Liga o chat a uma doação específica
        public int DoacaoId { get; set; }
        public virtual Doacao? Doacao { get; set; }

        public DateTime CriadoEm { get; set; } = DateTime.Now;

        // Lista de mensagens desta conversa
        public virtual ICollection<MensagemChat> Mensagens { get; set; } = new List<MensagemChat>();
    }
}