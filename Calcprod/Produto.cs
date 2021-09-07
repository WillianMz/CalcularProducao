using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calcprod
{
    public class Produto
    {
        public Produto(int id, double qtd, string nome)
        {
            Id = id;
            Qtd = qtd;
            Nome = nome;
        }

        public int Id { get; set; }
        public double Qtd { get; set; }
        public string Nome { get; set; }
    }
}
