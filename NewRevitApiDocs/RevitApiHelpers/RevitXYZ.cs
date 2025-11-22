using Autodesk.Revit.DB;

public static class RevitXYZ
{
	public static List<XYZ> FromXYDisplacementMatrix(int iterations, double displacement)
	{
		var result = new List<XYZ>();

		for (int i = 0; i < iterations; i++)
		{
			var currentXDisplacement = displacement * i;

			for (int j = 0; j < iterations; j++)
			{
				var currentYDisplacement = displacement * j;

				result.Add(new XYZ(currentXDisplacement, currentYDisplacement, 0));
			}
		}

		return result;
	}
}