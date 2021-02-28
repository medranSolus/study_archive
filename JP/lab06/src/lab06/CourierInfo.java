package lab06;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.TimeUnit;

public class CourierInfo
{
	private String host = "localhost";
	private int port = 1337;
	private OrderSize size = OrderSize.Small;
	private List<OrderType> orderTypes = new ArrayList<OrderType>();
	
	public CourierInfo(String hostName, int portNumber, OrderSize orderSize)
	{
		host = hostName;
		port = portNumber;
		size = orderSize;
	}
	public CourierInfo(BufferedReader input) throws IOException
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
		size = OrderSize.values()[input.read() - 48];
		buffer = new StringBuffer();
		x = (char)input.read();
		while (x != '_')
		{
			buffer.append(x);
			x = (char)input.read();
		}
		int typesCount = Integer.parseInt(buffer.toString());
		orderTypes = new ArrayList<OrderType>(typesCount);
		for (int i = 0; i < typesCount; ++i)
			orderTypes.add(OrderType.values()[input.read() - 48]);
	}
	
	public String getHost() { return host; }
	public int getPort() { return port; }
	public OrderSize getOrderSize() { return size; }
	public List<OrderType> getOrderTypes() { return orderTypes; }
	public String getAddress() { return host + ":" + port; }

	public void setHost(String newHost) { host = newHost; }
	public void setPort(int newport) { port = newport; }
	public void setOrderSize(OrderSize newSize) { size = newSize; }
	public void addOrderType(OrderType type) { orderTypes.add(type); }
	public void removeOrderType(OrderType type) { orderTypes.remove(type); }
	public boolean canProcessOrder(ClientOrder order) { return order.getSize().ordinal() <= size.ordinal() && orderTypes.contains(order.getType()); }
	
	public int sendOrder(ClientOrder order) throws UnknownHostException, IOException
	{
		Socket socket = new Socket(host, port);
		PrintWriter output = new PrintWriter(socket.getOutputStream());
		BufferedReader input = new BufferedReader(new InputStreamReader(socket.getInputStream()));
		output.print(0);
		output.flush();
		order.send(output);
		output.flush();
		int response = -1;
		for (int i = 0; i < 10; ++i)
		{
			if (input.ready())
			{
				response = input.read() - 48;
				break;
			}
			else
			{
				try
				{
					TimeUnit.SECONDS.sleep(3);
				}
				catch (InterruptedException e)
				{
					e.printStackTrace();
				}
			}
		}
		output.close();
		input.close();
		socket.close();
		return response;
	}
	public int abort(ClientOrder order, String serverHost, int serverPort) throws UnknownHostException, IOException
	{
		Socket socket = new Socket(serverHost, serverPort);
		PrintWriter output = new PrintWriter(socket.getOutputStream());
		BufferedReader input = new BufferedReader(new InputStreamReader(socket.getInputStream()));
		output.print(3);
		output.flush();
		order.send(output);
		output.flush();
		int response = -1;
		for (int i = 0; i < 10; ++i)
		{
			if (input.ready())
			{
				response = input.read() - 48;
				break;
			}
			else
			{
				try
				{
					TimeUnit.SECONDS.sleep(3);
				}
				catch (InterruptedException e)
				{
					e.printStackTrace();
				}
			}
		}
		output.close();
		input.close();
		socket.close();
		return response;
	}
	public int processOrder(ClientOrder order, String serverHost, int serverPort) throws UnknownHostException, IOException
	{
		Socket socket = new Socket(order.getHost(), order.getPort());
		PrintWriter output = new PrintWriter(socket.getOutputStream());
		output.print(0);
		output.flush();
		output.println(order.getOrder());
		output.flush();
		socket.close();
		socket = new Socket(serverHost, serverPort);
		output = new PrintWriter(socket.getOutputStream());
		BufferedReader input = new BufferedReader(new InputStreamReader(socket.getInputStream()));
		output.print(4);
		output.flush();
		int response = -1;
		for (int i = 0; i < 10; ++i)
		{
			if (input.ready())
			{
				response = input.read() - 48;
				break;
			}
			else
			{
				try
				{
					TimeUnit.SECONDS.sleep(3);
				}
				catch (InterruptedException e)
				{
					e.printStackTrace();
				}
			}
		}
		output.close();
		input.close();
		socket.close();
		return response;
	}
	public void send(PrintWriter output)
	{
		output.println(host);
		output.print(port);
		output.print("_");
		output.print(size.ordinal());
		output.print(orderTypes.size());
		output.print("_");
		for (OrderType type : orderTypes)
			output.print(type.ordinal());
	}
}
