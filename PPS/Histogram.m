t=0:0.3:10;
x=sin(2*pi*1*t);
subplot(311);
plot(t,x);
xlabel('Czêstotliwoœæ[Hz]');
[h,os]=hist(x,51); %Histogram o 51 przedzia³ach
n=randn(1,10000);
subplot(312);
plot(os,h/length(x)); %Dostosowanie d³ugoœci do wartoœci procentowej
subplot(313);
hist(n,51); %Histogram szumu
xlabel('Wartoœci sygna³u');