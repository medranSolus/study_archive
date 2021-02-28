package lab06;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.PrintWriter;

public class ClientOrder
{
	private String host = "localhost";
	private int port = 1337;
	private OrderType type = OrderType.AGD;
	private OrderSize size = OrderSize.Small;
	private String order = "none";
	
	public ClientOrder(String hostName, int portNumber, OrderType orderType, OrderSize orderSize, String itemToOrder)
	{
		host = hostName;
		port = portNumber;
		type = orderType;
		size = orderSize;
		order = itemToOrder;
	}
	public ClientOrder(BufferedReader input) throws IOException
	{
		host = input.readLine();
		StringBuffer buffer = new StringBuffer();
		char x = (char)input.read();
		while (x != '_')
		{
			buffer.append(x);
			x = (char)input.read();
		}
		port = Integer.parseInt(buffer.toString());
		buffer = new StringBuffer();
		x = (char)input.read();
		while (x != '_')
		{
			buffer.append(x);
			x = (char)input.read();
		}
		type = OrderType.values()[Integer.parseInt(buffer.toString())];
		size = OrderSize.values()[input.read() - 48];
		order = input.readLine();
	}

	public String getHost() { return host; }
	public int getPort() { return port; }
	public OrderType getType() { return type; }
	public OrderSize getSize() { return size; }
	public String getOrder() { return order; }
	public String getAddress() { return host + ":" + port; }
	
	public void send(PrintWriter output)
	{
		output.println(host);
		output.print(port);
		output.print("_");
		output.print(type.ordinal());
		output.print("_");
		output.print(size.ordinal());
		output.println(order);
	}
}
