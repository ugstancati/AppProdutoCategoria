using AppProdutoCategoria.AppWebAPI.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace AppProdutoCategoria.AppWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<CategoriaContexto, CategoriaContexto>();
            services.AddDbContext<CategoriaContexto>(options =>
                options.UseMySql(connection)
            );
            services.AddScoped<ProdutoContexto, ProdutoContexto>();
            services.AddDbContext<ProdutoContexto>(options =>
                options.UseMySql(connection)
            );

            services.AddControllers();

            // Configurando o servi�o de documenta��o do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "App Produtos - Categoria",
                        Version = "v1",
                        Description = "Exemplo de WebAPI criada com o ASP.NET Core 3.0 para consulta de Produtos/Categorias",
                        Contact = new OpenApiContact
                        {
                            Name = "Ulysses G.",
                            Url = new Uri("https://github.com/ugstancati")
                        }
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Ativando middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "App Produto/Categoria WebAPI V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Ativando middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Conversor de Temperaturas");
            });
        }
    }
}
