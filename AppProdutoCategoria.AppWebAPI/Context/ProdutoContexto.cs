using AppProdutoCategoria.AppWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppProdutoCategoria.AppWebAPI.Context
{
    public class ProdutoContexto : DbContext
    {
        public ProdutoContexto(DbContextOptions<ProdutoContexto> options) : base(options)
        {
        }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
    }
}
