package lab04_pop;

import java.io.PrintWriter;
import java.util.Map;
import java.util.Scanner;
import java.util.TreeMap;

public class Question
{
	private String question = "none";
	private Map<Integer, String> answers;
	private int correctAnswer = 0;
	
	public Question(String newQuestion, Map<Integer, String> newAnswers, int newCorrectAnswer)
	{
		question = newQuestion;
		answers = newAnswers;
		answers = newAnswers;
		correctAnswer = newCorrectAnswer;
	}
	public Question(Scanner input)
	{
		correctAnswer = input.nextInt();
		input.nextLine();
		question = input.nextLine();
		answers = new TreeMap<Integer, String>();
		for (int i = 1; i <= 4; ++i)
			answers.put(i, input.nextLine());
	}
	
	public String getQuestion() { return question; }
	public Map<Integer, String> getAnswers() { return answers; }
	public int getCorrectAnswer() { return correctAnswer; }
	public void setQuestion(String newQuestion) { question = newQuestion; }
	public boolean replaceAnswer(int id, String newAnswer) { return answers.replace(id, newAnswer) == null ? false : true; }
	
	public boolean setCorrectAnswer(int id)
	{
		if(answers.containsKey(id))
		{
			correctAnswer = id;
			return true;
		}
		return false;
	}
	public void save(PrintWriter output)
	{
		output.println(correctAnswer);
		output.println(question);
		for (var answer : answers.values())
			output.println(answer);
	}
}
