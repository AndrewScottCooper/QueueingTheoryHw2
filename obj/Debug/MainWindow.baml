   M S B A M L   `   `   `  ����U  QQueueing Theory Homework 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Queueing_Theory_Homework_2Queueing_Theory_Homework_2  5   	   U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5   	   +    %Queueing_Theory_Homework_2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:Queueing_Theory_Homework_2     5   	   Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5   	     � Title$  M/M/1 vs M/M/C��5	   	   $	��480��6    $	��873��6-   .��5
      � ���	#FF158995 J� RenderTransformOrigin$ 0.501,0.506&�6"   $��0,0,-33,-53q�6F    � RowDefinitions 5   
   �� 5      $��0* �6   �� 5      .+�5   
   �� -   5         ��Name$ NumOfCTextBox��5      $
��Left=�6)   $��23��6D   $��
279,22,0,0q�6P    �� TextWrapping$
 Wrap=�6d   $��4��6x   $	��Top=�6�   $	��126��6�   $��1��6�   �� 5   
   $.��(Number of servers/ number of mm1 queues:��6   $
��Left=�6C   $��31��6^   $��	10,18,0,0q�6j   
�������$	��Top=�6�   $	��246��6�   $��1��6�   �� 5   
   $��Additional Stats:��6   $
��Left=�6,   $��
553,10,0,0q�6G   $��2��6[   $	��Top=�6l   $	��121��6�   $��31��6�   
�������6�   #  LiveCharts.Wpf.CartesianChart  5   
   &   LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$
 Left=�6>   $��490,180,55,49q�6T   $��2��6k      Series 6   �� *BarCollection+   AxisX 5        LiveCharts.Wpf.Axis     Title$ X Axis��6   	   Labels	 6*   �� *Labels+6F   
   AxisY
 5        $ Y Axis��6      LabelFormatter 6*   �� *	Formatter+6Q   5        5   
   $ Right=�6A   $��31,180,438,49q�6X   $��2��6o    6   �� *SeriesCollection+
 5        $ Sales��6    6)   �� *
YFormatter+6Q    5"        $ Month��6   	 6)   �� *Labels+6E   5$      �� 5%   
   -   5         ��Name$ MMCNumServedLabel��5%      $��MMC Cust. Served:��6+   $
��Left=�6G   $��
440,38,0,0q�6b   $��2��6v   $	��Top=�6�   $	��169��6�   $��31��6�   
�������6�   �� 5&   
   -   5        $ LSLabel��5&      $��LS MMC:  MM1:��6!   $
��Left=�69   $��
666,77,0,0q�6T   $��2��6h   $	��Top=�6y   $	��232��6�   $��33��6�   
�������6�   �� 5'   
   -   5        $ LQLabel��5'      $��LQ MMC:  MM1:��6!   $
��Left=�69   $��666,113,0,0q�6T   $��2��6i   $	��Top=�6z   $	��232��6�   $��33��6�   
�������6�   �� 5(   
   -   5         ��Name$ SimStartBtn��5(      $��Run Simulation!��6&   $
��Left=�6@   $��279,100,0,0q�6[   $��2��6p   $	��Top=�6�   $	��126��6�   $��33��6�   ���	#FFD4D4466�   �� 5)   
   $��Simulation Time:��6   $
��Left=�6+   $��31��6F   $��	10,42,0,0q�6R   
�������$	��Top=�6   $��97��6�   $��1��6�   �� 5*   
   -   5        $ TimeTextBox��5*      $
��Left=�6'   $��23��6B   $��
112,46,0,0q�6N   $��2��6b   $
 Wrap=�6s   $
��1500��6�   $	��Top=�6�   $	��120��6�   �� 5+   
   -   5        $ LambdaTextBox��5+      $
��Left=�6)   $��23��6D   $��
112,78,0,0q�6P   $��2��6d   $
 Wrap=�6u   $��7��6�   $	��Top=�6�   $	��120��6�   �� 5,   
   -   5        $ 	MuTextBox��5,      $
��Left=�6%   $��23��6@   $��112,110,0,0q�6L   $��2��6a   $
 Wrap=�6r   $��15��6�   $	��Top=�6�   $	��120��6�   �� 5-   
   $��Arrival Rate λ:��6   $
��Left=�6*   $��31��6E   $��	10,74,0,0q�6Q   
�������$	��Top=�6~   $��97��6�   $��1��6�   �� 5.   
   $��Service Rate μ:��6   $
��Left=�6*   $��31��6E   $��
10,106,0,0q�6Q   
�������$	��Top=�6   $��97��6�   $��1��6�   �� 5/   
   -	   5        $ MM1NumServedLabel��5/      $��C MM1 Cust. Served:��6+   $
��Left=�6I   $��
616,38,0,0q�6d   $��2��6x   $	��Top=�6�   $	��169��6�   $��31��6�   
�������6�   �� 50   
   -
   5        $ WSLabel��50      $��WS MMC:  MM1:��6!   $
��Left=�69   $��
440,78,0,0q�6T   $��2��6h   $	��Top=�6y   $	��234��6�   $��31��6�   
�������6�   �� 51   
   -   5        $ WQLabel��51      $��WQ MMC:  MM1:��6!   $
��Left=�69   $��440,114,0,0q�6T   $��2��6i   $	��Top=�6z   $	��234��6�   $��31��6�   
�������6�   �� 52   
   $��WS Graph��6   $
��Left=�6#   $��31��6>   $��181,162,0,0q�6J   
�������$	��Top=�6y   $��97��6�   $��1��6�   �� 53   
   $��MMC VS C MM1 Comparison��6   $
��Left=�62   $��31��6M   $��630,149,0,0q�6Y   
�������$	��Top=�6�   $	��165��6�   $��1��6�   55      56      