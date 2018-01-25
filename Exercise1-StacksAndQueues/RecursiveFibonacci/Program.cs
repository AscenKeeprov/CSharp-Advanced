using System;

namespace RecursiveFibonacci
{
    class Program
    {
	static void Main(string[] args)
	{
	    int n = int.Parse(Console.ReadLine());
	    long[] sequence = new long[n];
	    sequence[0] = 1;
	    if (n > 1) sequence[1] = 1;
	    Console.WriteLine(Fibonacci(n - 1, sequence));
	}

	static long Fibonacci(int n, long[] sequence)
	{
	    if (sequence[n] == 0)
		sequence[n] = Fibonacci(n - 1, sequence) + Fibonacci(n - 2, sequence);
	    return sequence[n];
	}
    }
}
