using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Crud
{
    public partial class Form1 : Form
    {

        MySqlConnection conexao;
        MySqlCommand comando;
        MySqlDataAdapter da;
        MySqlDataReader dr;
        string strSQL;

        List<TextBox> lista_1 = new List<TextBox>();
        List<MaskedTextBox> lista_2 = new List<MaskedTextBox>();

        public Form1()
        {
            InitializeComponent();

            try
            {
                conexao = new MySqlConnection("Server=localhost;Database=cadastro;Uid=root;");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void tsbSalvar_Click(object sender, EventArgs e)
        {
            if (verifica_obrigatorio())
            {
                try
                {
                    strSQL = "insert into Funcionarios (Id, Nome, Endereco, CEP, Bairro, Cidade, UF, Telefone) values (@Id, @Nome, @Endereco, @CEP, @Bairro, @Cidade, @UF, @Telefone)";

                    comando = new MySqlCommand(strSQL, conexao);

                    comando.Parameters.Add("@Id", MySqlDbType.Int32).Value = txtId.Text;
                    comando.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = txtNome.Text;
                    comando.Parameters.Add("@Endereco", MySqlDbType.VarChar).Value = txtEndereco.Text;
                    comando.Parameters.Add("@CEP", MySqlDbType.VarChar).Value = mskCEP.Text;
                    comando.Parameters.Add("@Bairro", MySqlDbType.VarChar).Value = txtBairro.Text;
                    comando.Parameters.Add("@Cidade", MySqlDbType.VarChar).Value = txtCidade.Text;
                    comando.Parameters.Add("@UF", MySqlDbType.VarChar).Value = txtUF.Text;
                    comando.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = mskTelefone.Text;

                    conexao.Open();

                    comando.ExecuteNonQuery();
                    MessageBox.Show("Cadastro realizado com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            strSQL = "select * from funcionarios where Id=@Id";
            comando = new MySqlCommand(strSQL, conexao);
            comando.Parameters.Add("@Id", MySqlDbType.Int32).Value = tstIdBuscar.Text;
            try
            {
                if(tstIdBuscar.Text == string.Empty)
                {
                    throw new Exception("Você precisa digitar um Id!");
                }
                conexao.Open();

                dr = comando.ExecuteReader();

                if (dr.HasRows == false)
                {
                    throw new Exception("Id não cadastrado!");
                }

                while (dr.Read())
                {
                    txtId.Text = Convert.ToString(dr["Id"]);
                    txtNome.Text = Convert.ToString(dr["Nome"]);
                    txtEndereco.Text = Convert.ToString(dr["Endereco"]);
                    mskCEP.Text = Convert.ToString(dr["CEP"]);
                    txtBairro.Text = Convert.ToString(dr["Bairro"]);
                    txtCidade.Text = Convert.ToString(dr["Cidade"]);
                    txtUF.Text = Convert.ToString(dr["UF"]);
                    mskTelefone.Text = Convert.ToString(dr["Telefone"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void tsbAlterar_Click(object sender, EventArgs e)
        {
            if (verifica_obrigatorio()){
                strSQL = "update Funcionarios set Id=@Id, Nome=@Nome, Endereco=@Endereco, CEP=@CEP, Bairro=@Bairro, Cidade=@Cidade, UF=@UF, Telefone=@Telefone where Id=@IdBuscar";
                comando = new MySqlCommand(strSQL, conexao);

                comando.Parameters.Add("@IdBuscar", MySqlDbType.VarChar).Value = tstIdBuscar.Text;

                comando.Parameters.Add("@Id", MySqlDbType.Int32).Value = txtId.Text;
                comando.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = txtNome.Text;
                comando.Parameters.Add("@Endereco", MySqlDbType.VarChar).Value = txtEndereco.Text;
                comando.Parameters.Add("@CEP", MySqlDbType.VarChar).Value = mskCEP.Text;
                comando.Parameters.Add("@Bairro", MySqlDbType.VarChar).Value = txtBairro.Text;
                comando.Parameters.Add("@Cidade", MySqlDbType.VarChar).Value = txtCidade.Text;
                comando.Parameters.Add("@UF", MySqlDbType.VarChar).Value = txtUF.Text;
                comando.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = mskTelefone.Text;

                try
                {
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Cadastro atualizado com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        private void tsbExcluir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Deseja realmente excluir este funcionário?", "Cuidado", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                MessageBox.Show("Operação cancelada!");
            }
            else
            {
                strSQL = "delete from funcionarios where Id=@Id";
                comando = new MySqlCommand(strSQL, conexao);
                comando.Parameters.Add("@Id", MySqlDbType.Int32).Value = tstIdBuscar.Text;

                try
                {
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Funcionário deletado com sucesso!");

                    txtId.Text = "";
                    txtNome.Text = "";
                    txtEndereco.Text = "";
                    mskCEP.Text = "";
                    txtBairro.Text = "";
                    txtCidade.Text = "";
                    txtUF.Text = "";
                    mskTelefone.Text = "";
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }finally
                {
                    conexao.Close();
                }
            }
        }

        private void tsbCancelar_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtNome.Text = "";
            txtEndereco.Text = "";
            mskCEP.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            txtUF.Text = "";
            mskTelefone.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (verifica_obrigatorio())
            {
                MessageBox.Show("Tudo bem");
            }

            
        }

        public Boolean verifica_obrigatorio()
        {
            lista_1.Add(txtId);
            lista_1.Add(txtBairro);
            lista_1.Add(txtCidade);
            lista_1.Add(txtEndereco);
            lista_1.Add(txtId);
            lista_1.Add(txtNome);
            lista_1.Add(txtUF);

            lista_2.Add(mskCEP);
            lista_2.Add(mskTelefone);

            int qtd = 0;
            bool resposta = true;

            foreach (TextBox campo in lista_1)
            {
                if(qtd < 1)
                {
                    if (invalido(campo.Text))
                    {
                        qtd++;
                        MessageBox.Show("Campo "+ campo.Name+" precisa ser ter valor");
                        campo.Focus();
                        resposta = false;
                    }
                }
                
            }
            foreach (MaskedTextBox campo in lista_2)
            {
                if (qtd < 1)
                {
                    if (!campo.MaskCompleted)
                    {
                        qtd++;
                        MessageBox.Show("Campo " + campo.Name + " precisa ser ter valor");
                        campo.Focus();
                        resposta = false;
                    }
                }
            }

            return resposta;
        }
        public Boolean invalido(string texto)
        {
            if(string.IsNullOrEmpty(texto) || string.IsNullOrWhiteSpace(texto))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
