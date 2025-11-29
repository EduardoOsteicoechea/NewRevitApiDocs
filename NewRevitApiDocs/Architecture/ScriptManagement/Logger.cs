using System.Globalization;
using System.Text;

public class Logger : ILogger
{
	private StringBuilder? _stringBuilder;

	private readonly bool _isLoggingEnabled;

	public Logger(LogOptions logOptions)
	{
		_isLoggingEnabled = !logOptions.Equals(LogOptions.LogNone);

		if (_isLoggingEnabled)
		{
			_stringBuilder = new StringBuilder();
		}
	}

	public void Action(string stepName, double stepNumber, double stepTiming, string stepValue)
	{
		if (_isLoggingEnabled)
		{
			_stringBuilder?.AppendLine($"[ACTION]");
			_stringBuilder?.AppendLine(
				string.Format(
					CultureInfo.InvariantCulture,
					"N°: {0} | MS: {1:F2} | NAME: {2} | VALUE: {3}",
					stepNumber, 
					stepTiming, 
					stepName, 
					stepValue
					)
				);
			_stringBuilder?.AppendLine();
		}
	}
	public void Error(string value)
	{
		if (_isLoggingEnabled)
		{
			_stringBuilder?.AppendLine($"[ERROR]");
			_stringBuilder?.AppendLine($"{value}");
			_stringBuilder?.AppendLine();
		}
	}
	public void Info(string value)
	{
		if (_isLoggingEnabled)
		{
			_stringBuilder?.AppendLine($"[INFO]");
			_stringBuilder?.AppendLine($"{value}");
		}
	}
	public void Finish(double time)
	{
		if (_isLoggingEnabled)
		{
			_stringBuilder?.AppendLine();
			_stringBuilder?.AppendLine($"[END]");
			_stringBuilder?.AppendLine($"TOTAL_MS: {time.ToString("F2", CultureInfo.InvariantCulture)}");
		}
	}

	public string Print()
	{
		return _stringBuilder?.ToString() ?? string.Empty;
	}
}