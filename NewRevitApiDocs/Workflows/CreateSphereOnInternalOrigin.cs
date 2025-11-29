using Autodesk.Revit.DB;

public class CreateSphereOnInternalOrigin : WorkflowBase<CreateSphereOnInternalOriginDto>
{
	public CreateSphereOnInternalOrigin(Document doc, TransactionOptions transactionOptions = TransactionOptions.SingleTransaction, LogOptions logOptions = LogOptions.DoNotLog) : base(doc, transactionOptions, logOptions)
	{
		Add(CreatePoints);
		Add(CreateSphere);
		Add(CreateMaterialForEachPoint);
		Add(CollectCreatedMaterialsForStyling);
		Add(CreateColorForEachPoint);
		Add(RandomlyStyleMaterials);
		Add(CreateDirectShapes);
		Add(PaintDirectShapes);
	}

	void CreatePoints()
	{
		Dto.SpheresPoints = RevitXYZ.FromXYDisplacementMatrix(10, 20);
	}

	void CreateSphere()
	{
		var result = new List<Solid>();

		foreach (XYZ item in Dto.SpheresPoints)
		{
			result.Add(RevitSolid.SphereFromXYZAndRadius(Sm, Doc, item, 5));
		}

		Dto.SpheresSolids = result;
	}

	void CreateMaterialForEachPoint()
	{
		var result = new List<ElementId>();

		foreach (XYZ item in Dto.SpheresPoints)
		{
			result.Add(RevitMaterial.DefaultOnNewTransaction(Sm, Doc, Guid.NewGuid().ToString(), 0));
		}

		Dto.SpheresMaterialsIds = result;
	}

	void CollectCreatedMaterialsForStyling()
	{
		var result = new List<Material>();

		foreach (ElementId item in Dto.SpheresMaterialsIds)
		{
			result.Add(Doc.GetElement(item) as Material);
		}

		Dto.SpheresMaterials = result;
	}

	void CreateColorForEachPoint()
	{
		var result = new List<Color>();

		foreach (XYZ item in Dto.SpheresPoints)
		{
			result.Add(RevitColor.RandomScopedRGB(0, 256));
		}

		Dto.SpheresColors = result;
	}

	void RandomlyStyleMaterials()
	{
		var result = new List<Material>();

		for (int i = 0; i < Dto.SpheresMaterialsIds.Count; i++)
		{
			ElementId item = Dto.SpheresMaterialsIds[i];
			Material material = Dto.SpheresMaterials[i];
			Color color = Dto.SpheresColors[i];

			result.Add(RevitMaterial.SetColorAndTransparencyOnNewTransaction(Sm, Doc, item, color, 0));
		}
	}

	void CreateDirectShapes()
	{
		var result = new List<DirectShape>();

		foreach (Solid item in Dto.SpheresSolids)
		{
			result.Add(RevitDirectShape.SphereFromSphereSolidOnNewTransaction(Sm, Doc, item));
		}

		Dto.SpheresShapes = result;
	}

	void PaintDirectShapes()
	{
		for (int i = 0; i < Dto.SpheresShapes.Count; i++)
		{
			DirectShape item = Dto.SpheresShapes[i];

			Material material = Dto.SpheresMaterials[i];

			RevitDirectShape.PaintShapeFaces(Sm, Doc, item, material);
		}
	}
}