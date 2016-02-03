using System;
using System.Text;

namespace NaiveFizzBuzz
{
    class Program
    {
        static void Main()
        {
            var sb = new StringBuilder();

            for (var i = 1; i <= 100; i++)
            {
                if (i % 5 == 0 && i % 3 == 0)
                    sb.Append("FizzBuzz, ");
                else if (i % 5 == 0)
                    sb.Append("Buzz, ");
                else if (i % 3 == 0)
                    sb.Append("Fizz, ");
                else
                    sb.AppendFormat("{0}, ", i);
            }
            Console.WriteLine(sb.ToString());
            Console.ReadLine();
        }
    }
}
