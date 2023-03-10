using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using BlockPlanner.Commands;
using BlockPlanner.Models;
using BlockPlanner.Services;
using BlockPlanner.Stores;
using BlockPlanner.Utilities;
using MessageBox = System.Windows.MessageBox;
using Task = BlockPlanner.Models.Task;

namespace BlockPlanner.ViewModels
{
    public class PlanSettingsViewModel : ViewModelBase
    {
        private Scheduler _scheduler;
        private Plan _plan;
        private int _planId;
        private string _planName;
        private PlanCreatorMode _mode;
        private DateTime _selectedDate;
        private ObservableCollection<TaskViewModel> _currentTasks;
        private TaskDetailsViewModel _selectedTask;
        private WeekDay _currentSelectedDay;

        public ICommand DaySelectCommand { get; }
        public ICommand ModifyTaskCommand { get; }
        public ICommand AddNewTaskCommand { get; }
        public ICommand SelectColorCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand RandomizeColorCommand { get; }
        public ICommand SaveAndExitCommand { get; }
        public ICommand BackToMainMenuCommand { get; }
        public ICommand BackToPlanDetailsCommand { get; }

        public Plan Plan  => _plan;
        public int PlanId => _planId;
        public Scheduler Scheduler => _scheduler;

        public string PlanCreatorTitle => _mode == PlanCreatorMode.Add ? 
            "Plan creator (adding new plan)" :
            "Plan creator (modifying existing plan)";

        public string PlanName
        {
            get => _planName;
            set
            {
                _plan.Name = value;
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
                var selectedDay = DateTimeUtilities.GetWeekDay(value);
                DaySelectCommand.Execute(selectedDay.GetWeekDayShortName());
                UpdatePlanWeekDates();
                OnPropertyChanged(nameof(SelectedDate));
                OnPropertyChanged(nameof(WeekDateRange));
            }
        }

        public string WeekDateRange
        {
            get => DateTimeUtilities.GetWeekRange(_selectedDate);
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
                try
                {
                    _selectedTask.StartTime = DateTimeUtilities.ValidateTaskTimeStamp(value);
                    OnPropertyChanged(nameof(StartTime));
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid StartTime format.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public string EndTime
        {
            get => _selectedTask == null ? "" : _selectedTask.EndTime.ToString("t");
            set
            {
                try
                {
                    _selectedTask.EndTime = DateTimeUtilities.ValidateTaskTimeStamp(value);
                    OnPropertyChanged(nameof(EndTime));
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid StartTime format.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
            get => (int)_currentSelectedDay;
            set
            {
                _currentSelectedDay = (WeekDay)value;
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

        public int Mode => (int)_mode;

        public PlanSettingsViewModel(Scheduler scheduler, 
            Plan plan,
            PlanCreatorMode mode,
            int planId,
            NavigationService createMainMenuNavigationService,
            ParameterNavigationService<int> createPlanDetailsNavigationService)
        {
            if (mode == PlanCreatorMode.Add)
            {
                _plan = new Plan();
                _planName = "<enterPlanName>";
                var weekStartDate = DateTimeUtilities.GetWeekStart(DateTime.Now);
                _plan.WeekStartTime = weekStartDate;
                _plan.WeekEndTime = DateTimeUtilities.GetWeekEnd(DateTime.Now);
                _selectedDate = weekStartDate;
                _currentSelectedDay = WeekDay.Monday;
                _currentTasks = new ObservableCollection<TaskViewModel>();
                _selectedTask = new TaskDetailsViewModel(TaskDetailsViewModel.GetDefaultTask(weekStartDate));
                _mode = mode;
                _scheduler = scheduler;
            }
            else if (mode == PlanCreatorMode.Modify)
            {
                _scheduler = scheduler;
                _plan = scheduler.Plans[planId-1];
                _planId = planId;
                _planName = _plan.Name;
                _mode = mode;
                _selectedDate = _plan.WeekStartTime;
                _currentTasks = new ObservableCollection<TaskViewModel>();
                _currentSelectedDay = WeekDay.Monday;
                foreach (var task in _plan.ScheduledDays[_currentSelectedDay.GetId()].DayTasks)
                {
                    _currentTasks.Add(new TaskDetailsViewModel(task));
                }

                if (_currentTasks.Count == 0)
                {
                    _selectedTask = TaskDetailsViewModel.GetDefaultTask(_selectedDate);
                }
                else
                {
                    _selectedTask = (TaskDetailsViewModel)_currentTasks[0];
                }
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
            SaveAndExitCommand = new SaveAndExitCommand(this);
            BackToMainMenuCommand = new NavigateCommand(createMainMenuNavigationService);
            BackToPlanDetailsCommand = new ParameterNavigationCommand<int>(createPlanDetailsNavigationService);
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

        public void UpdateSelectedDay(DateTime newSelectedDay)
        {
            var dayDiff = WeekDayMethods.GetWeekDayIdFromDateTime(newSelectedDay) -
                          WeekDayMethods.GetWeekDayIdFromDateTime(_selectedDate);
            _selectedDate = newSelectedDay;
            _selectedTask.StartTime = _selectedTask.StartTime.AddDays(dayDiff);
            _selectedTask.EndTime = _selectedTask.EndTime.AddDays(dayDiff);
            UpdatePlanWeekDates();
            OnPropertyChanged(nameof(SelectedDate));
        }

        private void UpdatePlanWeekDates()
        {
            _plan.WeekStartTime = DateTimeUtilities.GetWeekStart(_selectedDate);
            _plan.WeekEndTime = DateTimeUtilities.GetWeekEnd(_selectedDate);
        }

        
    }
}
