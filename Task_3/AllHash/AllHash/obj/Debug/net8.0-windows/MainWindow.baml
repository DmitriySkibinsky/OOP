   M S B A M L   `   `   `  ����  AllHash    HashCalculator.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$#  Хэш-калькулятор��5   	   $	��500��6!   $	��600��6.   .��5      � .+�5   
   �� -   5         ��Name$ InputTextBox��5      $��
10,10,10,0q�6(   $	��Top=�6<   $��30��6T   �� 5
   
    �� Orientation$ 
Horizontal=�6   $��Center=�6.   $	��Top=�6K   $��
10,50,10,0q�6c   .+�5      �� -   5         ��Name$ 
SHA1Button��5      $
��SHA1��6)   $��80��6Q   $��5q�6\   �� 5      -   5        $ SHA224Button��5      $��SHA224��6+   $��80��6W   $��5q�6b   �� 5      -   5        $ SHA256Button��5      $��SHA256��6+   $��80��6W   $��5q�6b   �� 5      -   5        $ SHA384Button��5      $��SHA384��6+   $��80��6W   $��5q�6b   �� 5      -   5        $ SHA512Button��5      $��SHA512��6+   $��80��6W   $��5q�6b   5      �� 5   
   $ 
Horizontal=�6   $��Center=�6.   $	��Top=�6K   $��10,100,10,0q�6c   .+�5      �� -   5        $ 	MD5Button��5      $	��MD5��6(   $��80��6N   $��5q�6Y   �� 5      -   5        $ RIPEMD160Button��5      $��	RIPEMD160��6.   $��80��6`   $��5q�6k   �� 5      -	   5        $ HMACSHA1Button��5      $��HMACSHA1��6-   $��80��6]   $��5q�6h   �� 5      -
   5        $ HMACSHA256Button��5      $��
HMACSHA256��6/   $��80��6c   $��5q�6n   �� 5      -   5        $ CopyToClipboardButton��5      $��Копировать��64   $��80��6m   $��5q�6x   5      �� 5   
   -   5        $ OutputTextBox��5      $��10,180,10,10q�6)   $��Stretch=�6?    �� 
IsReadOnly$
 True��6[    �� TextWrapping$
 Wrap=�6m   5      5      