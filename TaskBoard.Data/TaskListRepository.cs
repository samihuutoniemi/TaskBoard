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

        public IEnumerable<TaskList> GetTaskLists()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var list = conn.GetList<TaskList>();
                conn.Close();

                return list;
            }
        }

        public async Task InsertTaskList(string name)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                
                conn.InsertAsync(new TaskList
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
                conn.DeleteListAsync<TaskItem>($"where TaskListId = {id}");
                conn.Delete<TaskList>(id);
                conn.Close();
            }
        }
    }
}
