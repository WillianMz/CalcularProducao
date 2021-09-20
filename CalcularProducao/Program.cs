using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;

namespace CalcularProducao
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WN Calcular produção");

            //abrir planilha
            Console.WriteLine("Lendo planilha de dados");
            var wb = new XLWorkbook(@"D:\WN\Sobras padaria.xlsx");
            var planilha = wb.Worksheet(1);
                        
            //inicia da linha 3 da planilha, pois a 1 é cabecalho e a 2 os titulos das colunas
            var startLine = 3;


            List<Produto> produtos = new();

            while (true)
            {
                var id = planilha.Cell("A" + startLine.ToString()).Value.ToString();
                var qtd = planilha.Cell("B" + startLine.ToString()).Value.ToString();
                var nome = planilha.Cell("C" + startLine.ToString()).Value.ToString();

                //quando a linha estiver em branco ou vazia finaliza o laco de repeticao
                if (string.IsNullOrEmpty(id))
                    break;

                //Console.WriteLine(td);
                decimal qtdValor = Convert.ToDecimal(qtd);
                Console.WriteLine(qtdValor);
                var x = decimal.Round(qtdValor, 3);
                Console.WriteLine("x = " + decimal.Round(x,3));
                Produto p = new(Convert.ToInt32(id), qtdValor, nome);

                var existe = produtos.Exists(x => x.Id == p.Id);

                if (existe == true)
                {
                    Produto produto = produtos.Find(delegate (Produto pd) { return pd.Id == p.Id; });
                    decimal novaQtd = CalcularValor(produto.Qtd, p.Qtd);
                    p.Qtd = novaQtd;
                    produtos.Remove(produto);
                    Console.WriteLine("Produto removido: " + produto.Nome);
                }
                else
                {
                    Console.WriteLine("Novo produto adicionado");
                }

                produtos.Add(p);
                startLine++;
            }

            wb.Dispose();
            ExibirResultado(produtos);
            
        }

        public static decimal CalcularValor(decimal valor1, decimal valor2)
        {
            decimal soma = valor1 + valor2;
            return soma;
        }

        public static void ExibirResultado(List<Produto> produtos)
        {
            foreach (var p in produtos)
            {
                Console.WriteLine("-> " + p.Qtd + " Kg - " + p.Nome);
            }
        }

        
    }
}
