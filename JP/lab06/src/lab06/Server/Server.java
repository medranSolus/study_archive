package lab06.Server;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.URL;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.ResourceBundle;
import java.util.TreeMap;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.concurrent.atomic.LongAdder;
import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;

import javafx.application.Platform;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Alert;
import javafx.scene.control.Alert.AlertType;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.ListView;
import javafx.scene.control.TextInputDialog;
import javafx.scene.control.ToggleButton;
import javafx.scene.input.MouseEvent;
import javafx.util.Pair;
import lab06.ClientOrder;
import lab06.CourierInfo;

public class Server implements Initializable
{
	private AtomicBoolean processOrders = new AtomicBoolean(false);
	private AtomicBoolean listenClient = new AtomicBoolean(false);
	private AtomicBoolean listenCourier = new AtomicBoolean(false);
	private Thread listeningThreadClient = null;
	private Thread listeningThreadCourier = null;
	private Thread storageProcessingThread = null;
	private int portClient = 6666;
	private int portCourier = 6667;
	private ServerSocket serverClient = null;
	private ServerSocket serverCourier = null;
	private Map<String, Pair<Lock, Pair<CourierInfo, AtomicBoolean>>> couriers = new TreeMap<String, Pair<Lock, Pair<CourierInfo, AtomicBoolean>>>();
	private List<ClientOrder> orders = new LinkedList<ClientOrder>();
	private Lock couriersLock = new ReentrantLock();
	private Lock ordersLock = new ReentrantLock();
	private LongAdder ordersSize = new LongAdder();
	private DateTimeFormatter formatter = DateTimeFormatter.ofPattern("HH:mm dd-MM-yyyy");
	@FXML private ToggleButton toggleButtonOnOff;
	@FXML private ListView<String> listViewLog;
	@FXML private Label labelPortClient;
	@FXML private Label labelPortCourier;
	@FXML private Label labelOrders;
	@FXML private Button buttonChangePortClient;
	@FXML private Button buttonChangePortCourier;
	
	@Override public void initialize(URL location, ResourceBundle resources)
	{
		ordersSize.add(25);
		initializeListenningThreads();
		storageProcessingThread = new Thread(() ->
		{
			while (processOrders.get())
			{
				if (orders.size() > 0)
				{
					ordersLock.lock();
					List<ClientOrder> ordersBuffer = new ArrayList<ClientOrder>(orders);
					ordersLock.unlock();
					for (var order : ordersBuffer)
					{
						boolean processed = false;
						couriersLock.lock();
						for (var entry : couriers.values())
						{
							if (entry.getKey().tryLock())
							{
								if (entry.getValue().getValue().get() && entry.getValue().getKey().canProcessOrder(order))
								{
									entry.getValue().getValue().set(false);
									try
									{
										if (entry.getValue().getKey().sendOrder(order) == 0)
										{
											processed = true;
											Platform.runLater(() -> 
											{
												labelOrders.setText("Orders: " + ordersSize.longValue() + "/" + ordersSize.longValue());
												listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tOrder form [" + order.getAddress() + "] send to courier [" + entry.getValue().getKey().getAddress() + "]");
											});
										}
										else
											Platform.runLater(() -> listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tError processing order [" + order.getAddress() + "] by courier [" + entry.getValue().getKey().getAddress() + "]"));
										couriersLock.unlock();
										entry.getKey().unlock();
										if (processed)
											break;
									}
									catch (IOException e)
									{
										e.printStackTrace();
									}
								}
								entry.getKey().unlock();
							}
						}
						if (processed)
						{
							ordersLock.lock();
							orders.remove(order);
							Platform.runLater(() -> labelOrders.setText("Orders: " + orders.size() + "/" + ordersSize.longValue()));
							ordersLock.unlock();
						}
						couriersLock.unlock();
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
		storageProcessingThread.setName("Storage processing");
		labelPortClient.setText("Client port: " + portClient);
		labelPortCourier.setText("Courier port: " + portCourier);
		labelOrders.setText("Orders: 0/" + ordersSize.longValue());
	}
	
	private void initializeListenningThreads()
	{
		initializeClientThread();
		initializeCourierThread();
	}
	
	private void initializeClientThread()
	{
		listeningThreadClient = new Thread(() ->
		{
			while (listenClient.get())
			{
				try
				{
					final Socket socket = serverClient.accept();
					new Thread(() ->
					{
						try
						{
							BufferedReader input = new BufferedReader(new InputStreamReader(socket.getInputStream()));
							PrintWriter output = new PrintWriter(socket.getOutputStream());
							ClientOrder order = new ClientOrder(input);
							couriersLock.lock();
							for (var entry : couriers.values())
							{
								if (entry.getKey().tryLock())
								{
									if (entry.getValue().getValue().get() && entry.getValue().getKey().canProcessOrder(order))
									{
										entry.getValue().getValue().set(false);
										entry.getValue().getKey().sendOrder(order);
										couriersLock.unlock();
										entry.getKey().unlock();
										output.print(0);
										output.flush();
										input.close();
										output.close();
										socket.close();
										Platform.runLater(() -> listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tOrder form [" + order.getAddress() + "] send to courier [" + entry.getValue().getKey().getAddress() + "]"));
										return;
									}
									entry.getKey().unlock();
								}
							}
							couriersLock.unlock();
							ordersLock.lock();
							if (orders.size() < ordersSize.longValue())
							{
								orders.add(order);
								output.print(1);
								Platform.runLater(() -> 
								{
									listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tOrder form [" + order.getAddress() + "] send to storage.");
									labelOrders.setText("Orders: " + orders.size() + "/" + ordersSize.longValue());
								});
							}
							else
							{
								output.print(-1);
								Platform.runLater(() -> listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tOrder form [" + order.getAddress() + "] has been rejected, storage full."));
							}
							ordersLock.unlock();
							output.flush();
							input.close();
							output.close();
							socket.close();
						}
						catch (IOException e)
						{
							Platform.runLater(() -> listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tError connecting to client."));
							e.printStackTrace();
						}
					}).start();
				}
				catch (IOException e)
				{
					e.printStackTrace();
				}
			}
			try
			{
				serverClient.close();
			}
			catch (IOException e)
			{
				e.printStackTrace();
			}
		});
		listeningThreadClient.setName("Client listener");
	}
	
	private void initializeCourierThread()
	{
		listeningThreadCourier = new Thread(() ->
		{
			while (listenCourier.get())
			{
				try
				{
					final Socket socket = serverCourier.accept();
					new Thread(() ->
					{
						try
						{
							BufferedReader input = new BufferedReader(new InputStreamReader(socket.getInputStream()));
							PrintWriter output = new PrintWriter(socket.getOutputStream());
							int connectionType = input.read() - 48;
							switch (connectionType)
							{
							case 0:
							{//connect
								CourierInfo courier = new CourierInfo(input);
								couriersLock.lock();
								couriers.put(courier.getAddress(), new Pair<Lock, Pair<CourierInfo, AtomicBoolean>>(new ReentrantLock(), new Pair<CourierInfo, AtomicBoolean>(courier, new AtomicBoolean(true))));
								couriersLock.unlock();
								Platform.runLater(() -> listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tNew courier connected (" + courier.getAddress() + ")"));
								output.print(0);
								break;
							}
							case 1:
							{//disconnect
								String address = input.readLine();
								couriersLock.lock();
								couriers.get(address).getKey().lock();
								couriers.remove(address);
								couriersLock.unlock();
								Platform.runLater(() -> listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tCourier disconnected (" + address + ")"));
								output.print(0);
								break;
							}
							case 3:
							{//abort order
								ClientOrder order = new ClientOrder(input);
								Platform.runLater(() -> listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tCourier aborted order: " + order.getOrder()));
								if (orders.size() < ordersSize.longValue())
								{
									ordersLock.lock();
									orders.add(order);
									Platform.runLater(() -> labelOrders.setText("Orders: " + orders.size() + "/" + ordersSize.longValue()));
									ordersLock.unlock();
								}
								else
								{
									
									Platform.runLater(() -> listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tStorage full, order rejected."));
								}
								output.print(0);
								break;
							}
							case 4:
							{//finished delivery
								String address = input.readLine();
								couriersLock.lock();
								couriers.get(address).getKey().lock();
								couriers.get(address).getValue().getValue().set(true);
								couriers.get(address).getKey().unlock();
								couriersLock.unlock();
								Platform.runLater(() -> listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tOrder delivered by courier [" + address + "]"));
								output.print(0);
								break;
							}
							default:
							{
								Platform.runLater(() -> listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tUnknown courier connection type: " + connectionType));
								output.print(-1);
							}
							}
							output.flush();
							input.close();
							output.close();
							socket.close();
						}
						catch (IOException e)
						{
							Platform.runLater(() -> listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tError connecting to courier."));
							e.printStackTrace();
						}
					}).start();
				}
				catch (IOException e)
				{
					e.printStackTrace();
				}
			}
			try
			{
				serverCourier.close();
			}
			catch (IOException e)
			{
				e.printStackTrace();
			}
		});
		listeningThreadCourier.setName("Courier listener");
	}
	
	@FXML private void toggleButtonOnOffClicked(MouseEvent e)
	{
		if (toggleButtonOnOff.selectedProperty().get())
		{
			try
			{
				serverClient = new ServerSocket(portClient);
				serverCourier = new ServerSocket(portCourier);
			}
			catch (IOException e1)
			{
				e1.printStackTrace();
			}
			initializeListenningThreads();
			listenClient.set(true);
			listeningThreadClient.start();
			listenCourier.set(true);
			listeningThreadCourier.start();
			if (!processOrders.get())
			{
				processOrders.set(true);
				storageProcessingThread.start();
			}
			buttonChangePortClient.setDisable(false);
			buttonChangePortCourier.setDisable(false);
			toggleButtonOnOff.setText("ON");
			toggleButtonOnOff.setStyle("-fx-background-color: #CCCC00;");
			listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t----\tSERVER IS ON\t----");
			listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tServer port for clients: " + portClient);
			listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tServer port for couriers: " + portCourier);
		}
		else
		{
			try
			{
				buttonChangePortClient.setDisable(true);
				buttonChangePortCourier.setDisable(true);
				listenClient.set(false);
				listenCourier.set(false);
				serverClient.close();
				serverCourier.close();
				listeningThreadClient.join();
				listeningThreadCourier.join();
			}
			catch (InterruptedException | IOException e1)
			{
				e1.printStackTrace();
			}
			toggleButtonOnOff.setText("OFF");
			toggleButtonOnOff.setStyle("-fx-background-color: #FF3333;");
			listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t----\tSERVER IS OFF\t----");
		}
	}
	
	@FXML private void buttonChangePortClientClicked(MouseEvent e)
	{
		TextInputDialog dialog = new TextInputDialog("");
		dialog.setTitle("Client port change");
		dialog.setHeaderText(null);
		dialog.setContentText("Please enter new client port:");
		Optional<String> result = dialog.showAndWait();
		result.ifPresent(newPort -> 
		{
			int port;
			if (tryParseInt(newPort))
			{
				port = Integer.parseInt(newPort);
				if (port != portCourier)
				{
					try
					{
						listenClient.set(false);
						serverClient.close();
						listeningThreadClient.join();
						serverClient = new ServerSocket(port);
						portClient = port;
						listenClient.set(true);
						initializeClientThread();
						listeningThreadClient.start();
						listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tNew client port: " + portClient);
						labelPortClient.setText("Client port: " + portClient);
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
							serverClient.close();
							serverClient = new ServerSocket(portClient);
							listenClient.set(true);
							initializeClientThread();
							listeningThreadClient.start();
						}
						catch (IOException e2)
						{
							e2.printStackTrace();
						}
						listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tError changing port for clients: " + newPort);
					}
					catch (InterruptedException e1)
					{
						e1.printStackTrace();
					}
				}
				else
				{
					Alert alert = new Alert(AlertType.ERROR);
					alert.setTitle("Port not available");
					alert.setHeaderText(null);
					alert.setContentText("Port occupied by courier listener.");
					alert.showAndWait();
					listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tError changing port for clients: " + port + " port same as courier port.");
				}
			}
			else
			{
				Alert alert = new Alert(AlertType.ERROR);
				alert.setTitle("Wrong port format");
				alert.setHeaderText(null);
				alert.setContentText("Entered port is not a number!");
				alert.showAndWait();
				listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tError changing port for clients: " + newPort);
			}
		});
	}
	
	@FXML private void buttonChangePortCourierClicked(MouseEvent e)
	{
		TextInputDialog dialog = new TextInputDialog("");
		dialog.setTitle("Courier port change");
		dialog.setHeaderText(null);
		dialog.setContentText("Please enter new courier port:");
		Optional<String> result = dialog.showAndWait();
		result.ifPresent(newPort -> 
		{
			int port;
			if (tryParseInt(newPort))
			{
				port = Integer.parseInt(newPort);
				if (port != portClient)
				{
					try
					{
						listenCourier.set(false);
						serverCourier.close();
						listeningThreadCourier.join();
						serverCourier = new ServerSocket(port);
						portCourier = port;
						listenCourier.set(true);
						initializeCourierThread();
						listeningThreadCourier.start();
						listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tNew courier port: " + portCourier);
						labelPortCourier.setText("Courier port: " + portCourier);
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
							serverCourier.close();
							serverCourier = new ServerSocket(portCourier);
							listenCourier.set(true);
							initializeClientThread();
							listeningThreadCourier.start();
						}
						catch (IOException e2)
						{
							e2.printStackTrace();
						}
						listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tError changing port for couriers: " + newPort);
					}
					catch (InterruptedException e1)
					{
						e1.printStackTrace();
					}
				}
				else
				{
					Alert alert = new Alert(AlertType.ERROR);
					alert.setTitle("Port not available");
					alert.setHeaderText(null);
					alert.setContentText("Port occupied by client listener.");
					alert.showAndWait();
					listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tError changing port for couriers: " + port + " port same as client port.");
				}
			}
			else
			{
				Alert alert = new Alert(AlertType.ERROR);
				alert.setTitle("Wrong port format");
				alert.setHeaderText(null);
				alert.setContentText("Entered port is not a number!");
				alert.showAndWait();
				listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tError changing port for couriers: " + newPort);
				return;
			}
		});
	}
	
	@FXML private void buttonChangeStorageSizeClicked(MouseEvent e)
	{
		TextInputDialog dialog = new TextInputDialog("");
		dialog.setTitle("Order storage size change");
		dialog.setHeaderText(null);
		dialog.setContentText("Please enter new storage size:");
		Optional<String> result = dialog.showAndWait();
		result.ifPresent(newSize ->
		{
			if (tryParseInt(newSize))
			{
				ordersSize.reset();
				ordersSize.add(Integer.parseInt(newSize));
				labelOrders.setText("Orders: " + orders.size() + "/" + ordersSize.longValue());
				listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tNew size for order storage: " + ordersSize.longValue());
			}
			else
			{
				Alert alert = new Alert(AlertType.ERROR);
				alert.setTitle("Wrong number format");
				alert.setHeaderText(null);
				alert.setContentText("Entered size is not a number!");
				alert.showAndWait();
				listViewLog.getItems().add(0, LocalDateTime.now().format(formatter) + "\t\tError changing size for order storage: " + newSize);
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
		listenClient.set(false);
		listenCourier.set(false);
		processOrders.set(false);
		try
		{
			if (serverClient != null)
				serverClient.close();
			if (serverCourier != null)
				serverCourier.close();
		}
		catch (IOException e)
		{
			e.printStackTrace();
		}
	}
}
