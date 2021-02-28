%dekodowanie sygna�u na podstawie warto�ci 3 bit�w z rz�du
function outputSignal =  hammingDecoder(signal)
    outputSignal = 1 : (length(signal) / 3);
    for i = 1 : length(outputSignal)
        sum = 0;
        for j = (i-1)*3 + 1 : (i-1)*3 + 3
           sum = sum + signal(j);
        end
        if sum >= 2
         outputSignal(i) = 1;
        else
            outputSignal(i) = 0;
        end
    end
end