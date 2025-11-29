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

		using (Transaction transaction = new Transaction(doc, $"Creating {materialName} material"))
		{
			try
			{
				transaction.Start();

				result = Material.Create(doc, materialName);

				transaction.Commit();
			}
			catch (Exception ex)
			{
				transaction.RollBack();

				throw;
			}
		}

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

		using (Transaction transaction = new Transaction(doc, $"Assigning material {materialId.ToString()} appereance"))
		{
			try
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
			catch (Exception ex)
			{
				transaction.RollBack();

				throw;
			}
		}

		return result;
	}
}