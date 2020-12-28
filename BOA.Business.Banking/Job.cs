using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Business.Banking
{
    public class Job
    {

        public GetAllJobsResponse getAllJobs(JobRequest jobRequest)
        {
            DbOperation dbOperation = new DbOperation();
            List<JobContract> dataContracts = new List<JobContract>();
            SqlConnection sqlConnection = new SqlConnection(dbOperation.GetConnectionString());
            SqlCommand sqlCommand = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "CUS.sel_AllJobs"
            };
            using (sqlConnection)
            {
                sqlConnection.Open();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            dataContracts.Add(new JobContract
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                JobName = reader["JobName"].ToString(),
                                JobDescription = reader["JobDescription"].ToString()
                            });

                        } 
                    }
                }
            }

            if (dataContracts.Count > 0)
            {
                return new GetAllJobsResponse { DataContract = dataContracts, IsSuccess = true };
            }

            return new GetAllJobsResponse { ErrorMessage = "GetAllJobs işlemi başarısız oldu." };

        }
    }
}
