function [receivedFrames,wrongFrames] = decodeFramesSelect(receivedSignal, frameSize, previousFrames, framesToSend)
    frameNumber = size(framesToSend);
    wrongFrames = zeros(frameNumber(1),1);
    frame = 1:frameSize;
    offset = 0;
    receivedFrames=[];
    for i = 1:frameNumber
        if framesToSend(i) == 1
            for j = 1 : frameSize
                frame(j) = receivedSignal(j + offset*(frameSize+1));
            end
            parity = isParity(frame);
            offset = offset + 1;
            if parity ~= receivedSignal((frameSize+1)*offset)
                wrongFrames(i) = 1;
            end
        else
            for j = 1 : frameSize
                frame(j) = previousFrames(i,j);
            end
        end
        receivedFrames = [receivedFrames;frame];
    end
end