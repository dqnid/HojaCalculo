   M S B A M L   `   `   `  ����F  BHojaCalculo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:HojaCalculoHojaCalculo  5   	       HojaCalculo.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:HojaCalculo     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   $	��225��65   $	��400��6E   .��5	      �  � 	Resources 5
   
    �� 
TargetType �� Property �� Value��%�   '��       '� p     �� 5      " ��6   .�5      ��  � ��
 �����6/   �� 5       � ��
 �   �6/   5      �� 5      " �6   .�5      ��  � ��
 �����6/   5      5       � RowDefinitions 5   
   �� 5      -   5         ��Name$ Canvas_grid��5      $��* �61   �� 5      -   5         �Name$ PanelControl��5      $
��Auto �60    � ColumnDefinitions 5   
   �� 5      -   5        	 ��Name$	 Ancho_Total��5      $��* �64   .+�5   
   � -   5        
 �Name$
 theGrid��5      
������ $��0��6�   .+�5      �� -   5        $��Lienzo��5      }�. 
�������$��10q�6O    � LayoutTransform 5      �� 5       �� ScaleX$ 1Q�6!    �� ScaleY$ -1Q�6,   5      �� 5    
   .+�5!      �� -   5         ��Name$ selectionBox��5"      $y�	Collapsed=�5#      
���   �$��1��5%      5'      5(      � 5)      -   5        $��Panel_Inferior��5)      $��1��6&   $��Bottom=�63    � Orientation$ 
Horizontal=�6N   $��10q�6g   .+�5*      �� -   5        $��Anadir_coleccion��5*      $��Añadir colección��6-   $��0,0,5,0q�6g   5+      5,      5-      