using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConexaoSolidaria.Models
{
    [Table("Doacoes")]
    public class Doacao
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o doador")]
        [Display(Name = "Doador")]
        public string DoadorId { get; set; }

        [ForeignKey("DoadorId")]
        public Usuario? Doador { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a solicitação")]
        [Display(Name = "Solicitação de Ajuda")]
        public int SolicitacaoId { get; set; }

        [ForeignKey("SolicitacaoId")]
        public Solicitacao? Solicitacao { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a descrição dos itens")]
        [Display(Name = "Itens Doados")]
        public string? ItensDoados { get; set; }

        [Required]
        [Display(Name = "Data da Doação")]
        public DateTime DataDoacao { get; set; }

        public StatusDoacao Status { get; set; }
    }

    public enum StatusDoacao
    {
        Pendente,
        Entregue
    }
}