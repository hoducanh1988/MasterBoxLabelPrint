   M S B A M L   `   `   `  ����S  OMasterBoxLabelPrint_Ver1, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null_4clr-namespace:MasterBoxLabelPrint_Ver1.MyUserControl&MasterBoxLabelPrint_Ver1.MyUserControl  5      >    8MasterBoxLabelPrint_Ver1.MyUserControl.ucRework_SmallQty   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Flocal4clr-namespace:MasterBoxLabelPrint_Ver1.MyUserControl     5      
�������  � 	Resources  5
       �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value��%�  '��       '�� �    '�� r    �� 5   
   " ��6   .�5      ��  � ��$ 0,0,0,1q�60   �� 5       �@��CharacterCasing$ Upper=�60   �� 5       � ��$ 5,0,0,0q�6(   �� 5       � ��$
 Bold"�6+   �� 5       � ��
 �����6+   �� 5       � ��$	 200��6&   �� 5       � ��$ 30��6'   �� 5       � ��$ Center=�69   �� 5       � ��$ 15(�6)    5      P� 5       �@J�	IsFocused . . �5      ��  � �� �#e1f2b863   5      5      �� 5   
   " ��6   .�5      ��  � ��$ 80��6&   �� 5        � ��$ 30��6'   -   5        5"      �� 5$   
   " ��6   .�5%      ��  � ��$ 0,0,0,1q�60   �� 5&       � ��$ 5,0,0,0q�6(   �� 5'       � ��$
 Bold"�6+   �� 5(       � ��
 �����6+    5)      P� 5*       �@J�	IsFocused . . �5+      ��  � �� �#e1f2b863   5,      5.      50      .��52      � .+�53   
   �� 
����� �
���   �$	��Top=�65   $��50��6M   $H��BSố Lượng Nhỏ (1 vài sản phẩm trong 1 LOT sản xuất)��6Y   $��25(�6�   
����  �$��0,0,0,1q�6�   $��SemiBold"�6�   � 54   
   $��0,55,0,0q�6   $	��Top=�6&   $
��Left=�6>   .+�55      �� $��Product LOT: ��6   �� 56      -   5         ��Name$ txt_lot��56      �� 57      $��Product Serial: ��6   $��20,0,0,0q�6/   �� 58      -   5        $ txt_product_serial��58      �� 59      $��10,0,0,0q�6   
 � Tag$ search_lot_rework��6'   $��Search��6?   5:      &   System.Windows.Controls.DataGrid  5<   
   -   5        	  Name$	 data_grid_lot��5<      $��5,95,0,5q�6*   
������ 
   SelectionMode$
 Single=�5=      $��0q�5>         CanUserAddRows .     CanUserDeleteRows .     CanUserResizeRows .     CanUserSortColumns .     CanUserResizeColumns .   � ContextMenu 5E      �� 5F      .@�5G      m� -   5        $��Reprint label��5G      66   m� 5H      -   5        $��Rework��5H      6/   �� 5I      m� 5J      -   5        $��Refresh��5J      60   5K      5M      5O      5Q      