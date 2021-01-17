using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TaskBoard.Model;
using TaskBoard.UI.ViewModels;

namespace TaskBoard.UI.Pages
{
    public class IndexBase : ComponentBase
    {
        private string _apiEndpoint;

        [Inject]
        public IConfiguration Configuration { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        public List<TaskListViewModel> TaskLists { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var json = await HttpClient.GetStringAsync("http://taskboard.api/TaskList");
            var taskLists = JsonSerializer.Deserialize<IEnumerable<TaskList>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            TaskLists = taskLists.Select(tl => new TaskListViewModel { TaskList = tl }).ToList();

            await base.OnInitializedAsync();
        }

        public async Task AddTaskList()
        {
        }

        public async Task DeleteTaskList(int id)
        {
            await HttpClient.DeleteAsync($"http://taskboard.api/TaskList/{id}");
            TaskLists.RemoveAll(tl => tl.TaskList.Id == id);
        }

        public async Task ToggleTaskList(TaskListViewModel taskList)
        {
            if (!taskList.IsExpanded)
            {
                var json = await HttpClient.GetStringAsync($"http://taskboard.api/TaskItem/{taskList.TaskList.Id}");
                var taskItems = JsonSerializer.Deserialize<IEnumerable<TaskItem>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                taskList.Tasks = taskItems;
            }

            taskList.IsExpanded = !taskList.IsExpanded;
        }
    }
}
