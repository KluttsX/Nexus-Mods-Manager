   M S B A M L   `   `   `  ����@  <Nexus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:NexusNexus  5          Nexus.ToolsItems   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:Nexus     5      .��5      �� -   5          ��Name$  template��5       � Cursor$
 Hand��6   $	��177��6-   $	��220��69   $��	10 0 0 10q�6y   ���#2A3254���#3D4877$��1q�6�    �� CornerRadius$ 10��6�   ._�5   
   � .+�5	      �� $ 8,8,0,0��6    �� BitmapScalingMode$ HighQuality=�6,   $	��Top=�6Z   $	��110��6r   ��5
      �� 5      -   5        imgShow�� �� ImageSource$] Whttps://zonacraft.net/wp-content/uploads/2016/10/minecraft-server-zonacraft-384x216.png��5   2   5      �� 5      -   5         ��Name$ title��5      $��Minecraft Forge API��6'   $��DemiBold"�6B   $��17(�6X   $��
10 45 10 0q�6f   
�������$��Center=�6�   $
��Left=�6�   �� 5      -   5        $ description��5      $��CharacterEllipsis=�6-   
�������$
��Wrap=�6a   $��Center=�6u   $��Center=�6�   $��10 150 10 20q�6�   $����Minecraft Forge es un mod que actúa como gestor. Disponible sólo para Minecraft: Java Edition, permite una instalación más cómoda y segura de mods, evitando problemas de interferencia entre los distintos mods agregados que modifican las mismas mecánicas del juego. Actualmente la mayoría de mods se programan para funcionar con Minecraft Forge, así que contar con el instalado es esencial para la instalación de mods.��6�   �� 5      -   5        $  showdeitals��5       K� ZIndex$ 1��6*   $y�Hidden=�6;   ���#2A3254$ 10��6d   ._�5      � .+�5      �� -   5        $��Atras��5      $��40��6B   $��25��6M   
����������#2A3254���#2A3254$��	10 10 0 0q�6�      styleButton3#��[  6�   $	��Top=�6�   $
��Left=�6�   $ 1��6  �� 5      $	��175��6   ���#171B2D$	��Top=�6>   $��Center=�6V   $ 10��6s   $	��130��6�   $��0 0 0 0q�6�   ._�5      �� -   5         ��Name$ txtdescription��5   "   " �� VerticalScrollBarVisibility$ Visible=�6:   $	 �� HorizontalScrollBarVisibility$	 Disabled=�6`   $��10 10 10 10q�6�   
������ $��0q�6�   
 �� 
IsReadOnly
 . 
������� �� TextAlignment$
 Left=�6�    �� TextWrapping$
 Wrap=�6  $��11(�6  5      �� 5      $��Version: 1.16.5��6    $��14(�67   
�������$��DemiBold"�6X   $��	15 0 0 60q�6n   $��Bottom=�6�   $
��Left=�6�   �� 5      -   5         ��Name$ install��5      $��Install��6.   $��35��6S   
�������$��14(�6r   $��DemiBold"�6�   ���#2A3254���#3B56E1$��
10 0 10 15q�6�     styleButton5#��[ 6�   $��Bottom=�6�   5      5      �� 5      ���#FBBF24$��20��6*   $��Right=�65   $��20��6Q   $ 20��6]   $��0 0 15 0q�6o   ._�5      F� ����� � �Ӏ�? � ���H� �� h�	� ��`�@�b��@�%�������@,Bgffff�~@@�`����`;��)Ӡt�`�W������x@ �q��r �q� � �q� �� �q � � � �}
� �@�S �������\@��,`�`�Z�De�`g��d����f@�X �@ � @��������@� b���! ��� �������yp@     �q��D �q h�	� �q`٬
@��`��`����`�/��� �m  �] �e�mx�     �� �� ��` ���� �� �& �`~���T�`}��ځӠ�`u���� � � �� �� �Q� �� .� 6n 6n� �� �Q� 6n 6n	$��Center=�6�  $��12��6�  $��0q�6   �� Stretch$
 Fill=�6  $��Center=�6!  $��12��6<  
�������6G  5      5      ��5   
   �� 5       �� 
TargetType" ��6    �� Triggers 5       P� 5!       P� Property �@J�IsMouseOver P� Value .  N� EnterActions 5"      �� 5#      .��5$   "   �� .��5%   &   ��  �� TargetProperty$2 ,(Border.BorderBrush).(SolidColorBrush.Color)�6C   .g�5&   *   8  2System.Windows.Media.Animation.EasingColorKeyFrame   �� KeyTime$ 0:0:0.2��6>    �� Value #F963836P   5'   '   5(   #   5)       N� ExitActions 5+      �� 5,      .��5-   "   �� .��5.   &   �� $2 ,(Border.BorderBrush).(SolidColorBrush.Color)�6C   .g�5/   *     $ 0:0:0.2��6>    #3D48776P   50   '   51   #   52      54      56      58      59      