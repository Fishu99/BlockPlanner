using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using BlockPlanner.ViewModels;
using System.Drawing;
using DrawingColor = System.Drawing.Color;
using Color = System.Windows.Media.Color;


namespace BlockPlanner.Commands
{
    public class SelectColorCommand : CommandBase
    {
        private PlanSettingsViewModel _planSettingsViewModel;

        public SelectColorCommand(PlanSettingsViewModel planSettingsViewModel)
        {
            _planSettingsViewModel = planSettingsViewModel;
        }

        public override void Execute(object parameter)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                DrawingColor dARGB = dialog.Color;
                Color newColor = Color.FromArgb(dARGB.A,dARGB.R,dARGB.G,dARGB.B);
                _planSettingsViewModel.SelectedTask.BlockColor = newColor;
                _planSettingsViewModel.Color = newColor.ToString();
            }
        }
    }
}
