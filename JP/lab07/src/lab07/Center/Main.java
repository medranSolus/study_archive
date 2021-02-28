package lab07.Center;

import java.io.IOException;

import javafx.application.Application;
import javafx.application.Platform;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.layout.BorderPane;
import javafx.stage.Stage;

public class Main extends Application 
{
	private Stage centerStage;
	private BorderPane centerLayout;
	
	public static void main(String[] args) 
	{
		launch(args);
	}
	
	@Override
	public void start(Stage stageArgument) 
	{
		this.centerStage = stageArgument;
		centerStage.setTitle("Sensor center");
		initializeLayout();
	}
	
	private void initializeLayout()
	{
		try
		{
			FXMLLoader loader = new FXMLLoader(Main.class.getResource("Center.fxml"));
			centerLayout = (BorderPane)loader.load();
			Center controller = loader.getController();
			Scene scene = new Scene(centerLayout);
			centerStage.setScene(scene);
			centerStage.setOnHidden(e -> 
			{
				controller.shutdown();
				Platform.exit();
			});
			centerStage.setResizable(false);
			centerStage.show();
		}
		catch (IOException e)
		{
			e.printStackTrace();
		}
	}
}