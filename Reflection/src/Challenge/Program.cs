using System;
using System.Collections;
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
            var objToLog = new List<ClassePai>();

            var objP1 = new ClassePai { NomePai = "Papai", Filhos = new List<ClasseFilho>() };
            var objF1 = new ClasseFilho { NomeFilho = "Filho 1" };
            var objF2 = new ClasseFilho { NomeFilho = "Filho 2" };
            objP1.Filhos.Add(objF1);
            objP1.Filhos.Add(objF2);

            var objP2 = new ClassePai { NomePai = "Papai 2", Filhos = new List<ClasseFilho>() };
            var objF3 = new ClasseFilho { NomeFilho = "Filho 3" };
            var objF4 = new ClasseFilho { NomeFilho = "Filho 4" };
            objP2.Filhos.Add(objF3);
            objP2.Filhos.Add(objF4);

            objToLog.Add(objP1);
            objToLog.Add(objP2);

            var escrever = new Escrever();
            escrever.LogGenerico(objToLog);
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
            public void LogEscreveLinha(string linha)
            {
                Console.WriteLine(linha);
            }
            public void LogGenerico<T>(List<T> objParam)
            {
                foreach (var obj in objParam)
                {
                    var props = obj.GetType().GetProperties();
                    foreach (var prop in props)
                    {
                        var value = prop.GetValue(obj);
                        if (value is IList && value.GetType().IsGenericType)
                        {
                            foreach (var item in (IEnumerable)value)
                            {
                                LogGenerico(item);
                            }
                        }
                        else
                        {
                            var linha = prop.Name.ToString() + ":" + prop.GetValue(obj).ToString();
                            LogEscreveLinha(linha);
                        }
                    }
                }
            }
            public void LogGenerico<T>(T obj)
            {
                var props = obj.GetType().GetProperties();
                foreach (var prop in props)
                {
                    var value = prop.GetValue(obj);
                    if (value is IList && value.GetType().IsGenericType)
                    {
                        foreach(var item in (IEnumerable)value)
                        {
                            LogGenerico(item);
                        }
                    }
                    else
                    {
                        var linha = prop.Name.ToString() + ":" + prop.GetValue(obj).ToString();
                        LogEscreveLinha(linha);
                    }
                }
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

        public class ClassePai
        {
            public string NomePai { get; set; }
            public List<ClasseFilho> Filhos { get; set; }
        }
        public class ClasseFilho
        {
            public string NomeFilho { get; set; }
        }
    }
}
