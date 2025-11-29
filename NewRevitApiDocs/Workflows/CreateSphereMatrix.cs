using Autodesk.Revit.DB;

public class CreateSphereMatrix : WorkflowBase<CreateSphereMatrixDto>
{
	public CreateSphereMatrix(Document doc, TransactionOptions transactionOptions = TransactionOptions.SingleTransaction, LogOptions logOptions = LogOptions.DoNotLog, LogFlowOptions logFlowOptions = LogFlowOptions.LogAll) : base(doc, transactionOptions, logOptions, logFlowOptions)
	{
		Add(CreatePoints);
		Add(CreateSolids);
		Add(CreateMaterialForEachPoint, ItemTransactionOptions.RequiresTransaction);
		Add(CollectCreatedMaterialsForStyling);
		Add(CreateColorForEachPoint);
		Add(RandomlyStyleMaterials, ItemTransactionOptions.RequiresTransaction);
		Add(CreateDirectShapes, ItemTransactionOptions.RequiresTransaction);
		Add(PaintDirectShapes, ItemTransactionOptions.RequiresTransaction);
	}

	void CreatePoints()
	{
		Dto.Points = RevitXYZ.FromXYDisplacementMatrix(10, 20);
	}

	void CreateSolids()
	{
		var result = new List<Solid>();

		foreach (XYZ item in Dto.Points)
		{
			result.Add(RevitSolid.SphereFromXYZAndRadius(Sm, Doc, item, 5));
		}

		Dto.Solids = result;
	}

	void CreateMaterialForEachPoint()
	{
		var result = new List<ElementId>();

		foreach (XYZ item in Dto.Points)
		{
			result.Add(RevitMaterial.DefaultOnNewTransaction(Sm, Doc, Guid.NewGuid().ToString(), 0));
		}

		Dto.MaterialsIds = result;
	}

	void CollectCreatedMaterialsForStyling()
	{
		var result = new List<Material>();

		foreach (ElementId item in Dto.MaterialsIds)
		{
			result.Add(Doc.GetElement(item) as Material);
		}

		Dto.Materials = result;
	}

	void CreateColorForEachPoint()
	{
		var result = new List<Color>();

		foreach (XYZ item in Dto.Points)
		{
			result.Add(RevitColor.RandomScopedRGB(0, 256));
		}

		Dto.Colors = result;
	}

	void RandomlyStyleMaterials()
	{
		var result = new List<Material>();

		for (int i = 0; i < Dto.MaterialsIds.Count; i++)
		{
			ElementId item = Dto.MaterialsIds[i];

			Material material = Dto.Materials[i];

			Color color = Dto.Colors[i];

			result.Add(RevitMaterial.SetColorAndTransparencyOnNewTransaction(Sm, Doc, item, color, 0));
		}
	}

	void CreateDirectShapes()
	{
		var result = new List<DirectShape>();

		foreach (Solid item in Dto.Solids)
		{
			result.Add(RevitDirectShape.SphereFromSphereSolidOnNewTransaction(Sm, Doc, item));
		}

		Dto.Shapes = result;
	}

	void PaintDirectShapes()
	{
		for (int i = 0; i < Dto.Shapes.Count; i++)
		{
			DirectShape item = Dto.Shapes[i];

			Material material = Dto.Materials[i];

			RevitDirectShape.PaintShapeFacesOnNewTransaction(Sm, Doc, item, material);
		}
	}
}