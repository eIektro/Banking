using SoCBanking.Types.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoCBanking.Process.Banking
{
    public class Login
    {
        
        public GenericResponse<LoginContract> UserLogin(LoginRequest request)
        {
            Business.Banking.Login loginBusiness = new Business.Banking.Login();
            var response = loginBusiness.UserLogin(request);

            return response;

        }
            
        public GenericResponse<List<LoginContract>> GetAllUsers(LoginRequest request)
        {
            Business.Banking.Login loginBusiness = new Business.Banking.Login();
            var response = loginBusiness.GetAllUsers(request);
            return response;
        }

        
    }
}
