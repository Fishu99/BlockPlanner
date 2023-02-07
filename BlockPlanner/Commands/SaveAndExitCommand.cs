using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BlockPlanner.Exceptions;
using BlockPlanner.Models;
using BlockPlanner.ViewModels;

namespace BlockPlanner.Commands
{
    public class SaveAndExitCommand : CommandBase
    {
        private readonly PlanSettingsViewModel _planSettingsViewModel;

        public SaveAndExitCommand(PlanSettingsViewModel planSettingsViewModel)
        {
            this._planSettingsViewModel = planSettingsViewModel;
        }

        public override void Execute(object parameter)
        {
            var currentPlan = _planSettingsViewModel.Plan;
            var scheduler = _planSettingsViewModel.Scheduler;
            if (_planSettingsViewModel.Mode == (int)PlanCreatorMode.Add)
            {
                try
                {
                    scheduler.AddNewPlan(currentPlan);
                    _planSettingsViewModel.BackToMainMenuCommand.Execute(null);
                }
                catch (SchedulePlanAlreadyExistsException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else //_planSettingsViewModel.Mode == PlanCreatorMode.Modify
            {
                scheduler.ModifyPlan(currentPlan);
                _planSettingsViewModel.BackToPlanDetailsCommand.Execute(_planSettingsViewModel.PlanId);
            }
        }
    }
}
