using System.Collections.Generic;
using System.Linq;

namespace delegate_challenge
{
    class Program
    {

        static void Main(string[] args)
        {
        }

        public class DataBaseMock : List<int>
        {
            public DataBaseMock()
            {
                AddRange(Enumerable.Range(1, 999));
            }
        }

        public class DataBaseMockRepository
        {
            static DataBaseMock dataBaseInstance = new DataBaseMock();

            public IEnumerable<int> ReadEvenNumbers()
            {
                var numbers = new List<int>();

                foreach (var n in dataBaseInstance)
                    if (n % 2 == 0)
                        numbers.Add(n);

                return numbers;
            }

            public IEnumerable<int> ReadOddNumbers()
            {
                var numbers = new List<int>();

                foreach (var n in dataBaseInstance)
                    if (n % 2 != 0)
                        numbers.Add(n);

                return numbers;
            }

            public IEnumerable<int> ReadGreaterThanFifty()
            {
                var numbers = new List<int>();

                foreach (var n in dataBaseInstance)
                    if (n > 50)
                        numbers.Add(n);

                return numbers;
            }

            // escreva apenas um método que possa substituir os 3 métodos acima e ainda servir de maneira mais geral para outras condições
            // public IEnumerable<int> FindBy(...)  
        }
    }
}
