   M S B A M L   `   `   `  ����B  >HPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:HPlayerHPlayer  5   	       HPlayer.Setting   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:HPlayer     5   	     � Title$  Setting��5   	   $��78��6   $	��210��6%    � Icon$* $/HPlayer;component/Source/MyIcon.ico��62    � WindowStyle$
 None=�6^    � 
ResizeMode$ NoResize=�6q   $��0q�6�    � WindowStartupLocation$ CenterScreen=�6�    � AllowsTransparency . $��2q�6�   ���	#FFDCDCDD.��5	      � -   5         �Name$
 main��5	      .+�5
   
   � -   5        $ PowerOffGrid��5
      $��Right=�6%   $	��Top=�6A   .+�5      �� -   5         ��Name$ PowerOff��5      $��Right=�6&   $��18��6B   $��	0,14,14,0q�6N   $	��Top=�6a   $��18��6y   $,��&/HPlayer;component/Source/PowerOff.png��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   5      � 5   
   -   5        $ SettingContainer��5      $��0,37,0,0q�6)   $
��Left=�6;   $	��Top=�6V   .+�5      �� -   5        	 ��Name$	 
AutoUpdate��5      $!��自動更新，不再詢問��6+   $
��Left=�6?   $��10,0,0,0q�6Z   $	��Top=�6l   �� 5      -   5        $	 DontCheckUpdate��5      $��不要提醒更新��60   $
��Left=�6A   $��	10,20,0,0q�6\   $	��Top=�6o   5      5      5      