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
        public GenericResponse<CustomerPhoneContract> PhoneAdd(CustomerPhoneContract phoneContract)
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

                return new GenericResponse<CustomerPhoneContract>() { IsSuccess = true, Value = new CustomerPhoneContract() { CustomerPhoneId = id} };
            }
            catch (Exception)
            {

                return new GenericResponse<CustomerPhoneContract>() { IsSuccess = false, ErrorMessage = "CustomerPhoneAdd isteği başarısız." };
            }
        }
    }
}
