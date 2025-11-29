public class ScriptManager: IScriptManager
{
	public ILogger Logger { get; init; }
	public IExecutionTimer Timer { get; init; }
	public IFilePrinter FilePrinter { get; init; }
	public LogOptions LogOptions { get; init; }
	public bool IsLoggingEnabled { get; init; }

	public ScriptManager
	(
		ILogger logger,
		IExecutionTimer timer,
		IFilePrinter filePrinter,
		LogOptions logOptions = LogOptions.LogNone
	) 
	{
		Logger = logger;

		Timer = timer;

		FilePrinter = filePrinter;

		LogOptions = logOptions;

		IsLoggingEnabled = LogOptions.Equals(LogOptions.LogNone);
	}

	public void LogAction(string stepName, string stepValue)
	{
		Timer.MarkStep();

		if (IsLoggingEnabled)
		{
			var actionNumber = Timer.StepCount;

			var currentStepTiming = Timer.CurrentStepDuration;

			Logger.Action(stepName, actionNumber, currentStepTiming, stepValue);
		}
	}

	public void LogError(string value)
	{
		if (IsLoggingEnabled)
		{
			Logger.Error(value);
		}
	}

	public void LogInfo(string value)
	{
		if (IsLoggingEnabled)
		{
			Logger.Info(value);
		}
	}

	public void Finish()
	{
		Timer.Finish();

		if (IsLoggingEnabled)
		{
			var totalTime = Timer.TotalMilliseconds;

			Logger.Finish(totalTime);

			var logData = Logger.Print();

			FilePrinter.Print(logData);
		}
	}
}