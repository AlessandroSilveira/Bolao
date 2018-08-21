using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Bolao.Models;
using Bolao.Servicos.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Bolao.Servicos {
    public class PaginaRodada : IPaginaRodada {
        private readonly ISeleniumConfiguration _seleniumConfiguration;

        private IWebDriver _driver;

        public PaginaRodada (ISeleniumConfiguration seleniumConfiguration) {
            _seleniumConfiguration = seleniumConfiguration;
            FirefoxOptions options = new FirefoxOptions ();
            options.AddArgument ("--headless");
            var timespan = TimeSpan.FromMinutes (3);
            _driver = new FirefoxDriver (_seleniumConfiguration.CaminhoDriverFirefox, options, timespan);
        }

        public void CarregarPagina () {

        }
        private string ObterUrlRodadaDaVez (string numeroRodada) {
            var ulr = _seleniumConfiguration.UrlPaginaRodadaBrasileirao.Replace ("%s", numeroRodada);
            return ulr;
        }
        public List<Rodada> ObterResultadosRodada () {
            var rodadas = new List<Rodada> ();
            for (int a = 1; a <= 38; a++) {
                _driver.Manage ().Timeouts ().PageLoad = TimeSpan.FromSeconds (_seleniumConfiguration.Timeout);
                _driver.Navigate ().GoToUrl (ObterUrlRodadaDaVez (a.ToString ()));

                var tableRodada = _driver.FindElements (By.ClassName ("lista-de-jogos-item"));

                var rodada = new Rodada ();
                rodada.Nome = "Rodada " + a.ToString ();
                var jogos = new List<Jogo> ();
                foreach (var itens in tableRodada) {

                    var DataLocalHoraJogo = itens.FindElement (By.ClassName ("placar-jogo-informacoes")).Text.Split (" ");

                   
                    if(DataLocalHoraJogo.Length < 4){
                        List<string> listDataLocalHoraJogo = DataLocalHoraJogo.Cast<string>().ToList();

                        for (int i = DataLocalHoraJogo.Length; i <= 4; i++)
                        {
                           listDataLocalHoraJogo.Add("");
                        }

                        DataLocalHoraJogo = listDataLocalHoraJogo.ToArray();
                    }

                    var jogo = new Jogo {
                        EscudoTimeMandante = itens.FindElement (By.ClassName ("placar-jogo-equipes")).FindElement (By.ClassName ("placar-jogo-equipes-mandante")).FindElement (By.ClassName ("placar-jogo-equipes-escudo-mandante")).GetAttribute ("src"),
                        EscudoTimeVisitante = itens.FindElement (By.ClassName ("placar-jogo-equipes")).FindElement (By.ClassName ("placar-jogo-equipes-visitante")).FindElement (By.ClassName ("placar-jogo-equipes-escudo-visitante")).GetAttribute ("src"),
                        DataJogo = DataLocalHoraJogo[1] == null? "": DataLocalHoraJogo[1],
                        HoraJogo = DataLocalHoraJogo[3] == null? "": DataLocalHoraJogo[3],
                        TimeMandante = itens.FindElement (By.ClassName ("placar-jogo-equipes")).FindElement (By.ClassName ("placar-jogo-equipes-mandante")).FindElement (By.ClassName ("placar-jogo-equipes-nome")).GetAttribute ("innerHTML"),
                        PlacarTimeMandante = itens.FindElement (By.ClassName ("placar-jogo-equipes")).FindElement (By.ClassName ("placar-jogo-equipes-placar")).FindElement (By.ClassName ("placar-jogo-equipes-placar-mandante")).GetAttribute ("innerHTML"),
                        TimeVisitante = itens.FindElement (By.ClassName ("placar-jogo-equipes")).FindElement (By.ClassName ("placar-jogo-equipes-visitante")).FindElement (By.ClassName ("placar-jogo-equipes-nome")).GetAttribute ("innerHTML"),
                        PlacarTimeVisitante = itens.FindElement (By.ClassName ("placar-jogo-equipes")).FindElement (By.ClassName ("placar-jogo-equipes-placar")).FindElement (By.ClassName ("placar-jogo-equipes-placar-visitante")).GetAttribute ("innerHTML"),
                        LocalJogo = DataLocalHoraJogo[2] == null? "": DataLocalHoraJogo[2],
                    };

                    if (string.IsNullOrEmpty (jogo.PlacarTimeMandante))
                        jogo.PlacarTimeMandante = "0";

                    if (string.IsNullOrEmpty (jogo.PlacarTimeVisitante))
                        jogo.PlacarTimeVisitante = "0";

                    jogos.Add (jogo);
                }

                rodada.Jogos = jogos;
                rodadas.Add (rodada);
            }

            return rodadas;
        }

        public void Fechar () {
            _driver.Quit ();
            _driver = null;
        }
    }
}