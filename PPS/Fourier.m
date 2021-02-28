N=1024;
fp=1300; %Hz pr�bkowania
t=0:1/fp:(N-1)/fp;
x=1*sin(2*pi*750*t)+0*sin(2*pi*350*t)+0*randn(1,N);
subplot(211);
plot(t,x);
xlabel('Czas[s]');
ylabel('Sygnal');
Nf=1024;
v=fft(x,Nf); %Widmo zespolone
a=abs(v); %Widmo amplitudowe
r=real(v); %Cz�� rzeczywista
im=imag(v); %Cz�� urojona
faza=unwrap(angle(v)); %Faza
f=linspace(0,fp/2,Nf/2+1); %Generowanie osi cz�stotliwo�ci
subplot(212);
plot(f,a(1:Nf/2+1));
xlabel('Cz�stotliwo��[Hz]');
ylabel('Modul widma');