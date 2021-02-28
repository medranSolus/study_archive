function rate = errorRate(signal, receivedSignal)
    n = length(receivedSignal);
    signalSize = length(signal);
    rate = 0;
    for i = 1 : n
        if signal(i) ~= receivedSignal(i)
            rate = rate + 1;
        end
    end
    rate = rate + (signalSize - n);
    rate = rate / signalSize;
end