using System;
using System.Data;
using Microsoft.Data.SqlClient;
namespace IS349_Library.Models
{
    public class Dbcontext
    {
        private SqlConnection _connection;
        public Dbcontext()
        {
            string conString = @"Server=localhost;Database=IS349_Library;User Id=SA;Password=IS349SQL;TrustServerCertificate=true";
            _connection = new SqlConnection(conString);
        }
        public bool Is_exist(string sql)
        {
            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            {
                try
                {
                    _connection.Open();
                    var data = cmd.ExecuteReader();
                    return data.HasRows;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    _connection.Close();
                }
            }
        }
        public SqlDataReader ReadData(string sql)
        {
            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@empName", ((object)sql) ?? DBNull.Value);
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }
                return cmd.ExecuteReader();
            }
        }
        public SqlDataReader GetType(string search)
        {
            string sql = "SELECT * FROM Table_booktype WHERE booktype_name  Like N'%'+ @typeName +'%'";
            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@typeName", ((object)search) ?? DBNull.Value);
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }
                return cmd.ExecuteReader();
            }
        }
        public SqlDataReader GetEmployee(string search)
        {
            if (!string.IsNullOrEmpty(search)) { search = search.ToLower(); }
            string sql = "SELECT e.EmployeeId,e.EmployeeName,e.Gender,e.Address,e.Salary,p.PositionName,d.DepartmentName" +
                        "\nFROM Employee e INNER JOIN[Position] p ON e.PositionId = p.PositionId" +
                        "\nINNER JOIN Department d ON e.DepartmentId = d.DepartmentId WHERE LOWER(e.EmployeeName) LIKE N'%' + @empName +'%' OR LOWER(p.PositionName) LIKE N'%' + @empName +'%' OR @empName is Null";
            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@empName", ((object)search) ?? DBNull.Value);
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }
                return cmd.ExecuteReader();
            }
        }
        public bool ExecuteQuery(string sql)
        {
            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            {
                try
                {
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}

