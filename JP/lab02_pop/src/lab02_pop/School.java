package lab02_pop;

import java.util.Scanner;
import java.util.List;
import java.util.ArrayList;
import java.util.Collections;

public class School 
{
	int id = 0;
	int maxStudents = 0;
	List<Student> enlistedStudents = null;
	
	public School() {}
	public School(int newId, int numberOfStudents)
	{
		id = newId;
		maxStudents = numberOfStudents;
		enlistedStudents = new ArrayList<Student>();
	}
	public School(Scanner input)
	{
		id = input.nextInt();
		maxStudents = input.nextInt();
		enlistedStudents = new ArrayList<Student>();
	}
	
	public boolean addStudent(Student newStudent)
	{
		if(enlistedStudents.size() < maxStudents)
		{
			enlistedStudents.add(newStudent);
			newStudent.enlistToSchool(this);
			return true;
		}
		return false;
	}
	public List<Student> getLastStudents(int points)
	{
		points += 20;
		List<Student> studentsInSchool = new ArrayList<Student>(enlistedStudents);
		Collections.reverse(studentsInSchool);
		List<Student> studentsToRelocate = new ArrayList<Student>();
		for(var student : studentsInSchool)
		{
			if(student.getPoints() <= points)
				studentsToRelocate.add(student);
			else 
				break;
		}
		if(studentsToRelocate.size() == 0)
			studentsToRelocate.add(studentsInSchool.get(0));
		return studentsToRelocate;
	}
	public boolean replaceStudent(Student oldStudent, Student newStudent)
	{
		if(!enlistedStudents.contains(oldStudent))
			return false;
		enlistedStudents.set(enlistedStudents.indexOf(oldStudent), newStudent);
		newStudent.enlistToSchool(this);
		return true;
	}

	public int getId() { return id; }
	public boolean hasPlace() { return enlistedStudents.size() < maxStudents; }
}
