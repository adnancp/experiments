using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using WpfApplication1.BL;

namespace WpfApplication1.ViewModels
{
    class SchedulesViewModel : ModelBase
    {
        public SchedulesViewModel()
        {
            _schedules = new ObservableCollection<ScriptSchedule>();
        }

        private ObservableCollection<ScriptSchedule> _schedules;
        public ObservableCollection<ScriptSchedule> ScriptSchedules
        {
            get { return _schedules; }
            set { _schedules = value; OnPropertyChanged("ScriptSchedules"); }
        }

        private ScriptSchedule _currentScriptSchedule;
        public ScriptSchedule CurrentScriptSchedule
        {
            get { return _currentScriptSchedule; }
            set { _currentScriptSchedule = value; OnPropertyChanged("CurrentScriptSchedule"); }
        }

        public void AddScriptSchedule(ScriptSchedule scriptSchedule)
        {
            ScheduleManager.AddScriptSchedule(scriptSchedule);
            ScriptSchedules.Add(scriptSchedule);
        }

        public void DeleteScriptSchedule(ScriptSchedule scriptSchedule)
        {
            ScheduleManager.DeleteScriptSchedule(scriptSchedule.ScriptScheduleId);
            ScriptSchedules.Remove(scriptSchedule);
        }

        public void ReplaceCurrentScriptSchedule(ScriptSchedule scriptSchedule)
        {
            ScheduleManager.UpdateScriptSchedule(scriptSchedule);

            CurrentScriptSchedule.ScheduleName = scriptSchedule.ScheduleName;
            CurrentScriptSchedule.ScriptId = scriptSchedule.ScriptId;
            CurrentScriptSchedule.Schedule = scriptSchedule.Schedule;
        }
    }
}
