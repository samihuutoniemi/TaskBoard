using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TaskBoard.Model;

namespace TaskBoard.Data
{
    public class TaskListRepository
    {
        private readonly string _connectionString;

        public TaskListRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TaskBoardDatabase");
        }

        public async Task<IEnumerable<TaskList>> GetTaskLists()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var list = await conn.GetListAsync<TaskList>();
                    conn.Close();

                    return list;
                }
                catch
                {
                    return null;
                }
            }
        }

        public async Task InsertTaskList(string name)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                await conn.InsertAsync(new TaskList
                {
                    Name = name
                });

                conn.Close();
            }
        }

        public async Task DeleteTaskList(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                await conn.DeleteListAsync<TaskItem>($"where TaskListId = {id}");
                await conn.DeleteAsync<TaskList>(id);
                conn.Close();
            }
        }
    }
}
