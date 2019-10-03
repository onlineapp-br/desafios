using System;
using System.Collections.Generic;
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
            var list = new List<object>();
            list.Add(new NotaFiscal
            {
                Numero = 1210,
                Chave = "NFE1023023012302103012202"
            });

            list.Add(new NotaFiscalServico
            {
                Numero = 1210,
                Chave = "NFE1023023012302103012202",
                Empresa = "Onbusca",
                CNPJ = "40369878000120"
            });

            list.Add(new NotaFiscalTransporte
            {
                Chave = "NFE1023023012302103012202",
                Empresa = "Azul Linhas Aéreas",
                CNPJ = "12345678000113",
                Tipo = 1,
                ValorTotal = 42.50m,
                FreteTotal = 1.50m
            });

            var escrever = new Escrever();
            escrever.Log(list);
            Console.ReadKey();
        }


        public class Escrever
        {
            public void Log(List<object> documentos)
            {
                var dados = new StringBuilder(string.Empty);

                foreach (var documento in documentos)
                {
                    var typeDocumento = documento.GetType();

                    dados.AppendLine(typeDocumento.Name);

                    foreach (var propriedade in typeDocumento.GetProperties())
                        dados.AppendLine($"{propriedade.Name}: {propriedade.GetValue(documento)}");

                    dados.AppendLine();
                }

                Console.WriteLine(dados.ToString());
            }

            public void Log<T>(T documento)
            {
                var dados = new StringBuilder(string.Empty);
                var typeDocumento = documento.GetType();

                Console.WriteLine(typeDocumento.Name);

                foreach (var propriedade in typeDocumento.GetProperties())
                    dados.AppendLine($"{propriedade.Name}: {propriedade.GetValue(documento)}");

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
