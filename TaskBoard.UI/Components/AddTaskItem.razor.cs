using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskBoard.UI.Components
{
    public class AddTaskItemBase : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        [CascadingParameter]
        BlazoredModalInstance ModalInstance { get; set; }

        [Parameter]
        public int TaskListId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public async Task Ok()
        {
            var content = new Model.TaskItem
            {
                TaskListId = TaskListId,
                Name = Name,
                Description = Description
            };

            var json = JsonSerializer.Serialize(content);

            await HttpClient.PostAsync($"http://taskboard.api/TaskItem", new StringContent(json, Encoding.UTF8, "application/json"));

            ModalInstance.CloseAsync(ModalResult.Ok(""));
        }
    }
}
