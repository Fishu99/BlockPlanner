using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockPlanner.Utilities;
using BlockPlanner.ViewModels;
using Color = System.Windows.Media.Color;

namespace BlockPlanner.Commands
{
    public class RandomizeColorCommand : CommandBase
    {
        private readonly PlanSettingsViewModel _planSettingsViewModel;

        public RandomizeColorCommand(PlanSettingsViewModel planSettingsViewModel)
        {
            _planSettingsViewModel = planSettingsViewModel;
        }

        public override void Execute(object parameter)
        {
            var selectedTask = _planSettingsViewModel.SelectedTask;
            if (selectedTask == null) return;
            var newColor = ColorUtilities.GetRandomColor();
            _planSettingsViewModel.Color = newColor.ToString();
        }
    }
}
