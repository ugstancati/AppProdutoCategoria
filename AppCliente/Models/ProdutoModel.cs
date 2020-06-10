using AppProdutoCategoria.AppCliente.Util;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppProdutoCategoria.AppCliente.Models
{
    public class ProdutoModel
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
        [Column(TypeName = "decimal(18,2")]
        [DefaultValue("0,00")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O campo categoria é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Categoria inválida, deve ser maior que zero.")]
        [Display(Name = "Categoria")]
        public int Id_Categoria { get; set; }


        [ForeignKey("Id_Categoria")]
        public virtual CategoriaModel Categoria { get; set; }

        [Required(ErrorMessage = "O campo preço é obrigatório")]
        [Display(Name = "Data Criação Produto")]
        public DateTime Data_Criacao { get; set; }

        [Display(Name = "Produto Inativo")]
        public Boolean Produto_Inativo { get; set; }



        private string _Entidade = "Produtos";

        public List<ProdutoModel> ListarProdutos()
        {
            List<ProdutoModel> retorno = new List<ProdutoModel>();
            string json = WebAPI.RequestGET(_Entidade, string.Empty, string.Empty);
            retorno = JsonConvert.DeserializeObject<List<ProdutoModel>>(json);
            return retorno;
        }

        public ProdutoModel CarregaProduto(int id)
        {
            ProdutoModel retorno = new ProdutoModel();
            string json = WebAPI.RequestGET(_Entidade, string.Empty, id.ToString());
            retorno = JsonConvert.DeserializeObject<ProdutoModel>(json);
            return retorno;
        }

        public List<ProdutoModel> CarregaProdutoByCategoria(int idCategoria)
        {
            List<ProdutoModel> retorno = new List<ProdutoModel>();
            string json = WebAPI.RequestGET( _Entidade, "categorias", idCategoria.ToString());
            retorno = JsonConvert.DeserializeObject<List<ProdutoModel>>(json);
            return retorno;
        }

        public void IncluirProduto()
        {
            string jsonData = JsonConvert.SerializeObject(this);
            string json = string.Empty;
            WebAPI.RequestPOST(_Entidade, string.Empty, jsonData);
        }

        public void EditarProduto()
        {
            string jsonData = JsonConvert.SerializeObject(this);
            string json = string.Empty;
            WebAPI.RequestPUT(_Entidade, string.Empty, jsonData);
        }

        public void ExcluirProduto(int id)
        {
            string json = WebAPI.RequestDELETE(_Entidade, string.Empty, id.ToString());
        }





    }
}
