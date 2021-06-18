using SoCBanking.Types.Banking;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoCBanking.Business.Banking
{
    public class Currency
    {
        public GenericResponse<List<CurrencyContract>> GetAllCurrencies (AccountRequest request)
        {
            DbOperation dbOperation = new DbOperation();
            SqlDataReader reader = dbOperation.GetData("COR.sel_GetAllCurrencies");
            List<CurrencyContract> currencyContracts = new List<CurrencyContract>();

            while (reader.Read())
            {
                currencyContracts.Add(new CurrencyContract() { 
                    Symbol = reader["symbol"].ToString(),
                    Code = reader["code"].ToString(),
                    Id = Convert.ToInt32(reader["id"]),
                    Name = reader["name"].ToString()
                });
            }

            try
            {
                return new GenericResponse<List<CurrencyContract>>() { Value = currencyContracts, IsSuccess = true };
            }
            catch
            {
                return new GenericResponse<List<CurrencyContract>>() { ErrorMessage = "GetAllCurrencies operasyonu başarısız oldu.", IsSuccess = false };
            }
        }
    }
}
