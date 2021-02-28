package lab04_pop;

public class StatisticsModel
{
	private String category = "none";
	private String points = "0/0";
	private String testName = "none";
	
	public StatisticsModel(Statistics statistics)
	{
		category = Category.toString(statistics.getTestType());
		points = statistics.getScore() + "/" + statistics.getMaxPoints();
		testName = statistics.getTestName();
	}
	
	public String getCategory() { return category; }
	public String getPoints() { return points; }
	public String getTestName() { return testName; }
}
