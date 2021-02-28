package lab07;

import java.io.Serializable;
import java.util.concurrent.atomic.AtomicReference;

public class SensorReadings implements Serializable
{
	private static final long serialVersionUID = 8544014928125176027L;
	public SensorCategory category = SensorCategory.Temperature;
	public AtomicReference<Double> value = new AtomicReference<Double>(0.0);
}
