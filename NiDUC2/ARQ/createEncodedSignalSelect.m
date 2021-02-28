%tworzenie sygna�u do wys��nia  z ramek z zakodowanym bitem parzysto�ci
%frameSize = rozmair ramki bez bitu przysto�ci
function signal =  createEncodedSignalSelect(encodedFrames, frameSize, framesToSend)
    n = size(encodedFrames);
    frameNumber = 0;
    for i = 1 : n
        if framesToSend(i) == 1
            frameNumber = frameNumber + 1;
        end
    end
    signal = 1 : frameNumber * (frameSize + 1);
    offset = 0;
    for i = 1 : n
        if framesToSend(i) == 1
            for j = 1 : frameSize + 1
                signal(offset*(frameSize + 1) + j) = encodedFrames(i,j);      
            end
            offset = offset + 1;
        end
    end
end