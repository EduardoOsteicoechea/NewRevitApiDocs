using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

[Transaction(TransactionMode.Manual)]
public class CreateSphereOnInternalOriginCommand : IExternalCommand
{
	public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
	{
		var workflow = new CreateSphereOnInternalOrigin(
			commandData.Application.ActiveUIDocument.Document, 
			TransactionOptions.SingleTransaction, 
			LogOptions.FullLog
			);

		workflow.Run();

		return Result.Succeeded;
	}
}