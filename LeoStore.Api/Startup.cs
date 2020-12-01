using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeoStore.Domain.StoreContext.Commands.OrderCommands.Inputs;
using LeoStore.Domain.StoreContext.Handlers;
using LeoStore.Domain.StoreContext.Repositories;
using LeoStore.Domain.StoreContext.Services;
using LeoStore.Infra.StoreContext.DataContext;
using LeoStore.Infra.StoreContext.Repositories;
using LeoStore.Infra.StoreContext.Services;
using LeoStore.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace LeoStore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // Adiciono os middlewares
        public void ConfigureServices(IServiceCollection services)
        {
            // Para permitir as requisições localhost
            services.AddCors();
            // Vai comprimir o JSON antes de mandar para a tela
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                // Vai comprimir tudo que for application/json
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            });

            services.AddControllers();

            // Injeção de dependência
            // Resolvo as dependências
            // Sempre que ele chegar em um construtor que tiver a necessidade ICustomerRepository, ele vai instanciar um CustomerRepository
            // E isso para todas as outras dependências
            services.AddScoped<LeoDataContext, LeoDataContext>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<CustomerHandler, CustomerHandler>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<CreateProductHandler, CreateProductHandler>();
            // services.AddTransient<PlaceOrderCommand, PlaceOrderCommand>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<OrderHandler, OrderHandler>();

            // Documentação da api
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Leo Store", Version = "v1" });
            }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseSwagger(); // Permite que tenhamos uma especificação da api em formato JSON

            // Para ter uma ferramenta visual para testar a aplicação
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Leo Store V1"); // O /swagger/v1/swagger.json fica na requisição: http://localhost:5000/swagger/v1/swagger.json
                // E se usar no navegador: http://localhost:5000/swagger temos a documentação visual da API
            });

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );


            app.UseAuthorization();

            app.UseResponseCompression(); // Para usar a compressão das requisições. Todas as requisições vão ser compactadas com o GZip

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
