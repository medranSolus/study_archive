package lab04_pop;

import java.util.Map;
import java.util.TreeMap;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.scene.control.Button;
import javafx.scene.control.TextField;
import javafx.scene.control.MenuItem;
import javafx.scene.input.MouseEvent;
import javafx.stage.Stage;

public class AddQuestionWindow
{
	@FXML private TextField textFieldQuestion;
	@FXML private TextField textFieldAnswer1;
	@FXML private TextField textFieldAnswer2;
	@FXML private TextField textFieldAnswer3;
	@FXML private TextField textFieldAnswer4;
	@FXML private Button buttonAddQuestion;
	private int correctAnswer = 0;
	
	@FXML
	private void buttonAddQuestionClicked(MouseEvent e)
	{
		if (textFieldQuestion.getText() != null && textFieldAnswer1.getText() != null && textFieldAnswer2.getText() != null && textFieldAnswer3.getText() != null && textFieldAnswer4.getText() != null)
		{
			Map<Integer, String> answers = new TreeMap<Integer, String>();
			answers.put(1, textFieldAnswer1.getText());
			answers.put(2, textFieldAnswer2.getText());
			answers.put(3, textFieldAnswer3.getText());
			answers.put(4, textFieldAnswer4.getText());
			Window.setNewQuestion(new Question(textFieldQuestion.getText(), answers, correctAnswer));
			((Stage)textFieldQuestion.getScene().getWindow()).close();
		}
	}
	@FXML
	private void buttonCancelClicked(MouseEvent e)
	{
		Window.setNewQuestion(null);
		((Stage)textFieldQuestion.getScene().getWindow()).close();
	}
	@FXML
	private void menuItemClicked(ActionEvent e)
	{
		correctAnswer = Integer.parseInt(((MenuItem)e.getSource()).getText());
		buttonAddQuestion.setDisable(false);
	}
}
