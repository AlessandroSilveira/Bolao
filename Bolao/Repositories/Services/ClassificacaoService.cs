using Bolao.Data;
using Bolao.Interfaces;
using Bolao.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Bolao.Repositories.Services
{
    public class ClassificacaoService : IClassificacaoService
    {
        private readonly MongoContext _context = null;
        public ClassificacaoService()
        {
            _context = new MongoContext();
        }

        public IEnumerable<Campeonato> GetAll()
        {

            return  _context.Classificacao.Find(x => true).ToList();
        }
    }
}
