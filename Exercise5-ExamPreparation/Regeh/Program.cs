using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Regeh
{
    class Program
    {
	static void Main()
	{
	    string pattern = @"\[[a-zA-Z]+<(\d+)REGEH(\d+)>[a-zA-Z]+\]";
	    string input = Console.ReadLine();
	    MatchCollection matches = Regex.Matches(input, pattern);
	    List<int> indexes = new List<int>();
	    foreach (Match match in matches)
	    {
		indexes.Add(int.Parse(match.Groups[1].Value));
		indexes.Add(int.Parse(match.Groups[2].Value));
	    }
	    int index = 0;
	    for (int i = 0; i < indexes.Count; i++)
	    {
		index += indexes[i];
		index = index % (input.Length - 1);
		Console.Write(input[index]);
	    }
	}
    }
}
