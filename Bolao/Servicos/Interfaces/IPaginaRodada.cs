using Bolao.Models;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Bolao.Servicos.Interfaces
{
    public interface IPaginaRodada
    {
        void CarregarPagina();
        List<Rodada> ObterResultadosRodada();
        void Fechar();
    }
}
