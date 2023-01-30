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
using Task = BlockPlanner.Models.Task;

namespace BlockPlanner.ViewModels
{
    public class PlanSettingsViewModel : ViewModelBase
    {
        private Plan _plan;
        private string _planName;
        private DateTime _weekStartDate;
        private ObservableCollection<TaskViewModel> _currentTasks;
        private TaskDetailsViewModel _selectedTask;
        private WeekDay _currentSelectedDayId;

        private ICommand DaySelectCommand { get; }
        public ICommand ModifyTaskCommand { get; }
        public ICommand AddNewTaskCommand { get; }
        public ICommand SelectColorCommand { get; }
        public ICommand DeleteTaskCommand { get; }


        public string PlanName
        {
            get => _planName;
            set
            {
                _planName = value;
                OnPropertyChanged(nameof(PlanName));
            }
        }

        public DateTime WeekStartDate
        {
            get => _weekStartDate;
            set
            {
                _weekStartDate = value;
                OnPropertyChanged(nameof(WeekStartDate));
            }
        }

        public ObservableCollection<TaskViewModel> CurrentTasks => _currentTasks;


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
            set => _currentSelectedDayId = (WeekDay)value;
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


            //For tests;
            if (plan.ScheduledDays != null)
            {
                Task testTask = plan.ScheduledDays[(int)WeekDay.Monday].DayTasks[0];
                Task testTask2 = plan.ScheduledDays[(int)WeekDay.Monday].DayTasks[1];
                Task testTask3= plan.ScheduledDays[(int)WeekDay.Monday].DayTasks[2];
                TaskDetailsViewModel taskDetailsViewModelTest = new TaskDetailsViewModel(testTask, 1);
                TaskDetailsViewModel taskDetailsViewModelTest2 = new TaskDetailsViewModel(testTask2, 2);
                TaskDetailsViewModel taskDetailsViewModelTest3 = new TaskDetailsViewModel(testTask3, 3);
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

            AddNewTaskCommand = new AddNewTaskCommand(this);
            SelectColorCommand = new SelectColorCommand(this);
            ModifyTaskCommand = new ModifyTaskCommand(this);
            DeleteTaskCommand = new DeleteTaskCommand(this);
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

        private void AddTaskSelectionSubscription(object sender, PropertyChangedEventArgs args)
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
