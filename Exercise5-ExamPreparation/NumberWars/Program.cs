using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberWars
{
    class Program
    {
	static void Main()
	{
	    Queue<string> deckP1 = new Queue<string>(Console.ReadLine().Split());
	    Queue<string> deckP2 = new Queue<string>(Console.ReadLine().Split());
	    int turns = 0;
	    bool gameOver = false;
	    while (deckP1.Count > 0 && deckP2.Count > 0 && turns < 1000000 && gameOver == false)
	    {
		turns++;
		string cardP1 = deckP1.Dequeue();
		string cardP2 = deckP2.Dequeue();
		int cardP1Value = int.Parse(cardP1.Substring(0, cardP1.Length - 1));
		int cardP2Value = int.Parse(cardP2.Substring(0, cardP2.Length - 1));
		if (cardP1Value > cardP2Value)
		{
		    deckP1.Enqueue(cardP1);
		    deckP1.Enqueue(cardP2);
		}
		else if (cardP2Value > cardP1Value)
		{
		    deckP2.Enqueue(cardP2);
		    deckP2.Enqueue(cardP1);
		}
		else
		{
		    List<string> pot = new List<string> { cardP1, cardP2 };
		    while (gameOver == false)
		    {
			if (deckP1.Count >= 3 && deckP2.Count >= 3)
			{
			    int sumCardsP1 = 0;
			    int sumCardsP2 = 0;
			    for (int c = 1; c <= 3; c++)
			    {
				if (deckP1.Count == 0 || deckP2.Count == 0) break;
				cardP1 = deckP1.Dequeue();
				pot.Add(cardP1);
				sumCardsP1 += cardP1.Last();
				cardP2 = deckP2.Dequeue();
				pot.Add(cardP2);
				sumCardsP2 += cardP2.Last();
			    }
			    if (sumCardsP1 != sumCardsP2)
				pot = pot.OrderByDescending(c => int.Parse(c.Substring(0, c.Length - 1)))
				    .ThenByDescending(c => c.Last()).ToList();
			    if (sumCardsP1 > sumCardsP2)
			    {
				foreach (string card in pot) deckP1.Enqueue(card);
				break;
			    }
			    else if (sumCardsP2 > sumCardsP1)
			    {
				foreach (string card in pot) deckP2.Enqueue(card);
				break;
			    }
			}
			else gameOver = true;
		    }
		}
	    }
	    string result = String.Empty;
	    if (deckP1.Count > deckP2.Count) result = "First player wins";
	    else if (deckP2.Count > deckP1.Count) result = "Second player wins";
	    else if (deckP1.Count == deckP2.Count) result = "Draw";
	    Console.WriteLine($"{result} after {turns} turns");
	}
    }
}
