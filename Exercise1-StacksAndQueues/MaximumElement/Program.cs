using System;
using System.Collections.Generic;
using System.Linq;

namespace MaximumElement
{
    class Program
    {
        static void Main()
        {
	    Stack<int> stack = new Stack<int>();
	    Stack<int> maxStack = new Stack<int>();
	    maxStack.Push(0);
	    int n = int.Parse(Console.ReadLine());
	    for (int i = 1; i <= n; i++)
	    {
		int[] command = Console.ReadLine().Trim()
		    .Split(' ').Select(int.Parse).ToArray();
		switch (command[0])
		{
		    case 1:
			stack.Push(command[1]);
			if (stack.Peek() >= maxStack.Peek())
			    maxStack.Push(stack.Peek());
			break;
		    case 2:
			if (maxStack.Peek() == stack.Pop())
			    maxStack.Pop();
			break;
		    case 3:
			Console.WriteLine(maxStack.Peek());
			break;
		}
	    }
        }
    }
}
