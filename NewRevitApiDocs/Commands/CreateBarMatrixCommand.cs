using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

[Transaction(TransactionMode.Manual)]
public class CreateBarMatrixCommand : IExternalCommand
{
	public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
	{
		var workflow = new CreateBarMatrix(
			commandData.Application.ActiveUIDocument.Document, 
			TransactionOptions.SingleTransaction, 
			LogOptions.FullLog,
			LogFlowOptions.LogFinalOnly
			);

		workflow.Run();

		return Result.Succeeded;
	}
}