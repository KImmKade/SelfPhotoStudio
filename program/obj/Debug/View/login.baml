   M S B A M L   `   `   `  ����A  =Onecut, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:wpfTest.ViewwpfTest.View  5          wpfTest.View.login   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:wpfTest.View     5      _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a;mtD1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  login��5
       N� 
Background
 �����.��5      � -   5        .+�5   
   �� $��Center=�6   $��Center=�60    �� Orientation$ 
Horizontal=�6M   .+�5      �� $ Vertical=�6   .+�5      �� $ 
Horizontal=�6   .+�5      �� $��	아이디��6   $	��100��6+   $��15(�67   �� 5      -   5         ��Name$ 	IDTextBox��5      $	��200��61   $��15(�6=   
�������$��10,0,0,0q�6^      MaterialDesignTextBox#��[  6p   $��	아이디��6�   $��Center=�6�   $��Center=�6�   6.  5      �� 5      $ 
Horizontal=�6   .+�5      �� -   5        $��비밀번호��5      $	��100��6,   $��0,10,0,0q�68   $��15(�6J   6X   G� 5      -   5         G�Name$ PasswordBox��5   U     MaterialDesignPasswordBox#��[ 6"   $	��200��6j   $��15(�6v   
�������$��	10,10,0,0q�6�    G� Password$
 word��6�   $��Center=�6�   $��Center=�6�   6B  5      �� 5      $ 
Horizontal=�6   .+�5      (� .��5      �� -   5         ��Name$ autologinbtn��5   #   $��0,10,0,0q�69   $��자동 로그인��6K   $��15(�6\    d� 	IsChecked$
 TrueY�6j   
���   �$��Center=�6�   $��Center=�6�   6�   5      5      5      �� 5      -   5         ��Name$ LoginBtn��5      $	��100��6'   $��50��63   $��	로그인��6?   $��	10,0,0,32q�6M   $��15(�6`   6n   5      �� 5   
   $��Bottom=�6   $��Right=�60   $��	0,0,50,50q�6L   .+�5       �� -   5        $ exitbtn��5       $��종료��6&   
���\\��$	��100��6J   $��50��6V   $��15(�6b   6p   5!      5"      5#      