package lab02_pop;

import java.io.IOException;
import java.util.List;

public class Statistics 
{
	public static void showStatistics(List<Student> students, int numberOfSchools) throws IOException
	{
		int studentsInPreferedSchools = 0, studentsWithoutSchool = 0;
		double studentHappines = 0.0;
		for(var student : students)
		{
			if(student.getEnlistedSchool() == null)
				++studentsWithoutSchool;
			if(student.getPreferences().contains(student.getEnlistedSchool().getId()))
				++studentsInPreferedSchools;
			studentHappines += student.happinesLevel(numberOfSchools);
		}
		System.out.println("Students in schools of their preferences: " + studentsInPreferedSchools + "\nStudents not enlisted to schools of their preferences: " + (students.size() - studentsInPreferedSchools) + "\nStudents without schools: " + studentsWithoutSchool + "\nGeneral level of happines among students: " + String.format("%.2f", studentHappines/students.size()*100) + "%");
		System.in.read();
	}
}
