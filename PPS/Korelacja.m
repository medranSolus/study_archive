t=0:0.3:10;
x=sin(2*pi*1*t);
[r,tau]=xcorr(x,x,floor(0.4*length(x)),'biased'); %Autokorelacja, po prostu tak ma byæ
subplot(211);
plot(tau,r);
n=randn(1,10000);
n1=randn(1,1000);
[rn,taun]=xcorr(n,n1,floor(0.4*length(n))); %Korelacja dwóch szumów, te¿ tak ma byæ po prostu
subplot(212);
plot(taun,rn);