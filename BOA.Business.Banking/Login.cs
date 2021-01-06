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
        public ResponseBase UserLogin(LoginRequest request)
        {
            DbOperation dbOperation = new DbOperation();
            LoginContract loginContract = new LoginContract();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@UserName",request.DataContract.LoginName),
                new SqlParameter("@Password",request.DataContract.Password)
            };

            SqlDataReader reader = dbOperation.GetData("COR.sel_UserLogin", parameters);

            while (reader.Read())
            {
                loginContract.Id = Convert.ToInt32(reader["Id"]);
                loginContract.LoginName = Convert.ToString(reader["UserName"]);
            }

            try
            {
                return new ResponseBase { DataContract = loginContract, IsSuccess = true };
            }
            catch
            {
                return new ResponseBase { ErrorMessage = "Başarısız login denemesi", IsSuccess = false };
            }

            

        }
    }
}
