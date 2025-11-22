using Autodesk.Revit.DB;

public static class RevitColor
{
	public static Color RGB(byte r, byte g, byte b)
	{
		return new Color(r, g, b);
	}

	public static Color RandomRGB()
	{
		Random randomNumberGenerator = new Random();

		byte r = (byte)randomNumberGenerator.Next(0, 256);
		byte g = (byte)randomNumberGenerator.Next(0, 256);
		byte b = (byte)randomNumberGenerator.Next(0, 256);

		return new Color(r, g, b);
	}

	public static Color RandomScopedRGB(int start, int end)
	{
		Random randomNumberGenerator = new Random();

		byte r = (byte)randomNumberGenerator.Next(start, end);
		byte g = (byte)randomNumberGenerator.Next(start, end);
		byte b = (byte)randomNumberGenerator.Next(start, end);

		return new Color(r, g, b);
	}
}