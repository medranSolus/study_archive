package lab04_pop;

import java.io.PrintWriter;
import java.util.Map;
import java.util.Scanner;
import java.util.TreeMap;

public class Test
{
	private int idCount = 0;
	private Category testType = Category.Informatics;
	private Map<Integer, Question> questions = new TreeMap<Integer, Question>();
	private String name = "none";
	
	public Test(Category type, Map<Integer, Question> testQuestions, String testName)
	{
		testType = type;
		questions = testQuestions;
		name = testName;
	}
	public Test(Scanner input)
	{
		testType = Category.values()[input.nextInt()];
		idCount = input.nextInt();
		input.nextLine();
		name = input.nextLine();
		for (int i = 1; i <= idCount; ++i)
			questions.put(i, new Question(input));
		++idCount;
	}

	public Category getTestType() { return testType; }
	public Map<Integer, Question> getQuestions() { return questions; }
	public String getTestName() { return name; }
	public void setName(String testName) { name = testName; }
	public void deleteQuestion(int id) { questions.remove(id); }
	public int addQuestion(Question question)
	{
		questions.put(idCount, question);
		++idCount;
		return idCount - 1;
	}
	
	public void save(PrintWriter output)
	{
		output.println(testType.ordinal() + " " + questions.size());
		output.println(name);
		for (var question : questions.values())
			question.save(output);
	}
}
