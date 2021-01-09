using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Process.Banking
{
    public class Account
    {
        public ResponseBase GetAllCurrencies(AccountRequest request)
        {
            BOA.Business.Banking.Currency currencyBusiness = new Business.Banking.Currency();
            var response = currencyBusiness.GetAllCurrencies(request);
            return response;
        }

        public ResponseBase FilterAccountsByProperties(AccountRequest request)
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


            BOA.Business.Banking.Account accountBusiness = new Business.Banking.Account();
            var response = accountBusiness.FilterAccountsByProperties(request);
            return response;
        }

        public ResponseBase AddNewAccount(AccountRequest request)
        {
            BOA.Business.Banking.Account accountBusiness = new Business.Banking.Account();
            var response = accountBusiness.AddNewAccount(request);
            return response;

        }

        public ResponseBase GetAllAccounts(AccountRequest request)
        {
            BOA.Business.Banking.Account accountBusiness = new Business.Banking.Account();
            var response = accountBusiness.GetAllAccounts(request);
            return response;
        }

        public ResponseBase UpdateAccountDetailsById(AccountRequest request)
        {
            BOA.Business.Banking.Account accountBusiness = new Business.Banking.Account();
            var response = accountBusiness.UpdateAccountDetailsById(request);
            return response;
            
        }

    }
}
