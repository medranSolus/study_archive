package lab04_pop;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.net.URL;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.NoSuchElementException;
import java.util.ResourceBundle;
import java.util.Scanner;
import java.util.TreeMap;

import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.Alert.AlertType;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.BorderPane;
import javafx.stage.Stage;
import javafx.stage.StageStyle;

public class Window implements Initializable
{
	private static String dataFromDialog = null;
	private static Category testType = null;
	private static Question newQuestion = null;
	private static Map<String, User> users = new TreeMap<String, User>();
	private static Map<String, Test> tests = new TreeMap<String, Test>();
	private String sourcePath = "res\\";
	private User currentUser = null;
	private Test currentTest = null;
	private int correctAnswer = 0;
	private int score = 0;
	private Iterator<Question> questionId = null;
	
	@FXML private BorderPane borderPaneLogonScreen;
	@FXML private TableView<UserModel> tableViewUserList;
	@FXML private TableColumn<UserModel, String> tableColumnName;
	@FXML private TableColumn<UserModel, Integer> tableColumnCompletedTests;
	@FXML private TableColumn<UserModel, Double> tableColumnScore;
	@FXML private Button buttonLogIn;
	@FXML private Button buttonDeleteUser;
	@FXML private Label labelSelectedUser;
	@FXML private BorderPane borderPaneNormalScreen;
	@FXML private Label labelUserName;
	@FXML private Button buttonChangeUser;
	@FXML private Button buttonChangeUserName;
	@FXML private TableView<StatisticsModel> tableViewTestStatistics;
	@FXML private TableColumn<StatisticsModel, String> tableColumnTestName;
	@FXML private TableColumn<StatisticsModel, String> tableColumnCategory;
	@FXML private TableColumn<StatisticsModel, String> tableColumnPoints;
	@FXML private TableView<AverageScoreModel> tableViewAverageTestScore;
	@FXML private TableColumn<AverageScoreModel, String> tableColumnAverageTestName;
	@FXML private TableColumn<AverageScoreModel, Double> tableColumnAverageScore;
	@FXML private TableView<AverageScoreModel> tableViewAverageGroupScore;
	@FXML private TableColumn<AverageScoreModel, String> tableColumnAverageCategory;
	@FXML private TableColumn<AverageScoreModel, Double> tableColumnGroupScore;
	@FXML private AnchorPane anchorPaneTestMode;
	@FXML private Label labelQuestion;
	@FXML private Button button1;
	@FXML private Button button2;
	@FXML private Button button3;
	@FXML private Button button4;
	@FXML private AnchorPane anchorPaneTestSelect;
	@FXML private TableView<TestModel> tableViewTestList;
	@FXML private TableColumn<TestModel, String> tableColumnChooseTestName;
	@FXML private TableColumn<TestModel, String> tableColumnChooseCategory;
	@FXML private Button buttonStartTest;
	@FXML private TableView<TestModel> tableViewTestEditList;
	@FXML private TableColumn<TestModel, String> tableColumnEditTestName;
	@FXML private TableColumn<TestModel, String> tableColumnEditCategory;
	@FXML private TableView<RecordModel> tableViewQuestionList;
	@FXML private TableColumn<RecordModel, Integer> tableColumnQuestionID;
	@FXML private TableColumn<RecordModel, String> tableColumnQuestionName;
	@FXML private TableView<RecordModel> tableViewAnswersList;
	@FXML private TableColumn<RecordModel, Integer> tableColumnAnswerID;
	@FXML private TableColumn<RecordModel, String> tableColumnAnswer;
	@FXML private Button buttonAddTest;
	@FXML private Button buttonChangeTestName;
	@FXML private Button buttonAddQuestion;
	@FXML private Button buttonChangeQuestion;
	@FXML private Button buttonChangeAnswer;
	@FXML private Button buttonSetAnswer;
	@FXML private Button buttonDeleteTest;
	@FXML private Button buttonDeleteQuestion;
	@FXML private Label labelCorrectAnswer;
	
	public static void setDialogData(String data) { dataFromDialog = data; }
	public static void setTestType(Category type) { testType = type; }
	public static void setNewQuestion(Question question) { newQuestion = question; }
	public static boolean containsUser(String userName) { return users.containsKey(userName); }
	public static boolean containsTest(String testName) { return tests.containsKey(testName); }
	
	@Override
	public void initialize(URL location, ResourceBundle resources)
	{
		tableColumnName.setCellValueFactory(new PropertyValueFactory<UserModel, String>("name"));
		tableColumnCompletedTests.setCellValueFactory(new PropertyValueFactory<UserModel, Integer>("completedTests"));
		tableColumnScore.setCellValueFactory(new PropertyValueFactory<UserModel, Double>("overallScore"));
		tableColumnTestName.setCellValueFactory(new PropertyValueFactory<StatisticsModel, String>("testName"));
		tableColumnCategory.setCellValueFactory(new PropertyValueFactory<StatisticsModel, String>("category"));
		tableColumnPoints.setCellValueFactory(new PropertyValueFactory<StatisticsModel, String>("points"));
		tableColumnAverageTestName.setCellValueFactory(new PropertyValueFactory<AverageScoreModel, String>("name"));
		tableColumnAverageScore.setCellValueFactory(new PropertyValueFactory<AverageScoreModel, Double>("averageScore"));
		tableColumnAverageCategory.setCellValueFactory(new PropertyValueFactory<AverageScoreModel, String>("name"));
		tableColumnGroupScore.setCellValueFactory(new PropertyValueFactory<AverageScoreModel, Double>("averageScore"));
		tableColumnChooseTestName.setCellValueFactory(new PropertyValueFactory<TestModel, String>("name"));
		tableColumnChooseCategory.setCellValueFactory(new PropertyValueFactory<TestModel, String>("category"));
		tableColumnEditTestName.setCellValueFactory(new PropertyValueFactory<TestModel, String>("name"));
		tableColumnEditCategory.setCellValueFactory(new PropertyValueFactory<TestModel, String>("category"));
		tableColumnQuestionID.setCellValueFactory(new PropertyValueFactory<RecordModel, Integer>("id"));
		tableColumnQuestionName.setCellValueFactory(new PropertyValueFactory<RecordModel, String>("text"));
		tableColumnAnswerID.setCellValueFactory(new PropertyValueFactory<RecordModel, Integer>("id"));
		tableColumnAnswer.setCellValueFactory(new PropertyValueFactory<RecordModel, String>("text"));
		try
		{
			Scanner input = new Scanner(new File(sourcePath + "users.txt"));
			int size = 0;
			size = input.nextInt();
			input.nextLine();
			List<UserModel> userList = new LinkedList<UserModel>();
			for (int i = 0; i < size; ++i)
			{
				User user = new User(input);
				users.put(user.getName(), user);
				userList.add(new UserModel(user));
			}
			input.close();
			tableViewUserList.getItems().setAll(userList);
			input = new Scanner(new File(sourcePath + "tests.txt"));
			size = input.nextInt();
			input.nextLine();
			List<TestModel> testList = new LinkedList<TestModel>();
			for (int i = 0; i < size; ++i)
			{
				Test test = new Test(input);
				tests.put(test.getTestName(), test);
				testList.add(new TestModel(test));
			}
			input.close();
			tableViewTestList.getItems().setAll(testList);
			tableViewTestEditList.getItems().setAll(testList);
		}
		catch (FileNotFoundException | NoSuchElementException e)
		{
			e.printStackTrace();
		}
	}
	@FXML
	private void button1Clicked(MouseEvent e)
	{
		Alert alert = new Alert(AlertType.INFORMATION);
		alert.setHeaderText(null);
		if (correctAnswer == 1)
		{
			++score;
			alert.setTitle("Correct");
			alert.setContentText("Your answer is correct!");
		}
		else
		{
			alert.setTitle("Wrong");
			alert.setContentText("Your answer is wrong, number " + correctAnswer + " is correct answer.");
		}
		alert.showAndWait();
		setQuestionDisplay();
	}
	@FXML
	private void button2Clicked(MouseEvent e)
	{
		Alert alert = new Alert(AlertType.INFORMATION);
		alert.setHeaderText(null);
		if (correctAnswer == 2)
		{
			++score;
			alert.setTitle("Correct");
			alert.setContentText("Your answer is correct!");
		}
		else
		{
			alert.setTitle("Wrong");
			alert.setContentText("Your answer is wrong, number " + correctAnswer + " is correct answer.");
		}
		alert.showAndWait();
		setQuestionDisplay();
	}
	@FXML
	private void button3Clicked(MouseEvent e)
	{
		Alert alert = new Alert(AlertType.INFORMATION);
		alert.setHeaderText(null);
		if (correctAnswer == 3)
		{
			++score;
			alert.setTitle("Correct");
			alert.setContentText("Your answer is correct!");
		}
		else
		{
			alert.setTitle("Wrong");
			alert.setContentText("Your answer is wrong, number " + correctAnswer + " is correct answer.");
		}
		alert.showAndWait();
		setQuestionDisplay();
	}
	@FXML
	private void button4Clicked(MouseEvent e)
	{
		Alert alert = new Alert(AlertType.INFORMATION);
		alert.setHeaderText(null);
		if (correctAnswer == 4)
		{
			++score;
			alert.setTitle("Correct");
			alert.setContentText("Your answer is correct!");
		}
		else
		{
			alert.setTitle("Wrong");
			alert.setContentText("Your answer is wrong, number " + correctAnswer + " is correct answer.");
		}
		alert.showAndWait();
		setQuestionDisplay();
	}
	@FXML
	private void buttonStartTestClicked(MouseEvent e)
	{
		if (tests.get(tableViewTestList.getSelectionModel().getSelectedItem().getName()).getQuestions().size() > 0)
		{
			currentTest = tests.get(tableViewTestList.getSelectionModel().getSelectedItem().getName());
			questionId = currentTest.getQuestions().values().iterator();
			setQuestionDisplay();
			anchorPaneTestSelect.setDisable(true);
			anchorPaneTestSelect.setVisible(false);
			anchorPaneTestMode.setVisible(true);
			anchorPaneTestMode.setDisable(false);
		}
	}
	@FXML
	private void buttonChangeUserClicked(MouseEvent e)
	{
		borderPaneNormalScreen.setDisable(true);
		borderPaneLogonScreen.setVisible(true);
		borderPaneNormalScreen.setVisible(false);
		borderPaneLogonScreen.setDisable(false);
	}
	@FXML
	private void buttonChangeUserNameClicked(MouseEvent e)
	{
		try
		{
			FXMLLoader loader = new FXMLLoader();
			loader.setLocation(getClass().getResource("ChangeNameWindow.fxml"));
			BorderPane layout;
			layout = (BorderPane)loader.load();
			Scene scene = new Scene(layout);
			Stage registerStage = new Stage(StageStyle.UNDECORATED);
			registerStage.setScene(scene);
			registerStage.setResizable(false);
			registerStage.showAndWait();
			if(dataFromDialog != null)
			{
				labelUserName.setText(dataFromDialog);
				labelSelectedUser.setText(dataFromDialog);
				users.remove(currentUser.getName());
				tableViewUserList.getItems().removeIf(user -> user.getName().equals(currentUser.getName()));
				currentUser.setName(dataFromDialog);
				users.put(currentUser.getName(), currentUser);
				tableViewUserList.getItems().add(new UserModel(currentUser));
				tableViewUserList.refresh();
				dataFromDialog = null;
				save();
			}
		}
		catch (IOException e1)
		{
			e1.printStackTrace();
		}
		
	}
	@FXML
	private void buttonRegisterClicked(MouseEvent e)
	{
		try
		{
			FXMLLoader loader = new FXMLLoader();
			loader.setLocation(getClass().getResource("RegisterWindow.fxml"));
			BorderPane layout = (BorderPane)loader.load();
			Scene scene = new Scene(layout);
			Stage registerStage = new Stage(StageStyle.UNDECORATED);
			registerStage.setScene(scene);
			registerStage.setResizable(false);
			registerStage.showAndWait();
			if(dataFromDialog != null)
			{
				User user = new User(dataFromDialog, new LinkedList<Statistics>());
				users.put(user.getName(), user);
				tableViewUserList.getItems().add(new UserModel(user));
				tableViewUserList.refresh();
				dataFromDialog = null;
				save();
			}
		}
		catch (IOException e1)
		{
			e1.printStackTrace();
		}
	}
	@FXML
	private void buttonDeleteUserClicked(MouseEvent e)
	{
		users.remove(tableViewUserList.getSelectionModel().getSelectedItem().getName());
		currentUser = null;
		labelSelectedUser.setText("");
		tableViewUserList.getItems().remove(tableViewUserList.getSelectionModel().getSelectedItem());
		tableViewUserList.refresh();
		buttonLogIn.setDisable(true);
		buttonDeleteUser.setDisable(true);
		save();
	}
	@FXML
	private void buttonLogInClicked(MouseEvent e)
	{
		tableViewTestStatistics.getItems().clear();
		labelUserName.setText(currentUser.getName());
		for (var statistic : currentUser.getStatistics())
			tableViewTestStatistics.getItems().add(new StatisticsModel(statistic));
		tableViewAverageTestScore.getItems().setAll(CreateModel.averageTestScoreModel(currentUser.getStatistics()));
		tableViewAverageGroupScore.getItems().setAll(CreateModel.averageGroupScoreModel(currentUser.getStatistics()));
		borderPaneLogonScreen.setDisable(true);
		borderPaneNormalScreen.setVisible(true);
		borderPaneLogonScreen.setVisible(false);
		borderPaneNormalScreen.setDisable(false);
	}
	@FXML
	private void tableViewUserListsClicked(MouseEvent e)
	{
		if (tableViewUserList.getSelectionModel().getSelectedItem() != null)
		{
			currentUser = users.get(tableViewUserList.getSelectionModel().getSelectedItem().getName());
			labelSelectedUser.setText(currentUser.getName());
			buttonLogIn.setDisable(false);
			buttonDeleteUser.setDisable(false);
		}
	}
	@FXML
	private void tableViewTestListClicked(MouseEvent e)
	{
		if (tableViewTestList.getSelectionModel().getSelectedItem() != null)
			buttonStartTest.setDisable(false);
	}
	@FXML
	private void tableViewTestEditListClicked(MouseEvent e)
	{
		if (tableViewTestEditList.getSelectionModel().getSelectedItem() != null)
		{
			tableViewAnswersList.getItems().clear();
			tableViewQuestionList.getItems().clear();
			buttonChangeTestName.setDisable(false);
			buttonDeleteTest.setDisable(false);
			buttonAddQuestion.setDisable(false);
			buttonDeleteQuestion.setDisable(true);
			buttonChangeQuestion.setDisable(true);
			buttonChangeAnswer.setDisable(true);
			buttonSetAnswer.setDisable(true);
			tests.get(tableViewTestEditList.getSelectionModel().getSelectedItem().getName()).getQuestions().forEach((id, question) -> tableViewQuestionList.getItems().add(new RecordModel(id, question.getQuestion())));
		}
	}
	@FXML
	private void tableViewQuestionListClicked(MouseEvent e)
	{
		if (tableViewQuestionList.getSelectionModel().getSelectedItem() != null)
		{
			tableViewAnswersList.getItems().clear();
			buttonChangeQuestion.setDisable(false);
			buttonDeleteQuestion.setDisable(false);
			buttonChangeAnswer.setDisable(true);
			buttonSetAnswer.setDisable(true);
			tests.get(tableViewTestEditList.getSelectionModel().getSelectedItem().getName()).getQuestions().get(tableViewQuestionList.getSelectionModel().getSelectedItem().getId()).getAnswers().forEach((id, answer) -> tableViewAnswersList.getItems().add(new RecordModel(id, answer)));
		}
	}
	@FXML
	private void tableViewAnswersListClicked(MouseEvent e)
	{
		if (tableViewAnswersList.getSelectionModel().getSelectedItem() != null)
		{
			buttonChangeAnswer.setDisable(false);
			buttonSetAnswer.setDisable(false);
		}
	}
	@FXML
	private void buttonAddTestClicked(MouseEvent e)
	{
		try
		{
			FXMLLoader loader = new FXMLLoader();
			loader.setLocation(getClass().getResource("AddTestWindow.fxml"));
			BorderPane layout = (BorderPane)loader.load();
			Scene scene = new Scene(layout);
			Stage registerStage = new Stage(StageStyle.UNDECORATED);
			registerStage.setScene(scene);
			registerStage.setResizable(false);
			registerStage.showAndWait();
			if(dataFromDialog != null && testType != null)
			{
				Test test = new Test(testType, new TreeMap<Integer, Question>(), dataFromDialog);
				tests.put(test.getTestName(), test);
				TestModel model = new TestModel(test);
				tableViewTestEditList.getItems().add(model);
				tableViewTestList.getItems().add(model);
				tableViewTestList.refresh();
				dataFromDialog = null;
				testType = null;
				save();
			}
		}
		catch (IOException e1)
		{
			e1.printStackTrace();
		}
	}
	@FXML
	private void buttonChangeTestNameClicked(MouseEvent e)
	{
		try
		{
			FXMLLoader loader = new FXMLLoader();
			loader.setLocation(getClass().getResource("ChangeTestWindow.fxml"));
			BorderPane layout = (BorderPane)loader.load();
			Scene scene = new Scene(layout);
			Stage registerStage = new Stage(StageStyle.UNDECORATED);
			registerStage.setScene(scene);
			registerStage.setResizable(false);
			registerStage.showAndWait();
			if(dataFromDialog != null)
			{
				Test test = tests.get(tableViewTestEditList.getSelectionModel().getSelectedItem().getName());
				tests.remove(test.getTestName());
				test.setName(dataFromDialog);
				tests.put(test.getTestName(), test);
				tableViewTestEditList.getSelectionModel().getSelectedItem().setName(test.getTestName());
				tableViewTestEditList.refresh();
				tableViewTestList.getItems().setAll(tableViewTestEditList.getItems());
				dataFromDialog = null;
				save();
			}
		}
		catch (IOException e1)
		{
			e1.printStackTrace();
		}
	}
	@FXML
	private void buttonAddQuestionClicked(MouseEvent e)
	{
		try
		{
			FXMLLoader loader = new FXMLLoader();
			loader.setLocation(getClass().getResource("AddQuestionWindow.fxml"));
			BorderPane layout = (BorderPane)loader.load();
			Scene scene = new Scene(layout);
			Stage registerStage = new Stage(StageStyle.UNDECORATED);
			registerStage.setScene(scene);
			registerStage.setResizable(false);
			registerStage.showAndWait();
			if(newQuestion != null)
			{
				tableViewQuestionList.getItems().add(new RecordModel(tests.get(tableViewTestEditList.getSelectionModel().getSelectedItem().getName()).addQuestion(newQuestion), newQuestion.getQuestion()));
				tableViewQuestionList.refresh();
				newQuestion = null;
				save();
			}
		}
		catch (IOException e1)
		{
			e1.printStackTrace();
		}
	}
	@FXML
	private void buttonChangeQuestionClicked(MouseEvent e)
	{
		try
		{
			FXMLLoader loader = new FXMLLoader();
			loader.setLocation(getClass().getResource("ChangeQuestionWindow.fxml"));
			BorderPane layout = (BorderPane)loader.load();
			Scene scene = new Scene(layout);
			Stage registerStage = new Stage(StageStyle.UNDECORATED);
			registerStage.setScene(scene);
			registerStage.setResizable(false);
			registerStage.showAndWait();
			if(dataFromDialog != null)
			{
				tests.get(tableViewTestEditList.getSelectionModel().getSelectedItem().getName()).getQuestions().get(tableViewQuestionList.getSelectionModel().getSelectedItem().getId()).setQuestion(dataFromDialog);
				tableViewQuestionList.getSelectionModel().getSelectedItem().setText(dataFromDialog);
				tableViewQuestionList.refresh();
				dataFromDialog = null;
				save();
			}
		}
		catch (IOException e1)
		{
			e1.printStackTrace();
		}
	}
	@FXML
	private void buttonChangeAnswerClicked(MouseEvent e)
	{
		try
		{
			FXMLLoader loader = new FXMLLoader();
			loader.setLocation(getClass().getResource("ChangeAnswerWindow.fxml"));
			BorderPane layout = (BorderPane)loader.load();
			Scene scene = new Scene(layout);
			Stage registerStage = new Stage(StageStyle.UNDECORATED);
			registerStage.setScene(scene);
			registerStage.setResizable(false);
			registerStage.showAndWait();
			if(dataFromDialog != null)
			{
				tests.get(tableViewTestEditList.getSelectionModel().getSelectedItem().getName()).getQuestions().get(tableViewQuestionList.getSelectionModel().getSelectedItem().getId()).getAnswers().replace(tableViewAnswersList.getSelectionModel().getSelectedItem().getId(), dataFromDialog);
				tableViewAnswersList.getSelectionModel().getSelectedItem().setText(dataFromDialog);
				tableViewAnswersList.refresh();
				dataFromDialog = null;
				save();
			}
		}
		catch (IOException e1)
		{
			e1.printStackTrace();
		}
	}
	@FXML
	private void buttonSetAnswerClicked(MouseEvent e)
	{
		tests.get(tableViewTestEditList.getSelectionModel().getSelectedItem().getName()).getQuestions().get(tableViewQuestionList.getSelectionModel().getSelectedItem().getId()).setCorrectAnswer(tableViewAnswersList.getSelectionModel().getSelectedItem().getId());
		save();
	}
	@FXML
	private void buttonDeleteTestClicked(MouseEvent e)
	{
		buttonChangeTestName.setDisable(true);
		buttonDeleteTest.setDisable(true);
		buttonAddQuestion.setDisable(true);
		buttonDeleteQuestion.setDisable(true);
		buttonChangeQuestion.setDisable(true);
		buttonChangeAnswer.setDisable(true);
		buttonSetAnswer.setDisable(true);
		tests.remove(tableViewTestEditList.getSelectionModel().getSelectedItem().getName());
		tableViewTestList.getItems().remove(tableViewTestEditList.getSelectionModel().getSelectedItem());
		tableViewTestEditList.getItems().remove(tableViewTestEditList.getSelectionModel().getSelectedItem());
		tableViewTestList.refresh();
		tableViewTestEditList.refresh();
		tableViewQuestionList.getItems().clear();
		tableViewAnswersList.getItems().clear();
		save();
	}
	@FXML
	private void buttonDeleteQuestionClicked(MouseEvent e)
	{
		buttonDeleteQuestion.setDisable(true);
		buttonChangeQuestion.setDisable(true);
		buttonChangeAnswer.setDisable(true);
		buttonSetAnswer.setDisable(true);
		tests.get(tableViewTestEditList.getSelectionModel().getSelectedItem().getName()).deleteQuestion(tableViewQuestionList.getSelectionModel().getSelectedItem().getId());
		tableViewQuestionList.getItems().remove(tableViewQuestionList.getSelectionModel().getSelectedItem());
		tableViewQuestionList.refresh();
		tableViewAnswersList.getItems().clear();
		save();
	}
	
	private void setQuestionDisplay()
	{
		if (questionId.hasNext())
		{
			Question question = questionId.next();
			labelQuestion.setText(question.getQuestion());
			button1.setText(question.getAnswers().get(1));
			button2.setText(question.getAnswers().get(2));
			button3.setText(question.getAnswers().get(3));
			button4.setText(question.getAnswers().get(4));
			correctAnswer = question.getCorrectAnswer();
		}
		else
		{
			Alert alert = new Alert(AlertType.INFORMATION);
			alert.setTitle("Test completed");
			alert.setHeaderText(null);
			alert.setContentText("You have gained " + score + "/" + currentTest.getQuestions().size() + " points!");
			alert.showAndWait();
			currentUser.addStatistic(new Statistics(currentTest.getTestType(), score, currentTest.getQuestions().size(), currentTest.getTestName()));
			tableViewUserList.getItems().remove(new UserModel(currentUser));
			tableViewUserList.getItems().add(new UserModel(currentUser));
			tableViewUserList.refresh();
			tableViewTestStatistics.getItems().clear();
			for (var statistic : currentUser.getStatistics())
				tableViewTestStatistics.getItems().add(new StatisticsModel(statistic));
			tableViewAverageTestScore.getItems().setAll(CreateModel.averageTestScoreModel(currentUser.getStatistics()));
			tableViewAverageGroupScore.getItems().setAll(CreateModel.averageGroupScoreModel(currentUser.getStatistics()));
			currentTest = null;
			questionId = null;
			score = correctAnswer = 0;
			anchorPaneTestSelect.setDisable(false);
			anchorPaneTestSelect.setVisible(true);
			anchorPaneTestMode.setVisible(false);
			anchorPaneTestMode.setDisable(true);
			save();
		}
	}
	private void save()
	{
		try
		{
			PrintWriter output = new PrintWriter(new BufferedWriter(new FileWriter(new File(sourcePath + "users.txt"))));
			output.println(users.size());
			for (var user : users.values())
				user.save(output);
			output.close();
			output = new PrintWriter(new BufferedWriter(new FileWriter(new File(sourcePath + "tests.txt"))));
			output.println(tests.size());
			for (var test : tests.values())
				test.save(output);
			output.close();
		}
		catch (IOException e)
		{
			e.printStackTrace();
		}
		
	}
}
