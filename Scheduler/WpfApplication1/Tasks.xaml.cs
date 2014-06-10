using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApplication1.BL;
using WpfApplication1.ViewModels;


namespace WpfApplication1
{
    /// <summary>
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class TasksWindow : Window
    {
        public TasksWindow()
        {
            InitializeComponent();
            var context = new SchedulesViewModel();
            foreach (var s in ScheduleManager.GetScriptschedules())
            {
                context.ScriptSchedules.Add(s);
            }
            DataContext = context;
        }


        private SchedulesViewModel Context
        {
            get { return (SchedulesViewModel)DataContext; }
        }

        private void AddTask(object sender, ExecutedRoutedEventArgs e)
        {
            var schedule = new ScriptSchedule();
            if (new ScheduleSettignsWindow(schedule).ShowDialog().GetValueOrDefault())
            {
                Context.AddScriptSchedule(schedule);
            }
        }

        private void CheckIfItemSelected(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Context != null && Context.CurrentScriptSchedule != null;
        }

        private void OpenSchedule(object sender, ExecutedRoutedEventArgs e)
        {
            if (new ScheduleSettignsWindow(Context.CurrentScriptSchedule).ShowDialog().GetValueOrDefault())
            {
                Context.UpdateScriptSchedule(Context.CurrentScriptSchedule);
            }
        }

        private void DeleteSchedule(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Do you really want to delete the The schedule", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Context.DeleteScriptSchedule(Context.CurrentScriptSchedule);
            } 
            
        }

        private void Synchronize(object sender, ExecutedRoutedEventArgs e)
        {
            ScheduleSettignsWindow schedule = new ScheduleSettignsWindow();
            schedule.Synchronize(Context.CurrentScriptSchedule);
        }
    }
}
