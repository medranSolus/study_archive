package lab02_pop;

import java.util.Comparator;

public class StudentsSort implements Comparator<Student>
{
	public int compare(Student student1, Student student2) 
    { 
        return student2.getPoints() - student1.getPoints(); 
    }
}
