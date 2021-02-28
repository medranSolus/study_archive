package lab02_pop;

import java.util.Scanner;
import java.util.List;
import java.util.ArrayList;

public class Student 
{
	int id = 0;
	int points = 0;
	List<Integer> preferences = null;
	School currentSchool = null;
	
	public Student() {}
	public Student(int newId, int newPoints, List<Integer> newPreferences)
	{
		id = newId;
		points = newPoints;
		preferences = newPreferences;
	}
	public Student(Scanner input)
	{
		String[] data = input.nextLine().split(" |\t");
		id = Integer.parseInt(data[0]);
		points = Integer.parseInt(data[1]);
		preferences = new ArrayList<Integer>();
		String[] preferenceList = data[2].split(",");
		for(String preference : preferenceList)
			preferences.add(Integer.parseInt(preference));
	}
	
	public void enlistToSchool(School newSchool)
	{
		currentSchool = newSchool;
	}
	public double happinesLevel(int numberOfSchools)
	{
		if(currentSchool == null)
			return -1.0;
		if(!preferences.contains(currentSchool.getId()))
			return preferences.size()/numberOfSchools;
		return ((double)(preferences.size() - preferences.indexOf(currentSchool.getId())))/preferences.size();
	}
	
	public int getId() { return id; }
	public int getPoints() { return points; }
	public List<Integer> getPreferences() { return preferences; }
	public School getEnlistedSchool() { return currentSchool; }
}
