using System;
using System.Collections.Generic;
using System.Linq;

namespace HitList
{
    class Program
    {
	static void Main()
	{
	    Dictionary<string, Dictionary<string, string>> people = new Dictionary<string, Dictionary<string, string>>();
	    int targetInfoIndex = int.Parse(Console.ReadLine());
	    string input;
	    while (!(input = Console.ReadLine()).Equals("end transmissions"))
	    {
		string[] personInfo = input
		    .Split(new char[] { '=', ';' }, StringSplitOptions.RemoveEmptyEntries);
		string personName = personInfo[0];
		if (!people.ContainsKey(personName))
		    people.Add(personName, new Dictionary<string, string>());
		for (int i = 1; i < personInfo.Length; i++)
		{
		    string[] personData = personInfo[i]
			.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
		    string personFactName = personData[0];
		    string personFactValue = personData[1];
		    if (!people[personName].ContainsKey(personFactName))
			people[personName].Add(personFactName, personFactValue);
		    people[personName][personFactName] = personFactValue;
		}
	    }
	    string target = Console.ReadLine().Split()[1];
	    foreach (var person in people.Where(p => p.Key == target))
	    {
		string personName = person.Key;
		var personInfo = person.Value;
		int keysLengths = 0;
		int valuesLengths = 0;
		foreach (var fact in personInfo)
		{
		    keysLengths += fact.Key.Length;
		    valuesLengths += fact.Value.Length;
		}
		int infoIndex = keysLengths + valuesLengths;
		people[personName].Add("zzzindex", $"{infoIndex}");
		if (infoIndex >= targetInfoIndex)
		{
		    Console.WriteLine($"Info on {personName}:");
		    foreach (var fact in personInfo.OrderBy(f => f.Key))
			if (fact.Key == "zzzindex") Console.WriteLine($"Info index: {fact.Value}");
			else Console.WriteLine($"---{fact.Key}: {fact.Value}");
		    Console.WriteLine("Proceed");
		}
		else
		{
		    Console.WriteLine($"Info on {personName}:");
		    foreach (var fact in personInfo.OrderBy(f => f.Key))
			if (fact.Key == "zzzindex") Console.WriteLine($"Info index: {fact.Value}");
			else Console.WriteLine($"---{fact.Key}: {fact.Value}");
		    Console.WriteLine($"Need {targetInfoIndex - infoIndex} more info.");
		}
	    }
	}
    }
}
