using Bolao.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bolao.Servicos.Interfaces
{
    public interface IMotorBuscaRodada
    {
        void GravarResultadosDaRodada();
        //void SalvarResultadosDaRodada(List<Rodada> resultados);
        PaginaRodada InstanciaPaginaRodada(ISeleniumConfiguration seleniumConfigurations);
        IConfigurationBuilder ObterConfiguracaoDoAppSettings();
    }
}
