using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using BlockPlanner.Commands;
using BlockPlanner.Models;
using BlockPlanner.Utilities;
using Task = BlockPlanner.Models.Task;

namespace BlockPlanner.ViewModels
{
    public class PlanSettingsViewModel : ViewModelBase
    {
        private Plan _plan;
        private string _planName;
        private DateTime _selectedDate;
        private ObservableCollection<TaskViewModel> _currentTasks;
        private TaskDetailsViewModel _selectedTask;
        private WeekDay _currentSelectedDayId;

        public ICommand DaySelectCommand { get; }
        public ICommand ModifyTaskCommand { get; }
        public ICommand AddNewTaskCommand { get; }
        public ICommand SelectColorCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand RandomizeColorCommand { get; }

        public Plan Plan  => _plan;
        public string PlanName
        {
            get => _planName;
            set
            {
                _planName = value;
                OnPropertyChanged(nameof(PlanName));
            }
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
                OnPropertyChanged(nameof(WeekDateRange));
            }
        }

        public string WeekDateRange
        {
            get => DateTimeUtilities.GetWeekRange(_selectedDate);
            set
            {
                WeekDateRange = value;
                OnPropertyChanged(nameof(WeekDateRange));
            }
        }

        public ObservableCollection<TaskViewModel> CurrentTasks
        {
            get => _currentTasks;
            set => _currentTasks = value;
        }


        public string TaskName
        {
            get => _selectedTask == null ? "" : _selectedTask.TaskName;
            set
            {
                _selectedTask.TaskName = value;
                OnPropertyChanged(nameof(TaskName));
            }
        }

        public string StartTime
        {
            get => _selectedTask == null ? "" : _selectedTask.StartTime.ToString("t");
            set
            {
                _selectedTask.StartTime = DateTime.Parse(value);
                OnPropertyChanged(nameof(StartTime));
            }
        }

        public string EndTime
        {
            get => _selectedTask == null ? "" : _selectedTask.EndTime.ToString("t");
            set
            {
                _selectedTask.EndTime = DateTime.Parse(value);
                OnPropertyChanged(nameof(EndTime));
            }
        }

        public string Color
        {
            get => _selectedTask == null ? "#FFFFFF" : _selectedTask.Color;
            set
            {
                _selectedTask.Color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        public string AdditionalInfo
        {
            get => _selectedTask == null ? "" : _selectedTask.AdditionalInfo;
            set
            {
                _selectedTask.AdditionalInfo = value;
                OnPropertyChanged(nameof(AdditionalInfo));
            }
        }

        public int CurrentSelectedDayId
        {
            get => (int)_currentSelectedDayId;
            set
            {
                _currentSelectedDayId = (WeekDay)value;
                OnPropertyChanged(nameof(CurrentSelectedDayId));
                OnPropertyChanged(nameof(CurrentDayPlan));
                OnPropertyChanged(nameof(CurrentTasks));
            }
        }

        public DayPlan CurrentDayPlan
        {
            get => _plan.ScheduledDays[CurrentSelectedDayId];
        }

        public TaskDetailsViewModel SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
                OnPropertyChanged(nameof(TaskName));
                OnPropertyChanged(nameof(StartTime));
                OnPropertyChanged(nameof(EndTime));
                OnPropertyChanged(nameof(Color));
                OnPropertyChanged(nameof(AdditionalInfo));
            }
        }

        public PlanSettingsViewModel()
        {
            
        }

        public PlanSettingsViewModel(Plan plan)
        {
            _plan = plan;
            _planName = plan.Name;
            _currentTasks = new ObservableCollection<TaskViewModel>();
            _selectedDate = plan.WeekStartTime;

            //For tests;
            if (plan.ScheduledDays != null)
            {
                var testTask = plan.ScheduledDays[WeekDay.Monday.GetId()].DayTasks[0];
                var testTask2 = plan.ScheduledDays[WeekDay.Monday.GetId()].DayTasks[1];
                var testTask3= plan.ScheduledDays[WeekDay.Monday.GetId()].DayTasks[2];
                var taskDetailsViewModelTest = new TaskDetailsViewModel(testTask, 1);
                var taskDetailsViewModelTest2 = new TaskDetailsViewModel(testTask2, 2);
                var taskDetailsViewModelTest3 = new TaskDetailsViewModel(testTask3, 3);
                _currentTasks.Add(taskDetailsViewModelTest);
                _currentTasks.Add(taskDetailsViewModelTest2);
                _currentTasks.Add(taskDetailsViewModelTest3);
                _selectedTask = new TaskDetailsViewModel(taskDetailsViewModelTest2);
                // _selectedTask.StartTime = _selectedTask.StartTime.AddHours(-10);
                // _selectedTask.EndTime = _selectedTask.EndTime.AddHours(-9);
                UpdateOrderId();
            }

            foreach (var task in _currentTasks)
            {
                task.PropertyChanged += AddTaskSelectionSubscription;
            }

            DaySelectCommand = new DaySelectCommand(this);
            AddNewTaskCommand = new AddNewTaskCommand(this);
            SelectColorCommand = new SelectColorCommand(this);
            ModifyTaskCommand = new ModifyTaskCommand(this);
            DeleteTaskCommand = new DeleteTaskCommand(this);
            RandomizeColorCommand = new RandomizeColorCommand(this);
        }


        public static void UpdateOrderId(ObservableCollection<TaskViewModel> currentTasks)
        {
            for (var elementId = 0; elementId < currentTasks.Count; elementId++)
            {
                currentTasks[elementId].Order = (elementId + 1).ToString();
            }
        }
        public void UpdateOrderId()
        {
            UpdateOrderId(CurrentTasks);
        }

        public void AddTaskSelectionSubscription(object sender, PropertyChangedEventArgs args)
        {
            var senderObj = (TaskDetailsViewModel)sender;
            if (args.PropertyName != "IsGroovy" || !senderObj.IsGroovy) return;

            Console.WriteLine("New task selected (" + _selectedTask.StartTime + ")");

            SelectedTask = new TaskDetailsViewModel(senderObj);
            TaskName = SelectedTask.TaskName;
            StartTime = SelectedTask.StartTime.ToString("t");
            EndTime = SelectedTask.EndTime.ToString("t");
            Color = SelectedTask.Color;
            AdditionalInfo = SelectedTask.AdditionalInfo;

            Console.WriteLine("Now is (" + SelectedTask.StartTime + ")");
        }
    }
}
