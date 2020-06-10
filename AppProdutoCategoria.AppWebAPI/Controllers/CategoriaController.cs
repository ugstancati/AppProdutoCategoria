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
    [Route("v1/Categorias")]

    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaContexto _context;

        public CategoriaController(CategoriaContexto context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Categoria>>> Get([FromServices] CategoriaContexto context)
        {
            var _categorias = await context.Categoria.ToListAsync();
            return _categorias;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> GetById([FromServices] CategoriaContexto context, int id)
        {
            var _categoria = await context.Categoria
                .AsNoTracking() //não cria proxy dos objetos. (utilizado quando não é insert/update/delete, só get é recomendável.)
                .FirstOrDefaultAsync(x => x.Id_Categoria == id);
            return _categoria;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Categoria>> Post([FromServices] CategoriaContexto context, [FromBody] Categoria model)
        {
            if (ModelState.IsValid)
            {
                context.Categoria.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> AtualizarById(int id, [FromBody] Categoria model)
        {
            if (id != model.Id_Categoria)
            {
                return BadRequest();
            }

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

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<Categoria>> Atualizar([FromBody] Categoria model)
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
        public async Task<ActionResult<Categoria>> DeleteConfirmed(int id)
        {
            try
            {
                var categoria = await _context.Categoria.SingleOrDefaultAsync(m => m.Id_Categoria == id);

                if (categoria == null)
                {
                    return NotFound();
                }

                _context.Categoria.Remove(categoria);
                await _context.SaveChangesAsync();
                return categoria;
            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
