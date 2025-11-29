using Autodesk.Revit.DB;

public class WorkflowBase<DTOBase> where DTOBase : new ()
{
	protected Document Doc { get; set; }
	protected DTOBase Dto { get; set; }
	protected ITransactionManager Tm { get; set; }
	protected IActionManager Am { get; set; }
	protected IScriptManager Sm { get; set; }

	public WorkflowBase
	(
		Document doc,
		IDTOBase dto,
		IScriptManager scriptManager,
		IActionManager actionManager,
		ITransactionManager transactionManager
	)
	{
		Doc = doc;

		Dto = (DTOBase)dto;

		Sm = scriptManager;

		Am = actionManager;

		Tm = transactionManager;
	}

	public void Add
	(
		Action action,
		ItemTransactionOptions transactionOptions = ItemTransactionOptions.Transactionless
	)
	{
		Am.AddAction(action, transactionOptions);
	}

	public void Run()
	{
		Tm.Run();
	}
}