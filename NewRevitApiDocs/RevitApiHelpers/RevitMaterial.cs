using Autodesk.Revit.DB;

public static class RevitMaterial
{
	public static ElementId DefaultOnNewTransaction
	(
		ScriptManager scriptManager,
		Document doc,
		string materialName,
		int transparency
	)
	{
		ElementId result = null;

		result = Material.Create(doc, materialName);

		return result;
	}

	public static Material SetColorAndTransparencyOnNewTransaction
	(
		ScriptManager scriptManager,
		Document doc,
		ElementId materialId,
		Autodesk.Revit.DB.Color color,
		int transparency
	)
	{
		Material result = null;

		result = doc.GetElement(materialId) as Material;

		if (result != null)
		{
			result.Color = color;

			result.Transparency = transparency;
		}

		return result;
	}
}