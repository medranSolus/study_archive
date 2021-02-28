[x,fp]=audioread('reward.wav');
tx=0:1/fp:(length(x)-1)/fp;
subplot(211);
plot(tx,x);
xlabel('Czas[s]');
y=decimate(x,5);
ty=0:5/fp:(length(y)-1)*5/fp; %Dostosowywanie d³ugoœci osi czasu
subplot(212);
plot(ty,y);
xlabel('Czas[s]');
