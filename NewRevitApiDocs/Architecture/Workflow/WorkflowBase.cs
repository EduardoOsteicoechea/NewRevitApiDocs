using Autodesk.Revit.DB;

public class WorkflowBase<DTOBase> where DTOBase : new()
{
	public DTOBase Dto { get; set; }
	public LogOptions LogOptions { get; set; }
	public LogFlowOptions LogFlowOptions { get; set; }
	public TransactionOptions TransactionOptions { get; set; }
	public ScriptManager Sm { get; set; }
	public Document Doc { get; set; }
	public Dictionary<Action, ItemTransactionOptions> Actions { get; set; } = new Dictionary<Action, ItemTransactionOptions>();

	public WorkflowBase(Document doc, TransactionOptions transactionOptions = TransactionOptions.SingleTransaction, LogOptions logOptions = LogOptions.DoNotLog, LogFlowOptions logFlowOptions = LogFlowOptions.LogAll)
	{
		Dto = new DTOBase();
		Doc = doc;
		TransactionOptions = transactionOptions;
		LogOptions = logOptions;
		LogFlowOptions = logFlowOptions;
		Sm = new ScriptManager();
		LogFlowOptions = logFlowOptions;
	}

	public void Add(Action action, ItemTransactionOptions transactionOptions = ItemTransactionOptions.Transactionless)
	{
		Actions.Add(action, ItemTransactionOptions.Transactionless);
	}

	public void Run()
	{
		if (TransactionOptions.Equals(TransactionOptions.Transactionless))
		{
			RunTransactionless();
		}
		else if (TransactionOptions.Equals(TransactionOptions.SingleTransaction))
		{
			RunSingleTransaction();
		}
		else
		{
			RunMultipleTransactions();
		}
	}

	public void RunTransactionless()
	{
		try
		{
			RunActions();
		}
		catch (Exception ex)
		{
			Sm.Set($"{ex.Message}\n{ex.StackTrace}");
		}
		finally
		{
			Finish();
		}
	}

	public void RunSingleTransaction()
	{
		using (var transaction = new Transaction(Doc, $"{nameof(Doc)}"))
		{
			using (Sm)
			{
				try
				{
					transaction.Start();

					RunActions();

					transaction.Commit();
				}
				catch (Exception ex)
				{
					if (transaction.GetStatus() == TransactionStatus.Started)
					{
						transaction.RollBack();
					}

					Sm.Set($"{ex.Message}\n{ex.StackTrace}");
				}
				finally
				{
					Finish();
				}
			}
		}
	}

	public void RunMultipleTransactions()
	{
		using (var transactionGruop = new TransactionGroup(Doc, $"{nameof(Doc)}"))
		{
			using (Sm)
			{
				try
				{
					transactionGruop.Start();

					RunActions();

					transactionGruop.Assimilate();
				}
				catch (Exception ex)
				{
					if (transactionGruop.GetStatus() == TransactionStatus.Started)
					{
						transactionGruop.RollBack();
					}

					Sm.Set($"{ex.Message}\n{ex.StackTrace}");
				}
				finally
				{
					Finish();
				}
			}
		}
	}

	public void RunActions()
	{
		for (int i = 0; i < Actions.Count; i++)
		{
			KeyValuePair<Action, ItemTransactionOptions> item = Actions.ElementAt(i);

			if (i.Equals(Actions.Count - 1))
			{
				ActionManager(item.Key.Method.Name, () =>
				{
					RunAction(item);
				}, true);
			}
			else
			{
				ActionManager(item.Key.Method.Name, () =>
				{
					RunAction(item);
				}, false);
			}
		}
	}

	public void RunAction(KeyValuePair<Action, ItemTransactionOptions> item)
	{
		if (item.Value.Equals(ItemTransactionOptions.Transactionless))
		{
			item.Key();
		}
		else if (item.Value.Equals(ItemTransactionOptions.RequiresTransaction))
		{
			using (var transaction = new Transaction(Doc, $"{item.Key.Method.Name}"))
			{
				try
				{
					transaction.Start();

					item.Key();

					transaction.Commit();
				}
				catch (Exception ex)
				{
					transaction.RollBack();

					Sm.Set($"{ex.Message}\n{ex.StackTrace}");
				}
				finally
				{
					Finish();
				}
			}
		}
	}

	public void Finish()
	{
		Sm.Finish();

		if (
			LogOptions.Equals(LogOptions.Log)
				||
			LogOptions.Equals(LogOptions.LogNamesOnly)
				||
			LogOptions.Equals(LogOptions.FullLog)
		)
		{
			LogService.Print(this.GetType().Name, Doc, Sm.Logger);
		}
	}

	public virtual void ActionManager(string methodName, Action action, bool isFinal = false)
	{
		string logMessage = "";

		try
		{
			action();

			if (LogFlowOptions.Equals(LogFlowOptions.LogFinalOnly))
			{
				if (isFinal)
				{
					logMessage += Dto.ToString();
				}
			}
			else
			{
				logMessage += Dto.ToString();
			}
		}
		catch (Exception ex)
		{
			logMessage += $"ERROR: {ex.Message}: {ex.StackTrace}";
		}
		finally
		{
			Sm.Set(methodName, logMessage);
		}
	}
}