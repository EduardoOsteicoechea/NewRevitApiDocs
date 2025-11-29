public interface ILogger
{
	void Action(string stepName, double stepNumber, double stepTiming, string stepValue);
	void Error(string value);
	void Finish(double time);
	void Info(string value);
	string Print();
}