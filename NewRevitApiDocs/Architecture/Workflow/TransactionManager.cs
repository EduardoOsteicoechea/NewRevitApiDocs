using Autodesk.Revit.DB;

public class TransactionManager : ITransactionManager
{
	private readonly Document _doc;
	private readonly IScriptManager _scriptManager;
	private readonly IActionManager _actionManager;
	private readonly TransactionOptions _transactionOptions;
	public TransactionManager
	(
		Document doc,
		IScriptManager scriptManager,
		IActionManager actionManager,
		TransactionOptions transactionOptions = TransactionOptions.SingleTransaction
	)
	{
		_doc = doc;
		_scriptManager = scriptManager;
		_actionManager = actionManager;
		_transactionOptions = transactionOptions;
	}

	public void Run()
	{
		switch (_transactionOptions)
		{
			case TransactionOptions.MultipleTransactions:
				MultipleTransactionsWorkflow();
				break;
			case TransactionOptions.SingleTransaction:
				SingleTransactionWorkflow();
				break;
			default:
				TransactionlessWorkflow();
				break;
		}
	}

	private void TransactionlessWorkflow()
	{
		try
		{
			_actionManager.RunActions();
		}
		catch (Exception ex)
		{
			_scriptManager.LogError($"{ex.Message}\n{ex.StackTrace}");
		}
	}

	private void SingleTransactionWorkflow()
	{
		using (var transaction = new Transaction(_doc, $"{nameof(_doc)}"))
		{
			try
			{
				transaction.Start();

				_actionManager.RunActions();

				transaction.Commit();
			}
			catch (Exception ex)
			{
				if (transaction.GetStatus() == TransactionStatus.Started)
				{
					transaction.RollBack();
				}

				_scriptManager.LogError($"{ex.Message}\n{ex.StackTrace}");
			}
		}
	}

	private void MultipleTransactionsWorkflow()
	{
		using (var transactionGruop = new TransactionGroup(_doc, $"{nameof(_doc)}"))
		{
			try
			{
				transactionGruop.Start();

				_actionManager.RunActions();

				transactionGruop.Assimilate();
			}
			catch (Exception ex)
			{
				if (transactionGruop.GetStatus() == TransactionStatus.Started)
				{
					transactionGruop.RollBack();
				}

				_scriptManager.LogError($"{ex.Message}\n{ex.StackTrace}");
			}
		}
	}
}
