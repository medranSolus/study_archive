function [receivedSignalSelectBit,repeatCountSelectBit,signalSizeRateSelectBit,bitErrorRateSelectBit] = selectiveRepeatParityBit(signal, frameSize, canalNoise, secondCanalProbability)
    repeat = true;
    repeatCountSelectBit = 0;
    signalSendSize = 0;
    
    frames = enframe(signal, frameSize);
    encodedFrames = frameEncode(frames, frameSize);
    frameNumber = size(encodedFrames);
    wrongFrames = ones(frameNumber(1),1);
    receivedFrames=zeros(frameNumber(1), frameSize);
    while repeat
        encodedSignal = createEncodedSignalSelect(encodedFrames, frameSize, wrongFrames);
        receivedSignalSelectBit = transmissionCanal(encodedSignal, canalNoise, secondCanalProbability);
        [receivedFrames,wrongFrames] = decodeFramesSelect(receivedSignalSelectBit, frameSize, receivedFrames, wrongFrames);
        repeat = sendAgain(wrongFrames);
        repeatCountSelectBit = repeatCountSelectBit + 1;
        signalSendSize = signalSendSize + length(encodedSignal);
    end
    receivedSignalSelectBit = decodeSignal(receivedFrames, frameSize);
    signalSizeRateSelectBit = signalSendSize/length(receivedSignalSelectBit);
    bitErrorRateSelectBit = errorRate(signal, receivedSignalSelectBit);
end