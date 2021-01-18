using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace TaskBoard.UI.Components
{
    public class TaskItemBase : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        [Parameter]
        public Model.TaskItem TaskItem { get; set; }

        [Parameter]
        public EventCallback RemoveTaskItem { get; set; }

        public async Task DeleteTaskItem()
        {
            await HttpClient.DeleteAsync($"http://taskboard.api/TaskItem/{TaskItem.Id}");
            RemoveTaskItem.InvokeAsync();
        }
    }
}
