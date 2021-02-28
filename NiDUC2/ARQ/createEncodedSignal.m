%tworzenie sygna³u do wys³¹nia  z ramek z zakodowanym bitem parzystoœci
%frameSize = rozmair ramki bez bitu przystoœci
function signal =  createEncodedSignal(encodedFrames, frameSize)
    b = length(encodedFrames{1});
    signal_length = length(encodedFrames{1}) * (frameSize + 1);
    signal = 1 : signal_length + length(encodedFrames{2});
    
    for i = 1 : (length(encodedFrames{1}))
        for j = 1 : frameSize + 1
            signal((i - 1) * (frameSize + 1) + j) = encodedFrames{1}(i,j);      
        end
    end
    
    for i = 1 : length(encodedFrames{2} + 1)
        signal(signal_length + i) = encodedFrames{2}(i);
    end
    a = 51;
end