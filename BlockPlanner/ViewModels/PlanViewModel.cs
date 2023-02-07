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
        private string _planId;

        public string PlanId
        {
            get => _planId;
            set
            {
                _planId = value;
                OnPropertyChanged(nameof(PlanId));
            }
        }

        public string PlanName => _plan.Name;
        public string WeekStartTime => _plan.WeekStartTime.ToString("d");
        public string WeekEndTime => _plan.WeekEndTime.ToString("d");

        public PlanViewModel(Plan plan)
        {
            _plan = plan;
            PlanId = 0.ToString();
        }
        public PlanViewModel(Plan plan, int planId)
        {
            _plan = plan;
            PlanId = planId.ToString();
        }
    }
}
