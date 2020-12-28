using BOA.Types.Banking;
using BOA.Types.Banking.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Process.Banking
{
    public class CustomerEmail
    {
        public ResponseBase EmailAdd(CustomerEmailRequest request)
        {
            Business.Banking.Email emailBusiness = new Business.Banking.Email();
            var response = emailBusiness.EmailAdd(request);




            return response;
        }
    }
}
