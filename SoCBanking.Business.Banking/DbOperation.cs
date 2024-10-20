using SoCBanking.Business.Banking.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SoCBanking.Business.Banking
{
    public class DbOperation
    {
        private SqlConnection conn;
        private string connectionString = @"Data Source=iissql.elektro.local;Initial Catalog=BOA;Integrated Security=False;User Id=sa;Password=sa.12345;TrustServerCertificate=true;Connect Timeout=60;Pooling=true;Max Pool Size=100;MultipleActiveResultSets=true;Application Name=BOA;";


        public string GetConnectionString()
        {
            return connectionString;
        }

        public SqlConnection ConOpen() //Aşağıdaki aynı exceptionları fırlatan satırlar için bu methodu ortak yapacağım. Şimdilik böyle dursun.
        {
            SqlConnection con = new SqlConnection(connectionString);
            return con;
        }

        public void CloseConnection()
        {
            if (conn != null)
            {
                conn.Close();
            }
        }




        public object spExecuteScalar(string spName, SqlParameter[] parameters)
        {
            if (conn == null)
            {
               conn = new SqlConnection(connectionString);
            }


            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = spName
            };
            if (parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }
            try
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {

                    throw new FalseConnectionStringException("Provided connection string is not connectable.");
                }
                object result = cmd.ExecuteScalar();
                conn.Close();
                return result;
            }


            catch (Exception)
            {
                conn.Close();
                return null;
            }


        }

        public bool SpExecute(string spName, SqlParameter[] parameters)
        {
            if (conn == null)
            {
                conn = new SqlConnection(connectionString);
            }


            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = spName
            };
            if (parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }




            try
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {

                    throw new FalseConnectionStringException("Provided connection string is not connectable.");
                }
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }

            catch (Exception)
            {
                conn.Close();
                return false;
            }
        }


        public SqlDataReader GetData(string spName, SqlParameter[] parameters=null)
        {
            if (conn == null)
            {
                conn = new SqlConnection(connectionString);
            }


            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = spName
            };
            if (parameters != null)
            {
                if (parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                } 
            }
            try
            {
                conn.Open();
            }
            catch (Exception)
            {

                throw new FalseConnectionStringException("Provided connection string is not connectable.");
            }
            try
            {
                return cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}