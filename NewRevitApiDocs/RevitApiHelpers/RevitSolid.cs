using Autodesk.Revit.DB;
using System.Text;

namespace NewRevitApiDocs
{
	public static class RevitSolid
	{
		public static Solid SphereFromXYZAndRadius
		(
			IScriptManager scriptManager,
			Document doc,
			XYZ point,
			double radius
		)
		{
			Solid result = null;
			
			XYZ center = point;
			XYZ startPoint = center + new XYZ(radius, 0, 0);
			XYZ endPoint = center + new XYZ(-radius, 0, 0);
			XYZ pointOnArc = center + new XYZ(0, radius, 0);

			Arc arc = RevitArc.ByPointAndRadiusOnXYPlane(scriptManager, point, radius);

			Line line = Autodesk.Revit.DB.Line.CreateBound(endPoint, startPoint);

			CurveLoop curveLoop = new CurveLoop();
			curveLoop.Append(arc);
			curveLoop.Append(line);

			Frame frame = RevitFrame.ZOnX(point);

			List<CurveLoop> curveLoops = new List<CurveLoop> { curveLoop };

			result = GeometryCreationUtilities.CreateRevolvedGeometry(
				frame,
				curveLoops,
				0,
				2 * Math.PI
			);

			return result;
		}

	}
}
