using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    }
}
