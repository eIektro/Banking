﻿using BOA.Types.Banking;
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

        public ResponseBase getAllJobs(JobRequest jobRequest)
        {
            DbOperation dbOperation = new DbOperation();
            List<JobContract> dataContracts = new List<JobContract>();
            SqlDataReader reader = dbOperation.GetData("CUS.sel_AllJobs");


            while (reader.Read())
            {
                dataContracts.Add(new JobContract
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    JobName = reader["JobName"].ToString(),
                    JobDescription = reader["JobDescription"].ToString()
                });
            }

            if (dataContracts.Count > 0)
            {
                return new ResponseBase { DataContract = dataContracts, IsSuccess = true };
            }

            return new ResponseBase { ErrorMessage = "GetAllJobs işlemi başarısız oldu." };

        }
    }
}