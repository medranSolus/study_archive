package lab04_pop;

import java.net.URL;
import java.util.ResourceBundle;

import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Button;
import javafx.scene.control.ListView;
import javafx.scene.control.TextField;
import javafx.scene.input.MouseEvent;
import javafx.stage.Stage;

public class AddTestWindow implements Initializable
{
	@FXML private TextField textFieldTest;
	@FXML private Button buttonAddTest;
	@FXML private ListView<Category> listViewCategory;
	
	@Override
	public void initialize(URL location, ResourceBundle resources)
	{
		listViewCategory.getItems().setAll(Category.values());
	}
	@FXML
	private void buttonAddTestClicked(MouseEvent e)
	{
		if (textFieldTest.getText() != null)
		{
			if (!Window.containsTest(textFieldTest.getText()))
			{
				Window.setDialogData(textFieldTest.getText());
				Window.setTestType(listViewCategory.getSelectionModel().getSelectedItem());
				((Stage)textFieldTest.getScene().getWindow()).close();
			}
		}
	}
	@FXML
	private void buttonCancelClicked(MouseEvent e)
	{
		Window.setDialogData(null);
		Window.setTestType(null);
		((Stage)textFieldTest.getScene().getWindow()).close();
	}
	@FXML
	private void listViewCategoryClicked(MouseEvent e)
	{
		if (listViewCategory.getSelectionModel().getSelectedItem() != null)
			buttonAddTest.setDisable(false);
	}
}
