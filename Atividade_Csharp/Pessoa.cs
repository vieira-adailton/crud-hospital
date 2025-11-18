using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace Atividade_CSharp
{
    class Pessoa
    {
        public string Nome; //define nome paci
        public string Cpf; //define cpf paci
        public int Idade; //define idade paci 
        public string Endereco; //define end paci
        public string Telefone; //define tel paci
        public string Email; //define email paci




        public void Cadastrar() //metodo cadastrar
        {
            Console.Clear();
            Console.WriteLine("Informe seu nome:");
            this.Nome = Console.ReadLine(); //armazena o nome digitado na variável
            Console.Clear();
            Console.WriteLine("Informe seu CPF:");
            this.Cpf = Console.ReadLine(); //armazena o cpf digitado na variável
            Console.Clear();
            Console.WriteLine("Informe sua idade:");
            this.Idade = int.Parse(Console.ReadLine()); //converte idade para numero e armazena na variável
            Console.Clear();
            Console.WriteLine("Informe seu endereco:");
            this.Endereco = Console.ReadLine(); //armazena o endereço digitado na variável
            Console.Clear();
            Console.WriteLine("Informe seu telefone:");
            this.Telefone = Console.ReadLine(); //armazena o telefone digitado na variável
            Console.Clear();
            Console.WriteLine("Informe seu e-mail:");
            this.Email = Console.ReadLine(); //armazena o email digitado na variável
        }


    }
}