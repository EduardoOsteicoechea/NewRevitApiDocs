using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NewRevitApiDocs;
using System.Reflection;

[Transaction(TransactionMode.Manual)]
public class CreateSphereOnInternalOrigin : RevitApiWorkflow<CreateSphereOnInternalOriginDto>
{
	public override void Workflow()
	{
		CreatePoints();
		CreateSphere();
		CreateMaterialForEachPoint();
		CollectCreatedMaterialsForStyling();
		CreateColorForEachPoint();
		RandomlyStyleMaterials();
		CreateDirectShapes();
		PaintDirectShapes();
	}

	void CreatePoints()
	{
		Run(MethodBase.GetCurrentMethod().Name, () =>
		{
			Dto.SpheresPoints = RevitXYZ.FromXYDisplacementMatrix(30, 20);
		});
	}

	void CreateSphere() 
	{
		Run(MethodBase.GetCurrentMethod().Name, () => 
		{
			var result = new List<Solid>();

			foreach (XYZ item in Dto.SpheresPoints)
			{
				result.Add(RevitSolid.SphereFromXYZAndRadius(Sm, Doc, item, 5));
			}

			Dto.SpheresSolids = result;
		});
	}

	void CreateMaterialForEachPoint()
	{
		Run(MethodBase.GetCurrentMethod().Name, () =>
		{
			var result = new List<ElementId>();

			foreach (XYZ item in Dto.SpheresPoints)
			{
				result.Add(RevitMaterial.DefaultOnNewTransaction(Sm, Doc, Guid.NewGuid().ToString(), 0));
			}

			Dto.SpheresMaterialsIds = result;
		});
	}

	void CollectCreatedMaterialsForStyling()
	{
		Run(MethodBase.GetCurrentMethod().Name, () =>
		{
			var result = new List<Material>();

			foreach (ElementId item in Dto.SpheresMaterialsIds)
			{
				result.Add(Doc.GetElement(item) as Material);
			}

			Dto.SpheresMaterials = result;
		});
	}

	void CreateColorForEachPoint()
	{
		Run(MethodBase.GetCurrentMethod().Name, () =>
		{
			var result = new List<Autodesk.Revit.DB.Color>();

			foreach (XYZ item in Dto.SpheresPoints)
			{
				result.Add(RevitColor.RandomScopedRGB(0, 256));
			}

			Dto.SpheresColors = result;
		});
	}

	void RandomlyStyleMaterials()
	{
		Run(MethodBase.GetCurrentMethod().Name, () =>
		{
			var result = new List<Material>();

			for (int i = 0; i < Dto.SpheresMaterialsIds.Count; i++)
			{
				ElementId item = Dto.SpheresMaterialsIds[i];
				Material material = Dto.SpheresMaterials[i];
				Autodesk.Revit.DB.Color color = Dto.SpheresColors[i]; 

				result.Add(RevitMaterial.SetColorAndTransparencyOnNewTransaction(Sm, Doc, item, color, 0));
			}
		});
	}

	void CreateDirectShapes()
	{
		Run(MethodBase.GetCurrentMethod().Name, () =>
		{
			var result = new List<DirectShape>();

			foreach (Solid item in Dto.SpheresSolids)
			{
				result.Add(RevitDirectShape.SphereFromSphereSolidOnNewTransaction(Sm, Doc, item));
			}

			Dto.SpheresShapes = result;
		});
	}

	void PaintDirectShapes()
	{
		Run(MethodBase.GetCurrentMethod().Name, () =>
		{
			for (int i = 0; i < Dto.SpheresShapes.Count; i++)
			{
				DirectShape item = Dto.SpheresShapes[i];

				Material material = Dto.SpheresMaterials[i];

				RevitDirectShape.PaintShapeFaces(Sm, Doc, item, material);
			}
		});
	}
}
