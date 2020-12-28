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
    public class Email
    {
        public ResponseBase EmailAdd(CustomerEmailRequest request)
        {
            DbOperation dbOperation = new DbOperation();

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@CustomerId",request.DataContract.CustomerId),
                new SqlParameter("@EmailType",request.DataContract.EmailType),
                new SqlParameter("@MailAdress",request.DataContract.MailAdress)

            };

            try
            {
                int id = Convert.ToInt32(dbOperation.SpExecute("CUS.ins_AddNewCustomerEmail", parameters));

                return new ResponseBase { IsSuccess = true };
            }
            catch (Exception)
            {

                return new ResponseBase { IsSuccess = false, ErrorMessage = "CustomerEmailAdd isteği başarısız." };
            }
        }
    }
}
