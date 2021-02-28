package lab06.Server;

import java.io.IOException;

import javafx.application.Application;
import javafx.application.Platform;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;

public class Main extends Application 
{
	private Stage serverStage;
	private AnchorPane serverLayout;
	
	public static void main(String[] args) 
	{
		launch(args);
	}
	
	@Override
	public void start(Stage stageArgument) 
	{
		this.serverStage = stageArgument;
		serverStage.setTitle("UPS Service");
		initializeLayout();
	}
	
	private void initializeLayout()
	{
		try
		{
			FXMLLoader loader = new FXMLLoader(Main.class.getResource("Server.fxml"));
			serverLayout = (AnchorPane)loader.load();
			Server controller = loader.getController();
			Scene scene = new Scene(serverLayout);
			serverStage.setScene(scene);
			serverStage.setOnHidden(e -> 
			{
				controller.shutdown();
				Platform.exit();
			});
			serverStage.setResizable(false);
			serverStage.show();
		}
		catch (IOException e)
		{
			e.printStackTrace();
		}
	}
}

/*- w systemie działają jako osobne aplikacje: kurier (może być ich wielu), dyspozytor, klient (może ich być wielu)
- kurier zgłasza dyspozytorowi gotowość przyjęcia przesyłki  (podając swoje namiary: host:port oraz kategorie przyjmowanych przesyłek)
- klient zleca dyspozytorowi zadanie nadania przesyłki (podając swoje namiary: host:port oraz zawartość przesyłki wraz z jej kategorią)
- dyspozytor odbiera zlecenie od klienta i, jeśli może je zrealizować, odpowiada potwierdzeniem otwarcia zlecenia (w przeciwnym wypadku odmawia przyjęcia zlecenia)
- dyspozytor przekazuje zlecenie do realizacji któremuś z kurierów będących w stanie gotowości 
- wybrany kurier otrzymuje przesyłkę do przekazania, przy czym kategoria przesyłki musi należeć do kategorii przez niego obsługiwanych
- jeśli aktulnie żaden kurier nie jest w stanie gotowości, to dyspozytor przekazuje przesyłkę do magazynu (o określonej pojemności)
- zapełnienie magazynu uniemożliwia dyspozytorowi przyjęcie zlecenia
- pobieranie zleceń z magazynu odbywa się w chwilach, gdy pojawiają się jacyś kurierzy w stanie gotowości
- kurier informuje dyspozytora o fakcie dostarczenia przesyłki, z kolei dyspozytor informuje klienta o zakończeniu zlecenia
- kurier może wycofać u dyspozytora zgłoszenie gotowości na przyjęcie przesyłki pod warunkiem, że nie realizuje w danej chwili żadnego zlecenia
- komunikacja pomiędzy elementami systemu następuje z wykorzystaniem gniazd TCP/IP (klasy Socket, ServerSocket)
- format komunikatów, protokół ich przekazywania, sposób serializacji danych - to kwestia do samodzielnego rozwiązania, 
- z założenia wszystkie komunikaty powinny mieć postać tekstową (format komunikatów należy zaproponować samemu)
- każdy z elementów systemu (kurierzy, dyspozytor, klient) powinien posiadać interaktywny graficzny interfejs pozwalający na prowadzenie symulacji i obserwacje zmian
- klienci oraz kurierzy powinni obsługiwać zarówno gniazda klienckie (by wysłać coś do dyspozytor) oraz gniazda serwerowe (by coś odebrać od dyspozytora)
*/