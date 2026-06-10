namespace ConexaoSolidaria.Models
{
    public class ChatApoio
    {
        public int Id { get; set; }

        // Liga o chat a uma oferta de ajuda específica (Substituído DoacaoId)
        public int OfertaAjudaId { get; set; }
        public virtual OfertaAjuda? OfertaAjuda { get; set; }

        public DateTime CriadoEm { get; set; } = DateTime.Now;

        // Lista de mensagens desta conversa
        public virtual ICollection<MensagemChat> Mensagens { get; set; } = new List<MensagemChat>();
    }
}