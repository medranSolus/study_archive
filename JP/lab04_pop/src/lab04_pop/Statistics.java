package lab04_pop;

import java.io.PrintWriter;
import java.util.Scanner;

import javafx.util.Pair;

public class Statistics
{
	private Category testType = Category.Informatics;
	private Pair<Integer, Integer> points = null;
	private String testName = "none";
	
	public Statistics(Category type, int pointsGained, int questionCount, String name)
	{
		testType = type;
		points = new Pair<Integer, Integer>(questionCount, pointsGained);
		testName = name;
	}
	public Statistics(Scanner input)
	{
		testType = Category.values()[input.nextInt()];
		int score = input.nextInt();
		points = new Pair<Integer, Integer>(input.nextInt(), score);
		input.nextLine();
		testName = input.nextLine();
	}
	
	public Category getTestType() { return testType; }
	public int getScore() { return points.getValue(); }
	public int getMaxPoints() { return points.getKey(); }
	public String getTestName() { return testName; }
	
	public void save(PrintWriter output)
	{
		output.println(testType.ordinal() + " " + points.getValue() + " " + points.getKey());
		output.println(testName);
	}
}
