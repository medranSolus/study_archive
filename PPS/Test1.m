clear;

%Wykres sygna³u o 3 razy mniejszej liczbie próbek
N=200;
fp=10000;
tx=0:1/fp:(N-1)/fp;
x=6*sin(2*pi*250*tx)+3*sin(2*pi*350*tx)+2*randn(1,N);
y=decimate(x,3);
t=0:3/fp:(length(y)-1)*3/fp;
subplot(711);
plot(t,y, 'm');

%Wykres widma amplitudowego sygna³u podstawowego (jak chcesz po decymacji
%to dostosuj jego d³ugoœæ
Nf=1024;
f=linspace(0,fp/2,Nf/2+1);
v=fft(x, Nf);
a=abs(v);
subplot(712);
plot(f,a(1:Nf/2+1));

%Wykres widma filtra dolno...
h=fir1(201,800/(fp/2));
hy=abs(fft(h,Nf));
subplot(713);
plot(f,hy(1:Nf/2+1));

%Wykres widma sygna³u po filtracji
yh=filter(h,1,x);
tf=0:1/fp:(length(yh)-1)/fp;
ayh=abs(fft(yh,Nf));
subplot(714);
plot(f,ayh(1:Nf/2+1));

%---

%Autokorelacja
[rx, taux]=xcorr(x,x,400,'biased');
subplot(716);
plot(taux, rx);

%Korelacja
Y=sin(2*pi*1*tx)+2*randn(1,N);
[r,tau]=xcorr(x,Y,400);
subplot(717);
plot(tau,r);