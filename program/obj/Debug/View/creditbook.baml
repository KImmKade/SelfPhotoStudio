   M S B A M L   `   `   `  ����A  =Onecut, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:wpfTest.ViewwpfTest.View  5   	       wpfTest.View.creditbook   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:wpfTest.View     5   	   _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a;mtD1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          �Name$  creditbookwindow��5	       � Title$ 
creditbook��6	   $	��800��66   $
��1360��6C    � WindowStartupLocation$ CenterScreen=�6P   $��Center=�5
   	    � WindowStyle$
 None=�6$   .��5      � -   5         � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��* �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $	��420 �6#   �� 5      $	��420 �6#   �� 5      $	��420 �6#   .+�5      � $��0��6   .+�5      �� -   5         ��Name$ PreViewMonth��5      $��50��6/   $��50��6:   $��0,0,5,0q�6F   $��Right=�6W   
������.��5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ ChevronLeft=�6#   $��30��66   $��30��6A   $��Center=�6M   $��Center=�6h   5      5      � 5      $��1��6   .+�5      �� -   5         ��Name$ 
DateTimeTB��5      $��Center=�60   $��Center=�6M   $
��auto��6h   $
��auto��6u   $��30(�6�   $��Arial)�6�   5      � 5      $��2��6   .+�5      �� -   5        $ 	NextMonth��5      $��50��6,   $��50��67   $��5,0,0,0q�6C   $
��Left=�6T   
������.��5         $ ChevronRight=�6#   $��30��67   $��30��6B   $��Center=�6N   $��Center=�6i   5!      5"      5#      � 5$   
   $��1��6   .+�5%      �� 	 �� Orientation$	 
Horizontal=�6   .+�5&      �� -   5        
 ��Name$
 BankLV��5&   H   $	��800��6<   $��1��6X   $��0��6e   $��10q�6u   
���������6   �� *
instance+ �� View 5'      �� 5(      .P�5)      �� $��        날짜��6-    �� Width$	 140��6A    �� DisplayMemberBinding 6M   �� *
Data_Day+�� 5*      $��카드번호��6-   $	 120��6;    6G   �� *Data_CardNum+�� 5+      $��승인날짜��6-   $	 120��6;    6G   �� *	Data_Date+�� 5,      $��승인번호��6-   $	 120��6;    6G   �� *Data_Authorize+�� 5-      $��고유번호��6-   $	 120��6;    6G   �� *
Data_Vantr+�� 5.      $��가격��6-   $	 120��69    6E   �� *
Data_Price+5/      51      �� 52      $	 Vertical=�6   .+�53      �� -   5         ��Name$ CardSell��53      ��54      �� 55      $	 
Horizontal=�6)   .+�56   "     $ CreditCardCheck=�6/   $��15,0,0,0q�6F   
�������$��30��6k   $��30��6v   �� 57   "   
�������$��10,0,0,0q�6?   $��Center=�6Q   $��15(�6l   매출 취소6�   58      .��5:      �� $	 Vertical=�6%   .+�5;      �� $	 
Horizontal=�6)   .+�5<   "   �� $"��카드번호(앞 6자리) : ��6(   $��Center=�6A   �� 5=   "   -   5         ��Name$ Cardnum��5=   *   $	��100��6;   $��25��6G   $��5,5,5,5q�6S   {�.   �� TextAlignment$ Center=�6v   5>      �� 5?      $	 
Horizontal=�6)   $��0,5,0,0q�6B   .+�5@   "   �� $��승인날짜 : ��6(   $��Center=�6:   �� 5A   "   -	   5        $
 Date��5A   *   $	��100��68   $��25��6D   {�.  $��5,5,5,5q�6b   $ Center=�6s   �� 5B   "   $��금액(\) : ��6(   $��Center=�6;   �� 5C   "   -
   5        $ Price��5C   *   $	��100��69   $��25��6E   $ Center=�6Q   {�.  $��0,5,5,5q�6z   5D      �� 5E      $	 
Horizontal=�6)   .+�5F   "   �� $��승인번호 : ��6(   $��Center=�6:   �� 5G   "   -   5        $ 	Authorize��5G   *   {�.  $	��200��6O   $��25��6[   $��5,5,5,5q�6g   $ Center=�6x   5H      �� 5I      $	 
Horizontal=�6)   .+�5J   "   �� $��거래고유번호 : ��6(   $��Center=�6<   �� 5K   "   -   5        $ Vantr��5K   *   $	��200��69   {�.  $��25��6W   $��5,5,5,5q�6c   $ Center=�6t   5L      �� 5M      -   5        $ requestcancle��5M   %   $	��100��6<   $��30��6H   $��5,5,5,5q�6T   
���\\��{�.  $��취소신청��6�   $��Right=�6�    � 	Resources 5N   "    �� 
TargetType �� Property �� Value��%^   '��       �� 5O   &   " ��6,   .�5P   *   ��  �@��CornerRadius$ 3��6I   5Q   '   5R   #   5S      5T      5U      �� 5V      $	 
Horizontal=�6!   .+�5W      �� $��	총 계 :��6    $	��100��60   $��70��6=   $��Bottom=�6I   $��15(�6d   $��Center=�6r   $��Center=�6�   �� 5X      -   5         ��Name$ 
TotalMoney��5X       $��\��64   $	��100��6@   $��70��6L   $��Bottom=�6X   $��15(�6s   $��Center=�6�   $��Center=�6�   5Y      �� 5Z      -   5        $ CloseBtn��5Z      $	��100��6/   $��50��6;   
���\\��$��닫기��6^   6k   5[      5\      5]      5^      5_      