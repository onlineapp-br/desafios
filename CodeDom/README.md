# Desafio CodeDom
Criar a implementação de uma classe utilizanddo o CodeDom que imprime na tela a seguinte classe em um programa console.

```csharp
using System;

public class Calcular
{

  public int Soma(int num1, int num2)
  {
     return num1 + num2;
  }
  
  public void Multiplicacao(decimal num1, decimal num2)
  {
    var resultado = num1 * num2;
    Console.WriteLine(resultado);
  }
  
  public int Divisao(int dividendo, int divisor)
  {
    int resultado = 0;
    
    try
    {
      resultado =  checked(dividendo/ divisor);
    }
    catch(DivideByZeroException div)
    {
      Console.WriteLine("Problemas Divisão por zero não permitido: "+ div);
    }
    catch(Exception ex)
    {
      Console.WriteLine(ex);
    }
	  return resultado;
  }
}
```
