
public interface IActionManager
{
	IDTOBase Dto { get; }
	IScriptManager ScriptManager { get; }

	void AddAction(Action action, ItemTransactionOptions transactionOptions = ItemTransactionOptions.Transactionless);
	void RunActions();
}