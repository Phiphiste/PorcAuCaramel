   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.PlayerCard   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  
PlayerCard��5   	   $	��400��6   $	��500��6)   .��5	      �� .+�5
   
   �� 
�������
��� ���$��3q�6>    �� CornerRadius$ 8,8,8,8��6R   ._�5      �� -   5         ��Name$ 	CardPanel��5      $��-0.2,-0.2,-3.6,-2.8q�6,   .+�5      �� -   5        $ ImagePlayer��5      $	��200��62   $	��245��6?   $��	0,0,255,0q�6K   .+�5      �� -   5        $��image_player��5      $	��180��60   $	��180��6=   $��10,10,10,10q�6I   $��default.jpg��6^   5      �� 5      -   5        $ 	PersInfos��5      $	��180��60   $	��245��6=   $��240,-200,0,0q�6I   .+�5       �� -   5         ��Name$
 Name��5        $��0,0,0,20q�6/   $
��Wrap=�6B   $��Center=�6W   $��18(�6t   $
��Bold"�6�   �� 5!      -   5        $ 	Birthdate��5!       $
��Wrap=�64   $
��Left=�6I   $��Center=�6d   $��14(�6   �� 5"      -   5        $ Nationality��5"       $
��Wrap=�66   $
��Left=�6K   $��Center=�6f   $��14(�6�   �� 5#      -   5        $ Height��5#       $
��Wrap=�62   $
��Left=�6G   $��Center=�6b   $��14(�6}   �� 5$      -	   5        $ Weight��5$       $
��Wrap=�62   $
��Left=�6G   $��Center=�6b   $��14(�6}   �� 5%      -
   5        $ Position��5%       $
��Wrap=�64   $
��Left=�6I   $��Center=�6d   $��14(�6   �� 5&      -   5        $ Work_timespan��5&       $
��Wrap=�68   $
��Left=�6M   $��Center=�6h   $��14(�6�   5'      �� 5)      -   5        $ 	CompInfos��5)      $	��160��60   $	��500��6=    �� Orientation$ 
Horizontal=�6I   .+�5/      �� $ Vertical=�6!   .+�50      �� $��Teams��6    �� 51      -   5        $��Teams_��51   "   $	��100��60   $	��200��6=   
�������$��0q�6a   $��Visible=�6u   $��10,0,0,0q�6�   52      �� 53      $ Vertical=�6!   $��70,0,0,0q�68   .+�54      �� $��Awards��6    �� 55      -   5        $��Awards_��55   '   $	��200��66   
�������$��0q�6Z   56      57      5:      5;      5<      5=      