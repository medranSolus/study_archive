package lab03_pop;

import java.time.LocalDateTime;

public class TripQuery
{
	LocalDateTime startTime = LocalDateTime.now();
	LocalDateTime endTime = LocalDateTime.MAX;
	int maxCostPerPerson = Integer.MAX_VALUE;
	int minimalFreeSlots = 0;
	String country = "all";
	String city = "all";
	
	public TripQuery(LocalDateTime newStartTime, LocalDateTime newEndTime, int newMaxCostPerPerson, int newMinimalFreeSlots, String newCountry, String newCity)
	{
		startTime = newStartTime;
		endTime = newEndTime;
		maxCostPerPerson = newMaxCostPerPerson;
		country = newCountry;
		city = newCountry;
	}
	
	public LocalDateTime getStartTime() { return startTime; }
	public LocalDateTime getEndTime() { return endTime; }
	public int getMaxCostPerPerson() { return maxCostPerPerson; }
	public int getMinimalFreeSlots() { return minimalFreeSlots; }
	public String getCountry() { return country; }
	public String getCity() { return city; }
	
	public static LocalDateTime defaultStartTime() { return LocalDateTime.now(); }
	public static LocalDateTime defaultEndTime() { return LocalDateTime.MAX; }
	public static int defaultMaxCostPerPerson() { return Integer.MAX_VALUE; }
	public static int defaultMinimalFreeSlots() { return 0; }
	public static String defaultCountry() { return "all"; }
	public static String defaultCity() { return "all"; }
}
