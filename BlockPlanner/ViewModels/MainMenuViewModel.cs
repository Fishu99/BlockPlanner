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
        public ICommand DeletePlanCommand { get; }

        public MainMenuViewModel(Scheduler scheduler, NavigationService createPlanSettingsNavigationService, ParameterNavigationService<int> createPlanDetailsNavigationService)
        {
            _scheduler = scheduler;
            _plans = new ObservableCollection<PlanViewModel>();
            AddNewPlanCommand = new NavigateCommand(createPlanSettingsNavigationService);
            ShowPlanDetailsCommand = new ParameterNavigationCommand<int>(createPlanDetailsNavigationService);
            ReadPlansFromScheduler();

            //For tests
            // _plans.Add(new PlanViewModel(new Plan("Plan numer 1", DateTime.MinValue, DateTime.MaxValue, null)));
            // _plans.Add(new PlanViewModel(new Plan("Plan numer 2", DateTime.MinValue, DateTime.MaxValue, null)));
            // _plans.Add(new PlanViewModel(new Plan("Plan numer 3", DateTime.MinValue, DateTime.MaxValue, null)));
            // _plans.Add(new PlanViewModel(new Plan("Plan numer 4", DateTime.MinValue, DateTime.MaxValue, null)));
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
