package lab07.Board;

import java.math.RoundingMode;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;
import java.rmi.server.UnicastRemoteObject;
import java.text.DecimalFormat;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.concurrent.atomic.AtomicReference;

import lab07.BoardData;
import lab07.IBoard;
import lab07.ICenter;
import lab07.ISensor;
import lab07.SensorCategory;

public class BoardServer extends UnicastRemoteObject implements IBoard
{
	private static final long serialVersionUID = -3631630384280633376L;
	private static DecimalFormat formatter = new DecimalFormat("#.######");
	private long Id = 0;
	private AtomicBoolean connect = new AtomicBoolean(true);
	private Thread connectThread = null;
	private AtomicBoolean update = new AtomicBoolean(true);
	private BoardData data = new BoardData();
	private AtomicReference<String> serverName = new AtomicReference<String>(null);
	private AtomicReference<ISensor> temperatureSensor = new AtomicReference<ISensor>(null);
	private AtomicReference<ISensor> windSensor = new AtomicReference<ISensor>(null);
	private AtomicReference<ISensor> precipationSensor = new AtomicReference<ISensor>(null);

	protected BoardServer() throws RemoteException 
	{ 
		super(); 
		formatter.setRoundingMode(RoundingMode.DOWN);
	}

	@Override public long getBoardId() throws RemoteException { return Id; }
	public long getId() { return Id; }
	public boolean getUpdate() { return update.get(); }
	public long getUpdateFrequency() { return data.update.get(); }
	public String getTemperature() { return temperatureSensor.get() == null ? "No sensor connected" : formatter.format(data.temperature.getValue().get()) + "*C [" +  data.temperature.getKey().get() + "]"; }
	public String getWind() { return windSensor.get() == null ? "No sensor connected" : formatter.format(data.wind.getValue().get()) + " km/h [" +  data.wind.getKey().get() + "]"; }
	public String getPrecipation() { return precipationSensor.get() == null ? "No sensor connected" : formatter.format(data.precipation.getValue().get()) + " mm [" +  data.precipation.getKey().get() + "]"; }
	public void setId(long id) { Id = id; }
	@Override public void setUpdateInterval(long seconds) throws RemoteException { data.update.set(seconds); }
	@Override public boolean toggle() throws RemoteException { return !update.getAndSet(!update.get()); }
	
	@Override public boolean register(ISensor sensor, SensorCategory category) throws RemoteException
	{
		switch (category)
		{
		default:
		case Temperature:
		{
			if (temperatureSensor.get() == null)
			{
				temperatureSensor.set(sensor);
				try
				{
					data.temperature.getKey().set("sensor/" + temperatureSensor.get().getSensorId());
					data.temperature.getValue().set(temperatureSensor.get().getSensorReadings().value.get());
				}
				catch (RemoteException e)
				{
					e.printStackTrace();
				}
				return true;
			}
			return false;
		}
		case Wind:
		{
			if (windSensor.get() == null)
			{
				windSensor.set(sensor);
				try
				{
					data.wind.getKey().set("sensor/" + windSensor.get().getSensorId());
					data.wind.getValue().set(windSensor.get().getSensorReadings().value.get());
				}
				catch (RemoteException e)
				{
					e.printStackTrace();
				}
				return true;
			}
			return false;
		}
		case Precipitation:
		{
			if (precipationSensor.get() == null)
			{
				precipationSensor.set(sensor);
				try
				{
					data.precipation.getKey().set("sensor/" + precipationSensor.get().getSensorId());
					data.precipation.getValue().set(precipationSensor.get().getSensorReadings().value.get());
				}
				catch (RemoteException e)
				{
					e.printStackTrace();
				}
				return true;
			}
			return false;
		}	
		}
	}
	@Override public boolean unregister(long id) throws RemoteException
	{
		if (temperatureSensor.get() != null && temperatureSensor.get().getSensorId() == id)
		{
			temperatureSensor.set(null);
			return true;
		}
		if (windSensor.get() != null && windSensor.get().getSensorId() == id)
		{
			windSensor.set(null);
			return true;
		}
		if (precipationSensor.get() != null && precipationSensor.get().getSensorId() == id)
		{
			precipationSensor.set(null);
			return true;
		}
		return false;
	}
	@Override public boolean disconnectFormServer() throws RemoteException
	{
		serverName.set(null);
		connectToServer();
		return true;
	}
	public void connectToServer()
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
							if (name.substring(0, 7).equals("server/"))
								if (((ICenter)registry.lookup(name)).register(this))
								{
									serverName.set(name);
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
		connectThread.setName("Server connect thread");
		connectThread.start();
	}
	public void update()
	{
		if (temperatureSensor.get() != null)
		{
			try
			{
				data.temperature.getValue().set(temperatureSensor.get().getSensorReadings().value.get());
			}
			catch (RemoteException e)
			{
				e.printStackTrace();
			}
		}
		else
		{
			data.temperature.getKey().set(null);
			data.temperature.getValue().set(null);
		}
		if (windSensor.get() != null)
		{
			try
			{
				data.wind.getValue().set(windSensor.get().getSensorReadings().value.get());
			}
			catch (RemoteException e)
			{
				e.printStackTrace();
			}
		}
		else
		{
			data.wind.getKey().set(null);
			data.wind.getValue().set(null);
		}
		if (precipationSensor.get() != null)
		{
			try
			{
				data.precipation.getValue().set(precipationSensor.get().getSensorReadings().value.get());
			}
			catch (RemoteException e)
			{
				e.printStackTrace();
			}
		}
		else
		{
			data.precipation.getKey().set(null);
			data.precipation.getValue().set(null);
		}
		if (serverName.get() != null)
		{
			try
			{
				((ICenter)LocateRegistry.getRegistry(1099).lookup(serverName.get())).sendData(Id, data);
			}
			catch (RemoteException | NotBoundException e)
			{
				e.printStackTrace();
			}
		}
	}
	public void close()
	{
		connect.set(false);
		update.set(false);
		if (serverName.get() != null)
		{
			try
			{
				((ICenter)LocateRegistry.getRegistry(1099).lookup(serverName.get())).unregister(Id);
			}
			catch (RemoteException | NotBoundException e)
			{
				e.printStackTrace();
			}
		}
		if (temperatureSensor.get() != null)
		{
			try
			{
				temperatureSensor.get().disconnectFromBoard();
			}
			catch (RemoteException e)
			{
				e.printStackTrace();
			}
			temperatureSensor.set(null);
		}
		if (windSensor.get() != null)
		{
			try
			{
				windSensor.get().disconnectFromBoard();
			}
			catch (RemoteException e)
			{
				e.printStackTrace();
			}
			windSensor.set(null);
		}
		if (precipationSensor.get() != null)
		{
			try
			{
				precipationSensor.get().disconnectFromBoard();
			}
			catch (RemoteException e)
			{
				e.printStackTrace();
			}
			precipationSensor.set(null);
		}
	}
}
