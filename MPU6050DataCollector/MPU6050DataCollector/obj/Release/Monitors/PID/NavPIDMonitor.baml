   M S B A M L   `   `   `  ���� O  KMPU6050DataCollector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:MPU6050DataCollectorMPU6050DataCollector  (    "MPU6050DataCollector.NavPIDMonitor   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:MPU6050DataCollector       � Title$  NavPIDMonitor��$��437.697��$��517.967��.��� $��0,0,2,1q�.+��� -   $��
txtKpPitch��$
��Left=�$��23��$��
133,62,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$��65���� -    ��Name$ label��$��Kp��$
��Left=�$��26��$��	91,59,0,0q�$	��Top=�$��37��$��10.667(��� -   $��
txtKdPitch��$
��Left=�$��23��$��
133,88,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ 
label_Copy��$��Kd��$
��Left=�$��26��$��	91,85,0,0q�$	��Top=�$��37��$��10.667(��� -   $��
txtKiPitch��$
��Left=�$��23��$��133,116,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ label_Copy1��$��Ki��$
��Left=�$��26��$��
91,111,0,0q�$	��Top=�$��37��$��10.667(��� -   $ label1��$��Pitch��$
��Left=�$��24��$��
133,33,0,0q�$	��Top=�$��65���� -   $��	txtKpRoll��$
��Left=�$��23��$��
233,62,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -	   $��	txtKdRoll��$
��Left=�$��23��$��
233,88,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -
   $��	txtKiRoll��$
��Left=�$��23��$��233,116,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ label1_Copy��$
��Roll��$
��Left=�$��24��$��
233,33,0,0q�$	��Top=�$��65���� -   $��txtKpHeightAsc��$
��Left=�$��23��$��
329,62,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $��txtKdHeightAsc��$
��Left=�$��23��$��
329,88,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $��txtKiHeight��$
��Left=�$��23��$��362,116,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ label1_Copy1��$��Thrust/Height��$
��Left=�$��29��$��
349,15,0,0q�$	��Top=�$��95���� -   $��btnSetPitch��$��	Set Pitch��$
��Left=�$��21��$��133,155,0,0q�$	��Top=�$��65���� -   $��
btnSetRoll��$��Set Roll��$
��Left=�$��21��$��235,155,0,0q�$	��Top=�$��65���� -   $��btnSetHeight��$��
Set Height��$
��Left=�$��21��$��362,155,0,0q�$	��Top=�$��65���� -   $��txtKpHeightDesc��$
��Left=�$��23��$��
402,62,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $��txtKdHeightDesc��$
��Left=�$��23��$��
402,88,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ label2��$��Ascent��$
��Left=�$��24��$��
330,33,0,0q�$	��Top=�$��52���� -   $ label2_Copy��$��Descent��$
��Left=�$��24��$��
409,33,0,0q�$	��Top=�$��52���� -   $��txtKpPitchOutput��$
��Left=�$��24��$��135,262,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $��txtKdPitchOutput��$
��Left=�$��24��$��135,288,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $��txtKiPitchOutput��$
��Left=�$��24��$��135,316,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $��txtKpRollOutput��$
��Left=�$��24��$��248,262,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $��txtKdRollOutput��$
��Left=�$��24��$��248,288,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $��txtKiRollOutput��$
��Left=�$��24��$��248,316,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $��txtKiHeightOutput��$
��Left=�$��24��$��362,316,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $��txtKpHeightOutput��$
��Left=�$��24��$��362,261,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $��txtKdHeightOutput��$
��Left=�$��24��$��362,287,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -    $ label_Copy2��$��Kp��$
��Left=�$��26��$��
93,264,0,0q�$	��Top=�$��37��$��10.667(��� -!   $ label_Copy3��$��Kd��$
��Left=�$��26��$��
93,290,0,0q�$	��Top=�$��37��$��10.667(��� -"   $ label_Copy4��$��Ki��$
��Left=�$��26��$��
93,316,0,0q�$	��Top=�$��37��$��10.667(��� -#   $��btnNavOnOffRequest��$��Update OnOff��$
��Left=�$��22��$��
18,218,0,0q�$	��Top=�$��97���� -$   $��	btnXonoff��$	��Off��$
��Left=�$	��Top=�$��65��$��135,220,0,0q����	#FFE4E4EA�� -%   $��	btnYonoff��$	��Off��$
��Left=�$	��Top=�$��65��$��248,220,0,0q��� -&   $��	btnZonoff��$	��Off��$
��Left=�$	��Top=�$��65��$��362,218,0,0q�$��22���� -'   $��txtPitchOutput��$
��Left=�$��21��$��135,345,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -(   $��label_Copy5��$��output��$
��Left=�$��26��$��
86,342,0,0q�$	��Top=�$��44��$��10.667(��� -)    ��Name$ txtRollOutput��$
��Left=�$��21��$��248,345,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -*   $ txtHeightOutput��$
��Left=�$��21��$��362,347,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -+   $��btnRequestConstants��$��Renew Consts��$
��Left=�$��23��$��
18,155,0,0q�$	��Top=�$��97��