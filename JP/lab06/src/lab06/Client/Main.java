package lab06.Client;

import java.io.IOException;

import javafx.application.Application;
import javafx.application.Platform;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;

public class Main extends Application 
{
	private Stage clientStage;
	private AnchorPane clientLayout;
	
	public static void main(String[] args) 
	{
		launch(args);
	}
	
	@Override
	public void start(Stage stageArgument) 
	{
		this.clientStage = stageArgument;
		clientStage.setTitle("UPS Service - Client");
		initializeLayout();
	}
	
	private void initializeLayout()
	{
		try
		{
			FXMLLoader loader = new FXMLLoader(Main.class.getResource("Client.fxml"));
			clientLayout = (AnchorPane)loader.load();
			Client controller = loader.getController();
			Scene scene = new Scene(clientLayout);
			clientStage.setScene(scene);
			clientStage.setOnHidden(e -> 
			{
				controller.shutdown();
				Platform.exit();
			});
			clientStage.setResizable(false);
			clientStage.show();
		}
		catch (IOException e)
		{
			e.printStackTrace();
		}
	}
}
