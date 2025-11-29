public interface IFilePrinter
{
	string DirectoryPath { get; init; }
	string FileName { get; init; }
	void Print(string content);
}