using SoCBanking.Types.Banking;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoCBanking.Business.Banking
{
    public class Email
    {
        public GenericResponse<CustomerEmailContract> EmailAdd(CustomerEmailContract emailContract)
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

                return new GenericResponse<CustomerEmailContract>() { IsSuccess = true,Value = new CustomerEmailContract() { CustomerMailId = id } };
            }
            catch (Exception)
            {

                return new GenericResponse<CustomerEmailContract>() { IsSuccess = false, ErrorMessage = "CustomerEmailAdd isteği başarısız." };
            }
        }
    }
}
