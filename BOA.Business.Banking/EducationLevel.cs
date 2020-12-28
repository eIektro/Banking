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
    public class EducationLevel
    {
        public GetAllEducationLevelsResponse getAllEducationLevels(EducationLevelRequest jobRequest)
        {
            DbOperation dbOperation = new DbOperation();
            List<EducationLevelContract> dataContracts = new List<EducationLevelContract>();
            SqlConnection sqlConnection = new SqlConnection(dbOperation.GetConnectionString());
            SqlCommand sqlCommand = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "CUS.sel_AllEducationLevels"
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
                            dataContracts.Add(new EducationLevelContract
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                EducationLevel = reader["EducationLevel"].ToString(),
                                EducationLevelDescription = reader["EducationLevelDescription"].ToString()
                            });

                        }
                    }
                }
            }

            if (dataContracts.Count > 0)
            {
                return new GetAllEducationLevelsResponse { DataContract = dataContracts, IsSuccess = true };
            }

            return new GetAllEducationLevelsResponse { ErrorMessage = "GetAllEducationLevels işlemi başarısız oldu." };

        }
    }
}
