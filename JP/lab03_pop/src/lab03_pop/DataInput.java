package lab03_pop;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.LinkedList;
import java.util.List;

public class DataInput 
{
	public static DateTimeFormatter formatter = DateTimeFormatter.ofPattern("HH:mm dd-MM-yyyy");
	
	static int readInt(BufferedReader input)
	{
		try
		{
			return Integer.parseInt(input.readLine());
		}
		catch (NumberFormatException | IOException e)
		{
			System.out.println("Error reading integer: " + e.getMessage());
			System.out.println("Try again: ");
			return readInt(input);
		}
	}
	
	static String readLine(BufferedReader input)
	{
		try
		{
			return input.readLine();
		}
		catch (IOException e)
		{
			System.out.println("Error reading string: " + e.getMessage());
			System.out.println("Try again: ");
			return readLine(input);
		}
	}
	
	public static int money()
	{
		BufferedReader input = new BufferedReader(new InputStreamReader(System.in));
		int money = readInt(input);
		while (money < 0)
		{
			System.out.print("Amount of money must be obove 0, try again: ");
			money = readInt(input);
		}
		return money;
	}

	public static int menu(int optionCount)
	{
		BufferedReader input = new BufferedReader(new InputStreamReader(System.in));
		int option = readInt(input);
		while (option < 1 || option > optionCount)
		{
			System.out.print("Enter number between 1 and " + optionCount + ": ");
			option = readInt(input);
		}
		return option;
	}
	
	public static List<Client> chooseClients(Trip trip, TripAgency agency)
	{
		BufferedReader input = new BufferedReader(new InputStreamReader(System.in));
		List<Client> client = new LinkedList<Client>();
		System.out.print("Enter client IDs to make reservation for (enter -1 to end, places left in trip: " + trip.freeSlots() + "): ");
		for (int id = 0; ;)
		{
			id = readInt(input);
			if (id == -1)
			{
				if (client.size() == 0)
					System.out.print("Cannot create reservation with no clients, enter 1 client at least: ");
				else
					break;
			}
			else if (!agency.containsClient(id))
				System.out.print("There is no such client, try again: ");
			else
				client.add(agency.getClient(id));
		}
		return client;
	} 
	
	public static int tripIdInput(TripAgency agency)
	{
		BufferedReader input = new BufferedReader(new InputStreamReader(System.in));
		int tripId = readInt(input);
		while (!agency.containsTrip(tripId))
		{
			System.out.print("Trip not found, try again: ");
			tripId = readInt(input);
		}
		return tripId;
		
	}
	
	public static int reservationIdInput(TripAgency  agency)
	{
		BufferedReader input = new BufferedReader(new InputStreamReader(System.in));
		int reservationId = readInt(input);
		while (!agency.containsReservation(reservationId))
		{
			System.out.print("Reservation not found, try again: ");
			reservationId = readInt(input);
		}
		return reservationId;
	}
	
	public static void changeCurrentDate(TripAgency agency)
	{
		BufferedReader input = new BufferedReader(new InputStreamReader(System.in));
		System.out.print("Enter new current date (format: HH:mm dd-MM-yyyy): ");
		agency.setCurrentDate(LocalDateTime.parse(readLine(input), formatter));
	}
	
	public static Client createClient()
	{
		BufferedReader input = new BufferedReader(new InputStreamReader(System.in));
		System.out.print("Enter name: ");
		String name = readLine(input);
		System.out.print("Enter surname: ");
		String surname = readLine(input);
		return new Client(name, surname);
	}
	
	public static Trip createTrip()
	{
		BufferedReader input = new BufferedReader(new InputStreamReader(System.in));
		System.out.print("Enter name of a trip: ");
		String name = readLine(input);
		System.out.print("Enter beginning time of a trip (format: HH:mm dd-MM-yyyy): ");
		LocalDateTime startTime = LocalDateTime.parse(readLine(input), formatter);
		System.out.print("Enter end time of a trip (format: HH:mm dd-MM-yyyy): ");
		LocalDateTime endTime = LocalDateTime.parse(readLine(input), formatter);
		System.out.println("Enter country of a trip:");
		String country = readLine(input);
		System.out.println("Enter city of a trip:");
		String city = readLine(input);
		System.out.println("Enter description of a trip:");
		String description = readLine(input);
		System.out.print("Enter maximal number of people to go on a trip: ");
		int maxSlots = readInt(input);
		System.out.print("Enter cost per persona: ");
		int costPerClient = readInt(input);
		return new Trip(name, startTime, endTime, country, city, description, maxSlots, costPerClient);
	}
	
	public static Reservation createReservation(Trip trip, List<Client> clients)
	{
		BufferedReader input = new BufferedReader(new InputStreamReader(System.in));
		System.out.print("Enter expiration time of payment (format: HH:mm dd-MM-yyyy): ");
		LocalDateTime paymentDate = LocalDateTime.parse(readLine(input), formatter);
		return new Reservation(paymentDate, trip, clients);
	}
	
	public static LocalDateTime[] getTimeRange()
	{
		BufferedReader input = new BufferedReader(new InputStreamReader(System.in));
		LocalDateTime[] range = new LocalDateTime[2];
		System.out.print("Enter beginning time of a search range (format: HH:mm dd-MM-yyyy): ");
		range[0] = LocalDateTime.parse(readLine(input), formatter);
		System.out.print("Enter end time of a search range (format: HH:mm dd-MM-yyyy): ");
		range[1] = LocalDateTime.parse(readLine(input), formatter);
		return range;
	}

	public static TripQuery searchForTrip()
	{
		BufferedReader input = new BufferedReader(new InputStreamReader(System.in));
		int option, maxCost = TripQuery.defaultMaxCostPerPerson(), minSlots = TripQuery.defaultMinimalFreeSlots();
		LocalDateTime start = TripQuery.defaultStartTime(), end = TripQuery.defaultEndTime();
		String country = TripQuery.defaultCountry(), city = TripQuery.defaultCity();
		while(true)
		{
			System.out.println("Trip query mode");
			System.out.println("Choose witch trip property to specify:");
			System.out.println("[1] Start time");
			System.out.println("[2] End time");
			System.out.println("[3] Max cost per persona");	
			System.out.println("[4] Minimal free slots");	
			System.out.println("[5] Country");
			System.out.println("[6] City");
			System.out.println("[7] Execute query");
			option = menu(7);
			switch (option)
			{
			case 1:
			{
				System.out.print("Enter minimal beginning time of a trip (format: HH:mm dd-MM-yyyy): ");
				start = LocalDateTime.parse(readLine(input), formatter);
				break;
			}
			case 2:
			{
				System.out.print("Enter maximal end time of a trip (format: HH:mm dd-MM-yyyy): ");
				end = LocalDateTime.parse(readLine(input), formatter);
				break;
			}
			case 3:
			{
				System.out.print("Enter maximal cost per persona: ");
				maxCost = readInt(input);
				break;
			}
			case 4:
			{
				System.out.print("Enter minimal free slots in trip: ");
				minSlots = readInt(input);
				break;
			}
			case 5:
			{
				System.out.print("Enter prefered country of a trip: ");
				country = readLine(input);
				break;
			}
			case 6:
			{
				System.out.print("Enter prefered country of a trip: ");
				country = readLine(input);
				System.out.print("Enter prefered city of a trip: ");
				city = readLine(input);
				break;
			}
			case 7:
				return new TripQuery(start, end, maxCost, minSlots, country, city);
			}
			
		}
	}
	
	public static Trip updateTrip(Trip trip)
	{
		BufferedReader input = new BufferedReader(new InputStreamReader(System.in));
		int option;
		while(true)
		{
			System.out.println("Trip edition mode (" + trip.getName() + ")");
			System.out.println("Choose witch option to update:");
			System.out.println("[1] Name");
			System.out.println("[2] Start time");
			System.out.println("[3] End time");
			System.out.println("[4] Country");
			System.out.println("[5] City");
			System.out.println("[6] Description");
			System.out.println("[7] Max number of clients");
			System.out.println("[8] Cost per client");
			System.out.println("[9] Save & exit");
			option = menu(9);
			switch (option)
			{
			case 1:
			{
				System.out.print("Enter new name of a trip: ");
				trip.setName(readLine(input));
				break;
			}
			case 2:
			{
				System.out.print("Enter new beginning time of a trip (format: HH:mm dd-MM-yyyy): ");
				trip.setStartTime(LocalDateTime.parse(readLine(input), formatter));
				break;
			}
			case 3:
			{
				System.out.print("Enter new end time of a trip (format: HH:mm dd-MM-yyyy): ");
				trip.setEndTime(LocalDateTime.parse(readLine(input), formatter));
				break;
			}
			case 4:
			{
				System.out.print("Enter new country of a trip: ");
				trip.setCountry(readLine(input));
				System.out.print("Enter new city of a trip: ");
				trip.setCity(readLine(input));
				break;
			}
			case 5:
			{
				System.out.print("Enter new city of a trip: ");
				trip.setCity(readLine(input));
				break;
			}
			case 6:
			{
				System.out.println("Enter new description of a trip:");
				trip.setDescription(readLine(input));
				break;
			}
			case 7:
			{
				System.out.print("Enter new maximal number of people to go on a trip: ");
				trip.setMaxSlots(Integer.parseInt(readLine(input)));
				break;
			}
			case 8:
			{
				System.out.print("Enter new cost per persona: ");
				trip.setCostPerClient(Integer.parseInt(readLine(input)));
				break;
			}
			case 9:
			{
				System.out.println("Trip " + trip.getName() + " has been updated.");
				return trip;
			}
			}
			
		}
	}
}
