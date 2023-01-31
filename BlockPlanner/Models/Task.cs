using BlockPlanner.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BlockPlanner.Models
{
    public class Task
    {
        public string TaskName { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Color BlockColor { get; set; }

        public string AdditionalInfo { get; set; }

        public Task()
        {
        }

        public Task(string taskName, DateTime startTime, DateTime endTime, Color blockColor, string additionalInfo)
        {
            TaskName = taskName;
            StartTime = startTime;
            EndTime = endTime;
            BlockColor = blockColor;
            AdditionalInfo = additionalInfo;
        }

        public bool UpdateTaskInformation(Task newData, List<Task> dayTasks, out int placementId)
        {
            placementId = 0;
            var isChanged = false;

            Console.WriteLine(this);
            var isTimeStampModificationRequired = StartTime != newData.StartTime || EndTime != newData.EndTime;
            if (dayTasks != null)
            {
                foreach (var existingTask in dayTasks.Where(existingTask => existingTask != this))
                {
                    if (IsScheduledBetween(existingTask, newData))
                    {
                        throw new TaskCollisionException();
                    }
                    if (newData.StartTime > existingTask.EndTime)
                    {
                        placementId++;
                    }
                }

                if (isTimeStampModificationRequired)
                {

                    isChanged = true;
                    StartTime = newData.StartTime;
                    EndTime = newData.EndTime;
                }
            }

            if (TaskName != newData.TaskName)
            {
                isChanged = true;
                TaskName = newData.TaskName;
            }

            if (BlockColor != newData.BlockColor)
            {
                isChanged = true;
                BlockColor = newData.BlockColor;
            }

            if (AdditionalInfo != newData.AdditionalInfo)
            {
                isChanged = true;
                AdditionalInfo = newData.AdditionalInfo;
            }

            Console.WriteLine("Modified: " + this);
            return isChanged;
        }
        
        public static bool IsScheduledBetween(Task existingTask, Task newTask)
        {
            if (existingTask == null || newTask == null)
            {
                Console.WriteLine("Error - tasks were null");
                return false;
            }

            if (newTask.EndTime < existingTask.StartTime
                || newTask.StartTime > existingTask.EndTime)
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
        return
            $"Task {TaskName} info, start {StartTime:t} - end {EndTime:t}, color {BlockColor}, additionalInfo is: {AdditionalInfo}";
        }
    }
}
