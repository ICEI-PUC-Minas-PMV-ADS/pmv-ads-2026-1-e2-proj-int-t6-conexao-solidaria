using System.Collections.Generic;

namespace ConexaoSolidaria.Models
{
    public class CompartilharGrupoViewModel
    {
        public int GrupoId { get; set; }
        public string NomeGrupo { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public int TotalMembros { get; set; }
        public string LinkGrupo { get; set; }
        public string QrCodeUrl { get; set; }
        public List<UsuarioConviteViewModel> UsuariosSugeridos { get; set; }
    }

    public class UsuarioConviteViewModel
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Papel { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Iniciais { get; set; }
        public bool JaConvidado { get; set; }
    }

    public class ConvidarUsuarioRequest
    {
        public int GrupoId { get; set; }
        public int UsuarioId { get; set; }
    }
}