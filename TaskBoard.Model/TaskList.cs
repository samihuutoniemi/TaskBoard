using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoard.Model
{
    [Table("TaskLists")]
    public class TaskList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
