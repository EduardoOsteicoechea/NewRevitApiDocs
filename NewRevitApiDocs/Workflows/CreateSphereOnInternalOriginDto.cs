using Autodesk.Revit.DB;

public class CreateSphereOnInternalOriginDto: DTOBase
{
	public bool PrintValues { get; set; } = false;

	[Print(nameof(DataFormatter.XYZsData))]
	public List<XYZ> SpheresPoints { get; set; }

	[Print(nameof(DataFormatter.SolidsVolume))]
	public List<Solid> SpheresSolids { get; set; }

	[Print(nameof(DataFormatter.ElementIdsData))]
	public List<ElementId> SpheresMaterialsIds { get; set; }

	public List<Color> SpheresColors { get; set; }

	[Print(nameof(DataFormatter.MaterialsData))]
	public List<Material> SpheresMaterials { get; set; }

	[Print(nameof(DataFormatter.DirectShapesData))]
	public List<DirectShape> SpheresShapes { get; set; }

	public override string ToString()
	{
		return DtoFormater.Format(this);
	}
}