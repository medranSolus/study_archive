function [receivedSignalHamming,signalSizeRateHamming,bitErrorRateHamming] = hamming(signal, canalNoise, secondCanalProbability)
    hammingCode = hammingEncoder(signal);
    hammingSignalAfterSending = transmissionCanal(hammingCode, canalNoise, secondCanalProbability);
    receivedSignalHamming = hammingDecoder(hammingSignalAfterSending);
    signalSizeRateHamming = length(hammingCode)/length(signal);
    bitErrorRateHamming = errorRate(signal, receivedSignalHamming);
end