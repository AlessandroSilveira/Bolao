using Bolao.Models;
using Bolao.Servicos.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Bolao.Servicos
{
    public class PaginaRodada : IPaginaRodada
    {
        private readonly ISeleniumConfiguration _seleniumConfiguration;

        private IWebDriver _driver;

        public PaginaRodada(ISeleniumConfiguration seleniumConfiguration)
        {
            _seleniumConfiguration = seleniumConfiguration;
            FirefoxOptions options = new FirefoxOptions();
            options.AddArgument("--headless");
             var timespan = TimeSpan.FromMinutes(3);
            _driver = new FirefoxDriver(_seleniumConfiguration.CaminhoDriverFirefox, options,timespan);
        }

        public void CarregarPagina()
        {
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(_seleniumConfiguration.Timeout);
            _driver.Navigate().GoToUrl(_seleniumConfiguration.UrlPaginaRodadaBrasileirao);
        }

        public List<Rodada> ObterResultadosRodada()
        {
            var tableRodada = _driver.FindElement(By.ClassName("soccer"));

            var dadosRodada = tableRodada.FindElements(By.TagName("tbody"));

            var totalRodadas = tableRodada.FindElement(By.TagName("tbody")).FindElements(By.ClassName("event_round")).Count();
            
            var rodadas = new List<Rodada>();
            var jogos = new List<Jogo>();
            var numeroJogosPorRodada = 10;
            int i = 0;
            int linhasPorRodada = 11;
            int j=1;

            foreach(var itens in dadosRodada)
            {
                 var rodada = new Rodada();

                var linhastituloRodada = _driver.FindElement(By.ClassName("soccer")).FindElement(By.TagName("tbody")).FindElements(By.ClassName("event_round"));

                var linhas = _driver.FindElement(By.ClassName("soccer")).FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));

                rodada.Nome = linhastituloRodada[i].FindElement(By.TagName("td")).GetAttribute("innerHTML");  

                var placar = linhas[j].FindElement(By.ClassName("cell_sa")).GetAttribute("innerHTML").ToString().Trim().Split(':');


                var jogo = new Jogo
                {
                    DataJogo = linhas[j].FindElement(By.ClassName("cell_ad")).GetAttribute("innerHTML"),             
                    TimeMandante = linhas[j].FindElement(By.ClassName("cell_ab")).FindElement(By.ClassName("padr")).GetAttribute("innerHTML"),
                    PlacarTimeMandante = placar[0].Trim(),
                    TimeVisitante = linhas[j].FindElement(By.ClassName("cell_ac")).FindElement(By.ClassName("padl")).GetAttribute("innerHTML"),
                    PlacarTimeVisitante = placar[1].Trim()
                };
                jogos.Add(jogo);
                rodada.Jogos = jogos;
                rodadas.Add(rodada);
                i ++;
                j++;

                if(j ==linhasPorRodada){
                    break;
                }


            }
           
            return rodadas;
        }
     
        public void Fechar()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}
