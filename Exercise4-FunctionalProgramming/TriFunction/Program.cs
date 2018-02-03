using System;
using System.Collections.Generic;
using System.Linq;

namespace TriFunction
{
    class Program
    {
        static void Main()
        {
	    int sumInput = int.Parse(Console.ReadLine());
	    List<string> names = Console.ReadLine().Split().ToList();
	    Func<string, int, bool> filter = (name, sum) => name.ToCharArray().Sum(c => c) >= sum;
	    Console.WriteLine(names.FirstOrDefault(name => filter(name, sumInput)));
        }
    }
}
