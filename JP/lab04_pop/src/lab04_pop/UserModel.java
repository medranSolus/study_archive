package lab04_pop;

public class UserModel
{
	private String name = "none";
	private double overallScore = 0.0;
	private int completedTests = 0;
	
	public UserModel(User user)
	{
		name = user.getName();
		overallScore = user.overallScore();
		completedTests = user.getStatistics().size();
	}

	public String getName() { return name; }
	public double getOverallScore() { return Double.parseDouble(String.format("%.2f", overallScore*100).replace(",", ".")); }
	public int getCompletedTests() { return completedTests; }
	
	@Override
	public boolean equals(Object object)
	{
		if (object instanceof UserModel)
			return name.equals(((UserModel)object).getName());
		return super.equals(object);
	}
}
