using AppProdutoCategoria.AppWebAPI.Context;
using AppProdutoCategoria.AppWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AppProdutoCategoria.AppWebAPI.Controllers
{
    [ApiController]
    [Route("v1/Produtos")]

    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoContexto _context;

        public ProdutoController(ProdutoContexto context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Produto>>> Get([FromServices] ProdutoContexto context)
        {
            var _produtos = await context.Produto.Include(x => x.Categoria).ToListAsync();
            return _produtos;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Produto>> GetById([FromServices] ProdutoContexto context, int id)
        {
            var _produto = await context.Produto.Include(x => x.Categoria)
                .AsNoTracking() //não cria proxy dos objetos. (utilizado quando não é insert/update/delete, só get é recomendável.)
                .FirstOrDefaultAsync(x => x.Id_Produto == id);
            return _produto;
        }

        [HttpGet]
        [Route("categorias/{id:int}")]
        public async Task<ActionResult<List<Produto>>> GetByCategory([FromServices] ProdutoContexto context, int id)
        {
            var _produtos = await context.Produto
                .Include(x => x.Categoria)
                .AsNoTracking()
                .Where(x => x.Id_Categoria == id)
                .ToListAsync();
            return _produtos;
        }

        [HttpGet]
        [Route("{ProdutoAtivo:bool}")]
        public async Task<ActionResult<List<Produto>>> GetByCategory([FromServices] ProdutoContexto context, bool ProdutoAtivo)
        {
            var _produtos = await context.Produto
                .Include(x => x.Categoria)
                .AsNoTracking()
                .Where(x => x.Produto_Inativo != ProdutoAtivo)
                .ToListAsync();
            return _produtos;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Produto>> Post([FromServices] ProdutoContexto context, [FromBody] Produto model)
        {
            if (ModelState.IsValid)
            {
                context.Produto.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<Produto>> Atualizar([FromBody] Produto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();

                }
                return model;
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Produto>> DeleteConfirmed(int id)
        {
            try
            {
                var produto = await _context.Produto.SingleOrDefaultAsync(m => m.Id_Produto == id);

                if (produto == null)
                {
                    return NotFound();
                }

                _context.Produto.Remove(produto);
                await _context.SaveChangesAsync();
                return produto;
            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
