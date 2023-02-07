using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
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
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;
using Point = System.Windows.Point;

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



            //Background of task
            var colorString = task.Color;
            var mediaBrushConverter = new BrushConverter();
            var taskBrush = (Brush)mediaBrushConverter.ConvertFromString(colorString);
            var mediaColor = (Color)ColorConverter.ConvertFromString(colorString);

            var gradientBrush = new LinearGradientBrush();
            gradientBrush.StartPoint = new Point(0, 0);
            gradientBrush.EndPoint = new Point(1, 1);
            var startColor = mediaColor;
            var endColor = Color.FromRgb((byte)(mediaColor.R * 0.8), (byte)(mediaColor.G * 0.8), (byte)(mediaColor.B * 0.8));
            gradientBrush.GradientStops.Add(new GradientStop(startColor, 0));
            gradientBrush.GradientStops.Add(new GradientStop(endColor, 1));
            taskBrush = gradientBrush;

            var darkeningAmount = 100;
            var r = (byte)Math.Max(0, mediaColor.R - darkeningAmount);
            var g = (byte)Math.Max(0, mediaColor.G - darkeningAmount);
            var b = (byte)Math.Max(0, mediaColor.B - darkeningAmount);
            var darkerColor = Color.FromRgb(r, g, b);
            var taskBorderColor = new SolidColorBrush(darkerColor);

            //Text size of the task

            int fontSize;
            switch (task.RowSpan)
            {
                case 1: 
                    fontSize = 10;
                    break;
                case 2: 
                    fontSize = 20;
                    break;
                case 3:
                    fontSize = 30;
                    break;
                default: 
                    fontSize = 40;
                    break;
            }

            var radialGradientBrush = new RadialGradientBrush
            {
                Center = new Point(0.5, 0.5),
                RadiusX = 0.5,
                RadiusY = 0.5
            };
            var gradientStop1 = new GradientStop
            {
                Color = Color.FromArgb(150, darkerColor.R, darkerColor.G, darkerColor.B),
                Offset = 0
            };
            radialGradientBrush.GradientStops.Add(gradientStop1);
            var gradientStop2 = new GradientStop
            {
                Color = Color.FromArgb(0, darkerColor.R, darkerColor.G, darkerColor.B),
                Offset = 1
            };
            radialGradientBrush.GradientStops.Add(gradientStop2);


            //Final elements
            var taskBackgroundBorder = new Border
            {
                Margin = new Thickness(10, 0, 10, 0),
                Background = taskBorderColor,
                CornerRadius = new CornerRadius(10)
            };
            var taskTextForeground = new TextBlock
            {
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = fontSize,
                Text = task.Name,
                Foreground = Brushes.WhiteSmoke,
                Background = radialGradientBrush,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(20, 0, 20, 0)
            };

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
    }
}
