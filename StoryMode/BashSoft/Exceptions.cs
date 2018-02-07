namespace BashSoft
{
    public static class Exceptions
    {
	public const string InevitableMismatch =
	    "Compared files are of different sizes. Mismatch inevitable!";
	public const string DatabaseAlreadyInitialized = "Database has already been initialized!";
	public const string DatabaseNotInitialized =
	    "The data structure must be initialized before performing any operations with it.";
	public const string FileAlreadyDownloaded =
	    "The requested file already exists in the current folder. Overwrite? (Y/N) ";
	public const string FileNotSpecified = "The provided path does not point to a file!";
	public const string IncompletePath =
	    "Please provide a correct path to the desired resource!";
	public const string InexistantCourse =
	    "There is no information for a course with that name in the database.";
	public const string InexistantStudent =
	    "There are no records for a student with that username in the database";
	public const string InvalidCommand = " is not a valid command";
	public const string InvalidCommandParameter =
	    "Some of the command parameters are not in the expected format!";
	public const string InvalidFilter = "The selected filter could not be recognized!";
	public const string InvalidName =
	    "Folder/file names cannot contain any of the following symbols: \\/:*?\"<>|";
	public const string InvalidPath =
	    "The folder/file you are trying to access does not exist at the specified address.";
	public const string InvalidSorter = "The selected ordering method could not be recognized!";
	public const string MissingCommandParameter =
	    "A required command parameter is missing!";
	public const string RedundantCommandParameters =
	    "This command does not work with that many parameters!";
	public const string UnauthorizedAccess =
	    "You do not have sufficient privileges to work with that folder/file!";
    }
}
