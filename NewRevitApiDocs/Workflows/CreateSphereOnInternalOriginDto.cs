using Autodesk.Revit.DB;

public class CreateSphereOnInternalOriginDto: DTOBase
{
	public bool PrintValues { get; set; } = false;

	[Print(nameof(DataFormatter.PrintXYZsData))]
	public List<XYZ> SpheresPoints { get; set; }

	[Print(nameof(DataFormatter.PrintSolidsVolume))]
	public List<Solid> SpheresSolids { get; set; }

	[Print(nameof(DataFormatter.PrintElementIdsData))]
	public List<ElementId> SpheresMaterialsIds { get; set; }

	public List<Color> SpheresColors { get; set; }

	[Print(nameof(DataFormatter.PrintMaterialsData))]
	public List<Material> SpheresMaterials { get; set; }

	[Print(nameof(DataFormatter.PrintDirectShapesData))]
	public List<DirectShape> SpheresShapes { get; set; }
}