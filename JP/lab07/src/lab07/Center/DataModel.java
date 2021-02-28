package lab07.Center;

import lab07.BoardData;

public class DataModel
{
	public String boardName = null;
	public Double temperature = null;
	public Double wind = null;
	public Double precipation = null;
	public Long update = null;
	
	public DataModel(long id, BoardData data)
	{
		boardName = "board/" + id;
		temperature = data.temperature.getValue().get();
		wind = data.wind.getValue().get();
		precipation = data.precipation.getValue().get();
		update = data.update.get();
	}
	
	public String getBoardName() { return boardName; }
	public Double getTemperature() { return temperature; }
	public Double getWind() { return wind; }
	public Double getPrecipation() { return precipation; }
	public Long getUpdate() { return update; }
	
	@Override public boolean equals(Object object)
	{
		if (object instanceof DataModel)
			return ((DataModel)object).boardName.equals(boardName);
		if (object instanceof String)
			return ((String)object).equals(boardName);
		return false;
	}
}
