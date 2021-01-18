using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TaskBoard.UI.Components;
using TaskBoard.UI.ViewModels;

namespace TaskBoard.UI.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public IConfiguration Configuration { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [CascadingParameter]
        public IModalService Modal { get; set; }

        public List<TaskListViewModel> TaskLists { get; set; }

        public List<(int id, Components.TaskList taskList)> TaskListComponents { get; set; } = new List<(int id, Components.TaskList taskList)>();
        public Components.TaskList TaskListRef 
        {
            set
            {
                TaskListComponents.Add((value.TaskList.TaskList.Id, value));
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await GetTaskLists();
            await base.OnInitializedAsync();
        }

        private async Task GetTaskLists()
        {
            var json = await HttpClient.GetStringAsync("http://taskboard.api/TaskList");
            var taskLists = JsonSerializer.Deserialize<IEnumerable<Model.TaskList>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            TaskLists = taskLists.Select(tl => new TaskListViewModel { TaskList = tl }).ToList();
        }

        public async Task AddTaskList()
        {
            var modal = Modal.Show<AddTaskList>("Add new task list");

            var result = await modal.Result;

            if (!result.Cancelled)
            {
                await GetTaskLists();
            }
        }

        public void RemoveTaskList(int id)
        {
            TaskLists.RemoveAll(tl => tl.TaskList.Id == id);
        }
    }
}
