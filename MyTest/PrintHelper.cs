using System;

namespace MyTest
{
    class PrintHelper
    {
        public static void  Print<T>(ref  T a,ref  T b)
        {
            T temp;
            temp = a;
            a = b;
            b = temp;
            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.ReadKey();
        }
    }
}