clc, clear, close all;

filename = 'data05.xlsx';


tableCFilter = xlsread(filename, 'CFilter');
cFilter_xRaw = tableCFilter(:,1);

tableAccelerometer = xlsread(filename, 'Accelerometer');
acc_xRaw = tableAccelerometer(:,1);

tableGyro = xlsread(filename, 'Gyroscope');
gyro_xRaw = tableGyro(:,1);

figure(1)
x=1:1:length(cFilter_xRaw);
plot(x, cFilter_xRaw/8234*90, '-or')
hold on
%plot(x, sin(0.05*x+5)-1.388)
% x=1:1:length(gyro_xRaw);
% plot(x, gyro_xRaw, '-xg')
% 
% x=1:1:length(acc_xRaw);
% plot(x, acc_xRaw, '-b')

grid on
hold off
legend('compFilter', 'gyro integral', 'acc')
ylim([-10 10])
xlabel('iteration')
ylabel('degree')

mean(cFilter_xRaw/8234*90)


