package lab07.Center;

import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentMap;

import javafx.scene.control.TableView;
import lab07.BoardData;
import lab07.IBoard;
import lab07.ICenter;

public class CenterServer extends UnicastRemoteObject implements ICenter
{
	private static final long serialVersionUID = 6370974431702723626L;
	private long Id = 0;
	private ConcurrentMap<String, IBoard> boards = new ConcurrentHashMap<String, IBoard>();
	private TableView<DataModel> tableViewData;
	
	protected CenterServer(TableView<DataModel> tableView) throws RemoteException { super(); tableViewData = tableView; }

	public long getId() { return Id; }
	public void setId(long id) { Id = id; }
	public boolean toggleUpdate(String name) throws RemoteException { return boards.get(name).toggle(); }
	public void changeUpdate(String name, long seconds) throws RemoteException { boards.get(name).setUpdateInterval(seconds); }
	
	@Override public boolean unregister(long id) throws RemoteException 
	{
		tableViewData.getItems().removeIf(model -> model.boardName.equals("board/" + id));
		return boards.remove("board/" + id) == null ? false : true; 
	}
	@Override public boolean register(IBoard board) throws RemoteException
	{
		long id = board.getBoardId();
		boards.put("board/" + id, board);
		tableViewData.getItems().add(new DataModel(id, new BoardData()));
		tableViewData.refresh();
		return true;
	}
	@Override public boolean sendData(long id, BoardData data) throws RemoteException
	{
		DataModel model = new DataModel(id, data);
		int index = tableViewData.getItems().indexOf(new DataModel(id, data));
		if (index != -1)
			tableViewData.getItems().set(index, model);
		tableViewData.refresh();
		return true;
	}
	public void close()
	{
		for (IBoard board : boards.values())
		{
			try
			{
				board.disconnectFormServer();
			}
			catch (RemoteException e)
			{
				e.printStackTrace();
			}
		}
	}
}
