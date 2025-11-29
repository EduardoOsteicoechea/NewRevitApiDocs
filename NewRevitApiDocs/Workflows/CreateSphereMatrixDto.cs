using Autodesk.Revit.DB;

public class CreateSphereMatrixDto : DTOBase
{
	public bool PrintValues { get; set; } = false;


	[Print(nameof(DataFormatter.XYZsData))]
	public List<XYZ> Points { get; set; }


	[Print(nameof(DataFormatter.SolidsVolume))]
	public List<Solid> Solids { get; set; }


	[Print(nameof(DataFormatter.ElementIdsData))]
	public List<ElementId> MaterialsIds { get; set; }


	[Print(nameof(DataFormatter.ColorsData))]
	public List<Color> Colors { get; set; }


	[Print(nameof(DataFormatter.MaterialsData))]
	public List<Material> Materials { get; set; }


	[Print(nameof(DataFormatter.DirectShapesData))]
	public List<DirectShape> Shapes { get; set; }

	public override string ToString()
	{
		return DtoFormater.Format(this);
	}
}