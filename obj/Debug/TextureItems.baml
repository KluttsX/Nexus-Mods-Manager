   M S B A M L   `   `   `  ����@  <Nexus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:NexusNexus  5          Nexus.TextureItems   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:Nexus     5      .��5      �� -   5          ��Name$  template��5       � Cursor$
 Hand��6   $	��180��6y   $	��185��6�   $��	10 0 0 10q�6�   ���#2A3254���#3D4877$��1q�6�    �� CornerRadius$ 10��6�   ._�5   
   � .+�5	      �� $ 8,8,0,0��6    �� BitmapScalingMode$ HighQuality=�6,   $	��Top=�6Z   $	��100��6r   ��5
      �� 5      -   5        imgShow�� �� ImageSource$Y Shttps://zonacraft.net/wp-content/uploads/2023/02/DarkUi32x-TexturePack9-384x216.png��5   2   5      �� 5      -   5        $  status��5      ���#F96383���#F96383$��1q�6P   $ 10��6d   $	��Top=�6v   $��Right=�6�   $��70��6�   $��	0 10 10 0q�6�   $��35��6�   ._�5      �� -   5         ��Name$ statuslabel��5      $��	Installed��61   $��12(�6B   $��Center=�6P   $��Center=�6k   $��DemiBold"�6�   5      �� 5      -   5        $ title��5      $#��Clear Gui Light 1.15 - 1.19.4��6'   $��CharacterEllipsis=�6L   
�������$��14(�6�   $��DemiBold"�6�   $��Center=�6�   $
��Left=�6�   $��
10 50 10 0q�6�   �� 5      -   5        $ description��5      $�� ��6-   $��12(�65   $��CharacterEllipsis=�6C   
�������$
��Wrap=�6w   $��Center=�6�   $��Center=�6�   $��	5 125 5 0q�6�   �� 5      -   5        $
 link��5      $y�Hidden=�6&   �� 5      -   5        $  
popDeitals��5      $y�Hidden=�6)   ���#2A3254$ 10��6R   ._�5      � .+�5      �� -	   5        $��Atras��5      $��40��6B   $��25��6M   
����������#2A3254���#2A3254$��	10 10 0 0q�6�      styleButton3#��[  6�   $	��Top=�6�   $
��Left=�6�    K� ZIndex$ 1��6  �� 5      -
   5        $ titledetails��5       $��Dungeons GUI: 1.9 - 1.19��66   $��Center=�6V   $	��Top=�6s   
�������$��14(�6�    �� TextAlignment$ Center=�6�   $��DemiBold"�6�   $
��Wrap=�6�   $��10 10 10 10q�6�   �� 5      $	��175��6   $ 10��6)   $	��130��6;   ���#171B2D$	��Top=�6]   $��Center=�6u   ._�5      �� -   5         ��Name$ txtdescription��5   "   $y�Visible=�6:   	 �� TextAlignment$	 Center=�6O   "
 �� VerticalScrollBarVisibility$
 Visible=�6f    �� 
IsReadOnly .  �� IsReadOnlyCaretVisible .  $ �� HorizontalScrollBarVisibility$ Disabled=�6�   $��10 10 15 10q�6�   
������ $��0q�6  
������� �� TextWrapping$
 Wrap=�6<  $��11(�6P  5      �� 5      -   5         ��Name$ 
installbtn��5      $��2q�6J   $��Install��6^   ���#2A3254���#3B56E1
�������$��14(�6�   $��DemiBold"�6�   $	��160��6�   $��30��6�   $��0 0 0 10q�6�     styleButton5#��[ 6�   $��Bottom=�6"  $��Center=�6=  �� 5      -   5        $ 	deletebtn��5      $��Delete��60   $��2q�6X   ���#2A3254���#F96383
�������$��14(�6�   $��DemiBold"�6�   $	��160��6�   $��30��6�   $��0 0 0 10q�6�     deletebutton#��[ 6�   $��Bottom=�6  $��Center=�69  5      5      5      ��5    
   �� 5!       �� 
TargetType" ��6    �� Triggers 5"      P� 5#       P� Property �@J�IsMouseOver P� Value .  N� EnterActions 5$      �� 5%      .��5&   "   �� .��5'   &   ��  �� TargetProperty$2 ,(Border.BorderBrush).(SolidColorBrush.Color)�6C   .g�5(   *   8  2System.Windows.Media.Animation.EasingColorKeyFrame   �� KeyTime$ 0:0:0.2��6>    �� Value #F963836P   5)   '   5*   #   5+       N� ExitActions 5-      �� 5.      .��5/   "   �� .��50   &   �� $2 ,(Border.BorderBrush).(SolidColorBrush.Color)�6C   .g�51   *     $ 0:0:0.2��6>    #3D48776P   52   '   53   #   54      56      58      5:      5;      