using Autodesk.Revit.DB;
using System.Reflection;
using System.Text;


public static class LogService
{
	public static bool IsEnabled { get; } = true;
	public static string Tab1 { get; } = "  ";
	public static string Tab2 { get; } = "    ";
	public static string Tab3 { get; } = "      ";
	public static string Tab4 { get; } = "        ";

	public static StringBuilder Logger()
	{
		return IsEnabled ? new StringBuilder() : null;
	}

	public static void Print(MethodBase methodBase, StringBuilder _logger = null)
	{
		if (_logger != null)
		{
			File.WriteAllText(
				Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
					$"{methodBase.DeclaringType.Name}.txt"
				),
				_logger.ToString()
			);
		}
	}

	public static void Print(MethodBase methodBase, Document doc, StringBuilder _logger = null)
	{
		if (_logger != null)
		{
			File.WriteAllText(
				Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
					$"{methodBase.DeclaringType.Name}_{doc.Title}.txt"
				),
				_logger.ToString()
			);
		}
	}

	public static void Print(string workflowName, Document doc, StringBuilder _logger = null)
	{
		if (_logger != null)
		{
			File.WriteAllText(
				Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
					$"{workflowName}_{doc.Title}.txt"
				),
				_logger.ToString()
			);
		}
	}
}