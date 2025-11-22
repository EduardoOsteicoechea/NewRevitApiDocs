using System.Diagnostics;
using System.Globalization;
using System.Text;

public interface IScriptManager : IDisposable
{
	public StringBuilder Logger { get; }
	HelperShapesOptions HelperShapesOptions { get; }
	double PreviousTime { get; set; }
	double CurrentTime { get; set; }
	Dictionary<string, double> StepsTiming { get; set; }
	Stopwatch Stopwatch { get; set; }
	void SetSuccess(string stepName, dynamic stepValue = null);
	void SetError(string stepName, dynamic stepValue = null);
	void SetWarning(string stepName, dynamic stepValue = null);
	string SetTimming(string stepName);
	string FormatMessage(string stepName, dynamic stepValue, string stepTiming, ScriptManagerMessageFormattingOptions scriptManagerMessageFormattingOptions);
	string DetermineMessageTypeString(ScriptManagerMessageFormattingOptions scriptManagerMessageFormattingOptions);
	void Finish();
	void Dispose();
}
