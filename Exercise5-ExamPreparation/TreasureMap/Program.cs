using System;
using System.Text.RegularExpressions;

namespace TreasureMap
{
    class Program
    {
        static void Main()
        {
	    string pattern = @"(?:(?<symbol>#)|!)[^#!]*?(?<![a-zA-Z0-9])(?<strName>[a-zA-Z]{4})(?![a-zA-Z0-9])[^#!]*(?<!\d)(?<strNo>\d{3})-(?<pass>\d{4}|\d{6})(?!\d)[^#!]*?(?:(?(symbol)!|#))";
	    int n = int.Parse(Console.ReadLine());
	    for (int i = 1; i <= n; i++)
	    {
		string input = Console.ReadLine();
		MatchCollection matches = Regex.Matches(input, pattern);
		var address = matches[matches.Count / 2];
		string streetName = address.Groups["strName"].Value;
		string streetNumber = address.Groups["strNo"].Value;
		string password = address.Groups["pass"].Value;
		Console.WriteLine($"Go to str. {streetName} {streetNumber}. Secret pass: {password}.");
	    }
        }
    }
}
