using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atividade_CSharp
{
    class Menu
    {
        public string Interface()
        {
            Console.WriteLine("=================================================================");
            Console.WriteLine("Bem vindo ao sistema. Selecione uma opção:");
            Console.WriteLine("\n 1. Cadastrar Paciente. \n 2. Listar Pacientes. \n 3. Atender Paciente. \n 4. Alterar Dados Cadastrais. \n Q. Sair");
            Console.WriteLine("=================================================================");
            string valor = Console.ReadLine(); //armazena a escolha do usuário na variável
            valor = valor.ToUpper(); //recebe o valor digitado e o transforma em capslock e armazena
            return valor; //retorna o valor
        }
    }
}
