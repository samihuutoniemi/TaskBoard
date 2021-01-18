using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TaskBoard.UI.ViewModels;
using Blazored.Modal.Services;
using Blazored.Modal;

namespace TaskBoard.UI.Components
{
    public class TaskListBase : ComponentBase
    {
        [CascadingParameter]
        public IModalService Modal { get; set; }

        [Parameter]
        public TaskListViewModel TaskList { get; set; }

        [Parameter]
        public EventCallback RemoveTaskList { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        public async Task ToggleTaskList()
        {
            if (!TaskList.IsExpanded)
            {
                await GetTaskItems();
            }

            TaskList.IsExpanded = !TaskList.IsExpanded;
        }

        public void OpenTaskList()
        {
            TaskList.IsExpanded = true;
            StateHasChanged();
        }

        public async Task DeleteTaskList()
        {
            await HttpClient.DeleteAsync($"http://taskboard.api/TaskList/{TaskList.TaskList.Id}");
            await RemoveTaskList.InvokeAsync();
        }

        public async Task AddTaskItem()
        {
            var modalParameters = new ModalParameters();
            modalParameters.Add("TaskListId", TaskList.TaskList.Id);

            var modal = Modal.Show<AddTaskItem>("Add new task", modalParameters);

            var result = await modal.Result;

            if (!result.Cancelled)
            {
                await GetTaskItems();
                TaskList.IsExpanded = true;
            }
        }
        
        public async Task GetTaskItems()
        {
            var json = await HttpClient.GetStringAsync($"http://taskboard.api/TaskItem/{TaskList.TaskList.Id}");
            var taskItems = JsonSerializer.Deserialize<IEnumerable<Model.TaskItem>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            TaskList.Tasks = taskItems;
        }
    }
}
