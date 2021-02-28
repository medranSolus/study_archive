function [receivedSignalStopBit,additionalFramesCountStopBit,signalSizeRateStopBit,bitErrorRateStopBit] = stopAndWaitParityBit(signal, frameSize, canalNoise, secondCanalProbability)
    same = 0;
    additionalFramesCountStopBit = 0;
    signalSendSize = 0;
    
    frames = enframe(signal, frameSize);
    encodedFrames = frameEncode(frames, frameSize);
    frameNumber = size(encodedFrames);
    receivedFrames=zeros(frameNumber(1), frameSize + 1);
    %{
    while frameToRepeat ~= 0
        encodedSignal = createEncodedSignalStop(encodedFrames, frameSize, frameToRepeat);
        receivedSignalStopBit = transmissionCanal(encodedSignal, canalNoise, secondCanalProbability);
        [receivedFrames,frameToRepeat] = decodeFramesStop(receivedSignalStopBit, frameSize, receivedFrames, frameToRepeat);
        repeatCountStopBit = repeatCountStopBit + 1;
        signalSendSize = signalSendSize + length(encodedSignal);
    end
    %}
    for i = 1:frameNumber(1)
        while same == 0
            receivedFrames(i,:) = transmissionCanal(encodedFrames(i,:), canalNoise, secondCanalProbability);
            additionalFramesCountStopBit = additionalFramesCountStopBit + 1;
            signalSendSize = signalSendSize + frameSize;
            if isParity(receivedFrames(i,1:frameSize)) == receivedFrames(i, frameSize + 1)
                same = 1;
            end
        end
        same = 0;
    end
    additionalFramesCountStopBit = additionalFramesCountStopBit - frameNumber(1);
    receivedSignalStopBit = decodeSignal(receivedFrames, frameSize);
    signalSizeRateStopBit = signalSendSize/length(receivedSignalStopBit);
    bitErrorRateStopBit = errorRate(signal, receivedSignalStopBit);
end

