using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppProdutoCategoria.AppCliente.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AppProdutoCategoria.AppCliente.Controllers
{
    public class CategoriaController : Controller
    {


        public IActionResult Index()
        {
            CategoriaModel objCategoria = new CategoriaModel();
            List<CategoriaModel> _ListaCategorias = objCategoria.ListarCategorias();

            return View(_ListaCategorias);
        }

        [HttpGet]
        public IActionResult Incluir(int? id)
        {
            if (id != null)
            {
                ViewBag.Registro = new CategoriaModel();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Incluir(CategoriaModel dados)
        {
            if (ModelState.IsValid)
            {
                dados.InserirCategoria();
                return RedirectToAction(nameof(Index));
            }
            return View(dados);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            CategoriaModel _categoria = new CategoriaModel().CarregaCategoria(id);
            return View(_categoria);
        }

        [HttpPost]
        public IActionResult Editar(CategoriaModel dados)
        {
            if (ModelState.IsValid)
            {
                dados.EditarCategoria();
                return RedirectToAction(nameof(Index));
            }
            return View(dados);
        }        
        

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            CategoriaModel _categoria = new CategoriaModel().CarregaCategoria(id);
            return View(_categoria);
        }

        [HttpPost]
        public IActionResult Excluir(CategoriaModel dados)
        {
            ProdutoModel  _objProduto = new ProdutoModel();
            List<ProdutoModel> _ListaProdutos = _objProduto.CarregaProdutoByCategoria(dados.Id_Categoria);
            if (_ListaProdutos.Count == 0)
            {
                dados.ExcluirCategoria(dados.Id_Categoria);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("NaoPodeExcluir");
            }

        }


    }
}