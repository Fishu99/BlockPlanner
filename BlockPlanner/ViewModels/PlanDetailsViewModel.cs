using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BlockPlanner.Commands;
using BlockPlanner.Services;
using BlockPlanner.Stores;

namespace BlockPlanner.ViewModels
{
    public class PlanDetailsViewModel : ViewModelBase
    {
        public string PlanName { get; set; }
        public string WeekStartTime { get; set; }
        public string WeekEndTime { get; set; }
        public string PlanId { get; set; }

        public ICommand BackToMainMenuCommand { get; }
        public ICommand ModifyPlanSettingsCommand { get; }

        public PlanDetailsViewModel(NavigationService createMainMenuNavigationService, NavigationService createPlanSettingsNavigationService)
        {
            BackToMainMenuCommand = new NavigateCommand(createMainMenuNavigationService);
            ModifyPlanSettingsCommand = new NavigateCommand(createPlanSettingsNavigationService);

        }



    }
}
