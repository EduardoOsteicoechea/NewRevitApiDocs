using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

public class RevitApiWorkflow<T> : IExternalCommand where T : new()
{
	public Document Doc { get; set; }
	public IScriptManager Sm { get; set; }
	public T Dto { get; set; }

	public RevitApiWorkflow()
	{
		Dto = new T();
		Sm = new ScriptManager();
	}

	public virtual void Workflow() { }

	public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
	{
		Doc = commandData.Application.ActiveUIDocument.Document;

		using (TransactionGroup transactionGroup = new TransactionGroup(Doc))
		{
			using (Sm)
			{
				try
				{
					transactionGroup.Start();

					Workflow();

					transactionGroup.Commit();

					return Result.Succeeded;
				}
				catch (Exception ex)
				{
					transactionGroup.RollBack();

					Sm.SetError($"{ex.Message}\n{ex.StackTrace}");

					return Result.Failed;
				}
				finally
				{
					Sm.Finish();

					LogService.Print(this.GetType().Name, Doc, Sm.Logger);
				}
			}
		}
	}

	public void Run(string methodName, Action action, string logMessage = "")
	{
		try
		{
			action();

			logMessage = Dto.ToString() ?? "null";

			Sm.SetSuccess(methodName, logMessage);
		}
		catch (Exception ex)
		{
			logMessage += $"{ex.Message}: {ex.StackTrace}"; 
			
			Sm.SetError(methodName, logMessage);
		}
	}









}
