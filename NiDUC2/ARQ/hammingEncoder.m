%Potrajanie ka¿dego bitu
function outputSignal =  hammingEncoder(signal)
    outputSignal = 1 : length(signal) * 3;
    j = 0;
    for i = 1 : length(signal)
        k = j;
        for k = j : k + 2
             outputSignal(i + k) = signal(i);
        end
       j = j + 2;
    end
end