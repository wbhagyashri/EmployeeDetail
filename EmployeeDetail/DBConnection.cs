using EmployeeAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace EmployeeAccess
{
    public class DBConnection
    {
        private string connectionString = "Data Source=BVP\\SQLEXPRESS;Initial Catalog=Employee;Integrated Security=True";
        SqlConnection conn = null;

        public DBConnection()
        {
            try
            {
                conn = new SqlConnection(connectionString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public string AddEmployeeDetails(EmpDetails empDetails)
        {
            string status = "";
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand("AddEmployeeDet", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = empDetails.EmpFirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = empDetails.EmpLastName;
                cmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = empDetails.EmpMobile;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = empDetails.EmpEmail;
                cmd.Parameters.Add("@returnVal", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
                status = cmd.Parameters["@returnVal"].Value.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return status;
        }

       
        public string GetUserId(string guid, string Module)
        {
            string userId = "";
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand("GetUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userToken", SqlDbType.VarChar).Value = guid;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userId = reader["EmpCode"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return userId;
        }

        public EmpDetails GetUserDetails(string EmpMobile)
        {
            EmpDetails empDet = new EmpDetails();
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand("GetUserInfo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EmpMobile", SqlDbType.VarChar)).Value = EmpMobile;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        empDet.IsAuthorized = true;
                    }
                }
                else
                {
                    empDet.IsAuthorized = false;
                }
            }
            catch (Exception ex)
            {
                empDet.IsAuthorized = false;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return empDet;
        }
    }
}