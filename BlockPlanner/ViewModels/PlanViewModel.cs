using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockPlanner.Models;

namespace BlockPlanner.ViewModels
{
    public class PlanViewModel : ViewModelBase
    {
        private readonly Plan _plan;
        public string PlanId => "10";
        public string PlanName => _plan.Name;
        public string WeekStartTime => _plan.WeekStartTime.ToString("d");
        public string WeekEndTime => _plan.WeekEndTime.ToString("d");

        public PlanViewModel(Plan plan)
        {
            _plan = plan;
        }
    }
}
