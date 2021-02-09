using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Process.Banking
{
    public class DepositWithdrawal
    {
        public GenericResponse<DepositWithdrawalContract> DoNewDepositWithdrawalTransfer(DepositWithdrawalRequest request)
        {
            if (request.DataContract.TransferDate.GetValueOrDefault() < new DateTime(1753, 01, 01))
            {
                DateTime sqlRange = new DateTime(1753, 01, 01);
                request.DataContract.TransferDate = sqlRange;
            }

            BOA.Business.Banking.DepositWithdrawal depositWithdrawalBusiness = new Business.Banking.DepositWithdrawal();
            var response = depositWithdrawalBusiness.DoNewDepositWithdrawalTransfer(request);
            return response;

        }

        public GenericResponse<List<DepositWithdrawalContract>> GetAllDepositWithdrawals(DepositWithdrawalRequest request)
        {
            BOA.Business.Banking.DepositWithdrawal depositWithdrawalBusiness = new Business.Banking.DepositWithdrawal();
            var response = depositWithdrawalBusiness.GetAllDepositWithdrawals(request);
            return response;
        }

        public GenericResponse<List<DepositWithdrawalContract>> FilterDepositWithdrawalsByProperties(DepositWithdrawalRequest request)
        {
            BOA.Business.Banking.DepositWithdrawal depositWithdrawalBusiness = new Business.Banking.DepositWithdrawal();
            var response = depositWithdrawalBusiness.FilterDepositWithdrawalsByProperties(request);
            return response;
        }
    }
}
