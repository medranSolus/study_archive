package lab07.Center;

import java.net.URL;
import java.rmi.AlreadyBoundException;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;
import java.util.Optional;
import java.util.ResourceBundle;

import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Alert;
import javafx.scene.control.TextInputDialog;
import javafx.scene.control.Alert.AlertType;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.control.Button;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.input.MouseEvent;

public class Center implements Initializable
{
	private CenterServer server = null;
	@FXML private TableView<DataModel> tableViewData;
	@FXML private TableColumn<DataModel, String> tableColumnName;
	@FXML private TableColumn<DataModel, Double> tableColumnTemperature;
	@FXML private TableColumn<DataModel, Double> tableColumnWind;
	@FXML private TableColumn<DataModel, Double> tableColumnPrecipation;
	@FXML private TableColumn<DataModel, Long> tableColumnUpdate;
	@FXML private Button buttonChangeUpdate;
	@FXML private Button buttonONOFF;
	
	@Override
	public void initialize(URL location, ResourceBundle resources)
	{
		tableColumnName.setCellValueFactory(new PropertyValueFactory<DataModel, String>("boardName"));
		tableColumnTemperature.setCellValueFactory(new PropertyValueFactory<DataModel, Double>("temperature"));
		tableColumnWind.setCellValueFactory(new PropertyValueFactory<DataModel, Double>("wind"));
		tableColumnPrecipation.setCellValueFactory(new PropertyValueFactory<DataModel, Double>("precipation"));
		tableColumnUpdate.setCellValueFactory(new PropertyValueFactory<DataModel, Long>("update"));
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
			server = new CenterServer(tableViewData);
			for (long i = 1; ; ++i)
			{
				try
				{
					registry.bind("server/" + server.getId(), server);
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
				try
				{
					long frequency = Long.parseLong(newFrequency);
					server.changeUpdate(tableViewData.getSelectionModel().getSelectedItem().boardName, frequency);
					tableViewData.getSelectionModel().getSelectedItem().update = frequency;
				}
				catch (RemoteException e1)
				{
					e1.printStackTrace();
				}
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
	
	@FXML private void buttonONOFFClicked(MouseEvent e)
	{
		try
		{
			if (server.toggleUpdate(tableViewData.getSelectionModel().getSelectedItem().boardName))
				tableViewData.setStyle("");
			else	
				tableViewData.setStyle("-fx-background-color: #FF3333;");
		}
		catch (RemoteException e1)
		{
			e1.printStackTrace();
		}
	}
	
	@FXML private void tableViewDataClicked(MouseEvent e)
	{
		if (tableViewData.getSelectionModel().getSelectedItem() != null)
		{
			buttonChangeUpdate.setDisable(false);
			buttonONOFF.setDisable(false);
		}
		else
		{
			buttonChangeUpdate.setDisable(true);
			buttonONOFF.setDisable(true);
		}
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
		try
		{
			Registry registry = LocateRegistry.getRegistry(1099);
			registry.unbind("server/" + server.getId());
		}
		catch (RemoteException | NotBoundException e)
		{
			e.printStackTrace();
		}
		server.close();
	}
}
