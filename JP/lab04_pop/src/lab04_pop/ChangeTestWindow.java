package lab04_pop;

import javafx.fxml.FXML;
import javafx.scene.control.TextField;
import javafx.scene.input.MouseEvent;
import javafx.stage.Stage;

public class ChangeTestWindow
{
	@FXML private TextField textFieldTest;
	
	@FXML
	private void buttonChangeTestClicked(MouseEvent e)
	{
		if (textFieldTest.getText() != null)
		{
			if (!Window.containsTest(textFieldTest.getText()))
			{
				Window.setDialogData(textFieldTest.getText());
				((Stage)textFieldTest.getScene().getWindow()).close();
			}
		}
	}
	@FXML
	private void buttonCancelClicked(MouseEvent e)
	{
		Window.setDialogData(null);
		((Stage)textFieldTest.getScene().getWindow()).close();
	}
}
