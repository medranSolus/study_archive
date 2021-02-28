function encodedFrames = frameEncode(frames, frameSize)
    n =  size(frames{1},1);
    newFrames = zeros(n, frameSize + 1);
    for i = 1 : n
        for j = 1 : frameSize
            newFrames(i,j) = frames{1}(i,j);
        end
        newFrames(i,frameSize+1)= isParity(frames{1}(i,:));
    end
    lastRow = 1 : (length(frames{2}) + 1);
    for i = 1 : length(frames{2})
        lastRow(i) = frames{2}(i);
    end
    lastRow(length(frames{2}) + 1) = isParity(frames{2});
    encodedFrames = {newFrames; lastRow};
end