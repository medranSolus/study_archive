package lab04_pop;

import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

public class CreateModel
{
	public static List<AverageScoreModel> averageTestScoreModel(List<Statistics> statistics)
	{
		Map<String, AverageScoreModel> testScores = new TreeMap<String, AverageScoreModel>();
		for (var statistic : statistics)
		{
			if (testScores.containsKey(statistic.getTestName()))
				testScores.get(statistic.getTestName()).addScore(statistic.getScore(), statistic.getMaxPoints());
			else
				testScores.put(statistic.getTestName(), new AverageScoreModel(statistic.getTestName(), statistic.getScore(), statistic.getMaxPoints()));
		}
		return new LinkedList<AverageScoreModel>(testScores.values());
	}

	public static List<AverageScoreModel> averageGroupScoreModel(List<Statistics> statistics)
	{
		Map<Category, AverageScoreModel> groupScores = new TreeMap<Category, AverageScoreModel>();
		for (var statistic : statistics)
		{
			if (groupScores.containsKey(statistic.getTestType()))
				groupScores.get(statistic.getTestType()).addScore(statistic.getScore(), statistic.getMaxPoints());
			else
				groupScores.put(statistic.getTestType(), new AverageScoreModel(Category.toString(statistic.getTestType()), statistic.getScore(), statistic.getMaxPoints()));
		}
		return new LinkedList<AverageScoreModel>(groupScores.values());
	}
}
