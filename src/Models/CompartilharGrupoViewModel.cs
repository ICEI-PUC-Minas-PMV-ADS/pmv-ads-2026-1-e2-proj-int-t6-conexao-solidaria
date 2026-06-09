using System.Collections.Generic;

namespace ConexaoSolidaria.Models
{
    public class CompartilharGrupoViewModel
    {
        public int GrupoId { get; set; }
        public string NomeGrupo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int TotalMembros { get; set; }
        public string LinkGrupo { get; set; } = string.Empty;
        public string QrCodeUrl { get; set; } = string.Empty;
        public List<UsuarioConviteViewModel> UsuariosSugeridos { get; set; } = new();
    }

    public class UsuarioConviteViewModel
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Papel { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Iniciais { get; set; } = string.Empty;
        public bool JaConvidado { get; set; }
    }

    public class ConvidarUsuarioRequest
    {
        public int GrupoId { get; set; }
        public int UsuarioId { get; set; }
    }
}