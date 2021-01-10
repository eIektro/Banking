using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Business.Banking
{
    public class Phone
    {
        public ResponseBase PhoneAdd(CustomerPhoneContract phoneContract)
        {
            DbOperation dbOperation = new DbOperation();

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@CustomerId",phoneContract.CustomerId),
                new SqlParameter("@PhoneNumber",phoneContract.PhoneNumber),
                new SqlParameter("@PhoneType",phoneContract.PhoneType)

            };

            try
            {
                int id = Convert.ToInt32(dbOperation.SpExecute("CUS.ins_AddNewCustomerPhone", parameters));

                return new ResponseBase { IsSuccess = true };
            }
            catch (Exception)
            {

                return new ResponseBase { IsSuccess = false, ErrorMessage = "CustomerPhoneAdd isteği başarısız." };
            }
        }
    }
}
