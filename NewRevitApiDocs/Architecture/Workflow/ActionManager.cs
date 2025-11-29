public class ActionManager : IActionManager
{
	public IDTOBase Dto { get; private set; }
	public IScriptManager ScriptManager { get; private set; }
	private List<(Action Method, ItemTransactionOptions Options)> _actions { get; }
		= new List<(Action, ItemTransactionOptions)>();

	public ActionManager(IScriptManager scriptManager, IDTOBase dto)
	{
		ScriptManager = scriptManager;
		Dto = dto;
	}

	public void AddAction(Action action, ItemTransactionOptions transactionOptions = ItemTransactionOptions.Transactionless)
	{
		_actions.Add((action, transactionOptions));
	}

	private bool ShouldLog(bool isFinal)
	{
		if (ScriptManager.LogOptions == LogOptions.LogFinalOnly)
		{
			return isFinal;
		}

		return true;
	}

	public void RunActions()
	{
		for (int i = 0; i < _actions.Count; i++)
		{
			var (action, options) = _actions[i];

			bool isFinal = i.Equals(_actions.Count - 1);

			ExecuteStep(action, isFinal);
		}
	}

	protected virtual void ExecuteStep(Action action, bool isFinal)
	{
		var methodName = action.Method.Name;

		try
		{
			action();

			if (ShouldLog(isFinal))
			{
				ScriptManager.LogAction(methodName, Dto?.ToString() ?? "null");
			}
		}
		catch (Exception ex)
		{
			ScriptManager.LogError($"{methodName}: {ex.Message}");
		}
	}
}