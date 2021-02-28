function [sizeRatio, errorRatio]=stopAndWaitCRC(signal, frameSize, canalNoise, secondCanalProbability)
    b = logical(signal);

    frameNumber = fix(length(signal) / frameSize);
    frameCounter = 0;
    retransmissionCounter = - frameNumber;
    while frameCounter < frameNumber
        frame = zeros(1, frameSize);
        index = 1;
        for i = frameCounter * frameSize + 1 : (frameCounter + 1) * frameSize
            frame(index) = b(i);    
            index = index + 1;
        end
        frmError = 1;
        while frmError ~= 0
            retransmissionCounter = retransmissionCounter + 1;
            gen=comm.CRCGenerator([1 0 0 0 0 0 1 0 0 1 1 0 0 0 0 0 1 0 0 0 1 1 0 0 1 1 0 1 1 0 1 1 1]);
            encdata=step(gen,frame');
            encdata=encdata';
            afterTransmission = transmissionCanal(encdata, canalNoise, secondCanalProbability);
            det=comm.CRCDetector([1 0 0 0 0 0 1 0 0 1 1 0 0 0 0 0 1 0 0 0 1 1 0 0 1 1 0 1 1 0 1 1 1]);
            [~,frmError] = step(det,afterTransmission');
        end
        frameCounter = frameCounter + 1;
    end
    sizeRatio = ((frameNumber +  retransmissionCounter) * frameSize) / (frameNumber * frameSize);
    errorRatio = retransmissionCounter / (frameNumber + retransmissionCounter);
end