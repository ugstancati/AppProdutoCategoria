using AppProdutoCategoria.AppWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppProdutoCategoria.AppWebAPI.Context
{
    public class CategoriaContexto : DbContext
    {
        public CategoriaContexto(DbContextOptions<CategoriaContexto> options) : base(options)
        {
        }
        public DbSet<Categoria> Categoria { get; set; }
    }
}
