using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskBoard.UI.Components
{
    public class TransferTaskItemBase : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        [CascadingParameter]
        BlazoredModalInstance ModalInstance { get; set; }

        [Parameter]
        public int TaskItemId { get; set; }

        public IEnumerable<Model.TaskList> TaskLists { get; set; }

        public int SelectedTaskListId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var json = await HttpClient.GetStringAsync("http://taskboard.api/TaskList");
            TaskLists = JsonSerializer.Deserialize<IEnumerable<Model.TaskList>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            SelectedTaskListId = TaskLists.First().Id;

            await base.OnInitializedAsync();
        }

        public async Task Ok()
        {
            await HttpClient.PostAsync($"http://taskboard.api/TaskItem/Transfer/{TaskItemId}/{SelectedTaskListId}", null);
            await ModalInstance.CloseAsync(ModalResult.Ok(SelectedTaskListId, typeof(int)));
        }
    }
}
