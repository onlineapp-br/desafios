using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace codedom_challenge
{

    class Sample
    {
        CodeCompileUnit targetUnit;

        CodeTypeDeclaration classe;

        private const string outputFileName = "SampleCode.cs";

        public Sample()
        {
            targetUnit = new CodeCompileUnit();
            CodeNamespace samples = new CodeNamespace("");
            samples.Imports.Add(new CodeNamespaceImport("System"));
            classe = new CodeTypeDeclaration("Calcular");
            classe.IsClass = true;
            classe.TypeAttributes = TypeAttributes.Public;
            samples.Types.Add(classe);
            targetUnit.Namespaces.Add(samples);
        }

        public void AdicionaMetodos()
        {
            #region .: Soma :.

            CodeMemberMethod somaMetodo = new CodeMemberMethod();
            somaMetodo.Attributes = MemberAttributes.Public;
            somaMetodo.ReturnType = new CodeTypeReference(typeof(int));
            somaMetodo.Name = "Soma";

            somaMetodo.Parameters.Add(new CodeParameterDeclarationExpression("System.Int32", "num1"));
            somaMetodo.Parameters.Add(new CodeParameterDeclarationExpression("System.Int32", "num2"));

            CodeMethodReturnStatement somaRetorno = new CodeMethodReturnStatement(
                new CodeBinaryOperatorExpression(
                    new CodeArgumentReferenceExpression("num1"),
                    CodeBinaryOperatorType.Add,
                    new CodeArgumentReferenceExpression("num2")
                )
            );

            somaMetodo.Statements.Add(somaRetorno);

            classe.Members.Add(somaMetodo);

            #endregion

            #region .: Multiplicacao :.

            CodeMemberMethod multiplicacaoMetodo = new CodeMemberMethod();
            multiplicacaoMetodo.Attributes = MemberAttributes.Public;
            multiplicacaoMetodo.ReturnType = new CodeTypeReference(typeof(void));
            multiplicacaoMetodo.Name = "Multiplicacao";

            multiplicacaoMetodo.Parameters.Add(new CodeParameterDeclarationExpression("System.Decimal", "num1"));
            multiplicacaoMetodo.Parameters.Add(new CodeParameterDeclarationExpression("System.Decimal", "num2"));

            var multiplicacaoOperacao = new CodeBinaryOperatorExpression(
                    new CodeArgumentReferenceExpression("num1"),
                    CodeBinaryOperatorType.Multiply,
                    new CodeArgumentReferenceExpression("num2")
            );

            multiplicacaoMetodo.Statements.Add(
                new CodeVariableDeclarationStatement("var", "resultado", multiplicacaoOperacao)
            );

            multiplicacaoMetodo.Statements.Add(
                new CodeMethodInvokeExpression(
                    new CodeMethodReferenceExpression(new CodeVariableReferenceExpression("Console"), "WriteLine"),
                    new CodeVariableReferenceExpression("resultado")
                )
            );

            classe.Members.Add(multiplicacaoMetodo);

            #endregion

            #region .: Divisao :.

            CodeMemberMethod divisaoMetodo = new CodeMemberMethod();
            divisaoMetodo.Attributes = MemberAttributes.Public;
            divisaoMetodo.ReturnType = new CodeTypeReference(typeof(int));
            divisaoMetodo.Name = "Soma";

            divisaoMetodo.Parameters.Add(new CodeParameterDeclarationExpression("System.Int32", "dividendo"));
            divisaoMetodo.Parameters.Add(new CodeParameterDeclarationExpression("System.Int32", "divisor"));

            divisaoMetodo.Statements.Add(
                new CodeVariableDeclarationStatement(new CodeTypeReference(typeof(int)), "resultado", new CodePrimitiveExpression(0))
            );

            var tryCatchBlock = new CodeTryCatchFinallyStatement();

            var tryStatement = new CodeAssignStatement(
                    new CodeVariableReferenceExpression("resultado"),
                    new CodeMethodInvokeExpression(
                        new CodeMethodReferenceExpression(null, " checked"),
                        (new CodeBinaryOperatorExpression(
                                    new CodeArgumentReferenceExpression("dividendo"),
                                    CodeBinaryOperatorType.Divide,
                                    new CodeArgumentReferenceExpression("divisor")
                        ))
                    )
            );

            var catch1 = new CodeCatchClause();
            catch1.CatchExceptionType = new CodeTypeReference(typeof(DivideByZeroException));
            catch1.LocalName = "div";
            catch1.Statements.Add(
                new CodeMethodInvokeExpression(
                    new CodeMethodReferenceExpression(new CodeVariableReferenceExpression("Console"), "WriteLine"),
                    new CodeVariableReferenceExpression("\"Problemas Divisão por zero não permitido: \"+ div")
                )
            );

            var catch2 = new CodeCatchClause();
            catch2.CatchExceptionType = new CodeTypeReference(typeof(Exception));
            catch2.LocalName = "ex";
            catch2.Statements.Add(
                new CodeMethodInvokeExpression(
                    new CodeMethodReferenceExpression(new CodeVariableReferenceExpression("Console"), "WriteLine"),
                    new CodeVariableReferenceExpression("ex")
                )
            );

            tryCatchBlock.TryStatements.Add(tryStatement);
            tryCatchBlock.CatchClauses.Add(catch1);
            tryCatchBlock.CatchClauses.Add(catch2);

            divisaoMetodo.Statements.Add(tryCatchBlock);
            divisaoMetodo.Statements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("resultado")));

            classe.Members.Add(divisaoMetodo);

            #endregion
        }

        public void GenerateCSharpCode(string fileName)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            using (StreamWriter sourceWriter = new StreamWriter(fileName))
            {
                provider.GenerateCodeFromCompileUnit(targetUnit, sourceWriter, options);
            }
            string line = "";
            using (StreamReader sr = new StreamReader(fileName))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }

        static void Main()
        {
            Sample sample = new Sample();
            sample.AdicionaMetodos();
            sample.GenerateCSharpCode(outputFileName);
            Console.ReadKey();
        }
    }
}
