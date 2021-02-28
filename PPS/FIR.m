N=200;
fp=1000;
t=0:1/fp:(N-1)/fp;
x=6*sin(2*pi*250*t)+3*sin(2*pi*350*t)+1*randn(1,N);
subplot(411);
plot(t,x);
xlabel('Czas[s]');
ylabel('Sygnal');
h=fir1(1001,[200/(fp/2),300/(fp/2)]); %Filtr 200-300Hz
bhi = fir1(33,400/(fp/2),'high',chebwin(35,30));
y=filter(bhi,1,x); %Filtracja sygna³u
subplot(412);
plot(t,y);
Nf=1024;
hy=abs(fft(bhi,Nf));
f=linspace(0,fp/2,Nf/2+1);
subplot(413);
plot(f,hy(1:Nf/2+1));
v=fft(x,Nf);
a=abs(v);
subplot(414);
plot(f,a(1:Nf/2+1));
xlabel('Czêstotliwoœæ[Hz]');
ylabel('Modul widma');
%freqz(h,1);