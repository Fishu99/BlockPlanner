using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BlockPlanner.Commands;
using BlockPlanner.Exceptions;
using BlockPlanner.Models;
using BlockPlanner.Services;
using BlockPlanner.Stores;

namespace BlockPlanner.ViewModels
{
    public class PlanDetailsViewModel : ViewModelBase
    {
        private readonly Scheduler _scheduler;
        private readonly Plan _plan;
        private string _planName;
        private string _weekStartTime;
        private string _weekEndTime;
        private int _planId;
        private WeekPlanViewModel _weekPlanData;

        public WeekPlanViewModel WeekPlanData => _weekPlanData;

        public string PlanName
        {
            get => _planName;
            set
            {
                _planName = value;
                OnPropertyChanged(nameof(PlanName));
            }
        }

        public string WeekStartTime
        {
            get => _weekStartTime;
            set
            {
                _weekStartTime = value;
                OnPropertyChanged(nameof(WeekStartTime));
            }
        }
        public string WeekEndTime
        {
            get => _weekEndTime;
            set
            {
                _weekEndTime = value;
                OnPropertyChanged(nameof(WeekEndTime));
            }
        }

        public int PlanId
        {
            get => _planId;
            set
            {
                _planId = value;
                OnPropertyChanged(nameof(PlanId));
            }
        }

        public ICommand BackToMainMenuCommand { get; }
        public ICommand ModifyPlanSettingsCommand { get; }

        public PlanDetailsViewModel(Scheduler scheduler, int planId, NavigationService createMainMenuNavigationService, ParameterNavigationService<int> createPlanSettingsNavigationService)
        {
            _scheduler = scheduler;
            PlanId = planId;
            _plan = RetrievePlanFromSchedulerById(planId);
            if (_plan != null)
            {
                PlanName = _plan.Name;
                WeekStartTime = _plan.WeekStartTime.ToString("d");
                WeekEndTime = _plan.WeekEndTime.ToString("d");
                _weekPlanData = new WeekPlanViewModel(_plan);
            }
            BackToMainMenuCommand = new NavigateCommand(createMainMenuNavigationService);
            ModifyPlanSettingsCommand = new ParameterNavigationCommand<int>(createPlanSettingsNavigationService);

        }

        private Plan RetrievePlanFromSchedulerById(int planId)
        {
            var planInListId = planId - 1;
            var schedulerPlans = _scheduler.Plans;
            if (schedulerPlans == null || schedulerPlans.Count == 0)
            {
                throw new SchedulerPlansEmptyException("Scheduler plans are empty list.");
            }

            if (schedulerPlans.Count < planInListId)
            {
                Console.WriteLine("There is no plan with {0} id", planInListId);
                return null;
            }
            else
            {
                var plan = schedulerPlans[planInListId];
                return plan;
            }
        }
    }
}
