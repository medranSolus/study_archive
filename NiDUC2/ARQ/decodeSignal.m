function signalFinal = decodeSignal(receivedFrames, frameSize)
    frameNumber = size(receivedFrames);
    signalFinal = 1:frameNumber*frameSize;
    for i = 1:frameNumber
        for j = 1 : frameSize
            signalFinal(j+(i-1)*frameSize)=receivedFrames(i,j);
        end
    end
end