package lab07.Sensor;

import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;
import java.rmi.server.UnicastRemoteObject;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.concurrent.atomic.AtomicReference;

import lab07.IBoard;
import lab07.ISensor;
import lab07.SensorCategory;
import lab07.SensorReadings;

public class SensorServer extends UnicastRemoteObject implements ISensor
{
	private static final long serialVersionUID = 1365416868191319263L;
	private long Id = 0;
	private SensorReadings data = new SensorReadings();
	private AtomicBoolean connect = new AtomicBoolean(true);
	private Thread connectThread = null;
	private AtomicReference<String> boardName = new AtomicReference<String>(null);
	
	protected SensorServer() throws RemoteException { super(); }

	@Override public SensorReadings getSensorReadings() throws RemoteException { return data; }
	@Override public SensorCategory getCategory() throws RemoteException { return data.category; }
	@Override public long getSensorId() throws RemoteException { return Id; }
	public long getId() { return Id; }
	public void setId(long id) { Id = id; }
	public void setDataCategory(SensorCategory category) { data.category = category; }
	public void setDataValue(Double value) { data.value.set(value); }

	@Override public boolean disconnectFromBoard() throws RemoteException
	{
		boardName.set(null);
		connectToBoard();
		return true;
	}
	public void connectToBoard()
	{
		connectThread = new Thread(() ->
		{
			try
			{
				Registry registry = LocateRegistry.getRegistry(1099);
				while (connect.get())
				{
					try
					{
						String[] names = registry.list();
						for (String name : names)
							if (name.substring(0, 6).equals("board/"))
								if (((IBoard)registry.lookup(name)).register(this, data.category))
								{
									boardName.set(name);
									return;
								}
					}
					catch (RemoteException | NotBoundException e)
					{
						e.printStackTrace();
					}
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
			catch (RemoteException e)
			{
				e.printStackTrace();
			}
		});
		connectThread.setName("Board connect thread");
		connectThread.start();
	}
	public void close()
	{
		connect.set(false);
		if (boardName.get() != null)
		{
			try
			{
				((IBoard)LocateRegistry.getRegistry(1099).lookup(boardName.get())).unregister(Id);
			}
			catch (RemoteException | NotBoundException e)
			{
				e.printStackTrace();
			}
		}
	}
	
}