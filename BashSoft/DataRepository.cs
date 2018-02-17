using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BashSoft
{
    public static class DataRepository      /* ENABLES DATA PERSISTENCE AND RETRIEVAL */
    {
	private static Dictionary<string /*Courses*/, Dictionary<string /*Students*/, List<int> /*Scores*/>> database;
	public static bool isDatabaseInitialized = false;

	public static void InitializeDatabase()
	{
	    if (!isDatabaseInitialized)
	    {
		Console.WriteLine("Initializing database...");
		database = new Dictionary<string, Dictionary<string, List<int>>>();
		Console.WriteLine("Database initialized successfully!");
	    }
	    else Console.WriteLine(Exceptions.DatabaseAlreadyInitialized);
	}

	public static void LoadData(string path)
	{
	    string fileName = IOManager.ExtractFileName(path);
	    path = IOManager.BuildAbsolutePath(path);
	    try
	    {
		string[] databaseSource = File.ReadAllLines($"{path}\\{fileName}");
		Console.WriteLine("Populating database structure...");
		string pattern = @"([A-Z]\#?\+{0,2}[a-zA-Z]*_[A-Z][a-z]{2}_201[4-8])\s+([A-Z][a-z]{0,3}\d{2}_\d{2,4})\s+(100|[1-9][0-9]|[0-9])";
		database.Clear();
		foreach (string record in databaseSource)
		{
		    if (!string.IsNullOrEmpty(record) && Regex.IsMatch(record, pattern))
		    {
			Match validRecord = Regex.Match(record, pattern);
			string course = validRecord.Groups[1].Value;
			string student = validRecord.Groups[2].Value;
			int score = int.Parse(validRecord.Groups[3].Value);
			if (!database.ContainsKey(course))
			    database.Add(course, new Dictionary<string, List<int>>());
			if (!database[course].ContainsKey(student))
			    database[course].Add(student, new List<int>());
			database[course][student].Add(score);
		    }
		}
		isDatabaseInitialized = true;
		Console.WriteLine($"Done! {databaseSource.Length} unique records were loaded in the database.");
	    }
	    catch (Exception exception)
	    {
		if (exception is DirectoryNotFoundException || exception is FileNotFoundException)
		    IOManager.DisplayAlert(Exceptions.InvalidPath);
		else if (exception is UnauthorizedAccessException)
		    IOManager.DisplayAlert(Exceptions.UnauthorizedAccess);
	    }
	}

	public static void ReadDatabase(string course, string student, string filter, string order)
	{
	    if (IsQueryValid(course, student, filter, order))
	    {
		Dictionary<string, Dictionary<string, List<int>>> report =
		    database.ToDictionary(c => c.Key, s => s.Value);
		report = FilterCourses(report, course);
		report = FilterScores(report, filter);
		if (report.Values.Count != 0) report = FilterStudents(report, student, order);
		IOManager.PrintDatabaseReport(report, order);
	    }
	}

	private static Dictionary<string, Dictionary<string, List<int>>> FilterCourses(
	    Dictionary<string, Dictionary<string, List<int>>> report, string course)
	{
	    if (!course.ToUpper().Equals("ANY"))
		report = report.Where(c => c.Key == course).ToDictionary(c => c.Key, s => s.Value);
	    return report;
	}

	private static Dictionary<string, Dictionary<string, List<int>>> FilterStudents(
	    Dictionary<string, Dictionary<string, List<int>>> report, string student, string order)
	{
	    Dictionary<string, Dictionary<string, List<int>>> filteredReport =
			new Dictionary<string, Dictionary<string, List<int>>>();
	    if (!student.ToUpper().Equals("ALL"))
	    {
		if (!int.TryParse(student, out int studentsToTake))
		{
		    foreach (var course in report)
		    {
			if (!filteredReport.ContainsKey(course.Key))
			    filteredReport.Add(course.Key, new Dictionary<string, List<int>>());
			foreach (var studentData in course.Value)
			{
			    if (studentData.Key.Equals(student))
			    {
				if (!filteredReport[course.Key].ContainsKey(studentData.Key))
				    filteredReport[course.Key].Add(studentData.Key, new List<int>());
				filteredReport[course.Key][studentData.Key].AddRange(studentData.Value);
			    }
			}
		    }
		    report = filteredReport;
		}
		else
		{
		    foreach (var course in report)
		    {
			if (!filteredReport.ContainsKey(course.Key))
			    filteredReport.Add(course.Key, new Dictionary<string, List<int>>());
			int studentsTaken = 0;
			if (order.Equals("ASCENDING"))
			{
			    foreach (var studentData in course.Value.OrderBy(s => s.Value.Average()))
			    {
				if (studentsTaken < studentsToTake)
				{
				    if (!filteredReport[course.Key].ContainsKey(studentData.Key))
					filteredReport[course.Key].Add(studentData.Key, new List<int>());
				    filteredReport[course.Key][studentData.Key].AddRange(studentData.Value);
				    studentsTaken++;
				}
				else break;
			    }
			}
			else
			{
			    foreach (var studentData in course.Value.OrderByDescending(s => s.Value.Average()))
			    {
				if (studentsTaken < studentsToTake)
				{
				    if (!filteredReport[course.Key].ContainsKey(studentData.Key))
					filteredReport[course.Key].Add(studentData.Key, new List<int>());
				    filteredReport[course.Key][studentData.Key].AddRange(studentData.Value);
				    studentsTaken++;
				}
				else break;
			    }
			}
		    }
		    report = filteredReport;
		}
	    }
	    return report;
	}

	private static Dictionary<string, Dictionary<string, List<int>>> FilterScores(
	    Dictionary<string, Dictionary<string, List<int>>> report, string filter)
	{
	    if (!filter.Equals("OFF"))
	    {
		Dictionary<string, Dictionary<string, List<int>>> filteredReport =
		       new Dictionary<string, Dictionary<string, List<int>>>();
		double min = 2.00;
		double max = 6.01;
		if (filter.ToUpper().Equals("EXCELLENT")) min = 5.00;
		else if (filter.ToUpper().Equals("AVERAGE"))
		{
		    min = 3.50;
		    max = 5.00;
		}
		else if (filter.ToUpper().Equals("POOR")) max = 3.50;
		foreach (var course in report)
		{
		    if (!filteredReport.ContainsKey(course.Key))
			filteredReport.Add(course.Key, new Dictionary<string, List<int>>());
		    foreach (var student in course.Value)
		    {
			double averageScore = student.Value.Average() / 100;
			double studentMark = averageScore * 4 + 2;
			if (studentMark >= min && studentMark < max)
			{
			    if (!filteredReport[course.Key].ContainsKey(student.Key))
				filteredReport[course.Key].Add(student.Key, new List<int>());
			    filteredReport[course.Key][student.Key].AddRange(student.Value);
			}
		    }
		}
		report = filteredReport.Where(c => c.Value.Count != 0).ToDictionary(c => c.Key, s => s.Value);
	    }
	    return report;
	}

	private static bool IsQueryValid(string course, string student, string filter, string order)
	{
	    if (isDatabaseInitialized)
	    {
		if (database.ContainsKey(course) || course.ToUpper().Equals("ANY"))
		{
		    if (database.Values.Any(s => s.ContainsKey(student)) || student.ToUpper().Equals("ALL")
			|| int.TryParse(student, out int studentsToTake))
		    {
			string[] validFilters = { "EXCELLENT", "AVERAGE", "POOR", "OFF" };
			string[] validSorters = { "ASCENDING", "DESCENDING" };
			if (validFilters.Contains(filter) && validSorters.Contains(order)) return true;
			else IOManager.DisplayAlert(Exceptions.InvalidCommandParameter);
		    }
		    else IOManager.DisplayAlert(Exceptions.InexistantStudent);
		}
		else IOManager.DisplayAlert(Exceptions.InexistantCourse);
	    }
	    else IOManager.DisplayAlert(Exceptions.DatabaseNotInitialized);
	    return false;
	}
    }
}
