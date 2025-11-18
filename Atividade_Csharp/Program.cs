using MySql.Data.MySqlClient;
using Mysqlx.Session;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Atividade_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu inicio = new Menu(); //cria o objeto inicio (menu)
            Pessoa[] array = new Pessoa[15]; //cria o objeto array e o limita a 15 pacientes
            MySqlConnection conexao; //cria a variável conexão
            const int limite = 15; //define o limite de pacientes por vez à 15
            int contador;
            bool continuar = true;

            while (continuar) //enquanto a opção continuar for verdadeira:
            {

                conexao = new MySqlConnection("server=localhost;uid=root;pwd=;database=hospital; Port=3306"); //estabelece a conexão com o servidor local, acessa o banco de dados com o usuario e senha na porta de acesso

                try
                {
                    conexao.Open(); //abre a conexão
                }
                catch (MySqlException erro)
                {
                    Console.WriteLine("Erro ao conectar ao banco de dados. \n" + erro.Message); //caso haja erro, faz a devolutiva
                }

                string bd = "SELECT contador FROM paciente ORDER BY contador DESC LIMIT 1"; //faz o select de paciente e os ordena
                MySqlCommand cmdBd = new MySqlCommand(bd, conexao); //acesso ao banco
                object resultado = cmdBd.ExecuteScalar(); //executa a linha de select e retorna 
                contador = Convert.ToInt32(resultado);



                switch (inicio.Interface())
                {
                    case "1":
                        string res = "S";

                        while (res == "S" && contador < limite)
                        {
                            Pessoa paciente = new Pessoa(); //novo paciente
                            paciente.Cadastrar(); //realiza o cadastro

                            array[contador] = paciente; //adiciona a fila

                            contador++; //aumenta em 1 a quantidade na fila

                            Console.Clear(); //limpa o console
                            string sqlInsert = "INSERT INTO paciente (Nome, Cpf, Idade, Endereco, Telefone, Email, Contador) VALUES (@nome, @cpf, @idade, @endereco, @telefone, @email, @contador)"; //string de inserção de valores no banco
                            MySqlCommand cmdCa = new MySqlCommand(sqlInsert, conexao); //comando de inserção
                            cmdCa.Parameters.AddWithValue("@nome", paciente.Nome); //adiciona o parametro e o seu valor à coleção de dados
                            cmdCa.Parameters.AddWithValue("@cpf", paciente.Cpf);
                            cmdCa.Parameters.AddWithValue("@idade", paciente.Idade);
                            cmdCa.Parameters.AddWithValue("@endereco", paciente.Endereco);
                            cmdCa.Parameters.AddWithValue("@telefone", paciente.Telefone);
                            cmdCa.Parameters.AddWithValue("@email", paciente.Email);
                            cmdCa.Parameters.AddWithValue("@contador", contador);

                            int linhas = cmdCa.ExecuteNonQuery();
                            Console.WriteLine("Paciente Cadastrado!! \n\nPressione ENTER para continuar...");
                            Console.ReadLine();
                            Console.Clear();


                            if (contador == limite)
                            {
                                Console.Clear();
                                Console.WriteLine("Paciente Cadastrado!! \n\nLimite de Pacientes Excedido\n\n");
                                Console.WriteLine("Pressione ENTER para voltar ao Menu...");
                                Console.ReadLine();
                                Console.Clear();

                            }
                            else
                            {
                                Console.WriteLine("Deseja Cadastrar Outro Paciente? (S/N)");
                                res = Console.ReadLine();
                                res = res.ToUpper();
                                Console.Clear();
                                Console.WriteLine("Pressione ENTER para continuar...");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            if (res != "S" && res != "N")
                            {
                                Console.Clear();
                                Console.WriteLine("Opção inválida. Voltando ao Menu.");
                                res = "N";
                                Console.Clear();
                            }
                        }

                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine($"PACIENTES NA FILA ({contador + "/" + limite}):");
                        if (contador == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Não existem pacientes Cadastrados!\n\nPressione Enter para voltar ao Menu...");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            string sqlSelect = "SELECT Nome FROM paciente ORDER BY Idade DESC";
                            MySqlCommand cmdLI = new MySqlCommand(sqlSelect, conexao);
                            MySqlDataReader reader = cmdLI.ExecuteReader();

                            while (reader.Read())
                            {
                                string nome = reader.GetString("Nome");
                                Console.WriteLine($"Nome: {nome}");
                            }

                            Console.WriteLine("\n\n\nPressione Enter para voltar ao Menu...");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        break;

                    case "3":
                        Console.Clear();
                        if (contador == 0)
                        {
                            Console.WriteLine("A fila de atendimento está vazia!\n\n\nPressione ENTER para retornar ao Menu.");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {

                            string sqlSelect = "SELECT Id, Nome, Idade FROM paciente ORDER BY Idade DESC";
                            MySqlCommand cmdAt = new MySqlCommand(sqlSelect, conexao);
                            MySqlDataReader reader = cmdAt.ExecuteReader();

                            while (reader.Read())
                            {
                                string nome = reader.GetString("Nome");
                                int idade = reader.GetInt32("Idade");
                                int id = reader.GetInt32("Id");
                                Console.WriteLine($"O Paciente {nome}, finalizou o atendimento!!\n\n");
                            }
                            reader.Close();

                            string sqlBusca = "SELECT Id FROM paciente ORDER BY Idade DESC";
                            MySqlCommand cmdId = new MySqlCommand(sqlBusca, conexao);
                            object buscaId = cmdId.ExecuteScalar();
                            int valorId = Convert.ToInt32(buscaId);


                            string sqlDelete = "DELETE FROM paciente WHERE id = @id";
                            MySqlCommand cmdDel = new MySqlCommand(sqlDelete, conexao);
                            cmdDel.Parameters.AddWithValue("@id", buscaId);
                            cmdDel.ExecuteNonQuery();

                            Console.WriteLine($"|*LISTA DE PACIENTES NA FILA ATUALIZADA ({contador - 1}/{limite})*|");
                            Console.WriteLine("\n\nPressione ENTER para retornar ao Menu");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        break;

                    case "4":

                        Console.Clear();
                        if (contador == 0)
                        {
                            Console.WriteLine("Não existe nenhum paciente cadastrado no sistema. Voltando ao Menu. \n\n\n");
                        }

                        Console.WriteLine("Digite o CPF do paciente que voce deseja alterar os dados: ");
                        string cpfDigt = Console.ReadLine();

                        string sql = "SELECT COUNT(*) FROM paciente WHERE Cpf = @cpf";

                        MySqlCommand cmd = new MySqlCommand(sql, conexao);

                        cmd.Parameters.AddWithValue("@cpf", cpfDigt);

                        int qtd = Convert.ToInt32(cmd.ExecuteScalar());

                        if (qtd > 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Digite seu nome novamente: \n");
                            string novoNome = Console.ReadLine();
                            Console.Clear();
                            Console.WriteLine("Digite sua idade novamente: \n");
                            string novaIdade = Console.ReadLine();
                            Console.Clear();
                            Console.WriteLine("Digite seu endereco novamente: \n");
                            string novoEndereco = Console.ReadLine();
                            Console.Clear();
                            Console.WriteLine("Digite seu telefone novamente: \n");
                            string novoTel = Console.ReadLine();
                            Console.Clear();
                            Console.WriteLine("Digite seu email novamente: \n");
                            string novoEmail = Console.ReadLine();
                            Console.Clear();
                            Console.WriteLine("Todas suas informações foram alteradas.\n\nPressione ENTER para continuar...");
                            Console.ReadLine();
                            Console.Clear();

                            string sqlupd = "UPDATE paciente SET nome=@Nome, idade=@Idade, endereco=@Endereco, telefone=@Telefone, email=@Email WHERE cpf = @cpf";

                            MySqlCommand cmdUpd = new MySqlCommand(sqlupd, conexao);

                            cmdUpd.Parameters.AddWithValue("@Nome", novoNome);
                            cmdUpd.Parameters.AddWithValue("@cpf", cpfDigt);
                            cmdUpd.Parameters.AddWithValue("@Idade", novaIdade);
                            cmdUpd.Parameters.AddWithValue("@Endereco", novoEndereco);
                            cmdUpd.Parameters.AddWithValue("@Telefone", novoTel);
                            cmdUpd.Parameters.AddWithValue("@Email", novoEmail);
                            cmdUpd.ExecuteNonQuery();

                        }
                        else
                        {
                            Console.WriteLine("CPF NAO CADASTRADO!!");
                            Console.Clear();
                        }
                        break;

                    case "Q":
                        Console.Clear();
                        continuar = false;
                        Console.WriteLine("Saindo do programa. Aperte ESC para fechar.");
                        conexao.Close();//encerra a conexão
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida! Tente novamente:");
                        Console.WriteLine("Pressione ENTER para voltar ao Menu...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            }
        }
    }
}