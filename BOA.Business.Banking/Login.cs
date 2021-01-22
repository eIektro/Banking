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
        public GenericResponse<LoginContract> UserLogin(LoginRequest request)
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
                return new GenericResponse<LoginContract>() { Value = loginContract, IsSuccess = true };
            }
            catch
            {
                return new GenericResponse<LoginContract>() { ErrorMessage = "Başarısız login denemesi", IsSuccess = false };
            }

            

        }

        public GenericResponse<List<LoginContract>> GetAllUsers(LoginRequest request)
        {
            DbOperation dbOperation = new DbOperation();
            SqlDataReader reader = dbOperation.GetData("COR.sel_GetAllUsers");
            try
            {
                List<LoginContract> users = new List<LoginContract>();
                while (reader.Read())
                {
                    users.Add(new LoginContract()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        LoginName = reader["UserName"].ToString()
                    });
                }
                return new GenericResponse<List<LoginContract>>() { Value = users, IsSuccess = true };
            }
            catch (Exception)
            {

                return new GenericResponse<List<LoginContract>>() { IsSuccess = false, ErrorMessage = "GetAllUsers operasyonu başarısız." };
            }
        }

    }
}
