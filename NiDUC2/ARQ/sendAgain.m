function sendRequest = sendAgain(wrongFrames)
    sendRequest = false;
    for i = 1:size(wrongFrames)
        if wrongFrames(i,1) == 1
            sendRequest = true;
            return;
        end
    end
end