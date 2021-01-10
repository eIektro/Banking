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

        public ResponseBase FilterCustomersByProperties(CustomerRequest request)
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

        public ResponseBase GetAllCustomers(CustomerRequest request) /* TO-DO: Connect sınıfından gelen GetAllCustomersRequest'ten buraya yönlendirme,
                                                                                        * connect sınıfındaki parse mantığından dolayı düzgün yapılamıyor. Düzeltilecek.
                                                                                        * (Linq ile contains e bakılıp yapılabilir.)
                                                                                        */
        {
            Business.Banking.Customer customerBusiness = new Business.Banking.Customer();
            var response = customerBusiness.GetAllCustomers(request);

            return response;

        }

        public ResponseBase CustomerDelete(CustomerRequest request)
        {
            Business.Banking.Customer customerBusiness = new Business.Banking.Customer();
            var response = customerBusiness.CustomerDelete(request);
            return response;
        }

        public ResponseBase UpdateCustomerbyId(CustomerRequest request)
        {
            Business.Banking.Customer customerBusiness = new Business.Banking.Customer();
            var response = customerBusiness.UpdateCustomerbyId(request);
            return response;
        }

        public ResponseBase CustomerAdd(CustomerRequest request)
        {
            Business.Banking.Customer customerBusiness = new Business.Banking.Customer();
           var response = customerBusiness.CustomerAdd(request);
            return response;
        }

        public ResponseBase getAllJobs(CustomerRequest request)
        {
            Business.Banking.Job jobBusiness = new Business.Banking.Job();
            var response = jobBusiness.getAllJobs(request);

            return response;

        }

        public ResponseBase getAllEducationLevels(CustomerRequest request)
        {
            Business.Banking.EducationLevel educationLvBusiness = new Business.Banking.EducationLevel();
            var response = educationLvBusiness.getAllEducationLevels(request);

            return response;

        }
    }
}
