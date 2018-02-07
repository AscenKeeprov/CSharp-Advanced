using System;

namespace BashSoft
{
    class Launcher
    {
	static void Main()	/* PROGRAM ACCESS POINT */
	{
	    Console.Title = "BASHSOFT";
	    DataRepository.InitializeDatabase();
	    IOManager.DisplayWelcome();
	    CommandInterpreter.StartProcessingCommands();
	}
    }
}
