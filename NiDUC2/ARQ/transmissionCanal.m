%zamiana losowej iloœci (ustalanej na podstawie wartoœci alpha) bitów w
%sygnale, mo¿liwoœæ wejœcia w inny kana³ o du¿o gorszej jakoœci
function encodedSignal =  transmissionCanal(encodedSignal, alpha, enterSecondCanalProbability)
    for i = 1 : length(encodedSignal)
        if isBitChanged(alpha * (enterSecondCanalProbability / 100)) == 1
            secondCanalLength = floor(rand(1) * 50);
            j = i;
            if j + secondCanalLength > length(encodedSignal)
                secondCanalLength = length(encodedSignal) - i - 1;
            end
            for j = i : j + secondCanalLength
                if encodedSignal(j) == 1
                    encodedSignal(j) = 0;
                else
                    encodedSignal(j) = 1;
                end
            end
        else
            if isBitChanged(alpha) == 1
                if encodedSignal(i) == 0
                    encodedSignal(i) = 1;
                else
                    encodedSignal(i) = 0;
                end
            end
        end
    end
end