using System;
using System.Collections.Generic;

namespace BalancedParentheses
{
    class Program
    {
	static void Main()
	{
	    Dictionary<char, char> pairs = new Dictionary<char, char>()
	    {
		{'(',')'},
		{'[',']'},
		{'{','}'}
	    };
	    Stack<char> stack = new Stack<char>();
	    char[] parentheses = Console.ReadLine().ToCharArray();
	    if (parentheses.Length % 2 != 0) Terminate();
	    foreach (char p in parentheses)
	    {
		if (pairs.ContainsKey(p))
		{
		    char openingParenthesis = p;
		    stack.Push(openingParenthesis);
		}
		else
		{
		    char closingParenthesis = p;
		    char openingParantheses = stack.Pop();
		    if (closingParenthesis != pairs[openingParantheses])
			Terminate();
		}
	    }
	    Console.WriteLine("YES");
	}

	public static void Terminate()
	{
	    Console.WriteLine("NO");
	    Environment.Exit(0);
	}
    }
}
