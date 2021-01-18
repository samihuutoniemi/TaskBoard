using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TaskBoard.UI.Components
{
    public class TaskItemBase : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        [CascadingParameter]
        public Pages.Index IndexPage { get; set; }

        [CascadingParameter]
        public IModalService Modal { get; set; }

        [Parameter]
        public Model.TaskItem TaskItem { get; set; }

        [Parameter]
        public EventCallback RefreshTaskList { get; set; }

        public async Task EditTaskItem()
        {
            var modalParameters = new ModalParameters();
            modalParameters.Add("TaskItem", TaskItem);

            var modal = Modal.Show<AddTaskItem>("Edit task", modalParameters);

            var result = await modal.Result;

            if (!result.Cancelled)
            {
                await RefreshTaskList.InvokeAsync();
            }
        }

        public async Task DeleteTaskItem()
        {
            await HttpClient.DeleteAsync($"http://taskboard.api/TaskItem/{TaskItem.Id}");
            await RefreshTaskList.InvokeAsync();
        }

        public async Task TransferTaskItem()
        {
            var modalParameters = new ModalParameters();
            modalParameters.Add("TaskItemId", TaskItem.Id);

            var modal = Modal.Show<TransferTaskItem>("Transfer task", modalParameters);

            var result = await modal.Result;

            if (!result.Cancelled)
            {
                await RefreshTaskList.InvokeAsync();

                var targetTaskListComponent = IndexPage.TaskListComponents.First(tl => tl.id == (int)result.Data).taskList;
                await targetTaskListComponent.GetTaskItems();
                targetTaskListComponent.OpenTaskList();
            }
        }
    }
}
