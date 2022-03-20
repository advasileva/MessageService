using MessageService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.IO;

namespace MessageService
{
    /// <summary>
    /// Класс для определения поведения программы.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Инициализация конфигурации.
        /// </summary>
        /// <param name="configuration">Конфигурация.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Конфигурация.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Настройка сервисов конфигурации.
        /// </summary>
        /// <param name="services">Сервисы.</param>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MessageService", Version = "v1" });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "MessageService.xml");
                c.IncludeXmlComments(filePath);
            });
            services.AddSingleton<DataStore>();
        }

        /// <summary>
        /// Настройка конфигурации.
        /// </summary>
        /// <param name="app">Приложение.</param>
        /// <param name="env">Среда.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MessageService v1"));
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
