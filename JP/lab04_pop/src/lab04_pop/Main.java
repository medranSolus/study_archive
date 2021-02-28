package lab04_pop;

import java.io.IOException;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;

public class Main extends Application 
{
	private Stage primaryStage;
	private AnchorPane layout;

	public static void main(String[] args) 
	{
		launch(args);
	}

	public Stage getPrimaryStage() { return primaryStage; }
	
	@Override
	public void start(Stage stageArgument) 
	{
		this.primaryStage = stageArgument;
		primaryStage.setTitle("Testo Fiesto");
		initializeLayout();
	}
	
	private void initializeLayout()
	{
		try
		{
			FXMLLoader loader = new FXMLLoader();
			loader.setLocation(getClass().getResource("Window.fxml"));
			layout = (AnchorPane)loader.load();
			Scene scene = new Scene(layout);
			primaryStage.setScene(scene);
			primaryStage.setResizable(false);
			primaryStage.show();
		}
		catch (IOException e)
		{
			e.printStackTrace();
		}
	}
}
