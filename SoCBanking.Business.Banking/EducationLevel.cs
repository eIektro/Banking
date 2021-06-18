using SoCBanking.Types.Banking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoCBanking.Business.Banking
{
    public class EducationLevel
    {
        public GenericResponse<List<EducationLevelContract>> getAllEducationLevels(CustomerRequest request)
        {
            DbOperation dbOperation = new DbOperation();
            List<EducationLevelContract> dataContracts = new List<EducationLevelContract>();
            SqlDataReader reader = dbOperation.GetData("CUS.sel_AllEducationLevels");

            while (reader.Read())
            {
                dataContracts.Add(new EducationLevelContract
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    EducationLevel = reader["EducationLevel"].ToString(),
                    EducationLevelDescription = reader["EducationLevelDescription"].ToString()
                });
            }
            
            if (dataContracts.Count > 0)
            {
                return new GenericResponse<List<EducationLevelContract>>() { Value = dataContracts, IsSuccess = true };
            }

            return new GenericResponse<List<EducationLevelContract>>() { ErrorMessage = "GetAllEducationLevels işlemi başarısız oldu.", IsSuccess = false };

        }
    }
}
