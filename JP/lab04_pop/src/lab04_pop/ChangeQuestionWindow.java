package lab04_pop;

import javafx.fxml.FXML;
import javafx.scene.control.TextField;
import javafx.scene.input.MouseEvent;
import javafx.stage.Stage;

public class ChangeQuestionWindow
{
	@FXML private TextField textFieldQuestion;

	@FXML
	private void buttonChangeQuestionClicked(MouseEvent e)
	{
		if (textFieldQuestion.getText() != null)
		{
			Window.setDialogData(textFieldQuestion.getText());
			((Stage)textFieldQuestion.getScene().getWindow()).close();
		}
	}
	@FXML
	private void buttonCancelClicked(MouseEvent e)
	{
		Window.setDialogData(null);
		((Stage)textFieldQuestion.getScene().getWindow()).close();
	}
}
