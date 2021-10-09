using ClosedXML.Excel;
using Figgle;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CalcularProducao
{
    class Program
    {
        public static string _urlPlanilha;
        public static string _urlArquivoTXT;
        
        static void Main(string[] args)
        {   
            Console.WriteLine(" 2021 - Willian Software");
            Console.WriteLine(" Calculo de Producao");
            Console.WriteLine(" Versão 1.0");
            Console.WriteLine(" Codigo fonte: https://github.com/WillianMz/CalcularProducao");
            Console.WriteLine("");
            Animation();
            Console.WriteLine("ATENÇÃO: Programa em fase de desenvolvimento!");
            Console.WriteLine("");
            //CreateFileJsonConfig();
            ReadFileJsonConfig();
            Console.WriteLine("");

            try
            {
                var arquivoXLS = _urlPlanilha;
                //abrir planilha
                Console.WriteLine("Lendo planilha de dados ............");
                Console.WriteLine("");
                var workBook = new XLWorkbook(@arquivoXLS);
                var planilha = workBook.Worksheet(1);
                //inicia da linha 3 da planilha, pois a 1 é cabecalho e a 2 os titulos das colunas
                var startLine = 4;
                List<Produto> produtos = new();

                while (true)
                {
                    var id = planilha.Cell("A" + startLine.ToString()).Value.ToString();
                    var qtd = planilha.Cell("B" + startLine.ToString()).Value.ToString();
                    var nome = planilha.Cell("C" + startLine.ToString()).Value.ToString();

                    //quando a linha estiver em branco ou vazia finaliza o laco de repeticao
                    if (string.IsNullOrEmpty(id))
                        break;
                    
                    var qtdValor = Convert.ToDecimal(qtd);
                    decimal xy = decimal.Round(qtdValor, 3);
                    Produto p = new(Convert.ToInt32(id), xy, nome);
                    var existe = produtos.Exists(x => x.Id == p.Id);

                    if (existe == true)
                    {
                        Produto produto = produtos.Find(delegate (Produto pd) { return pd.Id == p.Id; });
                        decimal novaQtd = 0;
                        novaQtd = produto.Qtd + p.Qtd;
                        p.Qtd = novaQtd;
                        produtos.Remove(produto);                        
                    }

                    produtos.Add(p);
                    startLine++;
                }

                workBook.Dispose();          
                CreateFileTXT(produtos);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erro na leitura da planilha de dados: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("");
                Console.WriteLine("Pressione qualquer tecla para sair da aplicação");
                Console.ReadKey();
            }
        }

        public static void CreateFileTXT(List<Produto> produtos)
        {
            StreamWriter sw = new(_urlArquivoTXT);
            sw.WriteLine($"Arquivo gerado em { DateTime.Now }");

            try
            {                
                sw.WriteLine("Quantidade a serem produzidas\n");
                sw.WriteLine("---------------- START ----------------\n");

                foreach (var p in produtos)
                {                    
                    sw.WriteLine($"{p.Id} .......... {p.Qtd} Kg/un .......... {p.Nome}");
                    Console.WriteLine($"{p.Id} - {p.Qtd} Kg/UN -- {p.Nome}");
                }

                sw.WriteLine("\n");
                sw.WriteLine("---------------- END ----------------");
                sw.WriteLine($"CALCPROD 1.0");
                sw.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erro na criação do arquivo com os calculos de produção: { ex.Message }");
            }
            finally
            {
                sw.Dispose();
            }
        }

        public static void Animation()
        {
            Console.WriteLine(FiggleFonts.Ivrit.Render("Willian 2021"));
        }

        public static void ReadFileJsonConfig()
        {                    
            try
            {
                //pega o caminho do executavel
                string path = AppDomain.CurrentDomain.BaseDirectory.ToString();
                //abre o arquivo e le o json
                using StreamReader file = File.OpenText(@$"{path}\config.txt");
                JsonSerializer jsonSerializer = new JsonSerializer();
                //deserealiza o json para o objeto de configuracao
                Configuration configuration = (Configuration)jsonSerializer.Deserialize(file, typeof(Configuration));
                //atribui as variaveis os valores carregados do arquivo json                
                _urlArquivoTXT = configuration.CaminhoArquivoTXT;
                _urlPlanilha = configuration.CaminhoPlanilha;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erro na leitura do arquivo Json: {ex.Message}");
                CreateFileJsonConfig();
            }
        }

        public static void CreateFileJsonConfig()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory.ToString();
                Configuration configuration = new Configuration(@"D:\WN\Sobras padaria.xlsx", @"C:\Users\Willian\Desktop\Calculo de Sobras.txt");
                JsonSerializer serializer = new();
                using StreamWriter sw = new StreamWriter(@$"{path}\config.txt");
                using JsonWriter writer = new JsonTextWriter(sw);
                serializer.Serialize(writer, configuration);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na criação do arquivo Json: {ex.Message}");
            }

        }
    }
}
