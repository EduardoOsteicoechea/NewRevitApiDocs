using System.Reflection;
using System.Text;

public static class DtoFormater
{
	public static string Format<T>(T item)
	{
		var printer = new StringBuilder();
		
		var type = typeof(T);

		printer.Append($"{type.Name}");

		var properties = type.GetProperties().Where(a => Attribute.IsDefined(a, typeof(PrintAttribute)));

		foreach (var property in properties) 
		{
			var attribute = (PrintAttribute)property.GetCustomAttributes(typeof(PrintAttribute), false).FirstOrDefault();

			object rawValue = property.GetValue(item);

			string displayValue;

			if (
				attribute.FormatterType is not null
					&&
				!string.IsNullOrEmpty(attribute.FormatterMethodName)
			) 
			{
				var method = attribute.FormatterType.GetMethod(attribute.FormatterMethodName, BindingFlags.Static | BindingFlags.Public);

				if (method is not null)
				{
					displayValue = (string)method.Invoke(null, new object[] { rawValue });
				}
				else 
				{
					displayValue = $"[{attribute.FormatterMethodName} method not found]";
				}
			}
			else 
			{
				displayValue = rawValue?.ToString() ?? "null";
			}

			printer.AppendLine($"{property.Name}: {displayValue}");
			printer.AppendLine();
		}

		return printer.ToString();
	}
}