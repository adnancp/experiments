   M S B A M L   `   `   `  ����  WpfApplication1!    WpfApplication1.TasksWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Window1��5   	   $	��451��6   $	��653��6&   .��5      ]�  J� CommandBindings 5   
   )  #System.Windows.Input.CommandBinding  5      -   5           Command$	 New��5      6+     5      -   5        $
 Open��5      6M     5	      -   5        $ Delete��5	      6O     5
      -   5        $
 Save��5
      6,   .+�5   
   a� $	��Top=�6   .+�5      c� .@�5      �� $	��Add��6   $	��120��6'   $	��New��63   �� 5      $
��Edit��6   $	��120��6)   $
��Open��65   �� 5      $��Remove��6   $	��120��6*   $��Delete��66   �� 5      $��Synchronize��6   $	��120��6/   $
��Save��6;   $	��Top=�6J   5      5      �� 5   
   $��Tasks��6   .��5      ��  �� DisplayMemberPath$ ScheduleName��6?   ��6   �� *ScriptSchedules+ �� SelectedItem 6`   �� *CurrentScriptSchedule+5      5      5      