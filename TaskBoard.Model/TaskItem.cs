using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoard.Model
{
    [Table("Tasks")]
    public class TaskItem
    {
        public int Id { get; set; }
        public int TaskListId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
