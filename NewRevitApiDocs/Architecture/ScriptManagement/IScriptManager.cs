public interface IScriptManager
{
	ILogger Logger { get; init; }
	IExecutionTimer Timer { get; init; }
	IFilePrinter FilePrinter { get; init; }
	LogOptions LogOptions { get; init; }
	void LogAction(string stepName, string stepValue);
	void LogError(string value);
	void LogInfo(string value);
	void Finish();
}