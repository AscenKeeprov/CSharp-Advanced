using System;
using System.Collections.Generic;

namespace StackFibonacci
{
    class Program
    {
        static void Main()
        {
	    Stack<long> fibonacci = new Stack<long>();
	    fibonacci.Push(0);
	    fibonacci.Push(1);
	    int n = int.Parse(Console.ReadLine());
	    for (int f = 2; f <= n; f++)
	    {
		long addend2 = fibonacci.Pop();
		long addend1 = fibonacci.Peek();
		long sum = addend1 + addend2;
		fibonacci.Push(addend2);
		fibonacci.Push(sum);
	    }
	    Console.WriteLine(fibonacci.Peek());
	}
    }
}
