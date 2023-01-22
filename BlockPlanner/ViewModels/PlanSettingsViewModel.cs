using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ICommand TaskSelectCommand { get; }
        public ICommand AddNewTaskCommand { get; }
        public ICommand SelectColorCommand { get; }
        private ICommand DeleteTaskCommand { get; }


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
                _selectedTask = new TaskDetailsViewModel(testTask);
            }

            foreach (var task in _currentTasks)
            {
                task.PropertyChanged += (sender, EventArgs) =>
                {
                    if (EventArgs.PropertyName == "IsGroovy")
                    {
                        Console.WriteLine("New task selected (" + _selectedTask.StartTime + ")");
                        this.SelectedTask = (TaskDetailsViewModel)sender;
                        this.TaskName = _selectedTask.TaskName;
                        this.StartTime = _selectedTask.StartTime.ToString("t");
                        this.EndTime = _selectedTask.EndTime.ToString("t");
                        this.Color = _selectedTask.Color;
                        this.AdditionalInfo = _selectedTask.AdditionalInfo;

                        Console.WriteLine("Now is (" + _selectedTask.StartTime + ")");
                    }
                };
            }

            AddNewTaskCommand = new AddNewTaskCommand(_plan.ScheduledDays, this);
            SelectColorCommand = new SelectColorCommand(this);
        }

        private void ChangeCurrentlySelectedTask(object sender, EventArgs args)
        {

        }
    }
}
