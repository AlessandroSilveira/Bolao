using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Bolao.Models
{
    public class Campeonato
    {
        public ObjectId _id { get; set; }
        public string Ano { get; set; }
        public string Nome { get; set; }
        public DateTime DataCarga { get; set; }
        public List<Equipe> Equipes { get; set; } = new List<Equipe>();        
    }
}
