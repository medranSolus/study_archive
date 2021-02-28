for i = 1 : 100
    ARQ;
    stats(i,1) = bitErrorRateHamming;
    stats(i,2) = bitErrorRateSelectBit;
    stats(i,3) = bitErrorRateStopBit;
    stats(i,4) = crcStatistics(2);  % bitErrorRateCRC
    stats(i,5) = signalSizeRateHamming;
    stats(i,6) = signalSizeRateSelectBit;
    stats(i,7) = signalSizeRateStopBit;
    stats(i,8) = crcStatistics(1);  %signalSizeRateCRC
end
string1 = strcat('n=', num2str(n));
string2 = strcat(string1, ', frameSize=');
string1 = strcat(string2, num2str(frameSize));
string2 = strcat(string1, ', canalNoise=');
string1 = strcat(string2, num2str(canalNoise));

figure(figureCounter)
subplot(2,2,1);
plot([1:100], stats(:,1) * 100, [1:100], stats(:,5));
legend('BER', 'Size ratio');
xlabel('Measurement index');
ylabel('Bit Error Rate [0-1]');
title('Hamming');
subplot(2,2,2);
plot([1:100], stats(:,2)*100, [1:100], stats(:,6));
legend('BER', 'Size ratio');
xlabel('Measurement index');
ylabel('Bit Error Rate [0-1]');
title('Select bit');
subplot(2,2,3);
plot([1:100], stats(:,3)*100, [1:100], stats(:,7));
legend('BER', 'Size ratio');
xlabel('Measurement index');
ylabel('Bit Error Rate [0-1]');
title('Stop bit');
subplot(2,2,4);
plot([1:100], stats(:,4)*100, [1:100], stats(:,8));
legend('BER', 'Size ratio');
xlabel('Measurement index');
ylabel('Bit Error Rate [0-1]');
title('CRC');
suptitle(string1);      % 'Comparision of (BER vs size) in 4 methods'
figureCounter = figureCounter + 1;

figure(figureCounter)
subplot(2,1,1);
plot([1:100], stats(:,1), [1:100], stats(:,2), [1:100], stats(:,3), [1:100], stats(:,4));
legend('Hamming', 'Select Bit', 'Stop Bit', 'CRC');
xlabel('Measurement index');
ylabel('Bit Error Rate [0-1]');
title('Comparision of BERs in 4 methods');

subplot(2,1,2);
plot([1:100], stats(:,5), [1:100], stats(:,6), [1:100], stats(:,7), [1:100], stats(:,8));
legend('Hamming', 'Select Bit', 'Stop Bit', 'CRC');
xlabel('Measurement index');
ylabel('Size ratio');
title('Comparision of sizes in 4 methods');
suptitle(string1);     % 'Comparision of BERs and sizes in 4 methods'
figureCounter = figureCounter + 1;
