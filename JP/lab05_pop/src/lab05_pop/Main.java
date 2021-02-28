package lab05_pop;

import java.io.IOException;

import javafx.application.Application;
import javafx.application.Platform;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;

public class Main extends Application 
{
	public static FXMLLoader loader = new FXMLLoader(Main.class.getResource("Window.fxml"));
	private Stage primaryStage;
	private AnchorPane layout;
	
	public static void main(String[] args) 
	{
		launch(args);
	}
	
	@Override
	public void start(Stage stageArgument) 
	{
		this.primaryStage = stageArgument;
		primaryStage.setTitle("Factorio");
		initializeLayout();
	}
	
	private void initializeLayout()
	{
		try
		{
			layout = (AnchorPane)loader.load();
			Window controller = loader.getController();
			Scene scene = new Scene(layout);
			primaryStage.setScene(scene);
			primaryStage.setOnHidden(e -> 
			{
				controller.shutdown();
				Platform.exit();
			});
			primaryStage.setResizable(false);
			primaryStage.show();
		}
		catch (IOException e)
		{
			e.printStackTrace();
		}
	}
}
