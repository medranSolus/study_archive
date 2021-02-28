package lab05_pop;

import java.util.Random;
import java.util.concurrent.TimeUnit;
public class Product
{
	private static Random random = new Random();
	private int delay = 0;
	private char symbol = 'w';
	
	public Product()
	{
		delay = random.nextInt(15) + 1;
		symbol = (char)(random.nextInt(26) + 97);
	}
	
	public char getSymbol() { return symbol; }
	public void assemble() 
	{
		try
		{
			TimeUnit.SECONDS.sleep(delay);
			symbol = Character.toUpperCase(symbol);
		}
		catch (InterruptedException e)
		{
			e.printStackTrace();
		}
	}
}
