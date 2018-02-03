using System;

namespace ActionPrint
{
    class Program
    {
        static void Main()
        {
	    Action<string> printLine = text => Console.WriteLine(text);
	    string[] input = Console.ReadLine().Split();
	    foreach (string word in input) printLine(word);
        }
    }
}
