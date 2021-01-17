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
        public async Task<IEnumerable<TaskList>> Get()
        {
            var result = await _taskListRepository.GetTaskLists();

            _logger.LogInformation($"Returned {result.Count()} tasklists.");

            return result;
        }

        [HttpPost("{name}")]
        public async Task Insert(string name)
        {
            await _taskListRepository.InsertTaskList(name);

            _logger.LogInformation($"Inserted new tasklist with name: {name}");
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _taskListRepository.DeleteTaskList(id);

            _logger.LogInformation($"Deleted tasklist with id: {id}");
        }
    }
}
