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
