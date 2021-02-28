package lab04_pop;

import javafx.fxml.FXML;
import javafx.scene.control.Button;
import javafx.scene.control.TextField;
import javafx.scene.input.MouseEvent;
import javafx.stage.Stage;

public class ChangeNameWindow
{
	@FXML private TextField textFieldUserName;
	@FXML private Button buttonChangeName;

	@FXML
	private void buttonChangeNameClicked(MouseEvent e)
	{
		if (textFieldUserName.getText() != null)
		{
			if (!Window.containsUser(textFieldUserName.getText()))
			{
				Window.setDialogData(textFieldUserName.getText());
				((Stage)textFieldUserName.getScene().getWindow()).close();
			}
		}
	}
	@FXML
	private void buttonCancelClicked(MouseEvent e)
	{
		Window.setDialogData(null);
		((Stage)textFieldUserName.getScene().getWindow()).close();
	}
}
