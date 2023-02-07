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
        public ObservableCollection<PlanViewModel> Plans => _plans;
        public ICommand AddNewPlanCommand { get; }
        public ICommand ShowPlanDetailsCommand { get; }
        public ICommand DeletePlanCommand { get; }

        public Scheduler Scheduler => _scheduler;

        public MainMenuViewModel(Scheduler scheduler, NavigationService createPlanSettingsNavigationService, ParameterNavigationService<int> createPlanDetailsNavigationService)
        {
            _scheduler = scheduler;
            _plans = new ObservableCollection<PlanViewModel>();
            AddNewPlanCommand = new NavigateCommand(createPlanSettingsNavigationService);
            ShowPlanDetailsCommand = new ParameterNavigationCommand<int>(createPlanDetailsNavigationService);
            DeletePlanCommand = new DeletePlanCommand(this);
            ReadPlansFromScheduler();
        }

        private void ReadPlansFromScheduler()
        {
            var listOfPlans = _scheduler.Plans;
            if (listOfPlans == null)
            {
                return;
            }


            var planId = 0;
            foreach (var plan in listOfPlans)
            {
                planId++;
                var planViewModel = new PlanViewModel(plan, planId);
                _plans.Add(planViewModel);
            }
        }

    }
}
