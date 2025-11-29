using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Reflection;

[Transaction(TransactionMode.Manual)]
public class CreateBarMatrixCommand : IExternalCommand
{
	public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
	{
		var doc = commandData.Application.ActiveUIDocument.Document;

		var logger = new Logger(LogOptions.LogFinalOnly);

		var timer = new ExecutionTimer();

		var filePrinter = new FilePrinter(
			Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
			$"{doc.Title}_{MethodBase.GetCurrentMethod().DeclaringType.Name}.txt"
			);

		var scriptManager = new ScriptManager(logger, timer, filePrinter);

		var sharedDto = new CreateBarMatrixDto();

		var actionManager = new ActionManager(
			scriptManager,
			sharedDto
			);

		var transactionManager = new TransactionManager(
			doc,
			scriptManager,
			actionManager,
			TransactionOptions.MultipleTransactions
			);

		var workflow = new CreateBarMatrix(
			doc,
			sharedDto,
			scriptManager,
			actionManager,
			transactionManager
			);

		try
		{
			workflow.Run();

			return Result.Succeeded;
		}
		catch (Exception ex)
		{
			scriptManager.LogError($"{ex.Message}\n{ex.StackTrace}");

			return Result.Failed;
		}
		finally
		{
			scriptManager.Finish();
		}
	}
}