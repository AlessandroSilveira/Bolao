using MongoDB.Bson;
using System.Collections.Generic;

namespace Bolao.Models
{
    public class Rodada
    {
        public ObjectId _id { get; set; }
        public string Nome { get; set; }        
        public List<Jogo> Jogos { get; set; } = new List<Jogo>();
    }
}
