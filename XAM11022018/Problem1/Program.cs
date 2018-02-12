using System;
using System.Collections.Generic;
using System.Linq;

namespace KeyRevolver
{
    class Program
    {
	static void Main()
	{
	    int bulletPrice = int.Parse(Console.ReadLine());
	    int clipSize = int.Parse(Console.ReadLine());
	    Queue<int> gunClip = new Queue<int>();
	    List<int> bullets = Console.ReadLine().Split().Select(int.Parse).ToList();
	    LoadGun(gunClip, clipSize, bullets);
	    Queue<int> locks = new Queue<int>(Console.ReadLine().Split().Select(int.Parse).ToList());
	    int intelligenceValue = int.Parse(Console.ReadLine());
	    int bulletsFired = 0;
	    while (locks.Count > 0)
	    {
		if (gunClip.Count == 0) break;
		int bulletFired = gunClip.Dequeue();
		int targetLock = locks.Peek();
		if (bulletFired <= targetLock)
		{
		    Console.WriteLine("Bang!");
		    locks.Dequeue();
		}
		else Console.WriteLine("Ping!");
		bulletsFired++;
		if (gunClip.Count == 0) ReloadGun(gunClip, clipSize, bullets);
	    }
	    int bulletsLeft = bullets.Count + gunClip.Count;
	    int missionCost = bulletsFired * bulletPrice;
	    int reward = intelligenceValue - missionCost;
	    if (locks.Count > 0) Console.WriteLine($"Couldn't get through. Locks left: {locks.Count}");
	    else Console.WriteLine($"{bulletsLeft} bullets left. Earned ${reward}");
	}

	private static void LoadGun(Queue<int> gunBarrel, int gunBarrelSize, List<int> bullets)
	{
	    for (int i = 1; i <= gunBarrelSize; i++)
	    {
		if (bullets.Count > 0)
		{
		    gunBarrel.Enqueue(bullets.Last());
		    bullets.RemoveAt(bullets.Count - 1);
		}
	    }
	}

	private static void ReloadGun(Queue<int> gunBarrel, int gunBarrelSize, List<int> bullets)
	{
	    if (bullets.Count > 0) Console.WriteLine("Reloading!");
	    for (int i = 1; i <= gunBarrelSize; i++)
	    {
		if (bullets.Count > 0)
		{
		    gunBarrel.Enqueue(bullets.Last());
		    bullets.RemoveAt(bullets.Count - 1);
		}
	    }
	}
    }
}
