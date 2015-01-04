clc, clear, close all;

filename = 'data01.xlsx';

tableCFilter = xlsread(filename, 'CFilter');
cFilter_xRaw = tableCFilter(:,1);
cFilter_xDirCos = tableCFilter(:,5);

tableAccelerometer = xlsread(filename, 'Accelerometer');
acc_xRaw = tableAccelerometer(:,1);
acc_xDirCos = tableAccelerometer(:,5);

figure(1)
plot(cFilter_xRaw/10^5, cFilter_xDirCos, 'or')
figure(2)
plot(acc_xRaw, acc_xDirCos, 'ob')

2147483647
20000000
16420201.81/10^5
