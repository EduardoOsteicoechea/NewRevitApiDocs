using Autodesk.Revit.DB;
using System.Text;

internal class DataFormatter
{
	public static string PrintXYZData(XYZ item)
	{
		if (item == null) return "[null]";

		return $"{item.ToString()}";
	}

	public static string PrintXYZsData(List<XYZ> items)
	{
		if (items == null) return "[null]";

		var result = new StringBuilder();

		foreach (XYZ item in items)
		{
			result.AppendLine(PrintXYZData(item));
		}

		return result.ToString();
	}

	public static string PrintSolidVolume(Solid item)
	{
		if (item == null) return "[null]";

		return $"{item.Volume}";
	}

	public static string PrintSolidsVolume(List<Solid> items)
	{
		if (items == null) return "[null]";

		var result = new StringBuilder();

		foreach (Solid item in items)
		{
			result.AppendLine(PrintSolidVolume(item));
		}

		return result.ToString();
	}

	public static string PrintElementIdData(ElementId item)
	{
		if (item == null) return "[null]";

		return $"{item.ToString()}";
	}

	public static string PrintElementIdsData(List<ElementId> items)
	{
		if (items == null) return "[null]";

		var result = new StringBuilder();

		foreach (ElementId item in items)
		{
			result.AppendLine(PrintElementIdData(item));
		}

		return result.ToString();
	}

	public static string PrintMaterialData(Material item)
	{
		if (item == null) return "[null]";

		return $"Material: {item.Id} | {item.Name}";
	}

	public static string PrintMaterialsData(List<Material> items)
	{
		if (items == null) return "[null]";

		var result = new StringBuilder();

		foreach (Material item in items)
		{
			result.AppendLine(PrintMaterialData(item));
		}

		return result.ToString();
	}

	public static string PrintDirectShapeData(DirectShape item)
	{
		if (item == null) return "[null]";

		return $"DirectShape: {item.Id} | {item.Name}";
	}

	public static string PrintDirectShapesData(List<DirectShape> items)
	{
		if (items == null) return "[null]";

		var result = new StringBuilder();

		foreach (DirectShape item in items)
		{
			result.AppendLine(PrintDirectShapeData(item));
		}

		return result.ToString();
	}

	public static string PrintElementIdHashSet(HashSet<ElementId> items)
	{
		if (items == null) return "[null]";
		if (items.Count == 0) return "[empty]";

		var data = items.Select(a => a.ToString());
		return $"[{string.Join(",", data)}]";
	}


	public static string PrintSolidMaterialDictionaryData(Dictionary<Solid, Material> items)
	{
		if (items == null) return "[null]";

		var data = new StringBuilder();

		foreach (KeyValuePair<Solid, Material> item in items)
		{
			data.Append($"\n{LogService.Tab2}{item.Key.Volume.ToString()} | {item.Value.Id.ToString()}");
		}

		return data.ToString();
	}


	public static string PrintElementIdElementIdDictionary(Dictionary<ElementId, ElementId> items)
	{
		if (items == null) return "[null]";

		var data = new StringBuilder();

		foreach (KeyValuePair<ElementId, ElementId> item in items)
		{
			data.Append($"\n{LogService.Tab2}{item.Key.ToString()} | {item.Value.ToString()}");
		}

		return data.ToString();
	}

	public static string PrintElementIdSolidDictionary(Dictionary<ElementId, Solid> items)
	{
		if (items == null) return "[null]";

		var data = new StringBuilder();

		foreach (KeyValuePair<ElementId, Solid> item in items)
		{
			data.Append($"\n{LogService.Tab2}{item.Key.ToString()} | {item.Value.Volume.ToString()}");
		}

		return data.ToString();
	}

	public static string PrintElementIdElementListDictionary(Dictionary<ElementId, List<Element>> items)
	{
		if (items == null) return "[null]";

		var data = new StringBuilder();

		foreach (KeyValuePair<ElementId, List<Element>> item in items)
		{
			var data2 = new StringBuilder();

			foreach (Element element in item.Value)
			{
				data2.Append($"{element.Id},");
			}

			data.Append($"\n{LogService.Tab2}{item.Key.ToString()}: [{data2.ToString()}]");
		}

		return data.ToString();
	}

	public static string PrintElementIdXYZListDictionary(Dictionary<ElementId, List<XYZ>> items)
	{
		if (items == null) return "[null]";

		var data = new StringBuilder();

		foreach (KeyValuePair<ElementId, List<XYZ>> item in items)
		{
			var data2 = new StringBuilder();

			foreach (XYZ element in item.Value)
			{
				data2.Append($"{element.ToString()},");
			}

			data.Append($"\n{LogService.Tab2}{item.Key.ToString()}: [{data2.ToString()}]");
		}

		return data.ToString();
	}

	public static string PrintElementIdXYZDictionary(Dictionary<ElementId, XYZ> items)
	{
		if (items == null) return "[null]";

		var data = new StringBuilder();

		foreach (KeyValuePair<ElementId, XYZ> item in items)
		{
			data.Append($"\n{LogService.Tab2}{item.Key.ToString()} | {item.Value.ToString()}");
		}

		return data.ToString();
	}

	public static string PrintElementIdLineDictionary(Dictionary<ElementId, Line> items)
	{
		if (items == null) return "[null]";

		var data = new StringBuilder();

		foreach (KeyValuePair<ElementId, Line> item in items)
		{
			data.Append($"\n{LogService.Tab2}{item.Key.ToString()} | {item.Value.ApproximateLength.ToString()}");
		}

		return data.ToString();
	}

	public static string PrintElementIdDoubleDictionary(Dictionary<ElementId, double> items)
	{
		if (items == null) return "[null]";

		var data = new StringBuilder();

		foreach (KeyValuePair<ElementId, double> item in items)
		{
			data.Append($"\n{LogService.Tab2}{item.Key.ToString()} | {item.Value.ToString()}");
		}

		return data.ToString();
	}

	public static string PrintElementIdElementDictionary(Dictionary<ElementId, Element> items)
	{
		if (items == null) return "[null]";

		var data = new StringBuilder();

		foreach (KeyValuePair<ElementId, Element> item in items)
		{
			data.Append($"\n{LogService.Tab2}{item.Key.ToString()} | {item.Value.Id.ToString()}");
		}

		return data.ToString();
	}
}