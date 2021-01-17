using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TaskBoard.UI.ViewModels;

namespace TaskBoard.UI.Components
{
    public class TaskListBase : ComponentBase
    {
        [Parameter]
        public TaskListViewModel TaskList { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        public async Task ToggleTaskList()
        {
            if (!TaskList.IsExpanded)
            {
                var json = await HttpClient.GetStringAsync($"http://taskboard.api/TaskItem/{TaskList.TaskList.Id}");
                var taskItems = JsonSerializer.Deserialize<IEnumerable<Model.TaskItem>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                TaskList.Tasks = taskItems;
            }

            TaskList.IsExpanded = !TaskList.IsExpanded;
        }

        public async Task DeleteTaskList()
        {
            await HttpClient.DeleteAsync($"http://taskboard.api/TaskList/{TaskList.TaskList.Id}");
            //TaskLists.RemoveAll(tl => tl.TaskList.Id == id);
        }
    }
}
