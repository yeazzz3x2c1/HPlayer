   M S B A M L   `   `   `  ����B  >HPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:HPlayerHPlayer  5   	   (    "HPlayer.UI.Member.Member_Interface   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   )HPlayerclr-namespace:HPlayer     5   	     � Title$  會員��5   	   $��308.5��6    � Icon$* $/HPlayer;component/Source/MyIcon.ico��6"    � WindowStyle$
 None=�6N    � 
ResizeMode$ NoResize=�6a   $��0q�6w    � WindowStartupLocation$ CenterScreen=�6�    � AllowsTransparency . $��2q�6�   ���	#FFDCDCDD$
��Left=�6�   $	��Top=�6  $	��320��6   .��5      � -   5         �Name$
 main��5      .+�5   
   � -   5        $ PowerOffGrid��5      $��Right=�6%   $	��Top=�6A   .+�5      �� -   5         ��Name$ PowerOff��5      $��Right=�6&   $��18��6B   $��	0,14,14,0q�6N   $	��Top=�6a   $��18��6y   $,��&/HPlayer;component/Source/PowerOff.png��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   5	      �� 5
   
   .+�5      �� $��5,5,0,0q�6   	 �� Orientation$	 Vertical=�6*   $
��Left=�6A   $	��Top=�6\   .+�5      �� $��歡迎��6   $
��Left=�6&   $	��Top=�6A   $��24(�6Y   �� 5      $	 
Horizontal=�6   .+�5      �� $	�� - ��6    $
��Left=�6+   $	��Top=�6F   $��18(�6^   �� 5      -   5        
 ��Name$
 Username��5       $��Guest��62   $
��Left=�6?   $	��Top=�6Z   $��18(�6r   �� 5      $��(��6!   $
��Left=�6*   $��Bottom=�6E   $��13(�6`   $��1,0,0,2q�6n   �� 5      -   5        $
 Email��5       $��guest@gmail.com��6/   $
��Left=�6F   $��Bottom=�6a   $��13(�6|   $��1,0,0,2q�6�   �� 5      $��)��6    $
��Left=�6)   $��Bottom=�6D   $��13(�6_   $��1,0,0,2q�6m   5      5      �� 5      $��5,0,0,0q�6   $	 Vertical=�6*   .+�5      �� $	 
Horizontal=�6   $	��Top=�66   .+�5      �� -   5        ���	#00DDDDDD.��5      �� $��上傳歌曲��6$   $��18(�60   5      �� 5      -   5        $
 Upload_State��5       $��上傳就緒.��66   $��15(�6C   $��Center=�6Q   $��Center=�6l   5      �� 5      -   5        ���	#00DDDDDD$
��Left=�5   C   $��0,5,0,0q�6^   .��5      �� $��刪除歌曲��6    $��18(�6,   5      5      5       �� 5!   
   $��	0,122,0,0q�6   .+�5"      � ���	#FFCFCFCF.+�5#      �� $��雲端上的歌曲��6   $��Center=�6*   5$         HPlayer.PlayerList  5%      -	   5          Name$ SongList��5%   !      MultiSelected .    DefaultItemForeground �	#FF595757   DefaultItemBackground
 ����    ScrollSpeed$	 0.2Q�6�   $��0q�6�   ���	#FFDCDCDD$��1,1,0,0q�6�      ScrollMinimumHeight$ 20Q�6�   $	��178��6     ScrollBackground 6�   Z� 6  5&      5'      5(      