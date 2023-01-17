using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockPlanner.ViewModels
{
    public class PlanDetailsViewModel : ViewModelBase
    {
        public string PlanName { get; set; }
        public string WeekStartTime { get; set; }
        public string WeekEndTime { get; set; }
        public string PlanId { get; set; }

    }
}
