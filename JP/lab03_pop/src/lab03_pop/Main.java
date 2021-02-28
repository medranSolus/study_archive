package lab03_pop;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;

public class Main 
{
	String sourcePath = "res\\";
	TripAgency agency = new TripAgency();
	
	public static void main(String[] args) 
	{
		Main program = new Main();
		program.Run();
	}
	
	public void Run()
	{
		try 
		{ 
			agency.read(sourcePath); 
		} 
		catch (FileNotFoundException e) 
		{
			try 
			{
				PrintWriter output = new PrintWriter(new BufferedWriter(new FileWriter(new File("error.txt"))));
				output.println(e.getMessage());
				output.close();
			}
			catch(IOException e1) {}
		}
		int option = 0;
		while(true)
		{
			menu();
			option = DataInput.menu(12);
			switch (option)
			{
			case 1:
			{
				menuShowList();
				option = DataInput.menu(4);
				switch (option)
				{
				case 1:
				{
					agency.showClients();
					break;
				}
				case 2:
				{
					agency.showTrips(false);
					break;
				}
				case 3:
				{
					agency.showReservations(false);
					break;
				}
				case 4:
					break;
				}
				break;
			}
			case 2:
			{
				menuCreation();
				option = DataInput.menu(4);
				switch (option)
				{
				case 1:
				{
					agency.addClient(DataInput.createClient());
					break;
				}
				case 2:
				{
					agency.addTrip(DataInput.createTrip());
					break;
				}
				case 3:
				{
					if (agency.tripsCount() == 0 )
					{
						System.out.println("No trips to select");
						break;
					}
					if (agency.clientsCount() == 0 )
					{
						System.out.println("No clients to select");
						break;
					}
					System.out.println("Choose trip to reserve:");
					agency.showTrips(true);
					int tripId = DataInput.tripIdInput(agency);
					agency.addReservation(DataInput.createReservation(agency.getTrip(tripId), DataInput.chooseClients(agency.getTrip(tripId), agency)));
					break;
				}
				case 4:
					break;
				}
				break;
			}
			case 3:
			{
				if (agency.tripsCount() == 0 )
				{
					System.out.println("No trips to select");
					break;
				}
				System.out.println("Choose trip to update:");
				agency.showTrips(true);
				agency.updateTrip(DataInput.updateTrip(agency.getTrip(DataInput.tripIdInput(agency))));
				break;
			}
			case 4:
			{
				if (agency.tripsCount() == 0 )
				{
					System.out.println("No trips to select");
					break;
				}
				System.out.println("Choose trip to delete:");
				agency.showTrips(true);
				agency.deleteTrip(DataInput.tripIdInput(agency));
				break;
			}
			case 5:
			{
				if (agency.reservationsCount() == 0 )
				{
					System.out.println("No reservations to select");
					break;
				}
				System.out.print("Amount of money you have right now: ");
				int money = DataInput.money();
				System.out.println("Choose reservation:");
				agency.showReservations(true);
				agency.payForReservation(DataInput.reservationIdInput(agency), money);
				break;
			}
			case 6:
			{
				if (agency.reservationsCount() == 0 )
				{
					System.out.println("No reservations to select");
					break;
				}
				System.out.println("Choose reservation:");
				agency.showReservations(true);
				agency.cancelReservation(DataInput.reservationIdInput(agency));
				break;
			}
			case 7:
			{
				SearchEngine.tripSearch(agency.getTrips(), DataInput.searchForTrip());
				break;
			}
			case 8:
			{
				SearchEngine.clientActivity(agency.getClients(), agency.getTrips(), DataInput.getTimeRange());
				break;
			}
			case 9:
			{
				SearchEngine.bestTrips(agency.getTrips());
				break;
			}
			case 10:
			{
				DataInput.changeCurrentDate(agency);
				break;
			}
			case 11:
			{
				try
				{
					agency.save(sourcePath);
				}
				catch (IOException e)
				{
					System.out.println("Error: Unable to save database.");
					try 
					{
						PrintWriter output = new PrintWriter(new BufferedWriter(new FileWriter(new File("error.txt"))));
						output.println(e.getMessage());
						output.close();
					} 
					catch (IOException e1) 
					{
						System.out.println("Error: Permission denied to use files.");
					}
					break;
				}
				System.out.println("Database successfully saved.");
				break;
			}
			case 12:
			{
				try
				{
					agency.save(sourcePath);
				}
				catch (IOException e)
				{
					try 
					{
						PrintWriter output = new PrintWriter(new BufferedWriter(new FileWriter(new File("error.txt"))));
						output.println(e.getMessage());
						output.close();
					} 
					catch (IOException e1)  {}
				}
				return;
			}
			}
		}
	}
	
	void menu()
	{;
		System.out.println("[1] Show list of...");
		System.out.println("[2] Create new...");
		System.out.println("[3] Change properties of a trip");
		System.out.println("[4] Delete trip");
		System.out.println("[5] Pay for reservation");
		System.out.println("[6] Cancel reservation");
		System.out.println("[7] Search for trip");
		System.out.println("[8] Clients trips statistics");
		System.out.println("[9] Best trips");
		System.out.println("[10] Change current date (" + agency.getCurrentDate().format(DataInput.formatter) + ")");
		System.out.println("[11] Save database");
		System.out.println("[12] Exit");
	}
	void menuShowList()
	{
		System.out.println("----------------");
		System.out.println("[1] Clients");
		System.out.println("[2] Trips");
		System.out.println("[3] Reservations");
		System.out.println("[4] <-Back");
		System.out.println("----------------");
	}
	void menuCreation()
	{
		System.out.println("----------------");
		System.out.println("[1] Client");
		System.out.println("[2] Trip");
		System.out.println("[3] Reservation");
		System.out.println("[4] <-Back");
		System.out.println("----------------");
	}
	
}
