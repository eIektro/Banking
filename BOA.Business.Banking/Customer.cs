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
using BOA.Types.Banking.Customer;

namespace BOA.Business.Banking
{
    public class Customer
    {
        

        public CustomerResponse CustomerAdd(CustomerRequest request)
        {
            DbOperation dbOperation = new DbOperation();

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@CustomerName",request.DataContract.CustomerName),
                new SqlParameter("@CustomerLastName",request.DataContract.CustomerLastName),
                new SqlParameter("@CitizenshipId",request.DataContract.CitizenshipId),
                new SqlParameter("@MotherName",request.DataContract.MotherName),
                new SqlParameter("@FatherName",request.DataContract.FatherName),
                new SqlParameter("@PlaceOfBirth",request.DataContract.PlaceOfBirth),
                new SqlParameter("@DateOfBirth",request.DataContract.DateOfBirth),
                new SqlParameter("@JobId",request.DataContract.JobId),
                new SqlParameter("@EducationLvId",request.DataContract.EducationLvId)
                
            };

            try
            {
                int id = Convert.ToInt32(dbOperation.spExecuteScalar("CUS.ins_AddNewCustomer", parameters));

                return new CustomerResponse { DataContract = new CustomerContract { CustomerId = id }, IsSuccess = true };
            }
            catch (Exception)
            {

                return new CustomerResponse { IsSuccess = false,ErrorMessage = "CustomerAdd isteği başarısız."};
            }
           

        }

        public CustomerResponse CustomerDelete(CustomerDeleteRequest request)
        {
            DbOperation dbOperation = new DbOperation();

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@CustomerId",request.DataContract.CustomerName),               
            };

            try
            {
                var response = dbOperation.SpExecute("CUS.del_DeleteCustomerbyId", parameters);

                return new CustomerResponse { IsSuccess = true };
            }
            catch (Exception)
            {

                return new CustomerResponse { IsSuccess = false, ErrorMessage = "CustomerAdd isteği başarısız." };
            }
        }

        public GetAllCustomersResponse GetAllCustomers(CustomerRequest request) 
        {
            DbOperation dbOperation = new DbOperation();
            List<CustomerContract> dataContracts = new List<CustomerContract>();
            SqlDataReader reader = dbOperation.GetData("CUS.sel_AllCustomers");
            while (reader.Read())
            {
                dataContracts.Add(new CustomerContract
                {
                    CustomerId = Convert.ToInt32(reader["CustomerId"]),
                    CustomerName = reader["CustomerName"].ToString(),
                    CustomerLastName = reader["CustomerLastName"].ToString(),
                    CitizenshipId = reader["CitizenshipId"].ToString(),
                    MotherName = reader["MotherName"].ToString(),
                    FatherName = reader["FatherName"].ToString(),
                    PlaceOfBirth = reader["PlaceOfBirth"].ToString(),
                    JobId = (int)reader["JobId"],
                    EducationLvId = (int)reader["EducationLvId"],
                    DateOfBirth = (DateTime)reader["DateOfBirth"],
                });
            }


        //     public int? CustomerId { get; set; }

        //public string CustomerName { get; set; }

        //public string CustomerLastName { get; set; }

        //public string CitizenshipId { get; set; }

        //public string MotherName { get; set; }

        //public string FatherName { get; set; }

        //public string PlaceOfBirth { get; set; }

        //public int JobId { get; set; }

        //public int EducationLvId { get; set; }

        //public DateTime DateOfBirth { get; set; }

        //public List<CustomerPhoneContract> PhoneNumbers { get; set; }

        //public List<CustomerEmailContract> Emails { get; set; }


            //SqlConnection sqlConnection = new SqlConnection(dbOperation.GetConnectionString());
            //SqlCommand sqlCommand = new SqlCommand
            //{
            //    Connection = sqlConnection,
            //    CommandType = CommandType.StoredProcedure,
            //    CommandText = "CUS.sel_AllCustomers"
            //};
            //using (sqlConnection)
            //{
            //    sqlConnection.Open();
            //    using (SqlDataReader reader = sqlCommand.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
                        //dataContracts.Add(new CustomerContract
                        //{
                        //    CustomerId = Convert.ToInt32(reader["CustomerId"]),
                        //    CustomerName = reader["CustomerName"].ToString(),
                        //    CitizenshipId = reader["CitizenshipId"].ToString()
                        //});
                        
            //        }
            //    }
            //}

            if (dataContracts.Count > 0)
            {
                return new GetAllCustomersResponse { CustomersList = dataContracts, IsSuccess = true };
            }

            return new GetAllCustomersResponse { ErrorMessage = "GetAllCustomers işlemi başarısız oldu." };

        }

    }
}
