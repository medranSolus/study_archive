package lab04_pop;

public class RecordModel
{
	private int id = 0;
	private String text = "none";
	
	public RecordModel(int newId, String newText)
	{
		id = newId;
		text = newText;
	}
	
	public int getId() { return id; }
	public String getText() { return text; }
	public void setText(String newText) { text = newText; }
}
