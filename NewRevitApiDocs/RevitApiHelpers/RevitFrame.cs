using Autodesk.Revit.DB;

namespace NewRevitApiDocs
{
	public static class RevitFrame
	{
		public static Frame ZOnX
		(
			XYZ point
		)
		{
			Frame result = null;

			XYZ frameOrigin = point;

			XYZ frameZDirection = XYZ.BasisX;

			XYZ frameXDirection = XYZ.BasisY;

			XYZ frameYDirection = frameZDirection.CrossProduct(frameXDirection);

			result = new Frame(frameOrigin, frameXDirection, frameYDirection, frameZDirection);			

			return result;
		}

		public static Frame ZOnY
		(
			XYZ point
		)
		{
			Frame result = null;

			XYZ frameOrigin = point;

			XYZ frameZDirection = XYZ.BasisY;

			XYZ frameXDirection = XYZ.BasisZ;

			XYZ frameYDirection = frameZDirection.CrossProduct(frameXDirection);

			result = new Frame(frameOrigin, frameXDirection, frameYDirection, frameZDirection);

			return result;
		}

		public static Frame ZOnZ
		(
			XYZ point
		)
		{
			Frame result = null;

			XYZ frameOrigin = point;

			XYZ frameZDirection = XYZ.BasisZ;

			XYZ frameXDirection = XYZ.BasisX;

			XYZ frameYDirection = XYZ.BasisY;

			result = new Frame(frameOrigin, frameXDirection, frameYDirection, frameZDirection);

			return result;
		}
	}
}
