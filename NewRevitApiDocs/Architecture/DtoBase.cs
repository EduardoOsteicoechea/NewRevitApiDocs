

public class DTOBase
{
	public bool PrintValues { get; set; } = true;
	public List<DTOItemBase>? DtoItems { get; set; }

	public string Format<T>(string propertyName, T propertyValue, Func<T, string> formatterMethod)
	{
		if (PrintValues)
		{
			var printerMethodResult = propertyValue == null ? "NULL" : formatterMethod(propertyValue);

			return $"{LogService.Tab1}{propertyName}: {printerMethodResult}";
		}
		else
		{
			return $"{LogService.Tab1}{propertyName}";
		}
	}

	public override string ToString()
	{
		return DtoFormater.Format(this);
	}
}