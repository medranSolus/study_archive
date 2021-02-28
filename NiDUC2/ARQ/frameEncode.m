function encodedFrames = frameEncode(frames, frameSize)
    n =  size(frames);
    newFrame = 1 : frameSize + 1;
    encodedFrames = [];
    for i = 1 : n
        for j = 1 : frameSize
            newFrame(j) = frames(i,j);
        end
        newFrame(frameSize+1)= isParity(frames(i,:));
        encodedFrames = [encodedFrames; newFrame];
    end
end