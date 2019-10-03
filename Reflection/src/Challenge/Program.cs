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
            var empresa = new Pessoal
            {
                Cpf = "23787094806",
                Nome = "Billy Tukija"
            };

            var nota2 = new NotaFiscal
            {
                Chave = "NFE1023023012302103012202",
                Numero = 1211
            };

            Log(nota2);

            var escrever = new Escrever();

            Escrever.ObjetoPorReflexao(empresa);

            var nota = new NotaFiscal { Numero = 1210, Chave = "NFE1023023012302103012202" };
            escrever.Log(nota);
            Console.ReadKey();

            //Escrever um Metodo na class Program que execute o objeto Escrever logar de forma generica.
            void Log<T>(T objeto)
            {
                var notasRecebidas = objeto.GetType().GetProperties();
                StringBuilder dados = new StringBuilder(string.Empty);

                foreach (var nta in notasRecebidas)
                    dados.AppendLine($"{nta.GetValue(objeto, null)}");

                Console.WriteLine(dados.ToString());
                Console.WriteLine("--------------------------------------------------");
            }

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

            //Escrever uma nova opção na classe Escrever para receber qualquer objeto e retorne todas seus valores e propriedades.
            public static void ObjetoPorReflexao<T>(T objeto)
            {
                var propriedades = objeto.GetType().GetProperties();

                foreach (var prop in propriedades)
                    Console.WriteLine($"{prop.Name} = {prop.GetValue(objeto, null)}");

                Console.ReadKey();
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

        public class Pessoal
        {
            public string Cpf { get; set; }
            public string Nome { get; set; }
        }
    }
}
