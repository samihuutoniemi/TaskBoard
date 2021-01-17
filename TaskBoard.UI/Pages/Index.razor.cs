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

        public IEnumerable<TaskListViewModel> TaskLists { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _apiEndpoint = Configuration.GetSection("ApiEndpoint").Value;

            var json = await HttpClient.GetStringAsync("http://taskboard.api/TaskList");
            var taskLists = JsonSerializer.Deserialize<IEnumerable<TaskList>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            TaskLists = taskLists.Select(tl => new TaskListViewModel { TaskList = tl });

            base.OnInitializedAsync();
        }
    }
}
