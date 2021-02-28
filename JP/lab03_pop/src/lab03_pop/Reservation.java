package lab03_pop;

import java.io.PrintWriter;
import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneOffset;
import java.util.List;
import java.util.Map;
import java.util.Scanner;
import java.util.ArrayList;

public class Reservation 
{
	static int ID = 0;
	int id = 0;
	LocalDateTime paymentDate = LocalDateTime.now();
	int tripId = 0;
	Trip selectedTrip = null;
	List<Client> clients = new ArrayList<Client>();
	boolean isPaid = false;
	
	public Reservation(Scanner input, Map<Integer, Trip> trips)
	{
		id = input.nextInt();
		if(ID < id)
			ID = id;
		++ID;
		paymentDate = LocalDateTime.ofInstant(Instant.ofEpochMilli(input.nextLong()), ZoneOffset.UTC);
		tripId = input.nextInt();
		selectedTrip = trips.get(tripId);
		for (int i = 0, size = input.nextInt(), id = 0; i < size; ++i)
		{
			id = input.nextInt();
			clients.add(selectedTrip.getEnlistedClient(id));
		}
		isPaid = input.nextBoolean();
		input.nextLine();
	}
	public Reservation(LocalDateTime newPaymentDate, Trip newSelectedTrip, List<Client> newClients)
	{
		id = ID;
		++ID;
		paymentDate = newPaymentDate;
		selectedTrip = newSelectedTrip;
		tripId = selectedTrip.getId();
		clients = newClients;
		for (var client : clients)
			newSelectedTrip.addClient(client);
	}
	
	public LocalDateTime getPaymentDate() { return paymentDate; }
	public List<Client> getClients() { return clients; }
	public Trip getSelectedTrip() { return selectedTrip; }
	public int getId() { return id; }
	public boolean isPaymentDone() { return isPaid; }
	public int reservationCost() { return selectedTrip.getCostPerClient()*clients.size(); }
	public boolean isActive(LocalDateTime currentDate) { return (!isPaid && paymentDate.isBefore(currentDate)) || paymentDate.isAfter(currentDate); } 
	
	public int payCost(int money)
	{
		if(money >= selectedTrip.getCostPerClient()*clients.size())
			isPaid = true;
		return money - selectedTrip.getCostPerClient()*clients.size();
	}
	public void show(LocalDateTime currentDate)
	{
		System.out.println("ID: " + id);
		System.out.println("Reserved trip: " + selectedTrip.getName());
		System.out.print("Client's ids: ");
		StringBuffer output = new StringBuffer();
		for (var client : clients)
			output.append(client.getId() + ", ");
		output.trimToSize();
		System.out.println(output.substring(0, output.length() -2));
		if (currentDate.isAfter(paymentDate))
		{
			if (isPaid)
				System.out.println("Status: finished");
			else
				System.out.println("Status: payment date expired (" + paymentDate + ")");
		}
		else
			System.out.println("Status: awaiting payment\nExpiration date: " + paymentDate + "\nAmount to pay: " + selectedTrip.getCostPerClient()*clients.size());
	}
	public void save(PrintWriter output)
	{
		output.print(id + " " + paymentDate.atOffset(ZoneOffset.UTC).toInstant().toEpochMilli() + " " + tripId + " " + clients.size());
		for (var client : clients)
			output.print(" " + client.getId());
		output.println(" " + isPaid);
	}
}
