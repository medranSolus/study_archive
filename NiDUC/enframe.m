function frames = enframe(tab, frameSize)
    frameNumber = floor(length(tab)/frameSize);
    lastFrameSize = frameSize;
    if mod(length(tab), frameSize) > 0
        frameNumber = frameNumber + 1;
        lastFrameSize = mod(length(tab), frameSize);
    end
    offset = frameSize;
    frames = [];
    for i = 1 : frameNumber
        if i == frameNumber
            frameSize = lastFrameSize;
        end
        newFrame = 1 : frameSize;
        for j = 1 : frameSize
            newFrame(j) = tab(j + (i - 1) * offset);
        end
        if i == frameNumber
            frames = {frames; newFrame};
        else
            frames = [frames; newFrame];
        end
    end
end