package lab07.Board;

import java.net.URL;
import java.rmi.AlreadyBoundException;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;
import java.util.ResourceBundle;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicBoolean;

import javafx.application.Platform;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Label;

public class Board implements Initializable
{
	private AtomicBoolean run = new AtomicBoolean(true);
	private Thread updateThread = null;
	private BoardServer server = null;
	@FXML private Label labelTemperature;
	@FXML private Label labelWind;
	@FXML private Label labelPrecipation;
	
	@Override public void initialize(URL location, ResourceBundle resources)
	{
		Registry registry = null;
		try
		{
			registry = LocateRegistry.createRegistry(1099);
		}
		catch (RemoteException e2)
		{
			try
			{
				registry = LocateRegistry.getRegistry(1099);
			}
			catch (RemoteException e)
			{
				e.printStackTrace();
			}
		}
		try
		{
			server = new BoardServer();
			for (long i = 1; ; ++i)
			{
				try
				{
					registry.bind("board/" + server.getId(), server);
					server.connectToServer();
					break;
				}
				catch (AlreadyBoundException e)
				{
					server.setId(i);
				}
			}
		}
		catch (RemoteException e1)
		{
			e1.printStackTrace();
		}
		updateThread = new Thread(() -> 
		{
			while (run.get())
			{
				if (server.getUpdate())
				{
					server.update();
					Platform.runLater(() ->
					{
						labelTemperature.setText("Temperature: " + server.getTemperature());
						labelWind.setText("Wind: " + server.getWind());
						labelPrecipation.setText("Precipation: " + server.getPrecipation());
					});
				}
				try
				{
					TimeUnit.SECONDS.sleep(server.getUpdateFrequency());
				}
				catch (InterruptedException e)
				{
					e.printStackTrace();
				}
			}
		});
		updateThread.setName("Update thread");
		updateThread.start();
	}
	
	public void shutdown()
	{
		try
		{
			Registry registry = LocateRegistry.getRegistry(1099);
			registry.unbind("board/" + server.getId());
		}
		catch (RemoteException | NotBoundException e)
		{
			e.printStackTrace();
		}
		server.close();
	}
}
