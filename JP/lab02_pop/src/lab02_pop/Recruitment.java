package lab02_pop;

import java.util.Collections;
import java.util.List;
import java.util.LinkedList;
import java.util.Map;

public class Recruitment 
{
	public static void enlistStudentsToSchools(List<Student> students, Map<Integer, School> schools)
	{
		Collections.sort(students, new StudentsSort());
		List<Student> studentsWithoutSchool = new LinkedList<Student>();
		for(var student : students)
		{
			for(var preference : student.getPreferences())
				if(schools.get(preference).addStudent(student))
					break;
			if(student.getEnlistedSchool() == null)
				studentsWithoutSchool.add(student);
		}
		List<Student> studentsLastInLine = new LinkedList<Student>();
		for(var student : studentsWithoutSchool)
		{
			for(var preference : student.getPreferences())
			{
				List<Student> studentsToRelocate = schools.get(preference).getLastStudents(student.getPoints());
				for(var studentToRelocation : studentsToRelocate)
				{
					for(var relocationStudentPreference : studentToRelocation.getPreferences())
						if(schools.get(relocationStudentPreference).addStudent(studentToRelocation))
							break;
					if(schools.get(preference) != studentToRelocation.getEnlistedSchool())
						if(schools.get(preference).replaceStudent(studentToRelocation, student))
							break;
				}
				if(student.getEnlistedSchool() != null)
					break;
			}
			if(student.getEnlistedSchool() == null)
				studentsLastInLine.add(student);
		}
		if(studentsLastInLine.size() > 0)
			for(var student : studentsLastInLine)
				for(var school : schools.values())
					if(school.addStudent(student))
						break;
	}
}
