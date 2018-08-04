using Bolao.Models;
using Microsoft.Extensions.Configuration;

namespace Bolao.Servicos.Interfaces
{
    public interface IMotorBuscaClassificacao
    {       
        void SalvarClassificacao(Campeonato classificacao);
        PaginaClassificacao ObterPaginaClassificacao(ISeleniumConfiguration seleniumConfigurations);      
        void MotorBuscaClassificacaoOnLine();
    }
}
