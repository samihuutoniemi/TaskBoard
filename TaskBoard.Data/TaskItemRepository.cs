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

        public async Task InsertOrEditTaskItem(TaskItem taskItem)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                if (taskItem.Id == 0)
                {
                    await conn.InsertAsync(taskItem);
                }
                else
                {
                    await conn.UpdateAsync(taskItem);
                }

                conn.Close();
            }
        }

        public async Task DeleteTaskItem(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                await conn.DeleteAsync<TaskItem>(id);
                conn.Close();
            }
        }
    }
}
