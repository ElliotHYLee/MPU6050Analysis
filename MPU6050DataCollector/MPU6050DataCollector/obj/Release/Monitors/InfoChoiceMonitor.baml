   M S B A M L   `   `   `  ���� O  KMPU6050DataCollector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:MPU6050DataCollectorMPU6050DataCollector  ,    &MPU6050DataCollector.InfoChoiceMonitor   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:MPU6050DataCollector       � Title$  InfoChoiceMonitor��$��344.373��$��658.65��.��� $��0,0,2,0q�.+��� -    ��Name$ chkEuler��$��Euler Angles��$
��Left=�$��18��$��	27,70,0,0q�$	��Top=�$��97���� -   $ chkGyro��$��	Gyroscope��$
��Left=�$��18��$��	27,88,0,0q�$	��Top=�$��97���� -   $ chkAcc��$��Accelerometer��$
��Left=�$��18��$��
27,106,0,0q�$	��Top=�$��97���� -   $ chkMag��$��Magnetometer��$
��Left=�$��18��$��
27,124,0,0q�$	��Top=�$	��109���� -   $ chkMotor��$��Motors��$
��Left=�$��18��$��
27,142,0,0q�$	��Top=�$��97���� -   $ chkThrottle��$��Throttle��$
��Left=�$��18��$��
27,160,0,0q�$	��Top=�$��97���� -   $ chkLocalCoord��$��Local Coordinate��$
��Left=�$��18��$��
27,178,0,0q�$	��Top=�$	��109���� -   $ chkDistGround��$��Dist2Gnd��$
��Left=�$��18��$��
27,196,0,0q�$	��Top=�$��97���� -	   $ chkAttPidOut��$��Att PID Output��$
��Left=�$��18��$��
168,70,0,0q�$	��Top=�$	��109���� -
   $ chkNavPidOut��$��Nav PID Output��$
��Left=�$��18��$��
168,88,0,0q�$	��Top=�$	��109���� -   $ chkAttCtrlRef��$��Att Ctrl Reference��$
��Left=�$��18��$��168,106,0,0q�$	��Top=�$	��125���� -   $ chkNavCtrlRef��$��Nav Ctrl Reference��$
��Left=�$��18��$��168,124,0,0q�$	��Top=�$	��125���� -    ��Name$ btnRequestCurrentStatus��$��Update��$
��Left=�$��24��$��
27,219,0,0q�$	��Top=�$��79���� -   $ btnSetUp��$	��Set��$
��Left=�$��24��$��168,219,0,0q�$	��Top=�$��79���� -    ��Name$ txtBin��$
��Left=�$��21��$��	61,10,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$	��253�� J� RenderTransformOrigin$ 0.5,0.5&�.��z�V� .~��� �� ��  �� Angle$ -0.006Q�T� .���� -    ��Name$ label��$	��Bin��$
��Left=�$	��Top=�$��	18,10,0,0q�$��26���� -   $ txtDec��$
��Left=�$��21��$��	61,39,0,0q�$
 Wrap=�$�� ��$	��Top=�$��63���� -   $ 
label_Copy��$	��Dec��$
��Left=�$	��Top=�$��	18,36,0,0q�$��24���� -   $ btnCheckDefault��$��Default��$
��Left=�$��28��$��
27,259,0,0q�$	��Top=�$��79���� -   $ btnChekcMonitor��$��Monitor��$
��Left=�$��28��$��111,259,0,0q�$	��Top=�$��79���� -   $ btnJoystick��$��Joystick��$
��Left=�$��28��$��195,259,0,0q�$	��Top=�$��79���� -   $ pidAttDebug��$��Att PID Debug��$
��Left=�$��28��$��279,259,0,0q�$	��Top=�$��84���� -   $ pidNavDebug��$��Nav PID Debug��$
��Left=�$��28��$��368,259,0,0q�$	��Top=�$��89��