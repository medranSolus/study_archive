package lab02_pop;

import java.io.File;
import java.util.Scanner;
import java.util.Map;
import java.util.TreeMap;
import java.util.List;
import java.util.LinkedList;
import java.io.IOException;
import java.io.FileNotFoundException;

public class Main 
{
	static String sourcePath = "src\\lab02_pop\\";
	
	public static void main(String[] args) throws FileNotFoundException, IOException
	{
		List<Student> students = new LinkedList<Student>();
		Map<Integer, School> schools = new TreeMap<Integer, School>();
		Scanner input = new Scanner(new File(sourcePath + "students.txt"));
		while(input.hasNext())
			students.add(new Student(input));
		input.close();
		input = new Scanner(new File(sourcePath + "schools.txt")); 
		while(input.hasNext())
		{
			School temporary = new School(input);
			schools.put(temporary.getId(), temporary);
		}
		input.close();
		Recruitment.enlistStudentsToSchools(students, schools);
		Statistics.showStatistics(students, schools.size());
	}
}
