using System.Collections.Generic;
using TaskBoard.Model;

namespace TaskBoard.UI.ViewModels
{
    public class TaskListViewModel
    {
        public TaskList TaskList { get; set; }
        public IEnumerable<TaskItem> Tasks { get; set; }
        public bool IsExpanded { get; set; }
    }
}
