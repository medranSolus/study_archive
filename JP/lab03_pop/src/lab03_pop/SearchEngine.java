package lab03_pop;

import java.time.LocalDateTime;
import java.util.TreeMap;
import java.util.List;
import java.util.Map;
import javafx.util.Pair;

public class SearchEngine
{
	public static void bestTrips(List<Trip> trips)
	{
		trips.sort((trip1, trip2) -> trip2.clientsCount() - trip1.clientsCount());
		int i = 1;
		System.out.println("------------------------");
		for (var trip : trips)
			System.out.println(i++ + ". [" + trip.getId() + "] " + trip.getName() + "\n   Clients: " + trip.clientsCount());
		System.out.println("------------------------");
	}
	
	public static void tripSearch(List<Trip> trips, TripQuery query)
	{
		trips.removeIf(trip -> trip.getStartTime().isBefore(query.getStartTime()) || trip.getEndTime().isAfter(query.getEndTime()) || trip.getCostPerClient() > query.getMaxCostPerPerson() || trip.freeSlots() < query.getMinimalFreeSlots());
		if (!query.getCity().equals(TripQuery.defaultCity()))
			trips.removeIf(trip -> !trip.getCountry().equals(query.getCountry()) && !trip.getCity().equals(query.getCity()));
		else if (!query.getCountry().equals(TripQuery.defaultCountry()))
				trips.removeIf(trip -> !trip.getCountry().equals(query.getCountry()));
		System.out.println("------------------------");
		for (var trip : trips)
			trip.show();
		System.out.println("------------------------");
	}
	
	public static void clientActivity(List<Client> clients, List<Trip> trips, LocalDateTime[] time)
	{
		trips.removeIf(trip -> trip.getStartTime().isAfter(time[1]) || trip.getEndTime().isBefore(time[0]));
		Map<Integer, Pair<Integer, Client>> statistics = new TreeMap<Integer, Pair<Integer, Client>>();
		for (var trip : trips)
		{
			for (var client : trip.getEnlistedClients())
			{
				if(statistics.containsKey(client.getId()))
					statistics.replace(client.getId(), new Pair<Integer, Client>(statistics.get(client.getId()).getKey() + 1, client));
				else
					statistics.put(client.getId(), new Pair<Integer, Client>(1, client));
			}
		}
		for (var client : clients)
			if(!statistics.containsKey(client.getId()))
				statistics.put(client.getId(), new Pair<Integer, Client>(0, client));
		System.out.println("------------------------");
		for (var stats : statistics.values())
			System.out.println(stats.getValue().getName() + " " + stats.getValue().getSurname() + " was on " + stats.getKey() + " trips");
		System.out.println("------------------------");
	}
}
