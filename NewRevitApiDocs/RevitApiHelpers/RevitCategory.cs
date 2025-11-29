using Autodesk.Revit.DB;

public static class RevitCategory
{
	public static ElementId GetGenericModelCategoryId
	(
		IScriptManager scriptManager,
		Document doc
	)
	{
		return Category.GetCategory(doc, BuiltInCategory.OST_GenericModel).Id;
	}
}