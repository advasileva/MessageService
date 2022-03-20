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
    /// ����� ��� ����������� ��������� ���������.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ������������� ������������.
        /// </summary>
        /// <param name="configuration">������������.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// ������������.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ��������� �������� ������������.
        /// </summary>
        /// <param name="services">�������.</param>
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
        /// ��������� ������������.
        /// </summary>
        /// <param name="app">����������.</param>
        /// <param name="env">�����.</param>
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
