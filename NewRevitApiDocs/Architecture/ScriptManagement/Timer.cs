using System.Diagnostics;

public class ExecutionTimer : IExecutionTimer
{
	public int StepCount { get; private set; } = 0;
	public long TotalMilliseconds { get; private set; }
	public long CurrentStepDuration { get; private set; }

	private long _previousTotalTime = 0;

	private readonly Stopwatch _stopwatch;

	public ExecutionTimer()
	{
		_stopwatch = Stopwatch.StartNew();
	}

	public void MarkStep()
	{
		var currentTotal = _stopwatch.ElapsedMilliseconds;

		CurrentStepDuration = currentTotal - _previousTotalTime;

		_previousTotalTime = currentTotal;

		TotalMilliseconds = currentTotal;

		StepCount++;
	}

	public void Finish()
	{
		_stopwatch.Stop();

		TotalMilliseconds = _stopwatch.ElapsedMilliseconds;
	}
}