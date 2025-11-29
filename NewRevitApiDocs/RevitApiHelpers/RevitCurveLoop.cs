using Autodesk.Revit.DB;

public static class RevitCurveLoop
{
	public static CurveLoop FromLineDirectionAndOffset(Line line, double offset)
	{
		XYZ standardAxis = XYZ.BasisZ;

		if (line.Direction.IsAlmostEqualTo(standardAxis) || line.Direction.IsAlmostEqualTo(-standardAxis))
		{
			standardAxis = XYZ.BasisY;
		}

		XYZ linePerpendicularDirection = line.Direction.CrossProduct(standardAxis).Normalize();

		var curves = new CurveLoop();

		var pointOffset = offset;

		XYZ thirdAxis = line.Direction.CrossProduct(linePerpendicularDirection).Normalize();

		XYZ point = line.Origin;

		XYZ p1 = point + (-linePerpendicularDirection * pointOffset) + (thirdAxis * pointOffset);
		XYZ p2 = point + (linePerpendicularDirection * pointOffset) + (thirdAxis * pointOffset);
		XYZ p3 = point + (linePerpendicularDirection * pointOffset) + (-thirdAxis * pointOffset);
		XYZ p4 = point + (-linePerpendicularDirection * pointOffset) + (-thirdAxis * pointOffset);

		curves.Append(Line.CreateBound(p1, p2));
		curves.Append(Line.CreateBound(p2, p3));
		curves.Append(Line.CreateBound(p3, p4));
		curves.Append(Line.CreateBound(p4, p1));

		return curves;
	}
}