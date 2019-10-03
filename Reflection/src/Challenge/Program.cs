using System;
using System.Text;

namespace Challenge
{

    /*
     * Desafio Reflection: 
     * Escrever uma nova opção na classe Escrever para receber qualquer objeto e retorne todas seus valores e propriedades.
     * Escrever um Metodo na class Program que execute o objeto Escrever logar de forma generica.
     */

    public class Program
    {
        static void Main(string[] args)
        {
            var nota = new NotaFiscalServico { Numero = 1210, Chave = "NFE1023023012302103012202", CNPJ = "123456789123134", Empresa = "Online Applications" };            

            var escrever = new Escrever();
            escrever.Log(nota);

            Console.ReadKey();
        }

        public class Escrever
        {
            public void Log<T>(T objeto)
            {
                var resultado = new StringBuilder();
                var propriedades = objeto.GetType().GetProperties();

                foreach (var propriedade in propriedades)
                {
                    var nome = propriedade.Name;
                    var valor = propriedade.GetValue(objeto);
                    var tipo = propriedade.PropertyType.UnderlyingSystemType.Name;

                    resultado.AppendLine($"Nome: {nome}, Valor: {valor}, Tipo {tipo}");
                }

                Console.WriteLine(resultado.ToString());
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
