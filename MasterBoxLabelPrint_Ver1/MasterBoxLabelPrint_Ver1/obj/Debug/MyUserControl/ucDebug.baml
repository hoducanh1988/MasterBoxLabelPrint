   M S B A M L   `   `   `  ����S  OMasterBoxLabelPrint_Ver1, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null_4clr-namespace:MasterBoxLabelPrint_Ver1.MyUserControl&MasterBoxLabelPrint_Ver1.MyUserControl  5      4    .MasterBoxLabelPrint_Ver1.MyUserControl.ucDebug   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Flocal4clr-namespace:MasterBoxLabelPrint_Ver1.MyUserControl     5      
�������.��5	      � $��5q�6     � RowDefinitions  5
   
   �� 5      $��60 �6   �� 5      $	��150 �6   �� 5      $��* �6   .+�5   
   �� $��0��6   .+�5      �� $��13(�6   $0��*Vui lòng chọn hạng mục cần debug:��6"   �� 5      -   5         ��Name$ cbb_debugtype��5      $��SemiBold"�6.   $��25��6D    �� 
IsEditable . $	��400��6b   $
��Left=�6n   5      �� 5   
   $��1��6   ���#777777$	��0.5q�64   ._�5      �� $��5q�6   .+�5      �� -   5        $��Connect Scale��5   .   $
��Left=�6F   $	��100��6a   $��25��6m   $��0,5,0,0q�6y   �� 5      -   5        $��Disconect Scale��5   .   $
��Left=�6H   $	��100��6c   $��25��6o   $��0,5,0,0q�6{   5      5      � 5   
   $��0,15,0,0q�6   $��2��6!     5      �� 5      $��30 �6    �� 5       $��* �6    .+�5#      �� $��0��6   $��13(�6!   $��Debug Message:��6/   �� 5%      -   5         ��Name$ debug_scroll_viewer��5%      $��2��68   .��5&      �� $��0,0,2,0q�6   
������ $��0.5,0.5,0.5,0.5q�6I    �� 
IsReadOnly .  �� 
LineHeight$ 1��6}   $��0,5,0,0q�6�   . �5'      /�  /� 
PageHeight$	 180��6#   .T�5(      J� .*�6$   .*�5)      �� ���#050928$��Normal"�67   �6K   ��  �� Path$ DebugLog� �� UpdateSourceTrigger$ PropertyChanged=�	 �� Mode$	 TwoWay=�5*      5+      5,      5-      5/      51      52      