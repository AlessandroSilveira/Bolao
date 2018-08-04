namespace Bolao.Models
{
    public class Equipe
    {
        public int Posicao { get; set; }
        public string Nome { get; set; }
        public int Pontos { get; set; }
        public int Jogos { get; set; }
        public int Vitorias { get; set; }
        public int Empates { get; set; }
        public int Derrotas { get; set; }      
        public int GolsPro { get; set; }
        public int GolsContra { get; set; }
        public int  SaldoDeGols { get; set; }
    }
}
