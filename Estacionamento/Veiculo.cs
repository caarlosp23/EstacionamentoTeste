using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento
{
    class Veiculo
    {

        Conexao conexao = new Conexao();
        SqlCommand cmd = new SqlCommand();
        public string mensagem = "";

        private string carro;
        private string diadaSemana;
        private int diadoMes;
        private int horaEstacionado;
        private string ladoEstacionado;
        private string situacao;
        private DateTime dataHora = DateTime.Now;

        public string Carro { get => carro; set => carro = value; }
        public string DiadaSemana { get => diadaSemana; set => diadaSemana = value; }
        public int DiadoMes { get => diadoMes; set => diadoMes = value; }
        public int HoraEstacionado { get => horaEstacionado; set => horaEstacionado = value; }
        public string LadoEstacionado { get => ladoEstacionado; set => ladoEstacionado = value; }
        public string Situacao { get => situacao; set => situacao = value; }
        public DateTime DataHora { get => dataHora; set => dataHora = value; }

        public Veiculo()
        {
        }

        public void gravarVeiculo()
        {
            //comando sql - insert, update, delete -- sql command

            cmd.CommandText = "insert into Estacionamento(Veiculo, DiaDaSemana,DiaDoMes,HoraEstacionado,LadoEstacionado,Situacao,DataHoraCadastro)" +
             "values (@Veiculo, @DiaDaSemana,@DiaDoMes,@HoraEstacionado,@LadoEstacionado,@Situacao,@DataHoraCadastro)";
            //parametros
            cmd.Parameters.AddWithValue("@Veiculo", carro);
            validarDiaSemana();
            cmd.Parameters.AddWithValue("@DiaDaSemana", diadaSemana);
            cmd.Parameters.AddWithValue("@DiaDoMes", diadoMes);
            cmd.Parameters.AddWithValue("@HoraEstacionado", horaEstacionado);
            cmd.Parameters.AddWithValue("@LadoEstacionado", ladoEstacionado.ToUpper());
            cmd.Parameters.AddWithValue("@Situacao", situacao);
            cmd.Parameters.AddWithValue("@DataHoraCadastro", DataHora);

            //conectar com o bd -- conexao 
            try
            {
                //conectar com o banco - conexao
                cmd.Connection = conexao.conectar();
                //executar comando
                cmd.ExecuteNonQuery();
                //desconectar
                conexao.desconectar();
                //mostrar msg de erro ou sucesso
                this.mensagem = "Dados salvos com sucesso!!";

            }
            catch (SqlException e)
            {
                this.mensagem = "Erro na conexão no banco de dados!";
            }
        }

        public bool validaCampos()
            {
            if (carro == "")
            {
                mensagem = "Veiculo obrigatório!";
                return false;

            } else if (int.Parse(diadaSemana) <= 0 || int.Parse(diadaSemana) >= 8)
            {

                mensagem = "Digite um dia da semana válido!\n\n1- Domingo\n2- Segunda" +
                    "\n3- Terça\n4- Quarta\n5- Quinta\n6- Sexta\n7- Sábado";
                return false;
            }
            else if (diadoMes < 1 || diadoMes > 31)
            {
                mensagem = "Digite um dia do mes válido!";
                return false;
            }
            else if (horaEstacionado < 0 || horaEstacionado > 23)
            {
                mensagem = "Digite uma hora válida!";
                return false;

            } else if (ladoEstacionado != "P" && ladoEstacionado != "I" && ladoEstacionado != "i" && ladoEstacionado != "p")
            {
                mensagem = "Digite par ou impar!\nP - PAR\nI - IMPAR";
                return false;
            } else
            {
                mensagem = "Cadastro efetuado com sucesso!!";
                return true;
            }

        }

        public void regularIrregular()
        {
            //2 a 6 das 7 as 20h

            //proibido estacionar lado par dia par
            if (ladoEstacionado.ToLower() == "p" && diadoMes % 2 == 0)
            {
                Situacao = "Irregular";
            }
            else if (ladoEstacionado.ToLower() == "i" && diadoMes % 2 != 0)
            {
                Situacao = "Irregular";
            }
            else if (int.Parse(diadaSemana) >= 2 || int.Parse(diadaSemana) <= 6)//dias proibidos!!
            {
                if (horaEstacionado >= 7 && horaEstacionado <= 20)//horario proibido
                {
                    Situacao = "Irregular";
                } else
                {
                    situacao = "Regular";
                }
            }


        }
   
        public void validarDiaSemana()
        {
            switch (diadaSemana)
            {
                case "1": diadaSemana = "Domingo";
                    break;
                case "2":
                    diadaSemana = "Segunda";
                    break;
                case "3":
                    diadaSemana = "Terça";
                    break;
                case "4":
                    diadaSemana = "Quarta";
                    break;
                case "5":
                    diadaSemana = "Quinta";
                    break;
                case "6":
                    diadaSemana = "Sexta";
                    break;
                case "7":
                    diadaSemana = "Sábado";
                    break;
            }
        }

        public bool carroEstacionado()
        {

            string stringSql = String.Concat("SELECT COUNT(1) FROM Estacionamento WHERE Veiculo = '", Carro, "' and HoraEstacionado = ", HoraEstacionado);

            using (SqlConnection connection = new SqlConnection("Data Source=localhost; Initial Catalog=BancoDesafio; User ID=usuario; password=senha;language=Portuguese"))
            {
                SqlCommand command = new SqlCommand(stringSql, connection);

                connection.Open();

                var result = command.ExecuteScalar();

                connection.Close();


                // Call Read before accessing data.
                if (result != null)
                {
                    return (int)result > 0;
                } 
                else 
                { 
                return false;
                }
            }
        }
            
    }
}