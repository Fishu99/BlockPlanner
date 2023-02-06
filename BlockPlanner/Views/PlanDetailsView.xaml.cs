using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlockPlanner.Models;
using BlockPlanner.ViewModels;

namespace BlockPlanner.Views
{
    /// <summary>
    /// Interaction logic for PlanDetailsView.xaml
    /// </summary>
    public partial class PlanDetailsView : UserControl
    {
        public PlanDetailsView()
        {
            InitializeComponent();
            PlanDataGrid.DataContextChanged += PlanDataGrid_DataContextChanged;
        }

        private void PlanDataGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DisplayTaskInformations();
        }

        private void DisplayTaskInformations()
        {
            var data = (WeekPlanViewModel)PlanDataGrid.DataContext;
            if (data == null)
            {
                return;
            }
            
            var weekLength = 7;
            for (var dayId = 0; dayId < weekLength; dayId++)
            {
                var taskViewModel = GetTasksByDayId(data, dayId);
                foreach (var task in taskViewModel)
                {
                    AddTaskBlock(task);
                }
            }

        }

        private ObservableCollection<TaskItemViewModel> GetTasksByDayId(WeekPlanViewModel data, int dayId)
        {
            if (data == null)
            {
                return new ObservableCollection<TaskItemViewModel>();
            }

            switch (dayId)
            {
                case (int)WeekDay.Monday:
                    return data.MondayDayPlan;
                case (int)WeekDay.Tuesday:
                    return data.TuesdayDayPlan;
                case (int)WeekDay.Wednesday:
                    return data.WednesdayPlan;
                case (int)WeekDay.Thursday:
                    return data.ThursdayDayPlan;
                case (int)WeekDay.Friday:
                    return data.FridayDayPlan;
                case (int)WeekDay.Saturday:
                    return data.SaturdayPlan;
                case (int)WeekDay.Sunday:
                    return data.SundayDayPlan;
                default:
                    return data.MondayDayPlan;
            }
        }

        private void AddTaskBlock(TaskItemViewModel task)
        {
            if (task == null)
            {
                return;
            }

            var taskBackgroundBorder = new Border
            {
                Margin = new Thickness(10, 0, 10, 0),
                Background = Brushes.Black,
                CornerRadius = new CornerRadius(10)
            };
            var taskTextForeground = new TextBlock
            {
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 40,
                Text = task.Name,
                TextAlignment = TextAlignment.Center
            };

            var colorString = task.Color;
            var brushConverter = new BrushConverter();
            var taskBrush = (Brush)brushConverter.ConvertFromString(colorString);
            var taskMainForegroundBorder = new Border
            {
                Margin = new Thickness(15, 2, 15, 2),
                Background = taskBrush,
                CornerRadius = new CornerRadius(10),
                Child = taskTextForeground
            };

            PlanDataGrid.Children.Add(taskBackgroundBorder);
            PlanDataGrid.Children.Add(taskMainForegroundBorder);
            Grid.SetRow(taskBackgroundBorder, task.Row);
            Grid.SetColumn(taskBackgroundBorder, task.Col);
            Grid.SetRowSpan(taskBackgroundBorder, task.RowSpan);
            Grid.SetRow(taskMainForegroundBorder, task.Row);
            Grid.SetColumn(taskMainForegroundBorder, task.Col);
            Grid.SetRowSpan(taskMainForegroundBorder, task.RowSpan);
        }

        private void AddTestBlock()
        {
            var testBorder = new Border
            {
                Margin = new Thickness(10, 0, 10, 0),
                Background = Brushes.Black,
                CornerRadius = new CornerRadius(10)
            };
            var testInsideTextBlock = new TextBlock
            {
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 40,
                Text = "OR",
                TextAlignment = TextAlignment.Center
            };
            var testInsideBorder = new Border
            {
                Margin = new Thickness(15, 2, 15, 2),
                Background = Brushes.DarkSeaGreen,
                CornerRadius = new CornerRadius(10),
                Child = testInsideTextBlock
            };

            PlanDataGrid.Children.Add(testBorder);
            PlanDataGrid.Children.Add(testInsideBorder);
            Grid.SetRow(testBorder, 10);
            Grid.SetColumn(testBorder, 1);
            Grid.SetRowSpan(testBorder, 5);
            Grid.SetRow(testInsideBorder, 10);
            Grid.SetColumn(testInsideBorder, 1);
            Grid.SetRowSpan(testInsideBorder, 5);
        }
    }
}
