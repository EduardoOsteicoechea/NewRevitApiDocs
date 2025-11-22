using Autodesk.Revit.DB;
using System.Text;

public class CreateSphereOnInternalOriginDto
{
	public bool PrintValues { get; set; } = false;
	public List<XYZ> SpheresPoints { get; set; }
	public List<Solid> SpheresSolids { get; set; }
	public List<ElementId> SpheresMaterialsIds { get; set; }
	public List<Color> SpheresColors { get; set; }
	public List<Material> SpheresMaterials { get; set; }
	public List<DirectShape> SpheresShapes { get; set; }

	public override string ToString()
	{
		var printer = new StringBuilder();

		printer.AppendLine(PrintProperty(nameof(SpheresPoints), SpheresPoints, DataFormatter.PrintXYZsData));
		printer.AppendLine(PrintProperty(nameof(SpheresSolids), SpheresSolids, DataFormatter.PrintSolidsVolume));
		printer.AppendLine(PrintProperty(nameof(SpheresMaterialsIds), SpheresMaterialsIds, DataFormatter.PrintElementIdsData));
		printer.AppendLine(PrintProperty(nameof(SpheresMaterials), SpheresMaterials, DataFormatter.PrintMaterialsData));
		printer.AppendLine(PrintProperty(nameof(SpheresShapes), SpheresShapes, DataFormatter.PrintDirectShapesData));

		return printer.ToString();
	}

	private string PrintProperty<T>(string name, T value, Func<T, string> method)
	{
		if (PrintValues)
		{
			var printerMethodResult = method(value);

			return $"{LogService.Tab1}{name}\n{printerMethodResult}";
		}
		else
		{
			return $"{LogService.Tab1}{name}";
		}
	}
}