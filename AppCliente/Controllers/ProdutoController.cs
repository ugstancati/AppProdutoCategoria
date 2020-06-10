using AppProdutoCategoria.AppCliente.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppProdutoCategoria.AppCliente.Controllers
{
    public class ProdutoController : Controller
    {

        public IActionResult Index()
        {
            ProdutoModel objProduto = new ProdutoModel();
            List<ProdutoModel> _ListaProdutos  = objProduto.ListarProdutos();

            return View(_ListaProdutos);
        }

        [HttpGet]
        public IActionResult Incluir()
        {
            CategoriaModel _objCategoria = new CategoriaModel();
            List<CategoriaModel> _ListCategorias = _objCategoria.ListarCategorias();
            ViewBag.ListaCategorias = _ListCategorias.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Incluir([Bind("Descricao,Preco,Id_Categoria,Data_Criacao,Produto_Inativo")] ProdutoModel dados)
        {
            if (ModelState.IsValid)
            {
                dados.IncluirProduto();
                return RedirectToAction(nameof(Index));
            }

            CategoriaModel _objCategoria = new CategoriaModel();
            List<CategoriaModel> _ListCategorias = _objCategoria.ListarCategorias();
            ViewBag.ListaCategorias = _ListCategorias.ToList();

            return View(dados);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            CategoriaModel _objCategoria = new CategoriaModel();
            List<CategoriaModel> _ListCategorias = _objCategoria.ListarCategorias();
            ViewBag.ListaCategorias = _ListCategorias.ToList();

            ProdutoModel _produto = new ProdutoModel().CarregaProduto(id);
            return View(_produto);
        }

        [HttpPost]
        public IActionResult Editar(ProdutoModel dados)
        {
            if (ModelState.IsValid)
            {
                dados.EditarProduto();
                return RedirectToAction(nameof(Index));
            }
            return View(dados);
            
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            ProdutoModel _produto = new ProdutoModel().CarregaProduto(id);
            return View(_produto);
        }

        [HttpPost]
        public IActionResult Excluir(ProdutoModel dados)
        {
            dados.ExcluirProduto(dados.Id_Produto);
            return RedirectToAction(nameof(Index));
        }


    }
}
