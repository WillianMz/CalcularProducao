using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calcprod
{
    public partial class FormCalcular : Form
    {
        List<Produto> produtos = new();

        public FormCalcular()
        {
            InitializeComponent();
        }

        public void Adicionar(int id, string nome, double valor)
        {
            Produto produto = new(id, valor, nome);
            produtos.Add(produto);
            atualizarGrid(produtos);
        }

        private void atualizarGrid(List<Produto> ps)
        {
            dgvProdutos.Rows.Clear();
            foreach (Produto p in ps)
            {
                dgvProdutos.Rows.Add(p.Id, p.Nome, p.Qtd);
            }            
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            Adicionar(Convert.ToInt32(txtCodProduto.Text), "Teste", Convert.ToDouble(txtQtd.Text));
        }
    }
}
