n = 1001;
frameSize = 5;
canalNoise = 1;
secondCanalProbability = 0;

signal = generate(n);
%[receivedSignalSelectBit,repeatCountSelectBit,signalSizeRateSelectBit,bitErrorRateSelectBit] = selectiveRepeatParityBit(signal, frameSize, canalNoise, secondCanalProbability);
%[receivedSignalStopBit,additionalFramesCountStopBit,signalSizeRateStopBit,bitErrorRateStopBit] = stopAndWaitParityBit(signal, frameSize, canalNoise, secondCanalProbability);
%[receivedSignalHamming,signalSizeRateHamming,bitErrorRateHamming] = hamming(signal, canalNoise, secondCanalProbability);
[crcSizeRate, crcBitErrorRate] = stopAndWaitCRC(signal, frameSize, canalNoise, secondCanalProbability);