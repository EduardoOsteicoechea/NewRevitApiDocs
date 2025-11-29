

public class DTOBase
{
	public bool PrintValues { get; set; } = true;
	public List<DTOItemBase>? DtoItems { get; set; }
	public override string ToString()
	{
		return DtoFormater.Format(this);
	}
}