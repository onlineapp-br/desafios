using System;
using System.Text;

namespace Challenge
{

    /*
     * Desafio Reflection: 
     * Escrever uma nova opção na classe Escrever para receber qualquer objeto e retorne todas seus valores e propriedades.
     * Escrever um Metodo na class Program que execute o objeto Escrever logar de forma generica.
     */
    
    class Program
    {
        static void Main(string[] args)
        {
            var nota = new NotaFiscal { Numero = 1210, Chave = "NFE1023023012302103012202" };
            var escrever = new Escrever();
            escrever.Log(nota);
            Console.ReadKey();

        }


        public class Escrever
        {
            public void Log(NotaFiscal nota)
            {
                StringBuilder dados = new StringBuilder(string.Empty);

                dados.AppendLine($"{nameof(nota.Numero)}: {nota.Numero}");
                dados.AppendLine($"{nameof(nota.Chave)}: {nota.Chave}");
                Console.WriteLine(dados.ToString());
            }
        }

        public class NotaFiscal
        {
            public int Numero { get; set; }
            public string Chave { get; set; }

        }

        public class NotaFiscalServico
        {
            public int Numero { get; set; }
            public string Chave { get; set; }
            public string Empresa { get; set; }
            public string CNPJ { get; set; }

        }

        public class NotaFiscalTransporte
        {
            public string Chave { get; set; }
            public string Empresa { get; set; }
            public string CNPJ { get; set; }
            public int Tipo { get; set; }
            public decimal ValorTotal { get; set; }
            public decimal FreteTotal { get; set; }

        }
    }
}
