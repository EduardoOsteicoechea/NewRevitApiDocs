using Autodesk.Revit.DB;
using System.Reflection;

public static class RevitMaterial
{
	public static ElementId DefaultOnNewTransaction
	(
		IScriptManager scriptManager,
		Document doc,
		string materialName,
		int transparency
	)
	{
		ElementId result = null;

		using (Transaction transaction = new Transaction(doc, MethodBase.GetCurrentMethod().Name))
		{
			transaction.Start();

			result = Material.Create(doc, materialName);

			transaction.Commit();
		}

		return result;
	}

	public static Material SetColorAndTransparencyOnNewTransaction
	(
		IScriptManager scriptManager,
		Document doc,
		ElementId materialId,
		Autodesk.Revit.DB.Color color,
		int transparency
	)
	{
		Material result = null;

		using (Transaction transaction = new Transaction(doc, MethodBase.GetCurrentMethod().Name))
		{
			transaction.Start();

			result = doc.GetElement(materialId) as Material;

			if (result != null)
			{
				result.Color = color;

				result.Transparency = transparency;
			}

			transaction.Commit();
		}

		return result;
	}
}