using System;
using Bolao.Data;
using Bolao.Interfaces;
using Bolao.Repositories.Services;
using Bolao.Servicos;
using Bolao.Servicos.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bolao
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            MongoContext.ConnectionString = Configuration.GetSection("ConnectionStrings:ConnectionString").Value;
            MongoContext.DatabaseName = Configuration.GetSection("ConnectionStrings:Database").Value;
            MongoContext.IsSSL = Convert.ToBoolean(this.Configuration.GetSection("ConnectionStrings:IsSSL").Value);

            services.AddTransient<IClassificacaoService, ClassificacaoService>();
            services.AddTransient<IPaginaClassificacao, PaginaClassificacao>();
            services.AddTransient<IMotorBuscaClassificacao, MotorBuscaClassificacao>();
            services.AddTransient<ISeleniumConfiguration, SeleniumConfigurations>();
            services.AddTransient<IPaginaRodada, PaginaRodada>();
            services.AddTransient<IMotorBuscaRodada, MotorBuscaRodada>();

			services.Configure<IISOptions>(options =>
			{
				options.ForwardClientCertificate = false;
			});
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMotorBuscaClassificacao _motorBuscaClassificacao, IMotorBuscaRodada _motorBuscaRodada)
        {
            _motorBuscaClassificacao.MotorBuscaClassificacaoOnLine();

            _motorBuscaRodada.GravarResultadosDaRodada();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
