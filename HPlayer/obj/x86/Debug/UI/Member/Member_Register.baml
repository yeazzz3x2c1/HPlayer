   M S B A M L   `   `   `  ����  HPlayer'    !HPlayer.UI.Member.Member_Register   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  會員註冊��5   	   $��210.5��6    � Icon$* $/HPlayer;component/Source/MyIcon.ico��6$    � WindowStyle$
 None=�6P    � 
ResizeMode$ NoResize=�6c   $��0q�6y    � WindowStartupLocation$ CenterScreen=�6�    � AllowsTransparency . $��2q�6�   ���	#FFDCDCDD$
��Left=�6�   $	��Top=�6
  $��160.957��6"  .��5      � -   5         �Name$
 main��5      .+�5   
   � -   5        $ PowerOffGrid��5      $��Right=�6%   $	��Top=�6A   .+�5      �� -   5         ��Name$ PowerOff��5      $��Right=�6&   $��18��6B   $��	0,14,14,0q�6N   $	��Top=�6a   $��18��6y   $,��&/HPlayer;component/Source/PowerOff.png��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   5      �� 5	   
   .+�5
      �� $��5,14,0,0q�6   	 �� Orientation$	 Vertical=�6+   $
��Left=�6B   $	��Top=�6]   .+�5      �� $��註冊��6   $
��Left=�6&   $	��Top=�6A   $��15(�6Y   �� 5      $	 
Horizontal=�6   5      �� 5      $	��Top=�6   $��5,14,0,0q�61   .+�5      �� $	 
Horizontal=�6   .+�5      �� $��帳號: ��6    �� 5      -   5        
 ��Name$
 Account��5      $	��163��6/    �� 	MaxLength$ 16��6;   5      �� 5      $	 
Horizontal=�6   $��0,5,0,0q�66   .+�5      �� $��密碼: ��6    G� 5      -   5         G�Name$ Password��5   "   $	��163��64   5      �� 5      $	 
Horizontal=�6   $��0,5,0,0q�66   .+�5      �� $��確認密碼: ��6    G� 5      -   5        $ Confirm_Password��5   "   $	��139��6<   5      �� 5      $	 
Horizontal=�6   $��0,5,0,0q�66   .+�5      �� $��電子郵件: ��6    �� 5      -   5        $
 Email��5      $	��139��6-   5      �� 5      -   5         ��Name$ Register��5      ���	#00DDDDDD$��0,4,8,0q�6B   ���	#FFABADB3.��5       �� $��確認註冊��6    $��0q�6,   5!      5"      5#      5$      5%      