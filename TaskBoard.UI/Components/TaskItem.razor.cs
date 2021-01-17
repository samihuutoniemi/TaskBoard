using Microsoft.AspNetCore.Components;

namespace TaskBoard.UI.Components
{
    public class TaskItemBase : ComponentBase
    {
        [Parameter]
        public Model.TaskItem TaskItem { get; set; }
    }
}
