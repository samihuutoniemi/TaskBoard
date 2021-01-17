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
        public IEnumerable<TaskItem> Get(int taskListId)
        {
            var result = _taskItemRepository.GetTaskItems(taskListId);

            _logger.LogInformation($"Returned {result.Count()} taskitems.");

            return result;
        }
    }
}
