using System.Diagnostics;
using System.Globalization;
using System.Text;

public class ScriptManager : IDisposable
{
	public StringBuilder Logger { get; } = LogService.Logger();
	public HelperShapesOptions HelperShapesOptions { get; } = HelperShapesOptions.ModelInRevit;
	public double PreviousTime { get; set; } = 0;
	public double CurrentTime { get; set; } = 0;
	public Dictionary<string, double> StepsTiming { get; set; } = new Dictionary<string, double>();
	public Stopwatch Stopwatch { get; set; } = Stopwatch.StartNew();

	public void Set(string stepName, dynamic stepValue = null)
	{
		var stepTimmingAsString = SetStepTiming(stepName);

		var valueString = stepValue is null ? "" : $" |\n{stepValue.ToString()}";

		Logger?.AppendLine($"ACTION | N°: {StepsTiming.Count} | MS: {stepTimmingAsString} | NAME: {stepName}{valueString}");
		Logger?.AppendLine();
	}

	public void SetError(string value)
	{
		Logger?.AppendLine($"ERROR: {value}");
		Logger?.AppendLine();
	}

	private string SetStepTiming(string stepName)
	{
		CurrentTime = Stopwatch.ElapsedMilliseconds;

		var stepTimming = CurrentTime - PreviousTime;

		StepsTiming.Add($"{stepName}{Guid.NewGuid()}", stepTimming);

		PreviousTime = CurrentTime;

		return Math.Round(stepTimming, 3).ToString(CultureInfo.InvariantCulture).PadLeft(8, '0');
	}

	public void Log(string value)
	{
		Logger?.AppendLine($"[NOTE]");
		Logger?.AppendLine($"{value}");
	}

	public void Finish()
	{
		Logger?.AppendLine();
		Logger?.AppendLine($"TOTAL_MS: {Stopwatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture)}");
	}

	public void Dispose()
	{
		Stopwatch.Stop();
	}
}
