   M S B A M L   `   `   `  ����F  BFileRenamer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:FileRenamer.ControlsFileRenamer.Controls  5      /    )FileRenamer.Controls.RenameTextBoxControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:FileRenamer.Controls     5      -   5          >�Name$  Control��5      .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   .+�5   
   �� -   5         ��Name$ TitleTextBlock��5      $��0��6,   $��4q�6i   ��6<   �� *Header+ �� ElementName$ Control��6v   �� 5   
   -   5         ��Name$ RenameTextBox��5      $��1��6)   $��0��67   $��4,0,4,4q�6G   ��5      �� *Text+$ Control���� 5   
   -   5         ��Name$ 	TipBorder��5      $��2��6$   $��2��61   $��0��6E   $��4,0,4,4q�5         grayGradient#���   6"     DisabledForegroundBrush#���  5      $��1q�5       �� CornerRadius$ 4��6%    �� Padding$ 4q�66   ._�5      �� .+�5      �� -   5        $ TipTextBlock��5      ��62   �� *Tip+$ Control��6]   �� 5      $��时间格式说明：��6   y�6+   �� *
Visibility+$ TimeFormatTextBox��6m   5      5      �� 5   
   $��时间格式设置:��6   $��0��6#   $��1��60   $��4q�6@   y�6K   �� *
Visibility+$ TimeFormatTextBox��6�   �� 5    
   -   5        $ TimeFormatTextBox��5       $��1��6-   $��1��6:   $	��150��6J   $��4,0,4,4q�6V   ��5!      �� *TimeFormatText+$ Control��6H   5#      5$      