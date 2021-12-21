using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Estacionamento
{
    public partial class Estacionamento : Form
    {
        public Estacionamento()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

            Veiculo x = new Veiculo();
            if (txtDiaMes.Text == "" || txtVeiculo.Text == "" || txtDiaSemana.Text == "" || txtHoraEstacio+"" == "" || txtParImpar.Text == "")
            {
                MessageBox.Show("Preencha todos os campos!!");
            }
            else
            {
            
            x.Carro = txtVeiculo.Text;
            x.DiadaSemana = txtDiaSemana.Text;
            x.DiadoMes = int.Parse(txtDiaMes.Text);
            x.HoraEstacionado = int.Parse(txtHoraEstacio.Text);
            x.LadoEstacionado = txtParImpar.Text;
            x.DataHora = DateTime.Now;

                if (x.carroEstacionado() == true)
                {
                    MessageBox.Show("Veiculo com mesmo nome e horario já cadastrado!!");
                    btnListar.PerformClick();

                } else
                {

                if (x.validaCampos() == true)
                {
                 
                 x.regularIrregular();
                 x.gravarVeiculo();
                 txtVeiculo.Text = "";
                 txtDiaSemana.Text="";
                 txtDiaMes.Text="";
                 txtHoraEstacio.Text="";
                 txtParImpar.Text="";
             
                 btnListar.PerformClick();
                } 
                 MessageBox.Show(x.mensagem); 
                }

               
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            Conexao bd = new Conexao();

            string sql = "select ID,Veiculo as 'Veiculo', diadasemana as 'Dia da Semana', diadomes as 'Dia do Mês', horaestacionado as 'Hora Estacionado', ladoestacionado as 'Lado Estacionado', Situacao as 'Situacao', datahoracadastro as 'Data/Hora (cadastro)' from Estacionamento";

            DataTable dt = new DataTable();

            dt = bd.executarConsultaGenerica(sql);

            dataGridView1.DataSource = dt;
        }

        private void txtVeiculo_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtDiaSemana_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void txtDiaMes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void txtHoraEstacio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
    }
}
