using System;
using System.Collections.Generic;
using System.IO;
using Bolao.Data;
using Bolao.Models;
using Bolao.Servicos.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Bolao.Servicos
{
    public class MotorBuscaRodada : IMotorBuscaRodada
    {
        private readonly ISeleniumConfiguration _seleniumConfiguration;
        public MotorBuscaRodada(ISeleniumConfiguration seleniumConfiguration)
        {
            _seleniumConfiguration = seleniumConfiguration;
        }

        public void GravarResultadosDaRodada()
        {
            _seleniumConfiguration.SetarVariavelDeAmbiente();

            IConfigurationBuilder builder = ObterConfiguracaoDoAppSettings();

            var configuration = builder.Build();

            new ConfigureFromConfigurationOptions<ISeleniumConfiguration>(
                configuration.GetSection("SeleniumConfigurations"))
                    .Configure(_seleniumConfiguration);


            PaginaRodada paginaRodada = InstanciaPaginaRodada(_seleniumConfiguration);

            paginaRodada.CarregarPagina();

            var resultados = paginaRodada.ObterResultadosRodada();

            paginaRodada.Fechar();

            SalvarResultadosDaRodada(resultados);
        }

        private void SalvarResultadosDaRodada(List<Rodada> resultados)
        {
           new MongoContext().IncluirResultados(resultados);
        }

        public PaginaRodada InstanciaPaginaRodada(ISeleniumConfiguration seleniumConfigurations)
        {
            return new PaginaRodada(seleniumConfigurations);
        }

        public IConfigurationBuilder ObterConfiguracaoDoAppSettings()
        {
            return new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile($"appsettings.json");
        }

       
    }
}
