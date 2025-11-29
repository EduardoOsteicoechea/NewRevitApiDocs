public class FilePrinter : IFilePrinter
{
	public string DirectoryPath { get; init; }
	public string FileName { get; init; }

	public FilePrinter
	(
		string directoryPath,
		string fileName
	)
	{
		DirectoryPath = directoryPath;

		FileName = fileName;
	}

	public void Print(string content)
	{
		try
		{
			if (!Directory.Exists(DirectoryPath))
			{
				Directory.CreateDirectory(DirectoryPath);
			}

			File.WriteAllText(
				Path.Combine(DirectoryPath, FileName),
				content
			);
		}
		catch (Exception ex)
		{
			System.Diagnostics.Debug.WriteLine($"Failed to write log file: {ex.Message}");
		}
	}
}