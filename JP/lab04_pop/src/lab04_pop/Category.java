package lab04_pop;

public enum Category
{
	Informatics, Biology, Chemistry, History, Games;
	
	public static String toString(Category category)
	{
		switch (category)
		{
		case Informatics:
			return "Informatics";
		case Biology:
			return "Biology";
		case Chemistry:
			return "Chemistry";
		case History:
			return "History";
		case Games:
			return "Games";
		default:
			return "Unknown";
		}
	}
}
