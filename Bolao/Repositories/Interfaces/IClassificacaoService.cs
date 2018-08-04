using Bolao.Models;
using System.Collections.Generic;

namespace Bolao.Interfaces
{
    public interface IClassificacaoService
    {
        IEnumerable<Campeonato> GetAll();       
    }
}
