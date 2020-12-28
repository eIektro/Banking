using BOA.Types.Banking;
using BOA.Types.Banking.Customer;
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
        public ResponseBase PhoneAdd(CustomerPhoneRequest request)
        {
            DbOperation dbOperation = new DbOperation();

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@CustomerId",request.DataContract.CustomerId),
                new SqlParameter("@PhoneNumber",request.DataContract.PhoneNumber),
                new SqlParameter("@PhoneType",request.DataContract.PhoneType)

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
