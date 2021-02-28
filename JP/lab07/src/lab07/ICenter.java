package lab07;

import java.rmi.Remote;
import java.rmi.RemoteException;

public interface ICenter extends Remote
{
	boolean register(IBoard board) throws RemoteException;
	boolean unregister(long id) throws RemoteException;
	boolean sendData(long id, BoardData data) throws RemoteException;
}
