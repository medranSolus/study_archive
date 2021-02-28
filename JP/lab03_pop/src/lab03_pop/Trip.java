package lab03_pop;

import java.io.PrintWriter;
import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneOffset;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.Scanner;
import java.util.TreeMap;

public class Trip 
{
	static int ID = 0;
	int id = 0;
	String name = "none";
	LocalDateTime startTime = LocalDateTime.now();
	LocalDateTime endTime = LocalDateTime.now();
	String country = "none";
	String city = "none";
	String description = "none";
	int maxSlots = 0;
	int costPerClient = 0;
	Map<Integer, Client> enlistedClients = new TreeMap<Integer, Client>();
	
	public Trip(Scanner input, Map<Integer, Client> clients)
	{
		id = input.nextInt();
		if (ID < id)
			ID = id;
		++ID;
		name = input.nextLine();
		startTime = LocalDateTime.ofInstant(Instant.ofEpochMilli(input.nextLong()), ZoneOffset.UTC);
		endTime = LocalDateTime.ofInstant(Instant.ofEpochMilli(input.nextLong()), ZoneOffset.UTC);
		country = input.nextLine();
		city = input.nextLine();
		description = input.nextLine();
		maxSlots = input.nextInt();
		costPerClient = input.nextInt();
		for (int size = input.nextInt(), i = 0, key = 0; i < size; ++i)
		{
			key = input.nextInt();
			enlistedClients.put(key, clients.get(key));
		}
		input.nextLine();
	}
	public Trip(String newName, LocalDateTime newStart, LocalDateTime newEnd, String newCountry, String newCity, String newDescription, int newMaxSlots, int newCostPerClient)
	{
		name = newName;
		startTime = newStart;
		endTime = newEnd;
		country = newCountry;
		city = newCity;
		description = newDescription;
		maxSlots = newMaxSlots;
		costPerClient = newCostPerClient;
	}

	public int getId() { return id; }
	public String getName() { return name; }
	public LocalDateTime getStartTime() { return startTime; }
	public LocalDateTime getEndTime() { return endTime; }
	public String getCountry() { return country; }
	public String getCity() { return city; }
	public String getDescription() { return description; }
	public int getMaxSlots() { return maxSlots; }
	public int getCostPerClient() { return costPerClient; }
	public Client getEnlistedClient(int id) { return enlistedClients.get(id); }
	public List<Client> getEnlistedClients() {return new ArrayList<Client>(enlistedClients.values()); }
	public int freeSlots() { return maxSlots - enlistedClients.size(); }
	public int clientsCount() { return enlistedClients.size(); }

	public void setName(String newName) { name = newName; }
	public void setStartTime(LocalDateTime newStart) { startTime = newStart; }
	public void setEndTime(LocalDateTime newEnd) { endTime = newEnd; }
	public void setCountry(String newCountry) { country = newCountry; }
	public void setCity(String newCity) { city = newCity; }
	public void setDescription(String newDescription) { description = newDescription; }
	public void setMaxSlots(int newMaxSlots) { maxSlots = newMaxSlots; }
	public void setCostPerClient(int newCostPerClient) { costPerClient = newCostPerClient; }
	
	public void deleteClient(int id) { enlistedClients.remove(id); }
	public boolean addClient(Client newClient) 
	{ 
		if (enlistedClients.size() < maxSlots)
		{
			enlistedClients.put(newClient.getId(), newClient);
			return true;
		}
		return false;
	}
	public void show()
	{
		System.out.println("ID: " + id);
		System.out.println("Name: " + name);
		System.out.println("Date: (" + startTime.format(DataInput.formatter) + ")-(" + endTime.format(DataInput.formatter) + ")");
		System.out.println("Location: " + country + ", " + city);
		System.out.println("\"" + description + "\"");
		System.out.println("Enlisted clients: " + enlistedClients.size() + "/" + maxSlots);
		if (enlistedClients.size() > 0)
		{
			System.out.print("Client's ids: ");
			StringBuffer output = new StringBuffer();
			for (var id : enlistedClients.keySet())
				output.append(id + ", ");
			output.trimToSize();
			System.out.println(output.substring(0, output.length() -2));
		}
	}
	public void save(PrintWriter output)
	{
		output.println(id + " " + name);
		output.println(startTime.atOffset(ZoneOffset.UTC).toInstant().toEpochMilli() + " " + endTime.atOffset(ZoneOffset.UTC).toInstant().toEpochMilli() + " " + country);
		output.println(city);
		output.println(description);
		output.println(maxSlots + " " + costPerClient + " " + enlistedClients.size());
		for (var id : enlistedClients.keySet())
			output.println(id);
	}
}
