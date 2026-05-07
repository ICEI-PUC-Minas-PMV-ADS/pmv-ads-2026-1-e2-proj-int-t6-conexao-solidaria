using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ConexaoSolidaria.Models
{
    [Table("Doacoes")]
    public class Doacao
    {
        [Key]
        public int Id { get; set; }


        [ValidateNever]
        [Display(Name = "Doador")]
        public string DoadorId { get; set; }

        [ForeignKey("DoadorId")]
        [ValidateNever]
        public Usuario? Doador { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a solicitação")]
        [Display(Name = "Solicitação de Ajuda")]
        public int SolicitacaoId { get; set; }

        [ForeignKey("SolicitacaoId")]
        [ValidateNever]
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