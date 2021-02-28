package lab05_pop;

import java.net.URL;
import java.util.ResourceBundle;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.concurrent.atomic.LongAdder;

import javafx.application.Platform;
import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Label;
import javafx.scene.control.Slider;

public class Window implements Initializable
{
	private AssemblyLine line1 = new AssemblyLine(20, Shift.Right, "labelToAssemble");
	private AssemblyLine line2 = new AssemblyLine(20, Shift.Left, "labelAssembled");
	private Thread[] workingThreads = new Thread[20];
	private AtomicBoolean work = new AtomicBoolean(true);
	private Thread inputThread = null;
	private Thread outputThread = null;
	private LongAdder delayWorkers = new LongAdder();
	private LongAdder delayInput = new LongAdder();
	private LongAdder delayOutput = new LongAdder();
	
	@FXML private Slider sliderLine1;
	@FXML private Slider sliderLine2;
	@FXML private Slider sliderWorkers;
	@FXML private Slider sliderInput;
	@FXML private Slider sliderOutput;
	
	@Override
	public void initialize(URL location, ResourceBundle resources)
	{
		sliderLine1.valueProperty().addListener(new ChangeListener<Number>()
		{
			@Override
			public void changed(ObservableValue<? extends Number> observable, Number oldValue, Number newValue)
			{
				line1.changeDelay(newValue.intValue() - oldValue.intValue());
			}
		});
		sliderLine2.valueProperty().addListener(new ChangeListener<Number>()
		{
			@Override
			public void changed(ObservableValue<? extends Number> observable, Number oldValue, Number newValue)
			{
				line2.changeDelay(newValue.intValue() - oldValue.intValue());
			}
		});
		sliderWorkers.valueProperty().addListener(new ChangeListener<Number>()
		{
			@Override
			public void changed(ObservableValue<? extends Number> observable, Number oldValue, Number newValue)
			{
				delayWorkers.add(newValue.intValue() - oldValue.intValue());
			}
		});
		sliderInput.valueProperty().addListener(new ChangeListener<Number>()
		{
			@Override
			public void changed(ObservableValue<? extends Number> observable, Number oldValue, Number newValue)
			{
				delayInput.add(newValue.intValue() - oldValue.intValue());
			}
		});
		sliderOutput.valueProperty().addListener(new ChangeListener<Number>()
		{
			@Override
			public void changed(ObservableValue<? extends Number> observable, Number oldValue, Number newValue)
			{
				delayOutput.add(newValue.intValue() - oldValue.intValue());
			}
		});
		delayWorkers.add(10);
		for (int i = 0; i < 20; ++i)
		{
			final int number = i;
			workingThreads[i] = new Thread(() ->
			{
				int position = number;
				Product product = null;
				while (work.get())
				{
					product = line1.getProduct(position);
					if (product != null)
					{
						final char symbol1 = product.getSymbol();
						Platform.runLater(() -> ((Label)Main.loader.getNamespace().get("labelWorker" + (position + 1))).setText("_" + symbol1 + "_"));
						product.assemble();
						final char symbol2 = product.getSymbol();
						Platform.runLater(() -> ((Label)Main.loader.getNamespace().get("labelWorker" + (position + 1))).setText("_" + symbol2 + "_"));
						do
						{
							try
							{
								TimeUnit.SECONDS.sleep(1);
							}
							catch (InterruptedException e)
							{
								e.printStackTrace();
							}
						} while (line2.putProduct(position, product));
						Platform.runLater(() -> ((Label)Main.loader.getNamespace().get("labelWorker" + (position + 1))).setText("____"));
					}
					else
					{
						try
						{
							TimeUnit.SECONDS.sleep(1);
						}
						catch (InterruptedException e)
						{
							e.printStackTrace();
						}
					}
					try
					{
						TimeUnit.SECONDS.sleep(delayWorkers.longValue());
					}
					catch (InterruptedException e)
					{
						e.printStackTrace();
					}
				}
			});
			workingThreads[i].setName("Worker " + i);
			workingThreads[i].start();
		}
		delayOutput.add(10);
		outputThread = new Thread(() ->
		{
			while (work.get())
			{
				line2.getProduct(0);
				try
				{
					TimeUnit.SECONDS.sleep(delayOutput.longValue());
				}
				catch (InterruptedException e)
				{
					e.printStackTrace();
				}
			}
		});
		outputThread.setName("Output");
		outputThread.start();
		delayInput.add(10);
		inputThread = new Thread(() ->
		{
			while (work.get())
			{
				line1.putProduct(0, new Product());
				try
				{
					TimeUnit.SECONDS.sleep(delayInput.longValue());
				}
				catch (InterruptedException e)
				{
					e.printStackTrace();
				}
			}
		});
		inputThread.setName("Input");
		inputThread.start();
		line1.start();
		line2.start();
	}
	
	public void shutdown()
	{
		work.set(false);
		line1.stop();
		line2.stop();
	}
}
