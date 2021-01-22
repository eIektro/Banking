using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Business.Banking
{
    public class City
    {
        public GenericResponse<List<CityContract>> getAllCities()
        {
            DbOperation dbOperation = new DbOperation();
            List<CityContract> cityContracts = new List<CityContract>();
            SqlDataReader reader = dbOperation.GetData("COR.sel_AllCities");
            while (reader.Read())
            {
                cityContracts.Add(new CityContract() { 
                    Id = Convert.ToInt32(reader["id"]),
                    Code = reader["code"].ToString(),
                    Name = reader["name"].ToString()
                    
                });
            }
            if (cityContracts.Count > 0)
            {
                return new GenericResponse<List<CityContract>>() { Value = cityContracts,IsSuccess=true};
            }
            else
            {
                return new GenericResponse<List<CityContract>>() { ErrorMessage = "getAllCities işlemi başarısız", IsSuccess = false };
            }
        }
    }
}
