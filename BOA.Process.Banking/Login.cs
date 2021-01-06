using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Process.Banking
{
    public class Login
    {
        
            public ResponseBase UserLogin(LoginRequest request)
            {
                Business.Banking.Login loginBusiness = new Business.Banking.Login();
                var response = loginBusiness.UserLogin(request);

                return response;

            }
        
    }
}
