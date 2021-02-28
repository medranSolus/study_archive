package lab07.Sensor;

import java.math.RoundingMode;
import java.net.URL;
import java.rmi.AlreadyBoundException;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;
import java.text.DecimalFormat;
import java.util.Optional;
import java.util.Random;
import java.util.ResourceBundle;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.concurrent.atomic.AtomicLong;

import javafx.application.Platform;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TextInputDialog;
import javafx.scene.control.Alert.AlertType;
import javafx.scene.input.MouseEvent;
import lab07.SensorCategory;

public class Sensor implements Initializable
{
	private static DecimalFormat formatter = new DecimalFormat("#.######");
	private Thread updateThread = null;
	private AtomicBoolean update = new AtomicBoolean(true);
	private AtomicLong updateFrequency = new AtomicLong(5);
	private SensorServer server = null;
	private SensorCategory sensorCategory;
	@FXML private Button buttonChangeUpdate;
	@FXML private Label labelUpdate;
	@FXML private Label labelValue;

	@Override
	public void initialize(URL location, ResourceBundle resources)
	{
		formatter.setRoundingMode(RoundingMode.DOWN);
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
			server = new SensorServer();
			for (long i = 1; ; ++i)
			{
				try
				{
					registry.bind("sensor/" + server.getId(), server);
					server.connectToBoard();
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
			Random random = new Random();
			final String type;
			switch (sensorCategory)
			{
			default:
			case Temperature:
			{
				type = "*C";
				break;
			}
			case Wind:
			{
				type = " km/h";
				break;
			}
			case Precipitation:
			{
				type = " mm";
				break;
			}
			}
			while (update.get())
			{
				switch (sensorCategory)
				{
				case Temperature:
				{
					server.setDataValue((double)random.nextInt(110) - 60.0 + random.nextGaussian());
					break;
				}
				case Wind:
				{
					server.setDataValue(random.nextDouble()*250);
					break;
				}
				case Precipitation:
				{
					server.setDataValue(random.nextDouble()*990);
					break;
				}
				}
				try
				{
					Double value = server.getSensorReadings().value.get();
					Platform.runLater(() -> labelValue.setText("Measured value: " + formatter.format(value) + type));
				}
				catch (RemoteException e1)
				{
					e1.printStackTrace();
				}
				try
				{
					if (updateFrequency.longValue() != 0)
						TimeUnit.SECONDS.sleep(updateFrequency.longValue());
					else
						TimeUnit.SECONDS.sleep(1);
				}
				catch (InterruptedException e)
				{
					e.printStackTrace();
				}
			}
		});
		updateThread.setName("Measurement thread");
		labelUpdate.setText("Update frequency: " + updateFrequency.longValue() + " [s]");
		try
		{
			labelValue.setText("Measured value: " + server.getSensorReadings().value.get());
		}
		catch (RemoteException e)
		{
			e.printStackTrace();
		}
	}
	
	@FXML private void buttonChangeUpdateClicked(MouseEvent e)
	{
		TextInputDialog dialog = new TextInputDialog("");
		dialog.setTitle("Update frequency change");
		dialog.setHeaderText(null);
		dialog.setContentText("Enter update frequency in seconds: ");
		Optional<String> result = dialog.showAndWait();
		result.ifPresent(newFrequency -> 
		{
			if (tryParseInt(newFrequency))
			{
				updateFrequency.set(Long.parseLong(newFrequency));
				labelUpdate.setText("Update frequency: " + updateFrequency.longValue() + " [s]");
			}
			else
			{
				Alert alert = new Alert(AlertType.ERROR);
				alert.setTitle("Wrong format");
				alert.setHeaderText(null);
				alert.setContentText("Entered frequency is not a number!");
				alert.showAndWait();
			}
		});
	}
	
	private boolean tryParseInt(String x)
	{
		try
		{
			Integer.parseInt(x);
			return true;
		}
		catch (NumberFormatException e)
		{
			return false;
		}
	}
	
	public void shutdown()
	{
		update.set(false);
		try
		{
			Registry registry = LocateRegistry.getRegistry(1099);
			registry.unbind("sensor/" + server.getId());
		}
		catch (RemoteException | NotBoundException e)
		{
			e.printStackTrace();
		}
		server.close();
	}
	
	public void setType(SensorCategory newCategory)
	{
		server.setDataCategory(newCategory);
		sensorCategory = newCategory;
		updateThread.start();
	}
}
