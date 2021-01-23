using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Process.Banking
{
    public class Customer
    {
        //TO-DO: Update fonksiyonu için email ve telefon numaralarına çözüm bulunacak
        public GenericResponse<List<CustomerContract>> FilterCustomersByProperties(CustomerRequest request)
        {
            Business.Banking.Customer customerBusiness = new Business.Banking.Customer();
            
            if(request.DataContract.DateOfBirth.GetValueOrDefault() < new DateTime(1753, 01, 01))
            {
                DateTime sqlRange = new DateTime(1753, 01, 01);
                request.DataContract.DateOfBirth = sqlRange;
            }
            var response = customerBusiness.FilterCustomersByProperties(request);
            return response;
        }

        public GenericResponse<List<CustomerContract>> GetAllCustomers(CustomerRequest request) 
        {
            Business.Banking.Customer customerBusiness = new Business.Banking.Customer();
            var response = customerBusiness.GetAllCustomers(request);

            return response;

        }

        public GenericResponse<CustomerContract> GetCustomerDetailsById(CustomerRequest request)
        {
            Business.Banking.Customer customerBusiness = new Business.Banking.Customer();
            var response = customerBusiness.GetCustomerDetailsById(request);

            return response;
        }

        public GenericResponse<CustomerContract> CustomerDelete(CustomerRequest request) //TODO: GenericResponse
        {
            Business.Banking.Customer customerBusiness = new Business.Banking.Customer();
            var response = customerBusiness.CustomerDelete(request);
            return response;
        }

        public GenericResponse<CustomerContract> UpdateCustomerbyId(CustomerRequest request)
        {
            Business.Banking.Customer customerBusiness = new Business.Banking.Customer();
            var response = customerBusiness.UpdateCustomerbyId(request);
            return response;
        }

        public GenericResponse<CustomerContract> CustomerAdd(CustomerRequest request)
        {
            Business.Banking.Customer customerBusiness = new Business.Banking.Customer();
           var response = customerBusiness.CustomerAdd(request);
            return response;
        }

        public GenericResponse<List<JobContract>> getAllJobs(CustomerRequest request)
        {
            Business.Banking.Job jobBusiness = new Business.Banking.Job();
            var response = jobBusiness.getAllJobs(request);

            return response;

        }

        public GenericResponse<List<EducationLevelContract>> getAllEducationLevels(CustomerRequest request)
        {
            Business.Banking.EducationLevel educationLvBusiness = new Business.Banking.EducationLevel();
            var response = educationLvBusiness.getAllEducationLevels(request);

            return response;

        }
    }
}
