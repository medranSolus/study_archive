package lab05_pop;

import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.concurrent.atomic.LongAdder;
import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;

import javafx.application.Platform;
import javafx.scene.control.Label;

enum Shift { Left, Right }

public class AssemblyLine
{
	private LongAdder delay = new LongAdder();
	private String labelName = "";
	private Shift direction = Shift.Right;
	private volatile Product[] line = null;
	private Lock[] lineLocks = null;
	private Thread engine = null;
	private AtomicBoolean run = new AtomicBoolean(false);
	
	public AssemblyLine(int size, Shift shiftDirection, String name)
	{
		labelName = name;
		delay.add(10);
		line = new Product[size];
		lineLocks = new Lock[size];
		for (int i = 0; i < size; ++i)
			lineLocks[i] = new ReentrantLock();
		direction = shiftDirection;
		engine = new Thread(() -> 
		{
			while (keepRunning())
			{
				shift();
				try
				{
					TimeUnit.SECONDS.sleep(delay.longValue());
				}
				catch (InterruptedException e)
				{
					e.printStackTrace();
				}
			}
		});
		engine.setName(labelName.substring(5));
	}
	
	public void changeDelay(int step) { delay.add(step); }
	public void stop() { run.set(false); }
	public void start()
	{
		if (!run.get())
		{
			run.set(true);
			engine.start();
		}
	}
	public Product getProduct(int index)
	{
		if (index < line.length)
		{
			if (line[index] != null)
			{
				if (lineLocks[index].tryLock())
				{
					Product product = line[index];
					line[index] = null;
					lineLocks[index].unlock();
					Platform.runLater(() -> ((Label)Main.loader.getNamespace().get(labelName + (index + 1))).setText("|----|"));
					return product;
				}
			}
		}
		return null;
	}
	public boolean putProduct(int index, Product product)
	{
		if (index < line.length)
		{
			if (line[index] == null)
			{
				if (lineLocks[index].tryLock())
				{
					line[index] = product;
					product = null;
					lineLocks[index].unlock();
					Platform.runLater(() -> ((Label)Main.loader.getNamespace().get(labelName + (index + 1))).setText("|-" + line[index].getSymbol() + "-|"));
					return false;
				}
			}
		}
		return true;
	}
	
	private boolean keepRunning() { return run.get(); }
	private void shift()
	{
		if (direction == Shift.Right && line[line.length - 1] == null)
		{
			lock();
			for (int i = line.length - 1; i > 0; --i)
				line[i] = line[i - 1];
			line[0] = null;
			update();
			unlock();
		}
		else if (line[0] == null)
		{
			lock();
			for (int i = 0; i < line.length - 1; ++i)
				line[i] = line[i + 1];
			line[line.length - 1] = null;
			update();
			unlock();
		}
	}
	private void update()
	{
		for (int i = 1; i <= line.length; ++i)
		{
			final int index = i;
			Platform.runLater(() ->
			{
				int id = index;
				Label label = (Label)Main.loader.getNamespace().get(labelName + id);
				if (line[id - 1] == null)
					label.setText("|----|");
				else
					label.setText("|-" + line[id - 1].getSymbol() + "-|");
			});
		}
	}
	private void lock()
	{
		for (int i = 0; i < lineLocks.length; ++i)
			lineLocks[i].lock();
	}
	private void unlock()
	{
		for (int i = 0; i < lineLocks.length; ++i)
			lineLocks[i].unlock();
	}
}
