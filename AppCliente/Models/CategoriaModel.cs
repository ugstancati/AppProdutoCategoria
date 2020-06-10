using AppProdutoCategoria.AppCliente.Controllers;
using AppProdutoCategoria.AppCliente.Util;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppProdutoCategoria.AppCliente.Models
{
    public class CategoriaModel
    {
        [Key]
        [Display(Name = "Id")]
        public int Id_Categoria { get; set; }

        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        [MinLength(3, ErrorMessage = "O campo descrição deve ter no mínimo 3 caracteres.")]
        [MaxLength(60, ErrorMessage = "O campo descrição deve ter no máximo 60 caracteres.")]
        [Display(Name = "Descrição Categoria")]
        public string Descricao { get; set; }



        private string _Entidade = "Categorias";

        public List<CategoriaModel> ListarCategorias()
        {
            List<CategoriaModel> retorno = new List<CategoriaModel>();
            string json = WebAPI.RequestGET(_Entidade, string.Empty, string.Empty);
            retorno = JsonConvert.DeserializeObject<List<CategoriaModel>>(json);
            return retorno;
        }

        public CategoriaModel CarregaCategoria(int id)
        {
            CategoriaModel retorno = new CategoriaModel();
            string json = WebAPI.RequestGET(_Entidade, string.Empty, id.ToString());
            retorno = JsonConvert.DeserializeObject<CategoriaModel>(json);
            return retorno;
        }

        public void InserirCategoria()
        {
            string jsonData = JsonConvert.SerializeObject(this);
            string json = string.Empty; 
            WebAPI.RequestPOST(_Entidade,string.Empty, jsonData);
        }

        public void EditarCategoria()
        {
            string jsonData = JsonConvert.SerializeObject(this);
            string json = string.Empty;
            WebAPI.RequestPUT(_Entidade, string.Empty  + Id_Categoria, jsonData);
        }

        public void ExcluirCategoria(int id)
        {
            string json = WebAPI.RequestDELETE(_Entidade, string.Empty , id.ToString());
        }


    }
}
