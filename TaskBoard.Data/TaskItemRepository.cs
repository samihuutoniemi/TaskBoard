using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TaskBoard.Model;

namespace TaskBoard.Data
{
    public class TaskItemRepository
    {
        private readonly string _connectionString;

        public TaskItemRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TaskBoardDatabase");
        }

        public async Task<IEnumerable<TaskItem>> GetTaskItems(int taskListId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var list = await conn.GetListAsync<TaskItem>($"where TaskListId = {taskListId}");
                conn.Close();

                return list;
            }
        }
    }
}
