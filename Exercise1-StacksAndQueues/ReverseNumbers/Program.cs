using System;
using System.Collections.Generic;
using System.Linq;

namespace ReverseNumbers
{
    class Program
    {
        static void Main()
        {
	    int[] numbers = Console.ReadLine()
		.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
		.Select(int.Parse).ToArray();
	    Stack<int> stack = new Stack<int>(numbers);
	    while (stack.Count > 0)
	    {
		Console.Write(stack.Pop() + " ");
	    }
	    Console.WriteLine();
        }
    }
}
