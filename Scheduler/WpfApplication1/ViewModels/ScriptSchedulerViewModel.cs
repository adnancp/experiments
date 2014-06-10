using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;
using WpfApplication1.BL;

namespace WpfApplication1.ViewModels
{
    public class ScriptSchedulerViewModel : ModelBase
    {
        public ScriptSchedulerViewModel()
        {
            _scripts = new ObservableCollection<ScriptItem>();
        }

        private ScriptSchedule _scriptschedule;
        public ScriptSchedule ScriptSchedule
        {
            get { return _scriptschedule; }
            set { _scriptschedule = value; OnPropertyChanged("ScriptSchedule"); }
        }

        private ObservableCollection<ScriptItem> _scripts;
        public ObservableCollection<ScriptItem> Scripts
        {
            get { return _scripts; }
            private set { _scripts = value; OnPropertyChanged("Scripts"); }
        }

        public ScriptItem GetSelectedScriptItem()
        {
            return Scripts.FirstOrDefault(i => i.Id == ScriptSchedule.ScriptId);
        }
    }
}
