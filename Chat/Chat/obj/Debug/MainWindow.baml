   M S B A M L   `   `   `  ����?  ;Chat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ChatChat  5   	       Chat.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Chat     5   	     � Title$  
MainWindow��5   	   $	��720��6   $
��1280��6)    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowState$ 	Maximized=�5
   	   .��5      �� $��Disabled=�6   $
��Auto=�6:   .��5   
   � -   5         �Name$ gridPrincipal��5      ���#F0F0FA � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $��* �6#   .+�5      �� $��1��6   ���#FFFFFF �� CornerRadius$ 5��6:   $��Stretch=�6K   $��Stretch=�6i   $	��350��6�   $��10q�6�    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 3Q�6'      ShadowDepth$ 0Q�66   ._�5      �� $
��Auto=�6   $��Disabled=�6B   .��5      �� -   5        	 ��Name$	 panelAmigos��5   !   $	��Top=�66   .+�5      � .+�5      �� $��Amigos��6(   $��Center=�66   $��5q�6S   $��16(�6^   $
��Bold"�6l   5      5      5      5      �� 5       $��2��6   ���#FFFFFF$ 5��6:   $��Stretch=�6K   $��Stretch=�6i   $	��500��6�   $��10q�6�    5!        5"      $ 3Q�6'   $ 0Q�66   ._�5$      � -   5        $ amigo��5$      
 � RowDefinitions
 5%      �� 5&      $
��Auto �6(   �� 5'      $��* �6(   .+�5)       � -   5          �Name$ frameAmigoSelecionado��5)      $��65��6;     � NavigationUIVisibility$ Hidden=�6G   � 5*      -   5        $ gridChat��5*      $��1��6-   $��0,10,0,0q�6:   
 5+      �� 5,      $��* �6,   �� 5-      $
��Auto �6,   .+�5/      �� -   5         ��Name$ 
scrollChat��5/   '   $
��Auto=�6;   $��Disabled=�6^   .��50      � -   5        $ gridMensagens��50   #   $��0��6:   
 51   "   �� 52   &   $��* �64   54      55      �� 56      $��1��6!   ���#37376E$��
0, 1, 0, 0q�6D   ._�57      �  58   "   �� 59   &   $��* �67   �� 5:   &   $��35 �67   .+�5<   "   �� -   5         ��Name$ campoMensagem��5<   *   $��0��6A   $��Stretch=�6Q   $��Center=�6m   $��35��6�   $��10,5,0,5q�6�   $��5q�6�   $��14(�6�    �� TextWrapping$
 Wrap=�6�   $��0q�6�   6  �� 5=   "   -	   5         ��Name$ labelMensagem��5=   (   $��Mensagem��6?   $��0��6R   $��Center=�6b   $��12,0,0,0q�6}   ���	#66000000$��14(�6�    J� IsHitTestVisible .  6�   �� 5>   "   -
   5         ��Name$ botaoEnviar��5>   (   $r��lC:\Users\ZettZ\OneDrive\Documentos\Fatec\Projeto ED\Johnny\CoisasDoProjetoOrbit\Chat\Chat\Imagens\Enviar.png��6=   $��1��6�   $��Center=�6�   $��0,0,0,0q�6�   $��25��6�   $��25��6�   6  5?      5@      5A      5B      5C      5D      5E      5F      