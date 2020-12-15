using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Business.Banking
{
    public class Login
    {
        public LoginResponse UserLogin(LoginRequest request)
        {
            DbOperation dbOperation = new DbOperation();
            
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@UserName",request.DataContract.LoginName),
                new SqlParameter("@Password",request.DataContract.Password)
            };


            using (SqlDataReader dbObject = dbOperation.GetData("COR.sel_UserLogin", parameters))
            {
                if (dbObject.HasRows == true)
                {
                    
                    while (dbObject.Read())
                    {

                        return new LoginResponse { isLoggedIn = true, IsSuccess = true, UserName = dbObject["UserName"].ToString() }; //Bu şekilde daha anlaşılır olabilir: string userName = dbObject["UserName"].ToString();
                    }
                }
            }

            return new LoginResponse { isLoggedIn = false, IsSuccess = false, ErrorMessage = "Başarısız login denemesi" };


            //BOA.Business.Banking.Login loginBusiness = new Business.Banking.Login();
            //loginBusiness.UserLogin(request.DataContract);

            //return null;

        }
    }
}
