namespace CalcularProducao
{
    public class Configuration
    {
        public Configuration(string caminhoPlanilha, string caminhoArquivoTXT)
        {
            CaminhoPlanilha = caminhoPlanilha;
            CaminhoArquivoTXT = caminhoArquivoTXT;
        }

        public string CaminhoPlanilha { get; set; }
        public string CaminhoArquivoTXT { get; set; }
    }
}
