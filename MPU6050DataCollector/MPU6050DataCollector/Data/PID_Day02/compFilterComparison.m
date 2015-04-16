clc, clear, close all;

filename1 = 'data01.xlsx';
filename2 = 'data02.xlsx';
filename3 = 'data03.xlsx';
filename4 = 'data04.xlsx';
filename5 = 'data05.xlsx';

tableCFilter1 = xlsread(filename1, 'CFilter');
cFilter_xRaw1 = tableCFilter1(:,1);

tableCFilter2 = xlsread(filename2, 'CFilter');
cFilter_xRaw2 = tableCFilter2(:,1);

tableCFilter3 = xlsread(filename3, 'CFilter');
cFilter_xRaw3 = tableCFilter3(:,1);

tableCFilter4 = xlsread(filename4, 'CFilter');
cFilter_xRaw4 = tableCFilter4(:,1);

tableCFilter5 = xlsread(filename5, 'CFilter');
cFilter_xRaw5 = tableCFilter5(:,1);

figure(1)
x=1:1:length(cFilter_xRaw1)
degree = cFilter_xRaw1/8234*90;
plot(x, degree, '-r')
hold on

x=1:1:length(cFilter_xRaw2)
degree = cFilter_xRaw2/8234*90;
plot(x, degree, '-g')

x=1:1:length(cFilter_xRaw3)
degree = cFilter_xRaw3/8234*90
plot(x, degree, '-b')

x=1:1:length(cFilter_xRaw4)
degree = cFilter_xRaw4/8234*90;
plot(x, degree, '-xblack')


grid on

ylim([-10 10])
ylabel('degree')
xlabel('iteration')
legend('kp=85 , ki=0     , kd=350, Ess=-6.6', 'kp=85 , ki=1000 , kd=350, Ess=1.706', 'kp=85 , ki=8000 , kd=350, Ess=-0.1743','kp=85 , ki=8000 , kd=300, Ess= 0.0228')

figure(2)
x=1:1:length(cFilter_xRaw5)
degree = cFilter_xRaw5/8234*90;
plot(x, degree, '-oy')
ylim([-100 100])
grid on
legend('kp=85 , ki=8000 , kd=250')
ylabel('degree')
xlabel('iteration')




