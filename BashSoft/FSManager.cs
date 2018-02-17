using System;
using System.IO;

namespace BashSoft
{
    public static class FSManager	    /* CARRIES OUT FILE SYSTEM OPERATIONS */
    {
	public static void CreateDirectory(string path)
	{
	    path = IOManager.BuildAbsolutePath(path);
	    string existingPath = String.Empty;
	    string[] pathToCreate = path.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
	    try
	    {
		for (int l = 0; l < pathToCreate.Length; l++)
		{
		    string currentPath = existingPath + pathToCreate[l];
		    if (!Directory.Exists(currentPath))
		    {
			Directory.CreateDirectory(currentPath);
			Console.WriteLine($"Directory \"{pathToCreate[l]}\" created in {existingPath}");
		    }
		    existingPath = $"{currentPath}\\";
		}
	    }
	    catch (Exception exception)
	    {
		if (exception is ArgumentException || exception is NotSupportedException)
		    IOManager.DisplayAlert(Exceptions.InvalidName);
		else if (exception is UnauthorizedAccessException)
		    IOManager.DisplayAlert(Exceptions.UnauthorizedAccess);
	    }
	}

	public static void ChangeDirectory(string destinationPath)
	{
	    string currentDir = Directory.GetCurrentDirectory();
	    destinationPath = IOManager.BuildAbsolutePath(destinationPath);
	    try
	    {
		Directory.SetCurrentDirectory(destinationPath);
	    }
	    catch (Exception exception)
	    {
		if (exception is DirectoryNotFoundException)
		    IOManager.DisplayAlert(Exceptions.InvalidPath);
		else if (exception is ArgumentOutOfRangeException)
		    IOManager.DisplayAlert(Exceptions.InvalidCommandParameter);
		else if (exception is UnauthorizedAccessException)
		    IOManager.DisplayAlert(Exceptions.UnauthorizedAccess);
	    }
	}

	public static void OpenFile(string path)
	{
	    string fileName = IOManager.ExtractFileName(path);
	    path = IOManager.BuildAbsolutePath(path);
	    string filePath = $"{path}\\{fileName}";
	    try
	    {
		Console.WriteLine(File.ReadAllText(filePath));
	    }
	    catch (Exception exception)
	    {
		if (exception is DirectoryNotFoundException || exception is FileNotFoundException)
		    IOManager.DisplayAlert(Exceptions.InvalidPath);
		else if (exception is UnauthorizedAccessException)
		    IOManager.DisplayAlert(Exceptions.UnauthorizedAccess);
	    }
	}

	public static void DownloadFile(string sourcePath)
	{
	    string destinationPath = Directory.GetCurrentDirectory();
	    string fileName = IOManager.ExtractFileName(sourcePath);
	    sourcePath = IOManager.BuildAbsolutePath(sourcePath);
	    if (String.IsNullOrEmpty(fileName))
		IOManager.DisplayAlert(Exceptions.FileNotSpecified);
	    else if (!sourcePath.Contains("\\") || sourcePath == destinationPath)
		IOManager.DisplayAlert(Exceptions.IncompletePath);
	    else
	    {
		try
		{
		    File.Copy($"{sourcePath}\\{fileName}", $"{destinationPath}\\{fileName}");
		    Console.WriteLine($"File \"{fileName}\" has been downloaded to your current working folder");
		}
		catch (Exception exception)
		{
		    if (exception is DirectoryNotFoundException || exception is FileNotFoundException)
			IOManager.DisplayAlert(Exceptions.InvalidPath);
		    else if (exception is UnauthorizedAccessException)
			IOManager.DisplayAlert(Exceptions.UnauthorizedAccess);
		    else if (exception is IOException)
		    {
			IOManager.DisplayAlert(Exceptions.FileAlreadyDownloaded);
			ConsoleKeyInfo choice = Console.ReadKey();
			Console.Write(Environment.NewLine);
			if (choice.Key == ConsoleKey.Y)
			{
			    File.Copy($"{sourcePath}\\{fileName}", $"{destinationPath}\\{fileName}", true);
			    Console.WriteLine($"File \"{fileName}\" has been overwritten.");
			}
			else Console.WriteLine("Download cancelled.");
		    }
		}
	    }
	}
    }
}
