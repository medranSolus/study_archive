package lab03_pop;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;
import java.util.Scanner;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.time.LocalDateTime;

public class TripAgency 
{
	String name = "none";
	Map<Integer, Client> clients = new TreeMap<Integer, Client>();
	Map<Integer, Trip> trips = new TreeMap<Integer, Trip>();
	Map<Integer, Reservation> reservations = new TreeMap<Integer, Reservation>();
	LocalDateTime currentDate = LocalDateTime.now();
	
	public TripAgency() {}
	public TripAgency(String newName) { name = newName; }
	
	public String getName() { return name; }
	public LocalDateTime getCurrentDate() { return currentDate; }
	
	public void setName(String newName) { name = newName; }
	public void setCurrentDate(LocalDateTime newDate) { currentDate = newDate; }

	public Client getClient(int id) { return clients.get(id); }
	public List<Client> getClients() { return new ArrayList<Client>(clients.values()); }
	public Trip getTrip(int id) { return trips.get(id); }
	public List<Trip> getTrips() { return new ArrayList<Trip>(trips.values()); }
	public void addClient(Client newClient) { clients.put(newClient.getId(), newClient); }
	public void addTrip(Trip newTrip) { trips.put(newTrip.getId(), newTrip); }
	public void addReservation(Reservation newReservation) { reservations.put(newReservation.getId(), newReservation); }
	public boolean containsClient(int id) { return clients.containsKey(id); }
	public boolean containsTrip(int id) { return trips.containsKey(id); }
	public boolean containsReservation(int id) { return reservations.containsKey(id); }
	public int payForReservation(int id, int money) { return reservations.get(id).payCost(money); }
	public void cancelReservation(int id) { reservations.remove(id); }
	public int clientsCount() { return clients.size(); }
	public int tripsCount() { return trips.size(); }
	public int reservationsCount() { return reservations.size(); }

	public void showClients()
	{
		System.out.println("----------------");
		for (var client : clients.values())
		{
			client.show();
			System.out.println("----------------");
		}
	}
	public void showTrips(boolean shortVersion)
	{
		if (shortVersion)
			for (var trip : trips.values())
				System.out.println("[" + trip.getId() + "] " + trip.getName());
		else
		{
			System.out.println("----------------");
			for (var trip : trips.values())
			{
				trip.show();
				System.out.println("----------------");
			}
		}
	}
	public void showReservations(boolean onlyActive)
	{
		System.out.println("----------------");
		if(onlyActive)
		{
			for (var reservation : reservations.values())
				if(reservation.isActive(currentDate))
				{
					reservation.show(currentDate);
					System.out.println("----------------");
				}
		}
		else
		{
			for (var reservation : reservations.values())
			{
				reservation.show(currentDate);
				System.out.println("----------------");
			}
		}
		
	}
	public void updateTrip(Trip trip)
	{
		trips.remove(trip.getId());
		trips.put(trip.getId(), trip);
	}
	public void deleteTrip(int id) 
	{ 
		reservations.entrySet().removeIf(reservation -> reservation.getValue().getSelectedTrip().getId() == id);
		trips.remove(id); 
	}
	
	public void save(String fileLocation) throws IOException
	{
		PrintWriter output = new PrintWriter(new BufferedWriter(new FileWriter(new File(fileLocation + "clients.txt"))));
		for (var client : clients.values())
			client.save(output);
		output.close();
		output = new PrintWriter(new BufferedWriter(new FileWriter(new File(fileLocation + "trips.txt"))));
		for (var trips : trips.values())
			trips.save(output);
		output.close();
		output = new PrintWriter(new BufferedWriter(new FileWriter(new File(fileLocation + "reservations.txt"))));
		for (var reservation : reservations.values())
			reservation.save(output);
		output.close();
	}
	public void read(String fileLocation) throws FileNotFoundException
	{
		Scanner input = new Scanner(new File(fileLocation + "clients.txt"));
		while (input.hasNext())
		{
			Client client = new Client(input);
			clients.put(client.getId(), client);
		}
		input.close();
		input = new Scanner(new File(fileLocation + "trips.txt"));
		while (input.hasNext())
		{
			Trip trip = new Trip(input, clients);
			trips.put(trip.getId(), trip);
		}
		input.close();
		input = new Scanner(new File(fileLocation + "reservations.txt"));
		while (input.hasNext())
		{
			Reservation reservation = new Reservation(input, trips);
			reservations.put(reservation.getId(), reservation);
		}
		input.close();
	}
}
