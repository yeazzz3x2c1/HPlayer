   M S B A M L   `   `   `  ���� B  >HPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:HPlayerHPlayer      HPlayer.Player   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:HPlayer     -     �Name$  
MainPlayer�� � Title$ MusicPlayer��$��311.812��$��836.22�� � Icon$* $/HPlayer;component/Source/MyIcon.ico�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=�$��0q� � WindowStartupLocation$ CenterScreen=� � AllowsTransparency . $��2q����	#FFDCDCDD.��� -    �Name$ PW�� J� RenderTransformOrigin$ 0.5,0.5&�
�������$��0q�	 � ColumnDefinitions	 �� .+�� -   $ EQ_Grid��$��Bottom=�$��0q�$��36��$
��Left=�$	��591��
 J� Opacity$	
 0.5Q�.+��� -    ��Name$ EQShow��$��0q�$ 0.5,0.5&��� -    ��Name$ 	SoundCard��$
��Left=�$��
264,92,0,0q�$	��Top=�$��20��$ 
0.95,1.381&����	#FFA62626���	#FFA47E7E J� OpacityMask
 �   �
���   �$��21��|�.  � $��0,220,241,0q�$	��Top=�.+�� -   $ TimeGrid��$��0,34,0,0q�$	��Top=�.+��� -    ��Name$ NowTimeL��$��00:00��$��11(����	#FF595757$��Courier New)�$
��Left=�$	��Top=�$ 0.5,0.5&�$��29.845,0,0,0q�$��0q���Z� �� -   $ AllTimeL��$��00:00��$��11(����	#FF595757$��Courier New)�$��0,0,29.845,0q�$��Right=�$	��Top=�$��0q���Z� ��  �� Orientation$ Vertical=�.+�   HPlayer.IconProgress  -	     Name$ MusicPogress��$��27.86��$	��Top=�$
��Auto��   ProgressForeground �	#FF595757   ProgressBackground �	#FFDCDCDD   Value$	 -50Q��� $��0,13.021,0,0q�$��Center=�$ 
Horizontal=�$��Center=�.+�� -
   $��0q�$
��Left=�$��Center=�$��34.016��$��34.016��.+�� $��8,8,8.016,10.016q�$��14��$��14��.+��� -   $
 Mode��$��Center=�$��Center=�$/��)/HPlayer;component/Source/Mode/Random.png��$��0q�� -   $��13,0,0,0q�$
��Left=�$��Center=�$��34.016��$��34.016�����	#00000000.+��� -   $
 Back��$��Center=�$��Center=�$(��"/HPlayer;component/Source/back.png��$��14��$��9��$
��Fill=�$��0,0,0,1q��� -   $
 Play��$��Center=�$��20,0,0,0q�$��Center=�$(��"/HPlayer;component/Source/Play.png��$ 0.5,0.5&�$��34.016��$��34.016��$
��Fill=�� -   $��20,0,0,0q�$��34.016��$��34.016��$��Center=�$��Right=����	#00000000.+��� -   $
 Next��$��Center=�$��0q�$��Center=�$(��"/HPlayer;component/Source/next.png��$ 0.5,0.5&�$
��Fill=�$��14��$��9��� $��13,0,0,0q�$��34.016��$��34.016��$��Center=�$��Right=�.+�� $��0q�$��14��$��14��.+��� -   $ Favorite��$��Center=�$��0,0,-0.016,0.984q�$��Center=�$,��&/HPlayer;component/Source/favorite.png��$ 0.5,0.5&�  -   $ 	VolumeBar��$��Center=�$��8��$��80,53,367,0q�$	��Top=�$	��385��$	 100Q�   MaxmumValue$	 100Q� �	#FF595757 �	#FFDCDDDD   IconVisibility$ Hidden=�   ProgressMargin$ 0q�    ProgressVerticalAlignment$ Stretch=�   
FullHeight . �� -   $��加入歌曲��$
��Left=�$��
486,24,0,0q�$	��Top=�$��75��|�.  � $��	591,0,0,0q����	#33DDDDCA.+��� $��0,42.604,0,0q�$ 
Horizontal=�.+�   HPlayer.PlayerList  -     Name$ SongList��   DefaultItemForeground �	#FF595757   DefaultItemBackground
 ����    ScrollSpeed$	 0.2Q�$��0q����	#FFDCDCDD$��1,1,0,0q�$��0q�   ScrollMinimumHeight$ 20Q�$	��241��$	��265��   ScrollBackground Z�   -   $ AllList�� �	#FF595757
 ���� $	 0.2Q�$��0q����	#FFDCDCDD$��1,1,0,0q�$��0q�$ 20Q�$	��241��$	��265�� Z� �� -   $ 	List_Name��$��我的歌單��$
��Left=�$	��Top=�$��	10,13,0,0q����	#FF595757$��0q�� -   $ Window_Control��$��	0,14,14,0q�$��Right=�$	��Top=�$��48��.+�� -   $ MinimumGrid��$��15.213��$
��Left=����	#00000000.+��� -   $ Minimum��$
��Left=�$��Center=�$/��)/HPlayer;component/Source/Minimumsize.png��$ 0.5,0.5&�� -   $ PowerOffGrid��$��Right=�.+��� -   $ PowerOff��$��Right=�$��18��$��0q�$	��Top=�$��18��$,��&/HPlayer;component/Source/PowerOff.png��$ 0.5,0.5&�� -   $ User_Control��$��
0,14,775,0q�$��Right=�$	��Top=�$��48��.+�� -   $ ListGrid��$
��Left=����	#00000000$��18��$��18��.+��� -   $
 List��$
��Left=�$��Center=�$(��"/HPlayer;component/Source/List.png��$ 0.5,0.5&�� -   $ 
SearchGrid��$��Right=�.+��� -    $ Search��$��Right=�$
��17.5��$��0q�$	��Top=�$��21��$*��$/HPlayer;component/Source/Search.png��$ 0.5,0.5&�� $
��Left=�$	��Top=�
���   �$��	52,45,0,0q�$��23��$��23��.+��� -!   $ VolumeImage��$��0q�$2��,/HPlayer;component/Source/Volume/volume0.png��$��UniformToFill=�� $
��Left=�$��	0,191,0,0q�$	��Top=�$	��591��.+��� -"   $ SongName��$��	-HPlayer-��$��Center=�$��0q�$��Center=�$��14(����	#FF595757�� -#   $��移除歌曲��$
��Left=�$��
406,24,0,0q�$	��Top=�$��75���� -$   $ OnlineCount��$��Oline: 0��$
��Left=�$��
152,10,0,0q�$	��Top=��� -%    ��Name$ SettingButton��$��Setting��$
��Left=�$��	62,13,0,0q�$	��Top=�$��75���� -&   $ VersionL��$��最新版本: N/A��$
��Left=�$��
228,10,0,0q�$	��Top=�$ 0.825,-0.08&��� -'   $ InfoL��$��Welcome��$
��Left=�$��	0,283,0,0q�$	��Top=�$ 0.825,-0.08&�