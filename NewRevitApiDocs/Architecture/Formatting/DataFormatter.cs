using Autodesk.Revit.DB;
using System.Globalization;
using System.Text;

internal class DataFormatter
{
	public static string Collection<T>(ICollection<T> data, Func<T, int, string> action)
	{
		if (data == null) return "[null]";

		var printer = new StringBuilder();

		printer.Append($"[{data.Count}],[");

		for (int i = 0; i < data.Count; i++)
		{
			printer.Append(action(data.ToList()[i], i));
		}

		printer.Append($"]");

		return printer.ToString();
	}

	public static string XYZData(XYZ item, int index)
	{
		if (item == null) return "[null]";

		return $"({index} ,{item.ToString()}), ";
	}

	public static string XYZsData(List<XYZ> data)
	{
		return Collection(data, XYZData);
	}

	public static string SolidVolume(Solid data, int index)
	{
		if (data == null) return "[null]";

		return $"({index}, ({nameof(data.ComputeCentroid)}: {data.ComputeCentroid().ToString()}, {nameof(data.Volume)}: {data.Volume.ToString("F2", CultureInfo.InvariantCulture)})), ";
	}

	public static string SolidsVolume(List<Solid> data)
	{
		return Collection(data, SolidVolume);
	}

	public static string ElementIdData(ElementId data, int index)
	{
		if (data == null) return "[null]";

		return $"({index}, ({nameof(data.ToString)}: {data.ToString()})), ";
	}

	public static string ElementIdsData(List<ElementId> data)
	{
		return Collection(data, ElementIdData);
	}

	public static string MaterialData(Material data, int index)
	{
		if (data == null) return "[null]";

		return $"({index}, ({nameof(data.Id)}: {data.Id}, {nameof(data.Name)}: {data.Name})), ";
	}

	public static string MaterialsData(List<Material> data)
	{
		return Collection(data, MaterialData);
	}

	public static string DirectShapeData(DirectShape data, int index)
	{
		if (data == null) return "[null]";

		return $"({index}, ({nameof(data.Id)}: {data.Id}, {nameof(data.Name)}: {data.Name})), ";
	}

	public static string DirectShapesData(List<DirectShape> data)
	{
		return Collection(data, DirectShapeData);
	}
}