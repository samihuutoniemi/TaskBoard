using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskBoard.Data;
using TaskBoard.Model;

namespace TaskBoard.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly ILogger<TaskListController> _logger;
        private readonly TaskItemRepository _taskItemRepository;

        public TaskItemController(ILogger<TaskListController> logger, TaskItemRepository taskItemRepository)
        {
            _logger = logger;
            _taskItemRepository = taskItemRepository;
        }

        [HttpGet("{taskListId}")]
        public async Task<IEnumerable<TaskItem>> Get(int taskListId)
        {
            var result = await _taskItemRepository.GetTaskItems(taskListId);

            _logger.LogInformation($"Returned {result.Count()} taskitems.");

            return result;
        }

        [HttpPost("InsertOrEdit")]
        public async Task InsertOrEdit([FromBody] TaskItem taskItem)
        {
            await _taskItemRepository.InsertOrEditTaskItem(taskItem);

            _logger.LogInformation($"Inserted new taskitem with name: {taskItem.Name}");
        }

        [HttpPost("Transfer/{id}/{newTaskListId}")]
        public async Task Transfer(int id, int newTaskListId)
        {
            await _taskItemRepository.TransferTaskItem(id, newTaskListId);

            _logger.LogInformation($"Transfered taskitem with id: {id} to list with id: {newTaskListId}");
        }


        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _taskItemRepository.DeleteTaskItem(id);

            _logger.LogInformation($"Deleted taskitem with id: {id}");
        }
    }
}
