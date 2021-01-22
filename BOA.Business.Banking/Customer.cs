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

        public GenericResponse<List<CustomerContract>> FilterCustomersByProperties(CustomerRequest request) {
            DbOperation dbOperation = new DbOperation();
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CustomerId", request.DataContract.CustomerId),
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
                List<CustomerContract> customerContracts = new List<CustomerContract>();
                SqlDataReader reader = dbOperation.GetData("CUS.sel_FilterCustomerByProperties", parameters);
                while (reader.Read())
                {
                    customerContracts.Add(new CustomerContract()
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
                        EducationLevelName = reader["EducationLevel"].ToString(),
                        JobName = reader["JobName"].ToString(),
                        PhoneNumbers = GetCustomerPhonesByCustomerId(Convert.ToInt32(reader["CustomerId"])),
                        Emails = GetCustomerEmailsByCustomerId(Convert.ToInt32(reader["CustomerId"]))


                    });
                }
                
                return new GenericResponse<List<CustomerContract>>() { IsSuccess = true, Value = customerContracts};
            }
            catch
            {
                return new GenericResponse<List<CustomerContract>>() { IsSuccess = false, ErrorMessage = "FilterCustomersByProperties operasyonu başarısız." };
            }
        }
        public GenericResponse<CustomerContract> CustomerAdd(CustomerRequest request)
        {
            Business.Banking.Email emailBusiness = new Email();
            Business.Banking.Phone phoneBusiness = new Phone();
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
                if (request.DataContract.Emails != null)
                {
                    try
                    {
                        foreach (CustomerEmailContract email in request.DataContract.Emails)
                        {
                            emailBusiness.EmailAdd(new CustomerEmailContract() { CustomerId = id, EmailType = email.EmailType, MailAdress = email.MailAdress });
                        }

                    }
                    catch (Exception)
                    {
                        return new GenericResponse<CustomerContract>() { IsSuccess = false, ErrorMessage = "EmailAdd operasyonu başarısız!" };
                    }
                }

                if (request.DataContract.PhoneNumbers != null)
                {
                    try
                    {
                        foreach (CustomerPhoneContract phone in request.DataContract.PhoneNumbers)
                        {
                            phoneBusiness.PhoneAdd(new CustomerPhoneContract() { CustomerId = id, PhoneType = phone.PhoneType, PhoneNumber = phone.PhoneNumber });
                        }
                    
                    }
                    catch (Exception)
                    {
                        return new GenericResponse<CustomerContract>() { IsSuccess = false, ErrorMessage = "PhoneNumberAdd operasyonu başarısız!" };
                    }
                }
                
                return new GenericResponse<CustomerContract>() { Value = new CustomerContract { CustomerId = id }, IsSuccess = true };
            }
            catch (Exception)
            {

                return new GenericResponse<CustomerContract>() { IsSuccess = false,ErrorMessage = "CustomerAdd isteği başarısız."};
            }
           

        }

        public GenericResponse<CustomerContract> UpdateCustomerbyId(CustomerRequest request)
        {
            DbOperation dbOperation = new DbOperation();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("CustomerId",request.DataContract.CustomerId),
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
                var response = dbOperation.spExecuteScalar("CUS.upd_UpdateCustomerbyId", parameters);
                //TO-DO: DataContract'taki telefon numaraları ve email adresleri için işlem yapılmıyor. Eklenecek.
                return new GenericResponse<CustomerContract>() { IsSuccess = true };
            }
            catch (Exception e)
            {
                
                return new GenericResponse<CustomerContract>() { IsSuccess = false, ErrorMessage = "UpdateCustomerbyId isteği başarısız oldu." };
            }
        }

        public GenericResponse<CustomerContract> CustomerDelete(CustomerRequest request)
        {
            DbOperation dbOperation = new DbOperation();

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@CustomerId",request.DataContract.CustomerId),               
            };

            try
            {
                var response = dbOperation.SpExecute("CUS.del_DeleteCustomerbyId", parameters);

                return new GenericResponse<CustomerContract>() { IsSuccess = true };
            }
            catch (Exception)
            {

                return new GenericResponse<CustomerContract>() { IsSuccess = false, ErrorMessage = "CustomerDelete isteği başarısız." };
            }
        }

        public GenericResponse<List<CustomerContract>> GetAllCustomers(CustomerRequest request) 
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
                    EducationLevelName = reader["EducationLevel"].ToString(),
                    JobName = reader["JobName"].ToString(),
                    PhoneNumbers = GetCustomerPhonesByCustomerId(Convert.ToInt32(reader["CustomerId"])), //Bunda sakınca var mı? Sor
                    Emails = GetCustomerEmailsByCustomerId(Convert.ToInt32(reader["CustomerId"]))
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
                return new GenericResponse<List<CustomerContract>>() { Value = dataContracts, IsSuccess = true };
            }

            return new GenericResponse<List<CustomerContract>>() { ErrorMessage = "GetAllCustomers işlemi başarısız oldu.",IsSuccess=false };

        }

        public List<CustomerPhoneContract> GetCustomerPhonesByCustomerId(int customerId) //TODO: Buraları düzenle, email ile birlikte
        {
            DbOperation dbOperation = new DbOperation();
            List<CustomerPhoneContract> customerPhones = new List<CustomerPhoneContract>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("CustomerId",customerId)
            };

            SqlDataReader reader = dbOperation.GetData("CUS.sel_CustomerPhonesByCustomerId",parameters);
            while (reader.Read())
            {
                customerPhones.Add(new CustomerPhoneContract
                {
                    PhoneType = (int)reader["PhoneType"],
                    CustomerId = (int?)reader["CustomerId"],
                    PhoneNumber = reader["PhoneNumber"].ToString(),
                    CustomerPhoneId = (int?)reader["CustomerPhoneId"]
                });
            }

            return customerPhones;
        }

        public List<CustomerEmailContract> GetCustomerEmailsByCustomerId(int customerId)
        {
            DbOperation dbOperation = new DbOperation();
            List<CustomerEmailContract> customerEmails = new List<CustomerEmailContract>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("CustomerId",customerId)
            };

            SqlDataReader reader = dbOperation.GetData("CUS.sel_CustomerEmailsByCustomerId", parameters);
            while (reader.Read())
            {
                customerEmails.Add(new CustomerEmailContract
                {
                    EmailType = (int)reader["EmailType"],
                    CustomerId = (int?)reader["CustomerId"],
                    MailAdress = reader["MailAdress"].ToString(),
                    CustomerMailId = (int?)reader["CustomerMailId"]
                });
            }

            return customerEmails;
        }

    }
}
