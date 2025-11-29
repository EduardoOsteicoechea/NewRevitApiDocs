using Autodesk.Revit.DB;
using System.Reflection;

public static class RevitDirectShape
{
	public static DirectShape SphereFromSphereSolidOnNewTransaction
	(
		IScriptManager scriptManager,
		Document doc,
		Solid solid
	)
	{
		DirectShape directShape = null;

		using (Transaction transaction = new Transaction(doc, MethodBase.GetCurrentMethod().Name))
		{
			transaction.Start();

			ElementId genericModelsCategoryId = RevitCategory.GetGenericModelCategoryId(scriptManager, doc);

			directShape = SphereFromSphereSolid(scriptManager, doc, genericModelsCategoryId, solid);

			transaction.Commit();
		}

		return directShape;
	}

	public static DirectShape SphereFromSphereSolidOnExistingTransaction
	(
		IScriptManager scriptManager,
		Document doc,
		Solid solid
	)
	{
		ElementId genericModelsCategoryId = RevitCategory.GetGenericModelCategoryId(scriptManager, doc);

		return SphereFromSphereSolid(scriptManager, doc, genericModelsCategoryId, solid);
	}

	public static DirectShape SphereFromSphereSolid
	(
		IScriptManager scriptManager,
		Document doc,
		ElementId genericModelsCategoryId,
		Solid solid
	)
	{
		DirectShape directShape = null;

		XYZ sphereCenter = null;

		try
		{
			sphereCenter = solid.ComputeCentroid();
		}
		catch (Exception ex)
		{
			scriptManager.LogError($"{ex.Message}\n{ex.StackTrace}");
		}

		if (sphereCenter is null)
		{
			return directShape;
		}

		try
		{
			directShape = DirectShape.CreateElement(doc, genericModelsCategoryId);

			directShape.Name = $"Sphere at ({sphereCenter.X:F2}, {sphereCenter.Y:F2}, {sphereCenter.Z:F2})";

			directShape.SetShape(new List<GeometryObject>() { solid });
		}
		catch (Exception ex)
		{
			scriptManager.LogError($"{ex.Message}\n{ex.StackTrace}");
		}

		return directShape;
	}

	public static void PaintShapeFacesOnNewTransaction
	(
		IScriptManager scriptManager,
		Document doc,
		DirectShape directShape,
		Material material
	)
	{
		using (Transaction transaction = new Transaction(doc, MethodBase.GetCurrentMethod().Name))
		{
			transaction.Start();

			doc.Regenerate();

			GeometryElement geometryElement = directShape.get_Geometry(RevitGeometryOptions.Full());

			foreach (GeometryObject geometryObject in geometryElement)
			{
				if (geometryObject is Solid solid)
				{
					foreach (Face face in solid.Faces)
					{
						try
						{
							doc.Paint(directShape.Id, face, material.Id);
						}
						catch (Exception ex)
						{
							scriptManager.LogError($"{ex.Message}\n{ex.StackTrace}");
						}
					}
				}
			}

			transaction.Commit();
		}
	}
}