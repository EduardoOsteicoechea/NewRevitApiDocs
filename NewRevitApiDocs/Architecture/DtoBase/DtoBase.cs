

public class DTOBase
{
	public List<DTOItemBase>? DtoItems { get; set; }
	public override string ToString()
	{
		return DtoFormater.Format(this);
	}
}