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

        [HttpPost]
        public async Task Insert([FromBody] TaskItem taskItem)
        {
            await _taskItemRepository.InsertTaskItem(taskItem.TaskListId, taskItem.Name, taskItem.Description);

            _logger.LogInformation($"Inserted new taskitem with name: {taskItem.Name}");
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _taskItemRepository.DeleteTaskItem(id);

            _logger.LogInformation($"Deleted taskitem with id: {id}");
        }
    }
}
