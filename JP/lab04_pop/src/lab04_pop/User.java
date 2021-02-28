package lab04_pop;

import java.io.PrintWriter;
import java.util.Collections;
import java.util.LinkedList;
import java.util.List;
import java.util.Scanner;

public class User
{
	private String name = "none";
	private List<Statistics> statistics = null;
	
	public User(String newName, List<Statistics> newStatistics)
	{
		name = newName;
		statistics = newStatistics;
	}
	public User(Scanner input)
	{
		name = input.nextLine();
		int size = input.nextInt();
		input.nextLine();
		statistics = new LinkedList<Statistics>();
		for (int i = 0; i < size; ++i)
			statistics.add(new Statistics(input));
	}
	
	public String getName() { return name; }
	public List<Statistics> getStatistics() { return Collections.unmodifiableList(statistics); }
	public void setName(String newName) { name = newName; }
	public void addStatistic(Statistics statistic) { statistics.add(statistic); }
	
	public double overallScore() 
	{ 
		if (statistics.size() == 0)
			return 0;
		double score = 0.0;
		for (var statistic : statistics)
			score += ((double)statistic.getScore())/statistic.getMaxPoints();
		return score/statistics.size();
	}
	public void save(PrintWriter output)
	{
		output.println(name);
		output.println(statistics.size());
		for (var statistic : statistics)
			statistic.save(output);
	}
}
