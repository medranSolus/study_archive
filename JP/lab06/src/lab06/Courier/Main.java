package lab06.Courier;

import java.io.IOException;

import javafx.application.Application;
import javafx.application.Platform;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;

public class Main extends Application 
{
	private Stage courierStage;
	private AnchorPane courierLayout;
	
	public static void main(String[] args) 
	{
		launch(args);
	}
	
	@Override
	public void start(Stage stageArgument) 
	{
		this.courierStage = stageArgument;
		courierStage.setTitle("UPS Service - Courier");
		initializeLayout();
	}
	
	private void initializeLayout()
	{
		try
		{
			FXMLLoader loader = new FXMLLoader(Main.class.getResource("Courier.fxml"));
			courierLayout = (AnchorPane)loader.load();
			Courier controller = loader.getController();
			Scene scene = new Scene(courierLayout);
			courierStage.setScene(scene);
			courierStage.setOnHidden(e -> 
			{
				controller.shutdown();
				Platform.exit();
			});
			courierStage.setResizable(false);
			courierStage.show();
		}
		catch (IOException e)
		{
			e.printStackTrace();
		}
	}
}
