using System;
using System.Collections.Generic;
using Bolao.Models;
using MongoDB.Driver;

namespace Bolao.Data {
    public class MongoContext {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static bool IsSSL { get; set; }
        private IMongoDatabase _database { get; }

        public MongoContext () {
            try {
                if (IsSSL) {
                    MongoClientSettings.FromUrl (new MongoUrl ("mongodb://localhost:27017")).SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                var mongoClient = new MongoClient (MongoClientSettings.FromUrl (new MongoUrl ("mongodb://localhost:27017")));
                _database = mongoClient.GetDatabase ("Brasileirao");
            } catch (Exception ex) {
                throw new Exception ("Não foi possível se conectar com o servidor.", ex);
            }
        }

        public IMongoCollection<Campeonato> Classificacao {
            get {
                return _database.GetCollection<Campeonato> ("Campeonato");
            }
        }

        public IMongoCollection<Rodada> ObterRodada {
            get {
                return _database.GetCollection<Rodada> ("Rodada");
            }
        }

        internal void IncluirResultados (List<Rodada> resultados) {

            _database.DropCollection ("Rodadas");

            var RodadaBrasileirao =
                _database.GetCollection<Rodada> ("Rodadas");
            RodadaBrasileirao.InsertMany (resultados);
        }

        public void Incluir (Campeonato temporada) {
            _database.DropCollection (temporada.Nome);
            var classificacaoBrasileirao =
                _database.GetCollection<Campeonato> (temporada.Nome);
            classificacaoBrasileirao.InsertOne (temporada);
        }
    }
}