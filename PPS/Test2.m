%zad 1
clear ;
T = 20;
fp = 150;
t=0:1/fp:T;
t1 = 0:1/fp:4;
t2 = 4:1/fp:8;
t3 =8:1/fp:12;
t4 =12:1/fp:20;
y = length(t3);
f1 = 9;
f2 = 10;
f4 = 9;
A = 1;
faza0 = 0;
faza1 = (pi/4);%45 stopni
faza2 = (pi/2);%90 stopni
x1=sin(2*pi*f1*t1+faza0);
x2=sin(2*pi*f2*t2+faza1);
x3=(rand(1,y)-0.5)*2;%nie wiem czemy przez 1/3 ale tak mi amplituda nie wypierdala poza 1
x4=sin(2*pi*f1*t4+faza2);
y1=xcorr(x1,sin(2*pi*9*t1),300);
y2=xcorr(x2,sin(2*pi*9*t1),300);
y3=xcorr(x3,sin(2*pi*9*t1),300);
y4=xcorr(x4,sin(2*pi*9*t1),600);
%figure();
%subplot(4,1,1);
plot(t1,x1,'g');
grid on;
hold on;

%subplot(4,1,2);
plot(t2,x2,'r');
grid on;
hold on;

%subplot(4,1,3);
plot(t3,x3);
grid on;
hold on;

%subplot(4,1,4);
plot(t4,x4,'b');
grid on;
hold on;

title('Sygna³y i szum');
