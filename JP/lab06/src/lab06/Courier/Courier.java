package lab06.Courier;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.URL;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.Optional;
import java.util.ResourceBundle;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicBoolean;

import javafx.application.Platform;
import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.collections.FXCollections;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.CheckBox;
import javafx.scene.control.ChoiceBox;
import javafx.scene.control.Label;
import javafx.scene.control.ListView;
import javafx.scene.control.TextField;
import javafx.scene.control.TextInputDialog;
import javafx.scene.control.ToggleButton;
import javafx.scene.control.Alert.AlertType;
import javafx.scene.input.MouseEvent;
import lab06.ClientOrder;
import lab06.CourierInfo;
import lab06.OrderSize;
import lab06.OrderType;

public class Courier implements Initializable
{
	private DateTimeFormatter formatter = DateTimeFormatter.ofPattern("HH:mm dd-MM-yyyy");
	private ClientOrder order = null;
	private CourierInfo info = new CourierInfo("localhost", 5555, OrderSize.Small);
	private ServerSocket orderServer = null;
	private Thread ordersListener = null;
	private AtomicBoolean listen = new AtomicBoolean(false);
	private String serverHost = "localhost";
	private int serverPort = 6667;
	@FXML private TextField textFieldHost;
	@FXML private TextField textFieldPort;
	@FXML private ToggleButton toogleButtonServer;
	@FXML private ListView<String> listViewLog;
	@FXML private Button buttonDeliver;
	@FXML private Button buttonChangePort;
	@FXML private Label labelOrder;
	@FXML private Label labelPort;
	@FXML private CheckBox checkBoxBooks;
	@FXML private CheckBox checkBoxMovies;
	@FXML private CheckBox checkBoxMusic;
	@FXML private CheckBox checkBoxElectronics;
	@FXML private CheckBox checkBoxGames;
	@FXML private CheckBox checkBoxAGD;
	@FXML private CheckBox checkBoxComputers;
	@FXML private CheckBox checkBoxRTV;
	@FXML private CheckBox checkBoxFood;
	@FXML private CheckBox checkBoxPlants;
	@FXML private ChoiceBox<OrderSize> choiceBoxSize;
	
	@Override public void initialize(URL location, ResourceBundle resources)
	{
		info.addOrderType(OrderType.Books);
		choiceBoxSize.setItems(FXCollections.observableArrayList(OrderSize.Small, OrderSize.Medium, OrderSize.Huge));
		choiceBoxSize.getSelectionModel().selectFirst();
		choiceBoxSize.getSelectionModel().selectedIndexProperty().addListener(new ChangeListener<Number>() 
		{
			@Override public void changed(ObservableValue<? extends Number> observableValue, Number oldValue, Number newValue) 
			{
				info.setOrderSize(OrderSize.values()[newValue.intValue()]); 
			}
	    });
		labelPort.setText("Courier port: " + info.getPort());
	}
	
	private boolean startOrderListener()
	{
		serverHost = textFieldHost.getText();
		if (tryParseInt(textFieldPort.getText()))
			serverPort = Integer.parseInt(textFieldPort.getText());
		else
			return false;
		listen.set(true);
		ordersListener = new Thread(() ->
		{
			while (listen.get())
			{
				if (order == null)
				{
					try
					{
						Socket socket = orderServer.accept();
						BufferedReader input = new BufferedReader(new InputStreamReader(socket.getInputStream()));
						PrintWriter output = new PrintWriter(socket.getOutputStream());
						int connectionType = input.read() - 48;
						switch (connectionType)
						{
						case 0:
						{
							order = new ClientOrder(input);
							output.print(0);
							buttonDeliver.setDisable(false);
							Platform.runLater(() -> 
							{
								labelOrder.setText("Order: " + order.getOrder());
								listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tReceived new order from [" + order.getAddress() + "]");
								
							});
							break;
						}
						default:
						{
							output.print(-1);
							Platform.runLater(() -> listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tUnknown connection type: " + connectionType));
						}
						}
						output.close();
						input.close();
						socket.close();
					}
					catch (IOException e)
					{
						e.printStackTrace();
					}
				}
				else
				{
					try
					{
						TimeUnit.SECONDS.sleep(5);
					}
					catch (InterruptedException e)
					{
						e.printStackTrace();
					}
				}
			}
		});
		ordersListener.setName("Order listener");
		try
		{
			orderServer = new ServerSocket(info.getPort());
		}
		catch (IOException e)
		{
			e.printStackTrace();
			return false;
		}
		ordersListener.start();
		return true;
	}
	
	private void stopOrderListener()
	{
		try
		{
			listen.set(false);
			orderServer.close();
			ordersListener.join();
		}
		catch (IOException | InterruptedException e1)
		{
			e1.printStackTrace();
		}
	}
	
	private void changeView(boolean connected)
	{
		textFieldHost.setDisable(connected);
		textFieldPort.setDisable(connected);
		checkBoxBooks.setDisable(connected);
		checkBoxMovies.setDisable(connected);
		checkBoxMusic.setDisable(connected);
		checkBoxElectronics.setDisable(connected);
		checkBoxGames.setDisable(connected);
		checkBoxAGD.setDisable(connected);
		checkBoxComputers.setDisable(connected);
		checkBoxRTV.setDisable(connected);
		checkBoxFood.setDisable(connected);
		checkBoxPlants.setDisable(connected);
		buttonChangePort.setDisable(connected);
		choiceBoxSize.setDisable(connected);
	}
	
	private int disconnect() throws IOException
	{
		Socket socket = new Socket(serverHost, serverPort);
		BufferedReader input = new BufferedReader(new InputStreamReader(socket.getInputStream()));
		PrintWriter output = new PrintWriter(socket.getOutputStream());
		output.print(1);
		output.flush();
		output.println(info.getAddress());
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
					TimeUnit.SECONDS.sleep(5);
				}
				catch (InterruptedException e1)
				{
					e1.printStackTrace();
				}
			}
		}
		output.close();
		input.close();
		socket.close();
		return response;
	}
	
	@FXML private void toogleButtonServerClicked(MouseEvent e)
	{
		if (toogleButtonServer.isSelected())
		{
			if (!startOrderListener())
			{
				listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t----\tERROR CONNECTING TO SERVER\t----");
				listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tServer not found, wrong host name or server port.");
				return;
			}
			try
			{
				Socket socket = new Socket(serverHost, serverPort);
				PrintWriter output = new PrintWriter(socket.getOutputStream());
				BufferedReader input = new BufferedReader(new InputStreamReader(socket.getInputStream()));
				output.print(0);
				output.flush();
				info.send(output);
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
							TimeUnit.SECONDS.sleep(5);
						}
						catch (InterruptedException e1)
						{
							e1.printStackTrace();
						}
					}
				}
				output.close();
				input.close();
				socket.close();
				if (response != 0)
				{
					listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t----\tERROR CONNECTING TO SERVER\t----");
					listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tUnknown response: " + response);
					return;
				}
			}
			catch (IOException e1)
			{
				listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t----\tERROR CONNECTING TO SERVER\t----");
				e1.printStackTrace();
				return;
			}
			changeView(true);
			toogleButtonServer.setText("Disconnect");
			toogleButtonServer.setStyle("-fx-background-color: #FF3333;");
			listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t----\tCONNECTED TO SERVER\t----");
			listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tListening for orders on port: " + info.getPort());
		}
		else
		{
			try
			{
				if (order != null)
				{
					if (info.abort(order, serverHost, serverPort) != 0)
						listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tError returning order to server.");
					buttonDeliver.setDisable(true);
					labelOrder.setText("No order received");
				}		
				int response = disconnect();
				if (response != 0)
				{
					listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t----\tERROR DISCONNECTING FROM SERVER\t----");
					listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tUnknown response: " + response);
				}
			}
			catch (IOException e1)
			{
				listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t----\tERROR DISCONNECTING FROM SERVER\t----");
				e1.printStackTrace();
			}
			stopOrderListener();
			changeView(false);
			toogleButtonServer.setText("Connect");
			toogleButtonServer.setStyle("-fx-background-color: #CCCC00;");
			listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t----\tDISCONNECTED FROM SERVER\t----");
		}
	}
	
	@FXML private void buttonDeliverClicked(MouseEvent e)
	{
		try
		{
			Socket socket = new Socket(order.getHost(), order.getPort());
			BufferedReader input = new BufferedReader(new InputStreamReader(socket.getInputStream()));
			PrintWriter output = new PrintWriter(socket.getOutputStream());
			output.print(0);
			output.flush();
			output.println(order.getOrder());
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
						TimeUnit.SECONDS.sleep(5);
					}
					catch (InterruptedException e1)
					{
						e1.printStackTrace();
					}
				}
			}
			input.close();
			output.close();
			socket.close();
			if (response != 0)
				listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tError sending order to: " + order.getAddress());
			else
			{
				order = null;
				buttonDeliver.setDisable(true);
				labelOrder.setText("No order received");
				try
				{
					socket = new Socket(serverHost, serverPort);
					input = new BufferedReader(new InputStreamReader(socket.getInputStream()));
					output = new PrintWriter(socket.getOutputStream());
					output.print(4);
					output.flush();
					output.println(info.getAddress());
					output.flush();
					response = -1;
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
								TimeUnit.SECONDS.sleep(5);
							}
							catch (InterruptedException e1)
							{
								e1.printStackTrace();
							}
						}
					}
					if (response != 0)
						listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tError sending order to: " + order.getAddress());
					output.close();
					input.close();
					socket.close();
					listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tOrder [" + order.getAddress() + "] delivered.");
				}
				catch (IOException e1)
				{
					e1.printStackTrace();
					listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tError sending notification to server about finished delivery.");
				}
			}
		}
		catch (IOException e1)
		{
			Alert alert = new Alert(AlertType.ERROR);
			alert.setTitle("Error");
			alert.setHeaderText(null);
			alert.setContentText("Due to some errors order cannot be send.");
			alert.showAndWait();
			e1.printStackTrace();
			listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tError sending order to: " + order.getAddress());
		}
	}
	
	@FXML private void buttonChangePortClicked(MouseEvent e)
	{
		TextInputDialog dialog = new TextInputDialog("");
		dialog.setTitle("Listening port change");
		dialog.setHeaderText(null);
		dialog.setContentText("Please enter new port:");
		Optional<String> result = dialog.showAndWait();
		result.ifPresent(newPort -> 
		{
			int port;
			if (tryParseInt(newPort))
			{
				port = Integer.parseInt(newPort);
				boolean closed = true;
				if (orderServer != null && !orderServer.isClosed())
				{
					stopOrderListener();
					closed = false;
				}
				try
				{
					orderServer = new ServerSocket(port);
					info.setPort(port);
					if (!closed)
					{
						if (!startOrderListener())
						{
							listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t----\tERROR CONNECTING TO SERVER\t----");
							listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tServer not found, wrong host name or server port.");
							return;
						}
					}
					else
						orderServer.close();
					listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tNew courier port: " + info.getPort());
					labelPort.setText("Courier port: " + info.getPort());
				}
				catch (IOException e1)
				{
					Alert alert = new Alert(AlertType.ERROR);
					alert.setTitle("Port not available");
					alert.setHeaderText(null);
					alert.setContentText("Cannot listen on entered port.");
					alert.showAndWait();
					try
					{
						orderServer.close();
						if (!closed)
						{
							if (!startOrderListener())
							{
								listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t----\tERROR CONNECTING TO SERVER\t----");
								listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tServer not found, wrong host name or server port.");
							}
						}
					}
					catch (IOException e2)
					{
						e2.printStackTrace();
					}
					listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tError changing port: " + newPort);
				}
			}
			else
			{
				Alert alert = new Alert(AlertType.ERROR);
				alert.setTitle("Wrong port format");
				alert.setHeaderText(null);
				alert.setContentText("Entered port is not a number!");
				alert.showAndWait();
				listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tError changing port: " + newPort);
			}
		});
	}
	
	@FXML private void checkBoxBooksClicked(MouseEvent e)
	{
		if (checkBoxBooks.isSelected())
			info.addOrderType(OrderType.Books);
		else
			info.removeOrderType(OrderType.Books);
	}
	@FXML private void checkBoxMoviesClicked(MouseEvent e)
	{
		if (checkBoxMovies.isSelected())
			info.addOrderType(OrderType.Movies);
		else
			info.removeOrderType(OrderType.Movies);
	}
	@FXML private void checkBoxMusicClicked(MouseEvent e)
	{
		if (checkBoxMusic.isSelected())
			info.addOrderType(OrderType.Music);
		else
			info.removeOrderType(OrderType.Music);
	}
	@FXML private void checkBoxElectronicsClicked(MouseEvent e)
	{
		if (checkBoxElectronics.isSelected())
			info.addOrderType(OrderType.Electronics);
		else
			info.removeOrderType(OrderType.Electronics);
	}
	@FXML private void checkBoxGamesClicked(MouseEvent e)
	{
		if (checkBoxGames.isSelected())
			info.addOrderType(OrderType.Games);
		else
			info.removeOrderType(OrderType.Games);
	}
	@FXML private void checkBoxAGDClicked(MouseEvent e)
	{
		if (checkBoxAGD.isSelected())
			info.addOrderType(OrderType.AGD);
		else
			info.removeOrderType(OrderType.AGD);
	}
	@FXML private void checkBoxComputersClicked(MouseEvent e)
	{
		if (checkBoxComputers.isSelected())
			info.addOrderType(OrderType.Computers);
		else
			info.removeOrderType(OrderType.Computers);
	}
	@FXML private void checkBoxRTVClicked(MouseEvent e)
	{
		if (checkBoxRTV.isSelected())
			info.addOrderType(OrderType.RTV);
		else
			info.removeOrderType(OrderType.RTV);
	}
	@FXML private void checkBoxFoodClicked(MouseEvent e)
	{
		if (checkBoxFood.isSelected())
			info.addOrderType(OrderType.Food);
		else
			info.removeOrderType(OrderType.Food);
	}
	@FXML private void checkBoxPlantsClicked(MouseEvent e)
	{
		if (checkBoxPlants.isSelected())
			info.addOrderType(OrderType.Plants);
		else
			info.removeOrderType(OrderType.Plants);
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
			if (order != null)
			{
				if (info.abort(order, serverHost, serverPort) != 0)
				{
					Alert alert = new Alert(AlertType.ERROR);
					alert.setTitle("Error");
					alert.setHeaderText(null);
					alert.setContentText("Cannot send back order, package has been lost!");
					alert.showAndWait();
				}
			}
			int response = disconnect();
			if (response != 0)
			{
				Alert alert = new Alert(AlertType.ERROR);
				alert.setTitle("Error");
				alert.setHeaderText(null);
				alert.setContentText("Error disconnecting from server!");
				alert.showAndWait();
			}
		}
		catch (IOException e)
		{
			e.printStackTrace();
		}
		if (orderServer != null && !orderServer.isClosed())
			stopOrderListener();
	}
}
