using Bolao.Servicos.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace Bolao.Servicos
{
    public class SeleniumConfigurations : ISeleniumConfiguration
    {
        public string CaminhoDriverFirefox { get; set; }
        public string UrlPaginaClassificacaoBrasileirao { get; set; }

        public string UrlPaginaRodadaBrasileirao {get;set;}
        public int Timeout { get; set; }     

        public void SetarVariavelDeAmbiente()
        {
            Environment.SetEnvironmentVariable("webdriver.gecko.driver", 
                "/home/alessandro/Documentos/Bolao/Bolao");
        }
       
    }
}
