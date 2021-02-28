function signal = createEncodedSignalStop(encodedFrames, frameSize, frameToBeginSend)
    frameNumber = size(encodedFrames) - frameToBeginSend + 1;
    signal = 1 : frameNumber * (frameSize + 1);
    offset = 0;
    for i = frameToBeginSend : frameNumber
        for j = 1 : frameSize + 1
            signal(offset*(frameSize + 1) + j) = encodedFrames(i,j);      
        end
        offset = offset + 1;
    end
end

