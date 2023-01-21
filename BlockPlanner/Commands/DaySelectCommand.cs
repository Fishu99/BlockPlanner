using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockPlanner.ViewModels;

namespace BlockPlanner.Commands
{
    public class DaySelectCommand : CommandBase
    {
        private readonly DayOfWeek _selectedDayOfWeek;
        private PlanSettingsViewModel _planSettingsViewModel;

        public DaySelectCommand(DayOfWeek selectedDayOfWeek, PlanSettingsViewModel planSettingsViewModel)
        {
            _selectedDayOfWeek = selectedDayOfWeek;
            _planSettingsViewModel = planSettingsViewModel;
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
