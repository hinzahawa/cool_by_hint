   M S B A M L   `   `   `  ���� K  Gcool_cal_by_hint, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:cool_cal_by_hintcool_cal_by_hint  #    cool_cal_by_hint.TableProduct   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:cool_cal_by_hint       � Title$  tableProduct��$	��600��$	��900�����	#FFFFC183.��� $��Center=�$	��573��$	��Top=�$	��880�� � RowDefinitions �� $	��300 ��� $	��300 �.+��  � ColumnDefinitions �� �� .+�� $��0��$��0��.+��� -    ��Name$ input1��$
��Left=�$	��167��$	��Top=�$	��226��$��/condenser.jpg���� -   $ input2��$
��Left=�$	��168��$	��Top=�$	��197��$��/assets/fan2-2.jpg��� $��0��$��1��
������� �� $��8* ��� $��2* � �� �� .+��� -   $ 	imageFan1��$��0��$��0��$��Center=�$	��167��$��Center=�$	��200��$��/assets/fan1-1.jpg���� -   $ 	imageFan2��$��0��$��1��$��Center=�$	��168��$��Center=�$	��200��$��/assets/fan1-1.jpg���� $��1��$��0��$	��100��$��30��$��SELECT���� $��1��$��1��$	��100��$��30��$
��BACK��&   System.Windows.Controls.DataGrid  -   $��productList��$��1��$��Center=�   AutoGenerateColumns$ False��   
IsReadOnly$
 True��   CanUserResizeColumns$
 True��   CanUserAddRows$ False��   CanUserSortColumns$
 True��$	��800��$��16(����� 	   Columns	 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn
   Header$
 Model��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *model+  $
 Capacity�� �� *
capacity+  $
 Surface�� �� *	surface+  $
 Number of Fan�� �� *number_of_fan+  $
 	Air Throw�� �� *	air_throw+  $
 Air flow�� �� *
air_flow+  $
 	Fan Speed�� �� *	fan_speed+  $
 Fan  Current�� �� *	current+  $
 Finpit�� �� *	fin_pitch+  $
 Price�� �� *price+