   M S B A M L   `   `   `  ���� B  >HPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:HPlayerHPlayer      HPlayer.Player   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:HPlayer     -     �Name$  
MainPlayer�� � Title$ MusicPlayer��$��311.812��$��836.22�� � Icon$ Source/MyIcon.ico�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=�$��0q� � WindowStartupLocation$ CenterScreen=� � AllowsTransparency . $	��1.5q����	#FFDCDCDD.��� -    �Name$ PW�� J� RenderTransformOrigin$ 0.5,0.5&�
�������	 � ColumnDefinitions	 �� .+��� -   
 ��Name$
 EQShow��$
��Left=�$��61��$��	80,31,0,0q�$	��Top=�$	��268��$ 0.5,0.5&��� -    ��Name$ 	SoundCard��$
��Left=�$��
264,92,0,0q�$	��Top=�$��20��$ 
0.95,1.381&����	#FFA62626���	#FFA47E7E J� OpacityMask
 �   �
���   �$��21��|�.  � $��0,219.712,257,0q�$	��Top=�.+�� -   $ TimeGrid��$��0,33.809,0,20q�$	��Top=�.+��� -    ��Name$ NowTimeL��$��00:00��$��11(����	#FF595757$��Courier New)�$
��Left=�$	��Top=�$ 0.5,0.5&�$��29.845,0,0,0q�$��0q���Z� �� -   $ AllTimeL��$��00:00��$��11(����	#FF595757$��Courier New)�$��0,0,29.845,0q�$��Right=�$	��Top=�$��0q���Z� ��  �� Orientation$ Vertical=�.+�   HPlayer.IconBar  -     Name$ MusicPogress��$��27.86��$	��Top=�$
��Auto��   Value$	 100Q�   ScrollColor �	#FF595757   BackGroundColor �	#FFDCDCDD�� $��0,13.021,0,0q�$��Center=�$ 
Horizontal=�$��Center=�.+�� $��0q�$
��Left=�$��Center=�$��34.016��$��34.016��.+��� $��Center=�$��Center=�$��Source/back.png��$��14��$��9��$
��Fill=�$��0,0,0,1q�� $��27,0,0,0q�$
��Left=�$��Center=�$��34.016��$��34.016��.+��� -	   $

 Back��$��Center=�$��Center=�$��Source/back.png��$��14��$��9��$
��Fill=�$��0,0,0,1q��� -
   $

 Play��$��Center=�$��27,0,0,0q�$��Center=�$��Source/Play.png��$ 0.5,0.5&�$��34.016��$��34.016��$
��Fill=�� $��27,0,0,0q�$��34.016��$��34.016��$��Center=�$��Right=�.+��� -   $

 Next��$��Center=�$��0q�$��Center=�$��Source/Next.png��$ 0.5,0.5&�$
��Fill=�$��14��$��9��� $��27,0,0,0q�$��34.016��$��34.016��$��Center=�$��Right=�.+��� $��Center=�$��0q�$��Center=�$��Source/Next.png��$ 0.5,0.5&�$
��Fill=�$��14��$��9��  -   $ 	VolumeBar�����	#FF000000$��Center=�$��34��$��91,24,194,0q�$	��Top=�$	��385��$	 100Q�   MaxValue$	 100Q� �	#FF595757 �	#FFDCDDDD�� -   $��加入歌曲��$
��Left=�$��
486,24,0,0q�$	��Top=�$��75��|�.  � $��	591,0,0,0q����	#33DDDDCA.+�   HPlayer.PlayerList  -     Name$ SongList��$
��Left=�$	��241��   DefaultItemForeground �	#FF595757   DefaultItemBackground
 ����    ScrollSpeed$	 0.2Q�$��0q����	#FFDCDCDD$��1,1,0,0q�$��0,42.604,0,0q�   ScrollBackground Z� �� $��我 的 歌 單��$
��Left=�$	��Top=�$��	10,13,0,0q����	#FF595757$��0q�  -   $ AllList��$
��Left=�$	��100��$��
80,120,0,0q�$	��Top=�$	��222�� �	#FF7C7C7C
 ���� $	 0.2Q�$��0q����	#FF7C7C7C$��1q� Z� � -   $ Window_Control��$��	0,14,14,0q�$��Right=�$	��Top=�$��48��.+�� -   $ MinimumGrid��$��15.213��$
��Left=�.+��� -   $
 Minimum��$
��Left=�$��Center=�$��Source/Minimumsize.png��$ 0.5,0.5&��� -   $
 PowerOff��$��Right=�$��18��$��0q�$	��Top=�$��18��$��Source/PowerOff.png��$ 0.5,0.5&�