   M S B A M L   `   `   `  ����S  OMasterBoxLabelPrint_Ver1, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null_4clr-namespace:MasterBoxLabelPrint_Ver1.MyUserControl&MasterBoxLabelPrint_Ver1.MyUserControl  5      2    ,MasterBoxLabelPrint_Ver1.MyUserControl.ucLog   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Flocal4clr-namespace:MasterBoxLabelPrint_Ver1.MyUserControl     5      
�������.��5
      � $��5q�6     � RowDefinitions  5   
   �� 5      $��60 �6   �� 5      $��* �6   .+�5   
   �� $��0��6   .+�5      �� $��13(�6   $?��9Vui lòng chọn loại dữ liệu log cần truy vấn:��6"   � 5      .+�5      �� -   5         ��Name$ cbb_logtype��5      $��SemiBold"�60   $��25��6F    �� 
IsEditable . $	��400��6d   $
��Left=�6p   �� 5      -   5        $��Go��5      $��10,0,0,0q�6&   $��70��68   6C   5      5      �� 5   
   $��1��6   ���#777777$	��0.5q�64   ._�5      �   5      �� 5      $��50 �6$   �� 5      $��50 �6$   �� 5      $��* �6$   .+�5"      �� $��0��6   
����� �
���   �$	��Top=�6J   $��50��6b   $)��#Calib trọng lượng sản phẩm��6n   $��25(�6�   
����  �$��0,0,0,1q�6�   $��SemiBold"�6�   �� 5#      $��1��6   ���#777777$��	0,0,0,0.5q�6<   ._�5$      � .+�5%      �� -   5         ��Name$ btn_startcalib��5%   !   $��5,5,0,0q�6N   $��Start Calib��6_   $��30��6u   $	��100��6�   {�. 6�   �� 5&      -   5        $ btn_stopcalib��5&   !   $��10,5,0,0q�6M   $��
Stop Calib��6_   $��30��6t   $	��100��6�   {�.  6�   �� 5'      $��20,5,0,0q�6    $9��3Số lượng sản phẩm calib trọng lượng: ��62   �� 5(      -   5         ��Name$ txt_CalibQty��5(   "   $��5,5,0,0q�69   $��30��6J   $	��100��6V   $��Center=�6b   $
��Left=�6�   $��10,0,0,0q�6�   $��15(�6�   $
��Bold"�6�   $	��100��5)   "   5*      5+      � 5-      $��2��6    � ColumnDefinitions 5.      �� 5/      $��* �6+   �� 50      $��* �6+   .+�53      &   System.Windows.Controls.DataGrid  -   5          Name$ datagrid_calib��53      $��1��67      AutoGenerateColumns .     CanUserAddRows .  	   CanUserDeleteRows	 .  
   CanUserResizeColumns
 .     CanUserResizeRows .     CanUserSortColumns .     CanUserReorderColumns .  "   VerticalScrollBarVisibility$
 Auto=�54   �   $   HorizontalScrollBarVisibility$
 Auto=�6�   
������ $��	0,0,0.5,0q�56         GridLinesVisibility$ 
Horizontal=�57         HeadersVisibility$ Column=�58      $��Center=�59      $��Bottom=�5:      $��14(�5;         SelectionUnit$ FullRow=�5<         SelectionMode$ Single=�67   $��5q�5=      ��53   D   ��    Columns 5?      0  *System.Windows.Controls.DataGridTextColumn  5@      ,  &System.Windows.Controls.DataGridColumn   Header$ ORDER��6}      Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.5* 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Order+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�6�     5A      $ WEIGHT��6~   $ * 6�      
FontWeight$
 Bold"�6�    61   �� *Weight+$ PropertyChanged=�$ TwoWay=�6�     5B      $ U.O.M��6{   $
 0.5* 6�    61   �� *UOM+$ PropertyChanged=�$ TwoWay=�6�   5E      � 5G      $��1��6   .+�5H      �� $	��Top=�6    $
��Left=�68   $��14(�6S   $-��'Giá trị trọng lượng trung bình��6a   �� 5I      -   5         ��Name$ lbl_avr��5I   e   $	��Top=�6    $
��Left=�68   $��0,25,0,0q�6S   $��30(�6v   $
��Bold"�6�   $��--��6�   �� 5J      -   5         ��Name$ _scrollViewer��5J   :   $��	0,100,0,0q�6'   .��5K      �� 
������ $��0q�6D    �� 
IsReadOnly .  �� 
LineHeight$ 1��6j   $��0,5,0,0q�6   . �5L   "   /�  /� 
PageHeight$	 180��6/   .T�5M   &   J� .*�60   .*�5N   *   �� ���#050928$��Normal"�6C   �6W   ��   �� Path$  
WeightData�$ PropertyChanged=�$ TwoWay=�5O   '   5P   #   5Q      5R      5S      5T      5V      5W      5Y      5Z      