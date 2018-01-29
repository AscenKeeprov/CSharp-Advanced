using System;

namespace HeiganDance
{
    class Program
    {
	static void Main()
	{
	    int[,] arena = new int[15, 15];
	    Boss boss = new Boss() { Health = 3000000, Status = "Alive" };
	    Player player = new Player()
	    {
		Health = 18500,
		Status = "Alive",
		Position = Tuple.Create(7, 7),
		Damage = double.Parse(Console.ReadLine())
	    };
	    string action = Console.ReadLine();
	    while (player.Status != "Killed" || boss.Status == "Defeated")
	    {
		if (player.Health > 0) boss.Health -= player.Damage;
		HealthCheck(boss, player, arena);
		Array.Clear(arena, 0, arena.Length);
		BossAttack(boss, action, arena, player);
		PlayerMove(player, action, arena, boss);
		action = Console.ReadLine();
	    }
	}

	private static void BossAttack(Boss boss, string action, int[,] arena, Player player)
	{
	    boss.Spell = action.Split()[0];
	    int firstAoERow = Math.Max(int.Parse(action.Split()[1]) - 1, -1);
	    int lastAoERow = Math.Min(int.Parse(action.Split()[1]) + 1, 15);
	    int firstAoECol = Math.Max(int.Parse(action.Split()[2]) - 1, -1);
	    int lastAoECol = Math.Min(int.Parse(action.Split()[2]) + 1, 15);
	    for (int r = firstAoERow; r <= lastAoERow; r++)
	    {
		for (int c = firstAoECol; c <= lastAoECol; c++)
		{
		    if (r >= 0 && r <= 14 && c >= 0 && c <= 14)
		    {
			if (boss.Spell == "Cloud") arena[r, c] = 3500;
			else if (boss.Spell == "Eruption") arena[r, c] = 6000;
		    }
		}
	    }
	}

	private static void PlayerMove(Player player, string action, int[,] arena, Boss boss)
	{
	    int row = player.Position.Item1;
	    int col = player.Position.Item2;
	    if (arena[row, col] != 0)
	    {
		if (row > 0 && arena[row - 1, col] == 0)
		    player.Position = Tuple.Create(row - 1, col);
		else if (col < 14 && arena[row, col + 1] == 0)
		    player.Position = Tuple.Create(row, col + 1);
		else if (row < 14 && arena[row + 1, col] == 0)
		    player.Position = Tuple.Create(row + 1, col);
		else if (col > 0 && arena[row, col - 1] == 0)
		    player.Position = Tuple.Create(row, col - 1);
		row = player.Position.Item1;
		col = player.Position.Item2;
		player.Health -= arena[row, col];
		HealthCheck(boss, player, arena);
		if (arena[row, col] == 3500)
		    player.Status = "Plagued";
	    }
	}

	private static void HealthCheck(Boss boss, Player player, int[,] arena)
	{
	    if (player.Health <= 0) player.Status = "Killed";
	    if (boss.Health <= 0) boss.Status = "Defeated";
	    if (player.Status == "Plagued")
	    {
		player.Health -= 3500;
		player.Status = "Alive";
		HealthCheck(boss, player, arena);
	    }
	    if (boss.Status == "Defeated" || player.Status == "Killed")
		EndGame(boss, player);
	}

	private static void EndGame(Boss boss, Player player)
	{
	    if (boss.Spell == "Cloud") boss.Spell = "Plague Cloud";
	    if (boss.Status == "Defeated" && player.Status == "Alive")
	    {
		Console.WriteLine($"Heigan: {boss.Status}!");
		Console.WriteLine($"Player: {player.Health}");
	    }
	    else if (boss.Status == "Alive" && player.Status == "Killed")
	    {
		Console.WriteLine($"Heigan: {boss.Health:F2}");
		Console.WriteLine($"Player: {player.Status} by {boss.Spell}");
	    }
	    else if (boss.Status == "Defeated" && player.Status == "Killed")
	    {
		Console.WriteLine($"Heigan: {boss.Status}!");
		Console.WriteLine($"Player: {player.Status} by {boss.Spell}");
	    }
	    Console.WriteLine($"Final position: {player.Position.Item1}, {player.Position.Item2}");
	    Environment.Exit(0);
	}
    }

    class Player
    {
	public int Health { get; set; }
	public string Status { get; set; }
	public Tuple<int, int> Position { get; set; }
	public double Damage { get; set; }
    }

    class Boss
    {
	public double Health { get; set; }
	public string Status { get; set; }
	public string Spell { get; set; }
    }
}
