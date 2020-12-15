using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BOA.Business.Banking;
using BOA.Types.Banking;

namespace BOA.Business.Banking
{
    public class Customer
    {
        

        public CustomerResponse CustomerSave(CustomerRequest request)
        {
            return null;
        }

       
        public GetAllCustomersResponse GetAllCustomers(GetAllCustomersRequest request) //request'in içerisinde zaten contract var bu kontrata göre filtreleme işlemleri daha sonra yapılacak
        {
            DbOperation dbOperation = new DbOperation();
            List<CustomerContract> dataContracts = new List<CustomerContract>();
            SqlConnection sqlConnection = new SqlConnection(dbOperation.GetConnectionString());
            SqlCommand sqlCommand = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "CUS.sel_AllCustomers"
            };
            using (sqlConnection)
            {
                sqlConnection.Open();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataContracts.Add(new CustomerContract
                        {
                            CustomerId = Convert.ToInt32(reader["CustomerId"]),
                            CustomerName = reader["CustomerName"].ToString(),
                            CitizenshipId = reader["CitizenshipId"].ToString()
                        });
                        
                    }
                }
            }

            if (dataContracts.Count > 0)
            {
                return new GetAllCustomersResponse { CustomersList = dataContracts, IsSuccess = true };
            }


            //DbOperation dbOperation = new DbOperation();
            //List<CustomerContract> customerListResponse = new List<CustomerContract>();
            //CustomerContract contract = new CustomerContract();
            //SqlDataReader dr = dbOperation.GetData("CUS.sel_AllCustomers");
            //DataTable dt = new DataTable();


            //using (dr)
            //{
            //    dt.Load(dr);
            //}

            //dr.Close();

            //foreach(var row in dt.Rows)
            //{
            //    contract.CustomerId = row[""]
            //}

            //if (customerListResponse.Count > 0)
            //{
            //    return new GetAllCustomersResponse { CustomersList = customerListResponse, IsSuccess = true };
            //}
            //using (var dbObject = dbOperation.GetData("CUS.sel_AllCustomers"))
            //{


            //    DataTable dt = new DataTable();
            //    dt.Load(dbObject);
            //    int rows = dt.Rows.Count;
            //    response = new GetAllCustomersResponse();
            //    for (int i = 0; i < rows; i++)
            //    {
            //        response.CustomersList.Add((CustomerContract)dbObject.GetValue(i));
            //    }

            //    response.IsSuccess = true;
            //    return response;

            //}

            return new GetAllCustomersResponse { ErrorMessage = "GetAllCustomers işlemi başarısız oldu." };

        }

        //public GenericResponse<GetAllCustomersResponse> GetAllCustomers(CustomerRequest request)
        //{

        //}





    }
}
