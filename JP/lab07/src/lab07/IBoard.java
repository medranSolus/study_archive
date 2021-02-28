package lab07;

import java.rmi.Remote;
import java.rmi.RemoteException;

public interface IBoard extends Remote
{
    boolean register(ISensor sensor, SensorCategory category) throws RemoteException; 
    boolean unregister(long id) throws RemoteException;
    boolean toggle() throws RemoteException;
    void setUpdateInterval(long seconds) throws RemoteException;
    boolean disconnectFormServer() throws RemoteException;
    long getBoardId() throws RemoteException;
}
