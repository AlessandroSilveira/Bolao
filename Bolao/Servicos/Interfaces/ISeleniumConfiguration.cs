namespace Bolao.Servicos.Interfaces
{
    public interface ISeleniumConfiguration
    {
        string CaminhoDriverFirefox { get; set; }
        string UrlPaginaClassificacaoBrasileirao { get; set; }
        string UrlPaginaRodadaBrasileirao { get; set; }
        int Timeout { get; set; }
        void SetarVariavelDeAmbiente();
    }
}
