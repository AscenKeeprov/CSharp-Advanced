using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordCount
{
    class Program
    {
	static void Main()
	{
	    Dictionary<string, int> words = new Dictionary<string, int>();
	    using (StreamReader wordReader = new StreamReader("words.txt"))
	    {
		string word;
		while ((word = wordReader.ReadLine()) != null) words.Add(word, 0);
	    }
	    string text = File.ReadAllText("text.txt");
	    char[] punctuation = text.Where(c => char.IsPunctuation(c) || c == ' ')
		.Except("'").Distinct().ToArray();
	    string[] textWords = text.ToLower()
		.Split(punctuation, StringSplitOptions.RemoveEmptyEntries);
	    for (int word = 0; word < textWords.Length; word++)
	    {
		if (words.ContainsKey(textWords[word])) words[textWords[word]]++;
	    }
	    using (StreamWriter results = new StreamWriter("results.txt"))
	    {
		foreach (var word in words.OrderByDescending(word => word.Value))
		{
		    results.WriteLine($"{word.Key} - {word.Value}");
		}
	    }
	}
    }
}
