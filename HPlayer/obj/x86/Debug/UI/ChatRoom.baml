   M S B A M L   `   `   `  ����B  >HPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:HPlayerHPlayer  5   	       HPlayer.ChatRoom   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:HPlayer     5   	     � Title$  	Chat room��5   	   $��297.812��6   $��488.22��6,    � Icon$* $/HPlayer;component/Source/MyIcon.ico��6;    � WindowStyle$
 None=�6g   $��0q�6z    � WindowStartupLocation$ CenterScreen=�6�    � AllowsTransparency . $��2q�6�   ���	#FFDCDCDD � 
ResizeMode$ 	CanResize=�6�   .��5	      � .+�5
   
   � -   5         �Name$ PowerOffGrid��5
      $��Right=�6%   $	��Top=�6A   .+�5      �� -   5         ��Name$ PowerOff��5      $��Right=�6&   $��18��6B   $��	0,14,14,0q�6N   $	��Top=�6a   $��18��6y   $,��&/HPlayer;component/Source/PowerOff.png��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   5      �� 5   
   $��	聊天室��6   $��18(�6   $
��Left=�6-   $	��Top=�6H   $��10,9,0,0q�6`   �� 5   
   ���	#FF919191$��1q�6)   $��10,43,10,34q�6=   ._�5         HPlayer.PlayerList  -   5        	  Name$	 	Chat_List��5      
   DefaultItemBackground

 �����   DefaultItemForeground
 �   �6U   5      �� 5   
   -   5         ��Name$ Input��5      $
��Left=�6!   $��23��6<   $��
10,265,0,0q�6H    �� TextWrapping$
 Wrap=�6\   $�� ��6p   $	��Top=�6x   $	��397��6�   $��Center=�6�   6�   �� 5   
   -   5        $��發送��5      $
��Left=�6   $��412,265,0,0q�69   $	��Top=�6N   $��62��6f   $��23��6q   ���	#00DDDDDD6�   5      5      