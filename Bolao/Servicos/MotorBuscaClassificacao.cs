using Bolao.Data;
using Bolao.Models;
using Bolao.Servicos.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace Bolao.Servicos
{
    public class MotorBuscaClassificacao : IMotorBuscaClassificacao
    {
        private readonly ISeleniumConfiguration _seleniumConfiguration;
        public MotorBuscaClassificacao(ISeleniumConfiguration seleniumConfiguration)
        {
            _seleniumConfiguration = seleniumConfiguration;
        }

        public void MotorBuscaClassificacaoOnLine()
        {
            _seleniumConfiguration.SetarVariavelDeAmbiente();

            IConfigurationBuilder builder = ObterConfiguracaoDoAppSettings();

            var configuration = builder.Build();

            new ConfigureFromConfigurationOptions<ISeleniumConfiguration>(
                configuration.GetSection("SeleniumConfigurations"))
                    .Configure(_seleniumConfiguration);



            PaginaClassificacao paginaClassificacao = ObterPaginaClassificacao(_seleniumConfiguration);

            paginaClassificacao.CarregarPagina();

            var classificacao = paginaClassificacao.ObterClassificacao();

            paginaClassificacao.Fechar();

            SalvarClassificacao(classificacao);
        }      

        public void SalvarClassificacao(Campeonato classificacao)
        {
            new MongoContext().Incluir(classificacao);
        }

        public PaginaClassificacao ObterPaginaClassificacao(ISeleniumConfiguration seleniumConfigurations)
        {
            return new PaginaClassificacao(seleniumConfigurations);
        }
        public IConfigurationBuilder ObterConfiguracaoDoAppSettings()
        {
            return new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile($"appsettings.json");
        }
    }
}
