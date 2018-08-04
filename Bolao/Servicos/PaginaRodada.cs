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

            var dadosRodada = tableRodada.FindElements(By.TagName("tr"));
            
            var rodadas = new List<Rodada>();
            var jogos = new List<Jogo>();

            foreach(var itens in dadosRodada)
            {
                var rodada = new Rodada()
                {
                    Nome = itens.FindElement(By.ClassName("event_round")).GetAttribute("innerHTML")
                };

                var placar = itens.FindElement(By.ClassName("score")).ToString().Split(':');

                var jogo = new Jogo
                {
                    DataJogo = itens.FindElement(By.ClassName("stage-finished")).FindElement(By.ClassName("time")).GetAttribute("innerHTML"),             
                    TimeMandante = itens.FindElement(By.ClassName("team-home")).FindElement(By.ClassName("padr")).GetAttribute("innerHTML"),
                    PlacarTimeMandante = placar[0],
                    TimeVisitante = itens.FindElement(By.ClassName("team-away")).FindElement(By.ClassName("padl")).GetAttribute("innerHTML"),
                    PlacarTimeVisitante = placar[1]
                };
                jogos.Add(jogo);
                rodada.Jogos = jogos;
                rodadas.Add(rodada);
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
