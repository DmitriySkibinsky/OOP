   M S B A M L   `   `   `  ����	  MyApp$    YourNamespace.AddProductWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Add Product��5   	   $	��300��6   $	��400��6*   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��Name:��6   $��Center=�6!   $��10q�6<   �� 5   
   $��1��6   $	��200��6d   $��10q�6p   ��6   �� *NewProductName+ �� UpdateSourceTrigger$ PropertyChanged=�6p   �� 5   
   $��2��6   $��Description:��6!   $��Center=�65   $��10q�6P   �� 5   
   $��3��6   $	��200��6k   $��10q�6w   ��6   �� *NewProductDescription+$ PropertyChanged=�6w   �� 5   
   $��4��6   $��Price:��6!   $��Center=�6/   $��10q�6J   �� 5   
   $��5��6   $	��200��6e   $��10q�6q   ��6   �� *NewProductPrice+$ PropertyChanged=�6q   �� 5   
   $��6��6   $	��Add��6   $��75��6R   $��Right=�6]   $��10q�6y   ��6,   �� *AddProductCommand+6y   5      5      