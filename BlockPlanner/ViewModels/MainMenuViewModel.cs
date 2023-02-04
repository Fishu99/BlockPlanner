using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BlockPlanner.Commands;
using BlockPlanner.Models;
using BlockPlanner.Stores;

namespace BlockPlanner.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        private readonly ObservableCollection<PlanViewModel> _plans;
        public IEnumerable<PlanViewModel> Plans => _plans;
        public ICommand AddNewPlanCommand { get; }
        public ICommand ShowPlanDetailsCommand { get; }

        public MainMenuViewModel(NavigationStore navigationStore, Func<PlanDetailsViewModel> createPlanDetailsViewModel, Func<PlanSettingsViewModel> createPlanSettingsViewModel)
        {
            _plans = new ObservableCollection<PlanViewModel>();
            AddNewPlanCommand = new NavigateCommand(navigationStore, createPlanSettingsViewModel);
            ShowPlanDetailsCommand = new NavigateCommand(navigationStore, createPlanDetailsViewModel);


            _plans.Add(new PlanViewModel(new Plan("Plan numer 1", DateTime.MinValue, DateTime.MaxValue, null)));
            _plans.Add(new PlanViewModel(new Plan("Plan numer 2", DateTime.MinValue, DateTime.MaxValue, null)));
            _plans.Add(new PlanViewModel(new Plan("Plan numer 3", DateTime.MinValue, DateTime.MaxValue, null)));
            _plans.Add(new PlanViewModel(new Plan("Plan numer 4", DateTime.MinValue, DateTime.MaxValue, null)));
        }

    }
}
