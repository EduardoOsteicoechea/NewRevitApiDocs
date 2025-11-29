using Autodesk.Revit.DB;

public static class RevitArc
{
	public static Arc ByPointAndRadiusOnXYPlane
	(
		ScriptManager scriptManager,
		XYZ point,
		double radius
	)
	{
		Arc arc = null;

		XYZ center = point;

		XYZ startPoint = center + new Autodesk.Revit.DB.XYZ(radius, 0, 0);

		XYZ endPoint = center + new Autodesk.Revit.DB.XYZ(-radius, 0, 0);

		XYZ pointOnArc = center + new Autodesk.Revit.DB.XYZ(0, radius, 0);

		arc = Arc.Create(startPoint, endPoint, pointOnArc);

		return arc;
	}

}