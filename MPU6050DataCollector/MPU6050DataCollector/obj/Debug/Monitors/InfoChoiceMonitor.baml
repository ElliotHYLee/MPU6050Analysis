   M S B A M L   `   `   `  ����O  KMPU6050DataCollector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:MPU6050DataCollectorMPU6050DataCollector  5   	   ,    &MPU6050DataCollector.InfoChoiceMonitor   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:MPU6050DataCollector     5   	     � Title$  InfoChoiceMonitor��5   	   $��344.373��6#   $��658.65��64   .��5	      � $��0,0,2,0q�6   .+�5
   
   �� -   5         ��Name$ chkEuler��5
      $��Euler Angles��6%   $
��Left=�6<   $��18��6W   $��	27,70,0,0q�6c   $	��Top=�6v   $��97��6�   6�   �� 5   
   -   5        $ chkGyro��5      $��	Gyroscope��6$   $
��Left=�68   $��18��6S   $��	27,88,0,0q�6_   $	��Top=�6r   $��97��6�   6�   �� 5   
   -   5        $ chkAcc��5      $��Accelerometer��6#   $
��Left=�6;   $��18��6V   $��
27,106,0,0q�6b   $	��Top=�6v   $��97��6�   6�   �� 5   
   -   5        $ chkMag��5      $��Magnetometer��6#   $
��Left=�6:   $��18��6U   $��
27,124,0,0q�6a   $	��Top=�6u   $	��109��6�   6�   �� 5   
   -   5        $ chkMotor��5      $��Motors��6%   $
��Left=�66   $��18��6Q   $��
27,142,0,0q�6]   $	��Top=�6q   $��97��6�   6�   �� 5   
   -   5        $ chkThrottle��5      $��Throttle��6(   $
��Left=�6;   $��18��6V   $��
27,160,0,0q�6b   $	��Top=�6v   $��97��6�   6�   �� 5   
   -   5        $ chkLocalCoord��5      $��Local Coordinate��6*   $
��Left=�6E   $��18��6`   $��
27,178,0,0q�6l   $	��Top=�6�   $	��109��6�   6�   �� 5   
   -   5        $ chkDistGround��5      $��Dist2Gnd��6*   $
��Left=�6=   $��18��6X   $��
27,196,0,0q�6d   $	��Top=�6x   $��97��6�   6�   �� 5   
   -	   5        $ chkAttPidOut��5      $��Att PID Output��6)   $
��Left=�6B   $��18��6]   $��
168,70,0,0q�6i   $	��Top=�6}   $	��109��6�   6�   �� 5   
   -
   5        $ chkNavPidOut��5      $��Nav PID Output��6)   $
��Left=�6B   $��18��6]   $��
168,88,0,0q�6i   $	��Top=�6}   $	��109��6�   6�   �� 5   
   -   5        $ chkAttCtrlRef��5      $��Att Ctrl Reference��6*   $
��Left=�6G   $��18��6b   $��168,106,0,0q�6n   $	��Top=�6�   $	��125��6�   6�   �� 5   
   -   5        $ chkNavCtrlRef��5      $��Nav Ctrl Reference��6*   $
��Left=�6G   $��18��6b   $��168,124,0,0q�6n   $	��Top=�6�   $	��125��6�   6�   �� 5   
   -   5         ��Name$ btnRequestCurrentStatus��5      $��Update��62   $
��Left=�6C   $��24��6^   $��
27,219,0,0q�6j   $	��Top=�6~   $��79��6�   6�   �� 5   
   -   5        $ btnSetUp��5      $	��Set��6#   $
��Left=�61   $��24��6L   $��168,219,0,0q�6X   $	��Top=�6m   $��79��6�   6�   �� 5   
   -   5         ��Name$ txtBin��5      $
��Left=�6"   $��21��6=   $��	61,10,0,0q�6I    �� TextWrapping$
 Wrap=�6\   $�� ��6p   $	��Top=�6x   $	��253��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   .��6�   z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle$ -0.006Q�6&   T� 5      5      .��5    '   5!      �� 5"   
   -   5         ��Name$ label��5"      $	��Bin��6   $
��Left=�6-   $	��Top=�6H   $��	18,10,0,0q�6`   $��26��6s   �� 5#   
   -   5        $ txtDec��5#      $
��Left=�6"   $��21��6=   $��	61,39,0,0q�6I   $
 Wrap=�6\   $�� ��6p   $	��Top=�6x   $��63��6�   �� 5$   
   -   5        $ 
label_Copy��5$      $	��Dec��6$   $
��Left=�62   $	��Top=�6M   $��	18,36,0,0q�6e   $��24��6x   �� 5%   
   -   5        $ btnCheckDefault��5%      $��Default��6*   $
��Left=�6<   $��28��6W   $��
27,259,0,0q�6c   $	��Top=�6w   $��79��6�   6�   �� 5&   
   -   5        $ btnChekcMonitor��5&      $��Monitor��6*   $
��Left=�6<   $��28��6W   $��111,259,0,0q�6c   $	��Top=�6x   $��79��6�   6�   �� 5'   
   -   5        $ btnJoystick��5'      $��Joystick��6&   $
��Left=�69   $��28��6T   $��195,259,0,0q�6`   $	��Top=�6u   $��79��6�   6�   �� 5(   
   -   5        $ pidAttDebug��5(      $��Att PID Debug��6&   $
��Left=�6>   $��28��6Y   $��279,259,0,0q�6e   $	��Top=�6z   $��84��6�   6�   �� 5)   
   -   5        $ pidNavDebug��5)      $��Nav PID Debug��6&   $
��Left=�6>   $��28��6Y   $��368,259,0,0q�6e   $	��Top=�6z   $��89��6�   6�   5,      5-      