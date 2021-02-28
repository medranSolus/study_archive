package lab03_pop;

import java.io.PrintWriter;
import java.util.Scanner;

public class Client 
{
	static int ID = 0;
	int id;
	String name = "noName";
	String surname = "none";
	
	public Client(Scanner input) 
	{
		id = input.nextInt();
		name = input.nextLine();
		surname = input.nextLine();
		if (ID < id)
			ID = id;
		++ID;
	}
	public Client(String newName, String newSurname)
	{
		name = newName;
		surname = newSurname;
		id = ID;
		++ID;
	}
	
	public int getId() { return id; }
	public String getName() { return name; }
	public String getSurname() { return surname; }
	
	public void setName(String newName) { name = newName; }
	public void setSurname(String newSurname) { surname = newSurname; }
	
	public void show()
	{
		System.out.println("ID: " + id);
		System.out.println("Name: " + name);
		System.out.println("Surname: " + surname);
	}
	public void save(PrintWriter output) 
	{ 
		output.println(id + " " + name); 
		output.println(surname);
	}
}
