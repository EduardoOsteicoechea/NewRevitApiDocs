using Autodesk.Revit.DB;

public static class RevitGeometryOptions
{
	public static Options Full()
	{
		return new Options()
		{
			DetailLevel = ViewDetailLevel.Fine,
			ComputeReferences = true,
			IncludeNonVisibleObjects = true,
		};
	}
}
