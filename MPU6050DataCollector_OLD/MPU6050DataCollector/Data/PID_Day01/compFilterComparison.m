clc, clear, close all;

filename1 = 'data02.xlsx';
filename2 = 'data05.xlsx';
filename3 = 'data06.xlsx';
filename4 = 'data07.xlsx';

tableCFilter1 = xlsread(filename1, 'CFilter');
cFilter_xRaw1 = tableCFilter1(:,1);

tableCFilter2 = xlsread(filename2, 'CFilter');
cFilter_xRaw2 = tableCFilter2(:,1);

tableCFilter3 = xlsread(filename3, 'CFilter');
cFilter_xRaw3 = tableCFilter3(:,1);

tableCFilter4 = xlsread(filename4, 'CFilter');
cFilter_xRaw4 = tableCFilter4(:,1);


figure(1)
x=1:1:length(cFilter_xRaw1)
plot(x, cFilter_xRaw1, '-r')
hold on

x=1:1:length(cFilter_xRaw2)
plot(x, cFilter_xRaw2, '-g')

x=1:1:length(cFilter_xRaw3)
plot(x, cFilter_xRaw3, '-b')

%x=1:1:length(cFilter_xRaw4)
%plot(x, cFilter_xRaw4, '-black')

grid on

legend('compFilter1-gyro 2%', 'compFilter2-gyro 1.5%', 'compFilter3-gyro 1%')







