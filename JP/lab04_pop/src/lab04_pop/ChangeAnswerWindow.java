package lab04_pop;

import javafx.fxml.FXML;
import javafx.scene.control.TextField;
import javafx.scene.input.MouseEvent;
import javafx.stage.Stage;

public class ChangeAnswerWindow
{
	@FXML private TextField textFieldAnswer;

	@FXML
	private void buttonChangeAnswerClicked(MouseEvent e)
	{
		if (textFieldAnswer.getText() != null)
		{
			Window.setDialogData(textFieldAnswer.getText());
			((Stage)textFieldAnswer.getScene().getWindow()).close();
		}
	}
	@FXML
	private void buttonCancelClicked(MouseEvent e)
	{
		Window.setDialogData(null);
		((Stage)textFieldAnswer.getScene().getWindow()).close();
	}
}
