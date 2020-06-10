using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppProdutoCategoria.AppWebAPI.Models
{
    public class Produto
    {
        [Key]
        [Display(Name = "Id")]
        public int Id_Produto { get; set; }

        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        [MinLength(3, ErrorMessage = "O campo descrição deve ter no mínimo 3 caracteres.")]
        [MaxLength(60, ErrorMessage = "O campo descrição deve ter no máximo 60 caracteres.")]
        [Display(Name = "Descrição Produto")]
        public string Descricao { get; set; }


        [Required(ErrorMessage = "O campo preço é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O campo categoria é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Categoria inválida, deve ser maior que zero.")]
        [Display(Name = "Categoria")]
        public int Id_Categoria { get; set; }


        [ForeignKey("Id_Categoria")]
        public virtual Categoria Categoria { get; set; }

        [Display(Name = "Data Criação Produto")]
        //[DataType(DataType.Date)]
        public DateTime Data_Criacao { get; set; }

        [Display(Name = "Produto Inativo")]
        public Boolean Produto_Inativo { get; set; }
    }
}
