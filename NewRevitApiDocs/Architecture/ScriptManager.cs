using System.Diagnostics;
using System.Globalization;
using System.Text;

public class ScriptManager : IScriptManager
{
	public StringBuilder Logger { get; } = LogService.Logger();
	public HelperShapesOptions HelperShapesOptions { get; } = HelperShapesOptions.ModelInRevit;
	public double PreviousTime { get; set; } = 0;
	public double CurrentTime { get; set; } = 0;
	public Dictionary<string, double> StepsTiming { get; set; } = new Dictionary<string, double>();
	public Stopwatch Stopwatch { get; set; } = Stopwatch.StartNew();

	public void SetSuccess(string stepName, dynamic stepValue = null)
	{
		Logger?.AppendLine(FormatMessage(stepName, stepValue, SetTimming(stepName), ScriptManagerMessageFormattingOptions.Success));
	}

	public void SetWarning(string stepName, dynamic stepValue = null)
	{
		Logger?.AppendLine(FormatMessage(stepName, stepValue, SetTimming(stepName), ScriptManagerMessageFormattingOptions.Warning));
	}

	public void SetError(string stepName, dynamic stepValue = null)
	{
		Logger?.AppendLine(FormatMessage(stepName, stepValue, SetTimming(stepName), ScriptManagerMessageFormattingOptions.Error));
	}

	public string SetTimming(string stepName)
	{
		CurrentTime = Stopwatch.ElapsedMilliseconds;

		double stepTimming = CurrentTime - PreviousTime;

		StepsTiming.Add($"{stepName}_{Guid.NewGuid()}", stepTimming);

		PreviousTime = CurrentTime;

		return Math.Round(stepTimming, 3).ToString(CultureInfo.InvariantCulture).PadLeft(8, '0');
	}

	public string FormatMessage(string stepName, dynamic stepValue, string timingValue, ScriptManagerMessageFormattingOptions scriptManagerMessageFormattingOptions)
	{
		var a = new StringBuilder();

		var valueString = stepValue is null ? "" : $" |\n{stepValue.ToString()}";

		a.AppendLine($"[{DetermineMessageTypeString(scriptManagerMessageFormattingOptions)}]");
		a.AppendLine("{");
		a.AppendLine($"{LogService.Tab1}N°: {StepsTiming.Count} | MS: {timingValue} | NAME: {stepName}{valueString}");
		a.AppendLine("}");

		return a.ToString();
	}

	public string DetermineMessageTypeString(ScriptManagerMessageFormattingOptions scriptManagerMessageFormattingOptions)
	{
		if (scriptManagerMessageFormattingOptions.Equals(ScriptManagerMessageFormattingOptions.Success))
		{
			return "SUCCESS";
		}
		else if (scriptManagerMessageFormattingOptions.Equals(ScriptManagerMessageFormattingOptions.Warning))
		{
			return "WARNING";
		}
		else
		{
			return "ERROR";
		}
	}

	public void Finish()
	{
		Logger?.AppendLine($"TOTAL_MS: {Stopwatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture)}");
	}

	public void Dispose()
	{
		Stopwatch.Stop();
	}
}
