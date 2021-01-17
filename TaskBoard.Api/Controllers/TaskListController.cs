using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TaskBoard.Data;
using TaskBoard.Model;

namespace TaskBoard.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskListController : ControllerBase
    {
        private readonly ILogger<TaskListController> _logger;
        private readonly TaskListRepository _taskListRepository;

        public TaskListController(ILogger<TaskListController> logger, TaskListRepository taskListRepository)
        {
            _logger = logger;
            _taskListRepository = taskListRepository;
        }

        [HttpGet]
        public IEnumerable<TaskList> Get()
        {
            var result = _taskListRepository.GetTaskLists();

            _logger.LogInformation($"Returned {result.Count()} tasklists.");

            return result;
        }
    }
}
