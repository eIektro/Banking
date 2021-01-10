using BOA.Types.Banking;
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
        public ResponseBase EmailAdd(CustomerEmailContract emailContract)
        {
            DbOperation dbOperation = new DbOperation();

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@CustomerId",emailContract.CustomerId),
                new SqlParameter("@EmailType",emailContract.EmailType),
                new SqlParameter("@MailAdress",emailContract.MailAdress)

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
