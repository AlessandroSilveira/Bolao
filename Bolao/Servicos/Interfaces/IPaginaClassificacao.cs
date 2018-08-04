using Bolao.Models;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Bolao.Servicos.Interfaces
{
    public interface IPaginaClassificacao
    {
        void CarregarPagina();
        Campeonato ObterClassificacao();
        Equipe PreencheDadosTimes(ReadOnlyCollection<IWebElement> times, int i, ReadOnlyCollection<IWebElement> estatisticasEquipe, ReadOnlyCollection<IWebElement> estatisticaTime);
        void BuscarEstatisticaTimes(ReadOnlyCollection<IWebElement> times, int i, out ReadOnlyCollection<IWebElement> estatisticasEquipe, out ReadOnlyCollection<IWebElement> estatisticaTime);
        void Fechar();
    }
}
