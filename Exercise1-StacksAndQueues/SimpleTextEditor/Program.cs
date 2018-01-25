using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTextEditor
{
    class Program
    {
        static void Main()
        {
	    int n = int.Parse(Console.ReadLine());
	    StringBuilder text = new StringBuilder();
	    Stack<string> oldText = new Stack<string>();
	    for (int i = 1; i <= n; i++)
	    {
		string[] command = Console.ReadLine().Split();
		int operation = int.Parse(command[0]);
		switch (operation)
		{
		    case 1:
			oldText.Push(text.ToString());
			text.Append(command[1]);
			break;
		    case 2:
			oldText.Push(text.ToString());
			int charsToErase = int.Parse(command[1]);
			text.Remove(text.Length - charsToErase, charsToErase);
			break;
		    case 3:
			int index = int.Parse(command[1]);
			Console.WriteLine(text[index - 1]);
			break;
		    case 4:
			text.Clear();
			text.Append(oldText.Pop());
			break;
		}
	    }
        }
    }
}
