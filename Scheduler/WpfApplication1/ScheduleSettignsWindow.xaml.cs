using System;
using System.Windows;
using Microsoft.Win32.TaskScheduler;
using Npgsql;
using System.Linq;
using System.Collections.Generic;
using WpfApplication1.ViewModels;
using WpfApplication1.BL;
using System.IO;
namespace WpfApplication1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class ScheduleSettignsWindow : Window
    {
        public ScheduleSettignsWindow()
            : this(null)
        {
        }

        public ScheduleSettignsWindow(ScriptSchedule scriptSchedule)
        {
            InitializeComponent();

            var context = new ScriptSchedulerViewModel();

            context.ScriptSchedule = scriptSchedule ?? new ScriptSchedule();
            
            ScheduleManager.AddScriptsToModel(context);
           
            DataContext = context;
        }

       

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            var context = DataContext.As<ScriptSchedulerViewModel>();
            DialogResult = true;
            Close();
        }

      
        public  void Synchronize(ScriptSchedule scriptSchedule)
        {
                // Connect to the computer "REMOTE" using credentials
               // TaskService ts = new TaskService("REMOTE", "myusername", "MYDOMAIN", "mypassword");

                using (TaskService ts = new TaskService())
                {
                    CreateNewTask(ts, scriptSchedule);
                }
            
        }
        private void Synchronizeall()
        {
            var scriptsschedules = ScheduleManager.GetScriptschedules();
            foreach (var schedule in scriptsschedules)
            {
                // Connect to the computer "REMOTE" using credentials
                // TaskService ts = new TaskService("REMOTE", "myusername", "MYDOMAIN", "mypassword");

                using (TaskService ts = new TaskService())
                {
                  
                    CreateNewTask(ts, schedule);
                }
            }
        }

        private void CreateNewTask(TaskService ts, ScriptSchedule scriptSchedule)
        {
            Microsoft.Win32.TaskScheduler.Trigger trigger = null;

            // create a new task definition and assign properties
            TaskDefinition td = ts.NewTask();
            td.RegistrationInfo.Description = "does something";

            // create a trigger 
            if (scriptSchedule.Schedule.OneTimeChecked)
            {
                trigger = td.Triggers.Add(new TimeTrigger
                {
                    RandomDelay = scriptSchedule.Schedule.DelayTaskForUpToValue
                });
            }

            else if (scriptSchedule.Schedule.DailyChecked)
            {
                trigger = td.Triggers.Add(new DailyTrigger
                {
                    DaysInterval = scriptSchedule.Schedule.DailyCheckedValue,
                    RandomDelay = scriptSchedule.Schedule.DelayTaskForUpToValue
                });
            }
            else if (scriptSchedule.Schedule.WeeklyCheked)
            {
                trigger = td.Triggers.Add(new WeeklyTrigger
                {
                    WeeksInterval = scriptSchedule.Schedule.WeeklySettings.WeeklyChekedValue,
                    RandomDelay = scriptSchedule.Schedule.DelayTaskForUpToValue,
                    DaysOfWeek = (DaysOfTheWeek)((scriptSchedule.Schedule.WeeklySettings.Monday ? (int)DaysOfTheWeek.Monday : 0)
                                                | (scriptSchedule.Schedule.WeeklySettings.Tuesday ? (int)DaysOfTheWeek.Tuesday : 0)
                                                | (scriptSchedule.Schedule.WeeklySettings.Wednesday ? (int)DaysOfTheWeek.Wednesday : 0)
                                                | (scriptSchedule.Schedule.WeeklySettings.Thursday ? (int)DaysOfTheWeek.Thursday : 0)
                                                | (scriptSchedule.Schedule.WeeklySettings.Friday ? (int)DaysOfTheWeek.Friday : 0)
                                                | (scriptSchedule.Schedule.WeeklySettings.Saturday ? (int)DaysOfTheWeek.Saturday : 0)
                                                | (scriptSchedule.Schedule.WeeklySettings.Sunday ? (int)DaysOfTheWeek.Sunday : 0))
                });

            }

            trigger.StartBoundary = new DateTime(scriptSchedule.Schedule.TaskStartDate.Year, scriptSchedule.Schedule.TaskStartDate.Month, scriptSchedule.Schedule.TaskStartDate.Day,
                                                scriptSchedule.Schedule.TaskStartTime.Hour, scriptSchedule.Schedule.TaskStartTime.Minute, scriptSchedule.Schedule.TaskStartTime.Second);
            trigger.Repetition.Interval = scriptSchedule.Schedule.RepateTaskEveryValue;
            trigger.Repetition.Duration = scriptSchedule.Schedule.ForDurationOf;
            trigger.ExecutionTimeLimit = scriptSchedule.Schedule.StopTaskIfItRunsLongerThanvalue;
            trigger.EndBoundary = new DateTime(scriptSchedule.Schedule.ExpireTaskStartDate.Year, scriptSchedule.Schedule.ExpireTaskStartDate.Month, scriptSchedule.Schedule.ExpireTaskStartDate.Day,
                                               scriptSchedule.Schedule.ExpireTaskStartDate.Hour, scriptSchedule.Schedule.ExpireTaskStartDate.Minute, scriptSchedule.Schedule.TaskStartTime.Second);
            trigger.Enabled = scriptSchedule.Schedule.TaskEnabled;
            // specify the action 
            // TODO change the name and the implementation to according to scriptname.
            td.Settings.Enabled = scriptSchedule.Schedule.TaskEnabled;
            td.Actions.Add(new ExecAction("notepad.exe", "c:\\test.log", null));
            var rootpath = ts.RootFolder.Path;
            if (!Directory.Exists(rootpath + "\\Naiton"))
            {
                ts.RootFolder.CreateFolder("Naiton");
                // register the task in the root folder
                ts.GetFolder("Naiton").RegisterTaskDefinition(scriptSchedule.ScheduleName, td);
            }
            else
            {
                ts.GetFolder("Naiton").RegisterTaskDefinition(scriptSchedule.ScheduleName, td);
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
