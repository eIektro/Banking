using SoCBanking.Types.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoCBanking.Process.Banking
{
    public class Account
    {
        public GenericResponse<List<CurrencyContract>> GetAllCurrencies(AccountRequest request)
        {
            SoCBanking.Business.Banking.Currency currencyBusiness = new Business.Banking.Currency();
            var response = currencyBusiness.GetAllCurrencies(request);
            return response;
        }

        public GenericResponse<List<AccountContract>> FilterAccountsByProperties(AccountRequest request)
        {
            if (request.DataContract.DateOfDeactivation.GetValueOrDefault() < new DateTime(1753, 01, 01))
            {
                DateTime sqlRange = new DateTime(1753, 01, 01);
                request.DataContract.DateOfDeactivation = sqlRange;
            }
            if (request.DataContract.DateOfFormation.GetValueOrDefault() < new DateTime(1753, 01, 01))
            {
                DateTime sqlRange = new DateTime(1753, 01, 01);
                request.DataContract.DateOfFormation = sqlRange;
            }
            if (request.DataContract.DateOfLastTrasaction.GetValueOrDefault() < new DateTime(1753, 01, 01))
            {
                DateTime sqlRange = new DateTime(1753, 01, 01);
                request.DataContract.DateOfLastTrasaction = sqlRange;
            }
            

            SoCBanking.Business.Banking.Account accountBusiness = new Business.Banking.Account();
            var response = accountBusiness.FilterAccountsByProperties(request);
            return response;
        }

        public GenericResponse<AccountContract> AddNewAccount(AccountRequest request)
        {
            SoCBanking.Business.Banking.Account accountBusiness = new Business.Banking.Account();
            var response = accountBusiness.AddNewAccount(request);
            return response;

        }

        public GenericResponse<List<AccountContract>> GetAllAccounts(AccountRequest request)
        {
            SoCBanking.Business.Banking.Account accountBusiness = new Business.Banking.Account();
            var response = accountBusiness.GetAllAccounts(request);
            return response;
        }

        public GenericResponse<AccountContract> UpdateAccountDetailsById(AccountRequest request)
        {
            SoCBanking.Business.Banking.Account accountBusiness = new Business.Banking.Account();
            var response = accountBusiness.UpdateAccountDetailsById(request);
            return response;
            
        }

    }
}
