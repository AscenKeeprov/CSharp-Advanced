using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BashSoft
{
    public static class CommandInterpreter  /* PROCESSES CONSOLE COMMANDS */
    {
	public static void StartProcessingCommands()
	{
	    Console.Write($"{Directory.GetCurrentDirectory()}> ");
	    string input;
	    while (!(input = Console.ReadLine().Trim()).Equals("EXIT"))
	    {
		Console.Write(Environment.NewLine);
		List<string> parameters = input.Split().ToList();
		string command = parameters[0].ToUpper();
		switch (command)
		{
		    case "COMPARE":
			if (ParametersCountValid(parameters, 3, 3))
			{
			    string file1 = parameters[1];
			    string file2 = parameters[2];
			    Tester.CompareContents(file1, file2);
			}
			break;
		    case "DOWNLOAD":
			if (ParametersCountValid(parameters, 2, 2))
			{
			    string sourcePath = parameters[1];
			    FSManager.DownloadFile(sourcePath);
			}
			break;
		    case "EXIT":
			if (ParametersCountValid(parameters, 1, 1))
			    IOManager.DisplayGoodbye();
			break;
		    case "GOTODIR":
			if (ParametersCountValid(parameters, 2, 2))
			{
			    string destinationPath = parameters[1];
			    FSManager.ChangeDirectory(destinationPath);
			}
			break;
		    case "HELP":
			if (ParametersCountValid(parameters, 1, 1))
			    IOManager.DisplayHelp();
			break;
		    case "LISTDIR":
			if (ParametersCountValid(parameters, 1, 2))
			{
			    int depth = 0;
			    if (parameters.Count == 2)
			    {
				try
				{
				    depth = int.Parse(parameters[1]);
				}
				catch (FormatException)
				{
				    depth = -1;
				    IOManager.DisplayAlert(Exceptions.InvalidCommandParameter);
				}
			    }
			    if (depth != -1) IOManager.TraverseDirectory(depth);
			}
			break;
		    case "LOADDB":
			if (ParametersCountValid(parameters, 2, 2))
			{
			    string path = parameters[1];
			    DataRepository.LoadData(path);
			}
			break;
		    case "MAKEDIR":
			if (ParametersCountValid(parameters, 2, 2))
			{
			    string path = parameters[1];
			    FSManager.CreateDirectory(path);
			}
			break;
		    case "OPEN":
			if (ParametersCountValid(parameters, 2, 2))
			{
			    string path = parameters[1];
			    FSManager.OpenFile(path);
			}
			break;
		    case "READDB":
			if (ParametersCountValid(parameters, 3, 5))
			{
			    string course = parameters[1];
			    string student = parameters[2];
			    string filter = "OFF";
			    string order = "DESCENDING";
			    if (parameters.Count == 4)
			    {
				if (parameters[3].Length <= 9 && !parameters[3].ToUpper().Equals("ASCENDING"))
				    filter = parameters[3].ToUpper();
				else order = parameters[3].ToUpper();
			    }
			    if (parameters.Count == 5)
			    {
				if (parameters[3].Length <= 9 && !parameters[3].ToUpper().Equals("ASCENDING"))
				{
				    filter = parameters[3].ToUpper();
				    order = parameters[4].ToUpper();
				}
				else
				{
				    filter = parameters[4];
				    order = parameters[3];
				}
			    }
			    DataRepository.ReadDatabase(course, student, filter, order);
			}
			break;
		    case "WIPE":
			Console.Clear();
			break;
		    default:
			IOManager.DisplayAlert(Exceptions.InvalidCommand, command);
			break;
		}
		Console.Write(Environment.NewLine);
		Console.Write($"{Directory.GetCurrentDirectory()}> ");
	    }
	}

	private static bool ParametersCountValid(List<string> parameters,
	    int minRequiredParameters, int maxAllowedParameters)
	{
	    if (parameters.Count < minRequiredParameters)
		IOManager.DisplayAlert(Exceptions.MissingCommandParameter);
	    else if (parameters.Count >= minRequiredParameters &&
		parameters.Count <= maxAllowedParameters) return true;
	    else if (parameters.Count > maxAllowedParameters)
		IOManager.DisplayAlert(Exceptions.RedundantCommandParameters);
	    return false;
	}
    }
}
