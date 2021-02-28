package lab07.Sensor;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

import javafx.application.Application;
import javafx.application.Platform;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.ChoiceDialog;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;
import lab07.SensorCategory;

public class Main extends Application 
{
	private Stage sensorStage;
	private AnchorPane sensorLayout;
	
	public static void main(String[] args) 
	{
		launch(args);
	}
	
	@Override
	public void start(Stage stageArgument) 
	{
		this.sensorStage = stageArgument;
		initializeLayout();
	}
	
	private void initializeLayout()
	{
		SensorCategory category = SensorCategory.Temperature;
		List<SensorCategory> choices = new ArrayList<>();
		choices.add(SensorCategory.Temperature);
		choices.add(SensorCategory.Wind);
		choices.add(SensorCategory.Precipitation);
		ChoiceDialog<SensorCategory> dialog = new ChoiceDialog<SensorCategory>(SensorCategory.Temperature, choices);
		dialog.setTitle("Sensor setup");
		dialog.setHeaderText(null);
		dialog.setContentText("Choose measurement parameter: ");
		Optional<SensorCategory> result = dialog.showAndWait();
		if (result.isPresent())
			category = result.get();
		try
		{
			FXMLLoader loader = new FXMLLoader(Main.class.getResource("Sensor.fxml"));
			sensorLayout = (AnchorPane)loader.load();
			Sensor controller = loader.getController();
			controller.setType(category);
			sensorStage.setTitle(category + " sensor");
			Scene scene = new Scene(sensorLayout);
			sensorStage.setScene(scene);
			sensorStage.setOnHidden(e -> 
			{
				controller.shutdown();
				Platform.exit();
			});
			sensorStage.setResizable(false);
			sensorStage.show();
		}
		catch (IOException e)
		{
			e.printStackTrace();
		}
	}
}