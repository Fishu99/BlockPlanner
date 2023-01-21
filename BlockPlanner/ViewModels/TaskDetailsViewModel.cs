using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = BlockPlanner.Models.Task;

namespace BlockPlanner.ViewModels
{
    public class TaskDetailsViewModel : TaskViewModel
    {
        public string AdditionalInfo
        {
            get => _task.AdditionalInfo;
            set => _task.AdditionalInfo = value;
        }


        public TaskDetailsViewModel(Task task) : base(task)
        {
        }

        public TaskDetailsViewModel(Task task, int order) : base(task)
        {
            this.Order = order.ToString();
        }
    }
}
