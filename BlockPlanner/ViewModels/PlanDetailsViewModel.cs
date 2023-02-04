using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BlockPlanner.Commands;
using BlockPlanner.Stores;

namespace BlockPlanner.ViewModels
{
    public class PlanDetailsViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public string PlanName { get; set; }
        public string WeekStartTime { get; set; }
        public string WeekEndTime { get; set; }
        public string PlanId { get; set; }

        public ICommand BackToMainMenuCommand { get; }
        public ICommand ModifyPlanSettingsCommand { get; }

        public PlanDetailsViewModel(NavigationStore navigationStore, Func<MainMenuViewModel> createMainMenuViewModel, Func<PlanSettingsViewModel> createPlanSettingsViewModel)
        {
            _navigationStore = navigationStore;
            BackToMainMenuCommand = new NavigateCommand(navigationStore, createMainMenuViewModel);
            ModifyPlanSettingsCommand = new NavigateCommand(navigationStore, createPlanSettingsViewModel);

        }



    }
}
