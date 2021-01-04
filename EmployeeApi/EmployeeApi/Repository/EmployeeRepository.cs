using EmployeeApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Repository
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();

        Employee GetEmployee(int empNo);

        void AddEmployee(Employee employee);
    }
    public class EmployeeRepository : IEmployeeRepository
    {
        string _dbConnection = "Server=tcp:surenserver.database.windows.net,1433;Initial Catalog=SurenDb;Persist Security Info=False;User ID=dbadmin;Password=Surendra12$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public void AddEmployee(Employee employee)
        {
            SqlConnection con = new SqlConnection(_dbConnection);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("InserEmployee", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpNo", employee.EmpNo);
                cmd.Parameters.AddWithValue("@EmpName", employee.EmpName);
                cmd.Parameters.AddWithValue("@Dept", employee.Dept);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while executing AddEmployee method - {ex.Message}");
            }
            finally
            {
                con.Close();
            }
        }

        public List<Employee> GetAllEmployees()
        {
            var employeeList = new List<Employee>();

            SqlConnection con = new SqlConnection(_dbConnection);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from dbo.Employee", con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    employeeList.Add(new Employee
                    {
                        EmpNo = (int)dr["EmpNo"],
                        EmpName = dr["EmpName"]?.ToString(),
                        Dept = dr["Dept"]?.ToString(),
                        Salary = (Decimal)dr["Salary"]
                    });
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error while executing GetAllEmployees method - {ex.Message}");
            }
            finally
            {
                con.Close();
            }
            
            return employeeList;
        }

        public Employee GetEmployee(int empNo)
        {
            SqlConnection con = new SqlConnection(_dbConnection);
            Employee employee = null;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"Select * from dbo.Employee where EmpNo = {empNo}", con);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    employee = new Employee
                    {
                        EmpNo = (int)dr["EmpNo"],
                        EmpName = dr["EmpName"]?.ToString(),
                        Dept = dr["Dept"]?.ToString(),
                        Salary = (Decimal)dr["Salary"]
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while executing GetEmployee method - {ex.Message}");
            }
            finally
            {
                con.Close();
            }

            return employee;
        }
    }
}
