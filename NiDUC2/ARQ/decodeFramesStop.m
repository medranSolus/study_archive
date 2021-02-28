function [receivedFrames,nextFrameToBegin] = decodeFramesStop(receivedSignal, frameSize, previousFrames, frameToBeginSend)
    frameNumber = size(previousFrames);
    frame = 1:frameSize;
    offset = 0;
    receivedFrames=[];
    nextFrameToBegin = 0;
    for i = 1:frameToBeginSend-1
        for j = 1 : frameSize
            frame(j) = previousFrames(i,j);
        end
        receivedFrames = [receivedFrames;frame];
    end
    for i = frameToBeginSend:frameNumber
        for j = 1 : frameSize
            frame(j) = receivedSignal(j + offset*(frameSize+1));
        end
        parity = isParity(frame);
        offset = offset + 1;
        if parity ~= receivedSignal((frameSize+1)*offset)
            nextFrameToBegin = i;
            for z = i:frameNumber
                receivedFrames = [receivedFrames;frame];
            end
            return;
        end
        receivedFrames = [receivedFrames;frame];
    end
end

