   M S B A M L   `   `   `  ���� O  KMPU6050DataCollector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:MPU6050DataCollectorMPU6050DataCollector  (    "MPU6050DataCollector.NavPIDMonitor   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:MPU6050DataCollector       � Title$  NavPIDMonitor��$��409.197��$��445.967��.��� $��0,0,2,1q�.+��� -    ��Name$ 
txtKpPitch��$
��Left=�$��23��$��	73,47,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$��65���� -    ��Name$ label��$��Kp��$
��Left=�$��26��$��	31,44,0,0q�$	��Top=�$��37��$��10.667(��� -   $ 
txtKdPitch��$
��Left=�$��23��$��	73,73,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ 
label_Copy��$��Kd��$
��Left=�$��26��$��	31,70,0,0q�$	��Top=�$��37��$��10.667(��� -   $ 
txtKiPitch��$
��Left=�$��23��$��
73,101,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ label_Copy1��$��Ki��$
��Left=�$��26��$��	31,96,0,0q�$	��Top=�$��37��$��10.667(��� -   $ label1��$��Pitch��$
��Left=�$��24��$��	73,18,0,0q�$	��Top=�$��65���� -   $ 	txtKpRoll��$
��Left=�$��23��$��
141,47,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -	   $ 	txtKdRoll��$
��Left=�$��23��$��
141,73,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -
   $ 	txtKiRoll��$
��Left=�$��23��$��141,101,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ label1_Copy��$
��Roll��$
��Left=�$��24��$��
141,18,0,0q�$	��Top=�$��65���� -   $ txtKpHeightAsc��$
��Left=�$��23��$��
211,47,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ txtKdHeightAsc��$
��Left=�$��23��$��
211,73,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ txtKiHeight��$
��Left=�$��23��$��251,101,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ label1_Copy1��$��Thrust/Height��$
��Left=�$��29��$��	238,0,0,0q�$	��Top=�$��95���� -    ��Name$ btnSetPitch��$��	Set Pitch��$
��Left=�$��21��$��
73,236,0,0q�$	��Top=�$��65���� -   $ 
btnSetRoll��$��Set Roll��$
��Left=�$��21��$��143,236,0,0q�$	��Top=�$��65���� -   $ btnSetHeight��$��
Set Height��$
��Left=�$��21��$��251,236,0,0q�$	��Top=�$��65���� -   $ txtKpHeightDesc��$
��Left=�$��23��$��
291,47,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ txtKdHeightDesc��$
��Left=�$��23��$��
291,73,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ label2��$��Ascent��$
��Left=�$��24��$��
219,18,0,0q�$	��Top=�$��52���� -   $ label2_Copy��$��Descent��$
��Left=�$��24��$��
298,18,0,0q�$	��Top=�$��52���� -   $ txtKpPitchSet��$
��Left=�$��23��$��
73,145,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ txtKdPitchSet��$
��Left=�$��23��$��
73,171,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ txtKiPitchSet��$
��Left=�$��23��$��
73,199,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ txtKpRollSet��$
��Left=�$��23��$��141,145,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ txtKdRollSet��$
��Left=�$��23��$��141,171,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ txtKiRollSet��$
��Left=�$��23��$��141,199,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ txtKpHeightAscSet��$
��Left=�$��23��$��211,145,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ txtKdHeightAscSet��$
��Left=�$��23��$��211,171,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -   $ txtKiHeightSet��$
��Left=�$��23��$��251,199,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -    $ txtKpHeightDescSet��$
��Left=�$��23��$��291,145,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -!   $ txtKdHeightDescSet��$
��Left=�$��23��$��291,171,0,0q�$
 Wrap=�$�� ��$	��Top=�$��65���� -"   $ label_Copy2��$��Kp��$
��Left=�$��26��$��
31,147,0,0q�$	��Top=�$��37��$��10.667(��� -#   $ label_Copy3��$��Kd��$
��Left=�$��26��$��
31,173,0,0q�$	��Top=�$��37��$��10.667(��� -$   $ label_Copy4��$��Ki��$
��Left=�$��26��$��
31,199,0,0q�$	��Top=�$��37��$��10.667(��� -%   $ btnSync��$
��Sync��$
��Left=�$��21��$��
31,236,0,0q�$	��Top=�$��37��