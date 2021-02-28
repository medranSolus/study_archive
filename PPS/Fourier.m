N=1024;
fp=1300; %Hz próbkowania
t=0:1/fp:(N-1)/fp;
x=1*sin(2*pi*750*t)+0*sin(2*pi*350*t)+0*randn(1,N);
subplot(211);
plot(t,x);
xlabel('Czas[s]');
ylabel('Sygnal');
Nf=1024;
v=fft(x,Nf); %Widmo zespolone
a=abs(v); %Widmo amplitudowe
r=real(v); %Czêœæ rzeczywista
im=imag(v); %Czêœæ urojona
faza=unwrap(angle(v)); %Faza
f=linspace(0,fp/2,Nf/2+1); %Generowanie osi czêstotliwoœci
subplot(212);
plot(f,a(1:Nf/2+1));
xlabel('Czêstotliwoœæ[Hz]');
ylabel('Modul widma');