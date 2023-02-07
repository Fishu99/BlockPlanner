using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BlockPlanner.ViewModels;

namespace BlockPlanner.Commands
{
    public class DeletePlanCommand : CommandBase
    {
        private MainMenuViewModel _mainMenuViewModel;

        public DeletePlanCommand(MainMenuViewModel mainMenuViewModel)
        {
            _mainMenuViewModel = mainMenuViewModel;
        }

        public override void Execute(object parameter)
        {
            if (parameter == null)
            {
                MessageBox.Show("Delete parameter was null.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete the item?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            var parameterText = (string)parameter;
            var planId = int.Parse(parameterText) - 1;
            var plans = _mainMenuViewModel.Plans;
            var schedulerPlans = _mainMenuViewModel.Scheduler.Plans;

            if (plans.Count < planId || schedulerPlans.Count < planId)
            {
                MessageBox.Show("Plan with id " + (planId + 1) + " does not exists!", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            plans.RemoveAt(planId);
            schedulerPlans.RemoveAt(planId);

            var newPlanId = 0;
            foreach (var plan in plans)
            {
                newPlanId++;
                plan.PlanId = newPlanId.ToString();
            }

        }
    }
}
