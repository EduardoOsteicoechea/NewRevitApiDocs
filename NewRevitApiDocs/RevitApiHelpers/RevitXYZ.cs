using Autodesk.Revit.DB;

public static class RevitXYZ
{
	public static List<XYZ> FromXYDisplacementMatrix(int iterations, double displacement)
	{
		var result = new List<XYZ>();

		for (int i = 0; i < iterations; i++)
		{
			var axis1 = displacement * i;

			for (int j = 0; j < iterations; j++)
			{
				var axis2 = displacement * j;

				result.Add(new XYZ(axis1, axis2, 0));
			}
		}

		return result;
	}
	public static List<XYZ> FromXZDisplacementMatrix(int iterations, double displacement)
	{
		var result = new List<XYZ>();

		for (int i = 0; i < iterations; i++)
		{
			var axis1 = displacement * i;

			for (int j = 0; j < iterations; j++)
			{
				var axis2 = displacement * j;

				result.Add(new XYZ(axis1, 0, axis2));
			}
		}

		return result;
	}
}