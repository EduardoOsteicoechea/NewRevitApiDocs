
[AttributeUsage(AttributeTargets.Property)]
public class Print: Attribute
{
	public Type FormatterType { get; }
	public string FormatterMethodName { get; }
	public Print(string formatterMethodName)
	{
		FormatterType = typeof(DataFormatter);
		FormatterMethodName = formatterMethodName;
	}
	public Print(Type formatterType, string formatterMethodName)
	{
		FormatterType = formatterType;
		FormatterMethodName = formatterMethodName;
	}
}