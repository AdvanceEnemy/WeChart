using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            WebHelper.GetAccessToken();
            WebHelper.SendRequest();
            int a = 1;
            int b = 3;
            PrintHelper.Print(ref a, ref b);

            var num = new List<int> { 3, 4, 2, 1 };
            ProcessItems<int>(num);
            PrintHello("");
        }

        public static void PrintHello<T>(T input)
        {
            T temp = default(T);
            string a = string.Empty;
            var test = a ?? "hello";
            Console.WriteLine(input.ToString() + a);
        }

        public Program()
        {
        }

        private static bool IsPrime(int number)
        {
            for (long i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                    break;
                }

                var a = new List<int> { 1, 2, 4, 3, 2 };
                foreach (int item in a)
                {
                    Console.WriteLine(item);
                    var test = new Program();
                }
                Console.ReadKey();
            }
            return true;
        }

        private static IEnumerable<int> FindPrimes(IEnumerable<int> values)
        {
            return values.Where(IsPrime).ToList();
        }

        static void ProcessItems<T>(IEnumerable<T> coll)
        {
            foreach (var item in coll)
            {
                Console.WriteLine(item.ToString() + "");
            }
            Console.ReadKey();
        }
    }
}
