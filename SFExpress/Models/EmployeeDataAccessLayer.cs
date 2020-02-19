using Newtonsoft.Json;
using SFExpress.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SFExpress.Models
{
    public class EmployeeDataAccessLayer
    {
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=EFGetStarted.ConsoleApp.NewDb;Trusted_Connection=True;";
        #region Employee CRUD
        public List<Employee> GetAllEmployee()
        {

            List<Employee> employeeList = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "dbo.EmployeeGetAll";
                cmd.CommandType = CommandType.StoredProcedure;

                using (IDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Employee employee = GetEmployeeMapper(reader);

                        if (employeeList == null)
                        {
                            employeeList = new List<Employee>();
                        }
                        employeeList.Add(employee);
                    }
                }
            }
            return employeeList;
        }
        public Employee GetEmployee(int id)
        {

            Employee employee = new Employee();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sqlQuery = "SELECT * FROM tblEmpolyees WHERE EmployeeID= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    employee = GetEmployeeMapper(reader);
                }
            }
            return employee;
        }
        public int AddEmployee(EmployeeAddRequest model)
        {
  
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "dbo.Employee_Insert";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                cmd.Parameters.AddWithValue("@LastName", model.LastName);
                cmd.Parameters.AddWithValue("@HiredDate", model.HiredDate);

                DataTable taskTable = null;

                if (model.Tasks.Count > 0)
                {
                    taskTable = new DataTable();
                    taskTable.Columns.Add("Name", typeof(string));
                    taskTable.Columns.Add("StartTime", typeof(DateTime));
                    taskTable.Columns.Add("Deadline", typeof(DateTime));

                    foreach (var item in model.Tasks)
                    {
                        DataRow dr = taskTable.NewRow();
                        dr[0] = item.Name;
                        dr[1] = item.StartTime;
                        dr[2] = item.Deadline;
                        taskTable.Rows.Add(dr);
                    }
                }
                cmd.Parameters.AddWithValue("@Tasks", taskTable);



                SqlParameter idParam = cmd.Parameters.Add("@Id", SqlDbType.Int);
                idParam.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                return (int)idParam.Value;
            }
        }
        public void UpdateEmployee(int id, EmployeeUpdateRequest model)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "dbo.Employee_Update";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                cmd.Parameters.AddWithValue("@LastName", model.LastName);
                cmd.Parameters.AddWithValue("@HiredDate", model.HiredDate);

                DataTable taskTable = null;

                if (model.Tasks.Count > 0)
                {
                    taskTable = new DataTable();
                    taskTable.Columns.Add("Name", typeof(string));
                    taskTable.Columns.Add("StartTime", typeof(DateTime));
                    taskTable.Columns.Add("Deadline", typeof(DateTime));

                    foreach (var item in model.Tasks)
                    {
                        DataRow dr = taskTable.NewRow();
                        dr[0] = item.Name;
                        dr[1] = item.StartTime;
                        dr[2] = item.Deadline;
                        taskTable.Rows.Add(dr);
                    }
                }
                cmd.Parameters.AddWithValue("@Tasks", taskTable);

                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteEmployee(int id)
        {
          
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "dbo.Employee_Delete";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }
        private static Employee GetEmployeeMapper(IDataReader reader)
        {
            int index = 0;
            Employee employee = new Employee();
            employee.FirstName = reader.GetString(index++);
            employee.LastName = reader.GetString(index++);
            employee.HiredDate = reader.GetDateTime(index++);
            string tasks = reader.GetString(index++);
            if (!string.IsNullOrEmpty(tasks))
            {
                employee.Task = JsonConvert.DeserializeObject<List<Task>>(tasks);
            }

            return employee;
        }
        #endregion
        #region Task CRUD
        public List<Task> GetAllTasks(int employeeId)
        {
           
            List<Task> taskList = new List<Task>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "dbo.Tasks_SelectByEmployee";
                cmd.CommandType = CommandType.StoredProcedure;

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int index = 0;

                        Task task = new Task();
                        task.Name = reader.GetString(index++);
                        task.StartTime = reader.GetDateTime(index++);
                        task.Deadline = reader.GetDateTime(index++);

                        if (taskList == null)
                        {
                            taskList = new List<Task>();
                        }
                        taskList.Add(task);
                    }
                }
            }
            return taskList;
        }
        public Task GetTask(int id)
        {
           
            Task task = new Task();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sqlQuery = "SELECT * FROM tblTasks WHERE EmployeeID= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    int index = 0;

                    task.Name = reader.GetString(index++);
                    task.StartTime = reader.GetDateTime(index++);
                    task.Deadline = reader.GetDateTime(index++);
                }
            }
            return task;
        }
        public int AddTask(TaskAddRequest model)
        {
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "dbo.Task_Insert";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@StartTime", model.StartTime);
                cmd.Parameters.AddWithValue("@Deadline", model.Deadline);

                SqlParameter idParam = cmd.Parameters.Add("@Id", SqlDbType.Int);
                idParam.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                return (int)idParam.Value;
            }
        }
        public void UpdateTask(TaskUpdateRequest model, int id)
        {
          
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "dbo.Task_Update";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@StartTime", model.StartTime);
                cmd.Parameters.AddWithValue("@Deadline", model.Deadline);

                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteTask(int id)
        {
           
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "dbo.Task_Delete";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}
