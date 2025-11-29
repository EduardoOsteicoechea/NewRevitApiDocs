public interface IExecutionTimer
{
	long CurrentStepDuration { get; }
	int StepCount { get; }
	long TotalMilliseconds { get; }
	void Finish();
	void MarkStep();
}