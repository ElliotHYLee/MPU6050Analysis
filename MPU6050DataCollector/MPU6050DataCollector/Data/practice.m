clc, clear, close all;

filename = 'data02.xlsx';


tableCFilter = xlsread(filename, 'CFilter');
cFilter_xRaw = tableCFilter(:,1);

tableAccelerometer = xlsread(filename, 'Accelerometer');
acc_xRaw = tableAccelerometer(:,1);

tableGyro = xlsread(filename, 'Gyroscope');
gyro_xRaw = tableGyro(:,1);

figure(1)
x=1:1:length(cFilter_xRaw);
degree = cFilter_xRaw/8234*90;
plot(x, degree, '-r')
hold on

% x=1:1:length(gyro_xRaw);
% plot(x, gyro_xRaw, '-xg')
% 
% x=1:1:length(acc_xRaw);
% plot(x, acc_xRaw, '-b')

Ess = mean(degree)
t=0:0.1:600;
plot(x, ones*Ess, '-b')

grid on
hold off
legend('compFilter', 'Ess')
ylim([-20 20])
xlabel('iteration')
ylabel('degree')
title('kp=85 , ki=8000 , kd=250 ')


highAmp = abs(max(cFilter_xRaw/8324*90) - Ess)
lowAmp = abs(min(cFilter_xRaw/8324*90) - Ess)





