clc, clear, close all;

<<<<<<< HEAD
filename = 'data06.xlsx';
=======
filename = 'data01.xlsx';
>>>>>>> 3db923b9613e5f26af1cf25d84385db8ef3eb4e0

tableCFilter = xlsread(filename, 'CFilter');
cFilter_xRaw = tableCFilter(:,1);

tableAccelerometer = xlsread(filename, 'Accelerometer');
acc_xRaw = tableAccelerometer(:,1);

tableGyro = xlsread(filename, 'Gyroscope');
gyro_xRaw = tableGyro(:,1);

figure(1)
x=1:1:length(cFilter_xRaw);
plot(x, cFilter_xRaw, '-r')
hold on

x=1:1:length(gyro_xRaw);
plot(x, gyro_xRaw*10, '-g')

x=1:1:length(acc_xRaw);
plot(x, acc_xRaw, '-b')

grid on

legend('compFilter', 'gyro*10', 'acc')






