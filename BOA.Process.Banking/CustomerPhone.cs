using BOA.Types.Banking;
using BOA.Types.Banking.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Process.Banking
{
    public class CustomerPhone
    {
        public ResponseBase PhoneAdd(CustomerPhoneRequest request)
        {
            Business.Banking.Phone customerBusiness = new Business.Banking.Phone();
            var response = customerBusiness.PhoneAdd(request);




            return response;
        }
    }
}
