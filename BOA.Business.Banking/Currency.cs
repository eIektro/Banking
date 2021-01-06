using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Business.Banking
{
    public class Currency
    {
        public ResponseBase GetAllCurrencies (AccountRequest request)
        {
            DbOperation dbOperation = new DbOperation();
            SqlDataReader reader = dbOperation.GetData("COR.sel_GetAllCurrencies");
            List<CurrencyContract> currencyContracts = new List<CurrencyContract>();

            while (reader.Read())
            {
                currencyContracts.Add(new CurrencyContract() { 
                    symbol = reader["symbol"].ToString(),
                    code = reader["code"].ToString(),
                    id = Convert.ToInt32(reader["id"]),
                    name = reader["name"].ToString()
                });
            }

            try
            {
                return new ResponseBase() { DataContract = currencyContracts, IsSuccess = true };
            }
            catch
            {
                return new ResponseBase() { ErrorMessage = "GetAllCurrencies operasyonu başarısız oldu.", IsSuccess = false };
            }
        }
    }
}
