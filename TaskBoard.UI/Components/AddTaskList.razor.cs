using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace TaskBoard.UI.Components
{
    public class AddTaskListBase : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        [CascadingParameter] 
        BlazoredModalInstance ModalInstance { get; set; }

        public string Name { get; set; }

        public async Task Ok()
        {
            await HttpClient.PostAsync($"http://taskboard.api/TaskList/{Name}", null);
            await ModalInstance.CloseAsync(ModalResult.Ok(""));
        }
    }
}
