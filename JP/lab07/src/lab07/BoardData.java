package lab07;

import java.io.Serializable;
import java.util.concurrent.atomic.AtomicLong;
import java.util.concurrent.atomic.AtomicReference;

import javafx.util.Pair;

public class BoardData implements Serializable
{
	private static final long serialVersionUID = -2157999791909443527L;
	public Pair<AtomicReference<String>, AtomicReference<Double>> temperature = new Pair<AtomicReference<String>, AtomicReference<Double>>(new AtomicReference<String>(null), new AtomicReference<Double>(null));
	public Pair<AtomicReference<String>, AtomicReference<Double>> wind = new Pair<AtomicReference<String>, AtomicReference<Double>>(new AtomicReference<String>(null), new AtomicReference<Double>(null));
	public Pair<AtomicReference<String>, AtomicReference<Double>> precipation = new Pair<AtomicReference<String>, AtomicReference<Double>>(new AtomicReference<String>(null), new AtomicReference<Double>(null));
	public AtomicLong update = new AtomicLong(5);
}
