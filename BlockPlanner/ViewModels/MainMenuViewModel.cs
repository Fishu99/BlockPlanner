using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BlockPlanner.Commands;
using BlockPlanner.Models;
using BlockPlanner.Services;
using BlockPlanner.Stores;

namespace BlockPlanner.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        private readonly Scheduler _scheduler;

        private readonly ObservableCollection<PlanViewModel> _plans;
        public IEnumerable<PlanViewModel> Plans => _plans;
        public ICommand AddNewPlanCommand { get; }
        public ICommand ShowPlanDetailsCommand { get; }

        public MainMenuViewModel(Scheduler scheduler, NavigationService createPlanSettingsNavigationService, NavigationService createPlanDetailsNavigationService)
        {
            _plans = new ObservableCollection<PlanViewModel>();
            AddNewPlanCommand = new NavigateCommand(createPlanSettingsNavigationService);
            ShowPlanDetailsCommand = new NavigateCommand(createPlanDetailsNavigationService);

            //For tests
            _plans.Add(new PlanViewModel(new Plan("Plan numer 1", DateTime.MinValue, DateTime.MaxValue, null)));
            _plans.Add(new PlanViewModel(new Plan("Plan numer 2", DateTime.MinValue, DateTime.MaxValue, null)));
            _plans.Add(new PlanViewModel(new Plan("Plan numer 3", DateTime.MinValue, DateTime.MaxValue, null)));
            _plans.Add(new PlanViewModel(new Plan("Plan numer 4", DateTime.MinValue, DateTime.MaxValue, null)));
        }

    }
}
