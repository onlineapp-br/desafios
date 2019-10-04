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
            //var escrever = new Escrever();
            //escrever.Log<NotaFiscal>(nota);

            Logar<NotaFiscal>(nota);

            Console.ReadKey();
        }

        public static void Logar<T>(T parametro)
        {
            var parametros = new object[] { parametro };
            var instancia = Activator.CreateInstance(typeof(Escrever));

            var metodo = typeof(Escrever).GetMethod("Log");
            metodo = metodo.MakeGenericMethod(parametro.GetType());

            metodo.Invoke(instancia, parametros);
        }

        public class Escrever
        {
            public void Log<T>(T objeto)
            {
                StringBuilder dados = new StringBuilder(string.Empty);

                var propriedades = objeto.GetType().GetProperties();

                foreach (var propriedade in propriedades)
                    dados.AppendLine($"{propriedade.Name}: {propriedade.GetValue(objeto, null)}");

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