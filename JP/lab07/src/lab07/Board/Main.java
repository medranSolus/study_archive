package lab07.Board;

import java.io.IOException;

import javafx.application.Application;
import javafx.application.Platform;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;

public class Main extends Application 
{
	private Stage boardStage;
	private AnchorPane boardLayout;
	
	public static void main(String[] args) 
	{
		launch(args);
	}
	
	@Override
	public void start(Stage stageArgument) 
	{
		this.boardStage = stageArgument;
		boardStage.setTitle("Board Display");
		initializeLayout();
	}
	
	private void initializeLayout()
	{
		try
		{
			FXMLLoader loader = new FXMLLoader(Main.class.getResource("Board.fxml"));
			boardLayout = (AnchorPane)loader.load();
			Board controller = loader.getController();
			Scene scene = new Scene(boardLayout);
			boardStage.setScene(scene);
			boardStage.setOnHidden(e -> 
			{
				controller.shutdown();
				Platform.exit();
			});
			boardStage.setResizable(false);
			boardStage.show();
		}
		catch (IOException e)
		{
			e.printStackTrace();
		}
	}
}