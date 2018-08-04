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
    public class PaginaClassificacao : IPaginaClassificacao
    {
        private readonly ISeleniumConfiguration _seleniumConfiguration;

        private IWebDriver _driver;

        public PaginaClassificacao(ISeleniumConfiguration seleniumConfiguration)
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
            _driver.Navigate().GoToUrl(_seleniumConfiguration.UrlPaginaClassificacaoBrasileirao);
        }

        public Campeonato ObterClassificacao()
        {
            var times = _driver.FindElement(By.ClassName("tabela-times")).FindElements(By.TagName("tbody"));
            var listaEquipes = new List<Equipe>();

            for (int i = 0; i < times.Count; i++)
            {
                ReadOnlyCollection<IWebElement> estatisticasEquipe, estatisticaTime;

                BuscarEstatisticaTimes(times, i, out estatisticasEquipe, out estatisticaTime);

                Equipe equipe = PreencheDadosTimes(times, i, estatisticasEquipe, estatisticaTime);

                listaEquipes.Add(equipe);
            }

            var campeonato = new Campeonato
            {
                Ano = DateTime.Now.Year.ToString(),
                Nome = "BRASILEIRÃO SÉRIE A",
                DataCarga = DateTime.Now,
                Equipes = listaEquipes
            };

            return campeonato;
        }

        public  Equipe PreencheDadosTimes(ReadOnlyCollection<IWebElement> times, int i, ReadOnlyCollection<IWebElement> estatisticasEquipe, ReadOnlyCollection<IWebElement> estatisticaTime)
        {
            List<IWebElement> listaestatisticasEquipe = new List<IWebElement>(estatisticaTime);
            var d = listaestatisticasEquipe.Skip((i)).Take(1);
            ReadOnlyCollection<IWebElement> myReadOnlyCollection = d.ToList().AsReadOnly();

            var equipe = new Equipe
            {
                Posicao = Convert.ToInt32(estatisticasEquipe[0].FindElement(By.ClassName("tabela-times-posicao")).GetAttribute("innerHTML")),
                Nome = times[i].FindElement(By.ClassName("tabela-times-time-nome")).GetAttribute("innerHTML"),
                Pontos = Convert.ToInt32(myReadOnlyCollection[0].FindElement(By.ClassName("tabela-pontos-ponto")).GetAttribute("innerHTML")),
                Jogos = Convert.ToInt32(myReadOnlyCollection[0].FindElement(By.XPath("td[2]")).GetAttribute("innerHTML").ToString()),
                Vitorias = Convert.ToInt32(myReadOnlyCollection[0].FindElement(By.XPath("td[3]")).GetAttribute("innerHTML").ToString()),
                Empates = Convert.ToInt32(myReadOnlyCollection[0].FindElement(By.XPath("td[4]")).GetAttribute("innerHTML").ToString()),
                Derrotas = Convert.ToInt32(myReadOnlyCollection[0].FindElement(By.XPath("td[5]")).GetAttribute("innerHTML").ToString()),
                GolsPro = Convert.ToInt32(myReadOnlyCollection[0].FindElement(By.XPath("td[6]")).GetAttribute("innerHTML").ToString()),
                GolsContra = Convert.ToInt32(myReadOnlyCollection[0].FindElement(By.XPath("td[7]")).GetAttribute("innerHTML").ToString()),
                SaldoDeGols = Convert.ToInt32(myReadOnlyCollection[0].FindElement(By.XPath("td[8]")).GetAttribute("innerHTML").ToString())
            };
            return equipe;
        }

        public void BuscarEstatisticaTimes(ReadOnlyCollection<IWebElement> times, int i, out ReadOnlyCollection<IWebElement> estatisticasEquipe, out ReadOnlyCollection<IWebElement> estatisticaTime)
        {
            estatisticasEquipe = times[i].FindElements(By.ClassName("tabela-body-linha"));
            estatisticaTime = _driver.FindElement(By.ClassName("tabela-pontos")).FindElements(By.ClassName("tabela-body-linha"));
        }

        public void Fechar()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}
