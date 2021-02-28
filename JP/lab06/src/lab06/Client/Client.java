package lab06.Client;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.URL;
import java.util.Optional;
import java.util.ResourceBundle;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicBoolean;

import javafx.application.Platform;
import javafx.collections.FXCollections;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.ChoiceBox;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.control.TextInputDialog;
import javafx.scene.control.Alert.AlertType;
import javafx.scene.input.MouseEvent;
import lab06.ClientOrder;
import lab06.OrderSize;
import lab06.OrderType;

public class Client implements Initializable
{
	private AtomicBoolean listen = new AtomicBoolean(true);
	private Thread orderThread = null;
	private ServerSocket orderServer = null;
	private int orderPort = 5454;
	@FXML private TextField textFieldOrder;
	@FXML private TextField textFieldHost;
	@FXML private TextField textFieldPort;
	@FXML private Button buttonSend;
	@FXML private Button buttonChangePort;
	@FXML private Label labelOrder;
	@FXML private Label labelPort;
	@FXML private ChoiceBox<OrderSize> choiceBoxSize;
	@FXML private ChoiceBox<OrderType> choiceBoxType;
	
	@Override public void initialize(URL location, ResourceBundle resources)
	{
		choiceBoxSize.setItems(FXCollections.observableArrayList(OrderSize.Small, OrderSize.Medium, OrderSize.Huge));
		choiceBoxSize.getSelectionModel().selectFirst();
		choiceBoxType.setItems(FXCollections.observableArrayList(OrderType.Books, OrderType.Movies, OrderType.Music, OrderType.Games, OrderType.Electronics, OrderType.Computers, OrderType.AGD, OrderType.RTV, OrderType.Food, OrderType.Plants));
		choiceBoxType.getSelectionModel().selectFirst();
		labelPort.setText("Listening port: " + orderPort);
	}
	
	@FXML private void buttonSendClicked(MouseEvent e)
	{
		if (textFieldOrder.getText() == null || textFieldOrder.getText().equals(""))
		{
			Alert alert = new Alert(AlertType.ERROR);
			alert.setTitle("Error");
			alert.setHeaderText(null);
			alert.setContentText("First enter your order.");
			alert.showAndWait();
			return;
		}
		if (!tryParseInt(textFieldPort.getText()))
		{
			Alert alert = new Alert(AlertType.ERROR);
			alert.setTitle("Error");
			alert.setHeaderText(null);
			alert.setContentText("Incorrect server port.");
			alert.showAndWait();
			return;
		}
		Socket orderSocket = null;
		PrintWriter outputOrder = null;
		BufferedReader inputOrder = null;
		try
		{
			orderSocket = new Socket(textFieldHost.getText(), Integer.parseInt(textFieldPort.getText()));
			outputOrder = new PrintWriter(orderSocket.getOutputStream());
			inputOrder = new BufferedReader(new InputStreamReader(orderSocket.getInputStream()));
		}
		catch (NumberFormatException | IOException e2)
		{
			try
			{
				if (inputOrder != null)
					inputOrder.close();
				if (outputOrder != null)
					outputOrder.close();
				if (orderSocket != null)
					orderSocket.close();
			}
			catch (IOException e1)
			{
				e1.printStackTrace();
			}
			Alert alert = new Alert(AlertType.ERROR);
			alert.setTitle("Error");
			alert.setHeaderText(null);
			alert.setContentText("Cannot connect to server, unable to send order.");
			alert.showAndWait();
			e2.printStackTrace();
			return;
		}
		try
		{
			orderServer = new ServerSocket(orderPort);
			new ClientOrder(orderServer.getInetAddress().getHostName(), orderPort, choiceBoxType.getSelectionModel().getSelectedItem(), choiceBoxSize.getSelectionModel().getSelectedItem(), textFieldOrder.getText()).send(outputOrder);
			outputOrder.flush();
			int response = -1;
			for (int i = 0; i < 10; ++i)
			{
				if (inputOrder.ready())
				{
					response = inputOrder.read() - 48;
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
			switch (response)
			{
			case 0:
			{
				labelOrder.setText("Order processed to courier.");
				break;
			}
			case 1:
			{
				labelOrder.setText("Order processed to server storage.");
				break;
			}
			case -1:
			{
				labelOrder.setText("Order canceled by server.");
				inputOrder.close();
				outputOrder.close();
				orderSocket.close();
				orderServer.close();
				return;
			}
			default:
			{
				inputOrder.close();
				outputOrder.close();
				orderSocket.close();
				orderServer.close();
				Alert alert = new Alert(AlertType.ERROR);
				alert.setTitle("Error");
				alert.setHeaderText(null);
				alert.setContentText("Unknown response from server: " + response);
				alert.showAndWait();
				return;
			}
			}
			inputOrder.close();
			outputOrder.close();
			orderSocket.close();
		}
		catch (IOException e1)
		{
			try
			{
				outputOrder.print(-1);
				outputOrder.flush();
				inputOrder.close();
				outputOrder.close();
				orderSocket.close();
				orderServer.close();
			}
			catch (IOException e2)
			{
				e2.printStackTrace();
			}
			Alert alert = new Alert(AlertType.ERROR);
			alert.setTitle("Error");
			alert.setHeaderText(null);
			alert.setContentText("Cannot send order.");
			alert.showAndWait();
			e1.printStackTrace();
			return;
		}
		orderThread = new Thread(() ->
		{
			listen.set(true);
			while (listen.get())
			{
				try
				{
					Socket socket = orderServer.accept();
					BufferedReader input = new BufferedReader(new InputStreamReader(socket.getInputStream()));
					PrintWriter output = new PrintWriter(socket.getOutputStream());
					int response = input.read() - 48;
					switch (response)
					{
					case 0:
					{
						final String order = input.readLine();
						output.print(0);
						output.flush();
						Platform.runLater(() ->
						{
							labelOrder.setText("Order: " + order);
							textFieldOrder.setText("");
							textFieldOrder.setDisable(false);
							textFieldHost.setDisable(false);
							textFieldPort.setDisable(false);
							buttonSend.setDisable(false);
							buttonChangePort.setDisable(false);
							choiceBoxSize.setDisable(false);
							choiceBoxType.setDisable(false);
						});
						listen.set(false);
						break;
					}
					case 1:
					{
						input.close();
						output.close();
						socket.close();
						orderServer.close();
						listen.set(false);
						Platform.runLater(() ->
						{
							labelOrder.setText("Order canceled by server.");
							textFieldOrder.setDisable(false);
							textFieldHost.setDisable(false);
							textFieldPort.setDisable(false);
							buttonSend.setDisable(false);
							buttonChangePort.setDisable(false);
							choiceBoxSize.setDisable(false);
							choiceBoxType.setDisable(false);
						});
						break;
					}
					default:
					{
						Alert alert = new Alert(AlertType.ERROR);
						alert.setTitle("Error");
						alert.setHeaderText(null);
						alert.setContentText("Unknown courier connection!");
						alert.showAndWait();						
					}
					}
					input.close();
					output.close();
					socket.close();
				}
				catch (IOException e1)
				{
					e1.printStackTrace();
				}
			}
		});
		orderThread.setName("Order listener");
		orderThread.start();
		textFieldOrder.setText("Waiting for order");
		textFieldOrder.setDisable(true);
		textFieldHost.setDisable(true);
		textFieldPort.setDisable(true);
		buttonSend.setDisable(true);
		buttonChangePort.setDisable(true);
		choiceBoxSize.setDisable(true);
		choiceBoxType.setDisable(true);
	}
	
	@FXML private void buttonChangePortClicked(MouseEvent e)
	{
		TextInputDialog dialog = new TextInputDialog("");
		dialog.setTitle("Order port change");
		dialog.setHeaderText(null);
		dialog.setContentText("Please enter new port:");
		Optional<String> result = dialog.showAndWait();
		result.ifPresent(newPort -> 
		{
			int port;
			if (tryParseInt(newPort))
			{
				port = Integer.parseInt(newPort);
				try
				{
					orderServer = new ServerSocket(port);
					orderServer.close();
					orderPort = port;
					labelPort.setText("Listening port: " + orderPort);
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
					}
					catch (IOException e2)
					{
						e2.printStackTrace();
					}
				}
			}
			else
			{
				Alert alert = new Alert(AlertType.ERROR);
				alert.setTitle("Wrong port format");
				alert.setHeaderText(null);
				alert.setContentText("Entered port is not a number!");
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
		listen.set(false);
		try
		{
			if (orderServer != null && !orderServer.isClosed())
				orderServer.close();
		}
		catch (IOException e)
		{
			e.printStackTrace();
		}
	}
}
