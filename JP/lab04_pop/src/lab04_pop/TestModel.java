package lab04_pop;

public class TestModel
{
	private String name = "none";
	private String category = "Unkw0n";
	
	public TestModel(Test test)
	{
		name = test.getTestName();
		category = Category.toString(test.getTestType());
	}
	
	public String getName() { return name; }
	public String getCategory() { return category; }
	public void setName(String newName) { name = newName; }
}
