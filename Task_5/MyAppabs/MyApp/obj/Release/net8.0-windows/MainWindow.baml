   M S B A M L   `   `   `  ���� 	  MyApp    YourNamespace.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Products��$	��450��$	��800��.���  � RowDefinitions �� $
��Auto ��� $��* �.+��� .@��� $��Products��.���  �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��Add Product��$	��100��$
��Left=�$��10q����� *OpenAddProductWindowCommand+�� $��1��$
��Auto=�$
��Auto=�.���� ���� *
Products+���� .R�$� ���� .R��� 
���   �$��1q�$��5q� �� Padding$ 10q�._��� .+��� $
��Bold"�$��16(����� *Name+�� $
��Wrap=�$��0,5,0,0q����� *Description+�� $��0,5,0,0q����� *Price+ �� StringFormat$ `{0:C}`���� $��Add to Cart������ *DataContext.AddToCartCommand+ �� RelativeSource ��  �� AncestorType ItemsControl���� �� $��2�� �� Orientation$ 
Horizontal=�$��Center=�$��10q�.+��� $��Previous��$��75��$��0,0,10,0q����� *PreviousPageCommand+�� $��Center=�$��0,0,10,0q����� *CurrentPage+�� $��/��$��Center=�$��0,0,10,0q��� $��Center=�$��0,0,10,0q����� *
TotalPages+�� $
��Next��$��75������ *NextPageCommand+�� $
��Cart��.���  �� $��* ��� $
��Auto �.+��� $��0��$
��Auto=�$
��Auto=�.���� ���� *	CartItems+���� .R�$� ���� .R��� 
���   �$��1q�$��5q�$ 10q�._��� .+��� $
��Bold"�$��16(����� *Name+�� $
��Wrap=�$��0,5,0,0q����� *Description+�� $��0,5,0,0q����� *Price+$ `{0:C}`���� $��0,5,0,0q����� *
Quantity+$ Quantity: {0}����  � Minimum$ 1Q� � Maximum$ 10Q�$	��100��$��0,0,10,0q�	 � Value	 �� *
Quantity+
 �� UpdateSourceTrigger$
 PropertyChanged=��� $��Remove������ *#!DataContext.RemoveFromCartCommand+ ��  ItemsControl���� �� $��1��$ 
Horizontal=�$��Right=�$��10q�.+��� $��Total Price:��$��Center=�$��0,0,5,0q��� $��Center=�$��0,0,10,0q����� *
TotalPrice+$ `{0:C}`���� $	��Buy��$��75������ *
BuyCommand+�� $��Order��.���  �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$ 
Horizontal=�$��10q�.+��� $��Address:��$��Center=�$��0,0,5,0q��� $	��200��$��0,0,10,0q����� *CustomerAddress+$
 PropertyChanged=��� $��Set Address��$	��100������ *SetCustomerAddressCommand+���� *CustomerAddress+�� $��1��$ 
Horizontal=�$��Right=�$��10q�.+��� $��Place Order��$	��100��$��0,0,10,0q����� *PlaceOrderCommand+�� $��Cancel Order��$	��100��$��0,0,10,0q����� *CancelOrderCommand+�� $��Confirm Order��$	��100������ *ConfirmOrderCommand+