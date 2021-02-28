function frames = enframe(tab, frameSize)
    frameNumber = floor(length(tab)/frameSize);
    frames = [];
    for i = 1 : frameNumber
        newFrame = 1 : frameSize;
        for j = 1 : frameSize
            newFrame(j) = tab(j + (i - 1) * frameSize);
        end
        frames = [frames; newFrame];
    end
end