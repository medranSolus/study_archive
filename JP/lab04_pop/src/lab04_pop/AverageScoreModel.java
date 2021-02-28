package lab04_pop;

public class AverageScoreModel
{

	private String name = "Unknown";
	private double averageScore = 0.0;
	private int currentPoints = 0;
	private int currentMaxPoints = 0;
	
	public AverageScoreModel(String newName, int points, int maxPoints)
	{
		name = newName;
		currentPoints = points;
		currentMaxPoints = maxPoints;
		averageScore = ((double)currentPoints)/currentMaxPoints;
	}
	
	public String getName() { return name; }
	public double getAverageScore() { return Double.parseDouble(String.format("%.2f", averageScore*100).replace(",", ".")); }
	public void addScore(int points, int maxPoints) 
	{ 
		currentPoints += points;
		currentMaxPoints += maxPoints;
		averageScore = (double)currentPoints/currentMaxPoints;
	}
}
