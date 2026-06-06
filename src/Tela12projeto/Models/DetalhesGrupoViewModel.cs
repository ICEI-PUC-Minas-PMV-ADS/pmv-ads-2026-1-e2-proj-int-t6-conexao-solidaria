namespace Tela_12_projeto.Models
{
    public class DetalhesGrupoViewModel
    {
        public GrupoDetalhe Grupo { get; set; } = new();
        public List<PessoaGrupo> Moderadores { get; set; } = new();
        public List<PessoaGrupo> Membros { get; set; } = new();
        public List<MensagemGrupo> Mensagens { get; set; } = new();
        public string? FeedbackMensagem { get; set; }
    }

    public class GrupoDetalhe
    {
        public string Nome { get; set; } = "";
        public string Tema { get; set; } = "";
        public string Regiao { get; set; } = "";
        public string TipoAcesso { get; set; } = "";
        public string Descricao { get; set; } = "";
        public int TotalMembros { get; set; }
        public int TotalMissoes { get; set; }
        public int TotalAjudas { get; set; }
    }

    public class PessoaGrupo
    {
        public string Nome { get; set; } = "";
        public string Funcao { get; set; } = "";
        public string Cidade { get; set; } = "";
        public string Iniciais { get; set; } = "";
    }

    public class MensagemGrupo
    {
        public string Nome { get; set; } = "";
        public string Texto { get; set; } = "";
        public string Horario { get; set; } = "";
        public string Iniciais { get; set; } = "";
    }
}