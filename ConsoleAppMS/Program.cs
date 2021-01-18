using System;
using LibraryMultiSet;

namespace ConsoleAppMS
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] znaki = new char[] { 'a', 'a', 'b', 'a', 'c', 'a', 'd' };

            var mz = new MultiSet<char>(znaki);

            Console.WriteLine(mz);
            Console.WriteLine(mz.ToStringExpanded());

            foreach (var x in mz)
            {
                Console.WriteLine(x);
            }

        }
    }
}

