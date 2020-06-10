using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppProdutoCategoria.AppWebAPI.Models
{
    public class Categoria
    {
        [Key]
        [Display(Name = "Id")]
        public int Id_Categoria { get; set; }

        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        [MinLength(3, ErrorMessage = "O campo descrição deve ter no mínimo 3 caracteres.")]
        [MaxLength(60, ErrorMessage = "O campo descrição deve ter no máximo 60 caracteres.")]
        [Display(Name = "Descrição Categoria")]
        public string Descricao { get; set; }

    }
}
