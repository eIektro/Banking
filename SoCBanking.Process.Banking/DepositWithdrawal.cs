using SoCBanking.Types.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoCBanking.Process.Banking
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

            SoCBanking.Business.Banking.DepositWithdrawal depositWithdrawalBusiness = new Business.Banking.DepositWithdrawal();
            var response = depositWithdrawalBusiness.DoNewDepositWithdrawalTransfer(request);
            return response;

        }

        public GenericResponse<List<DepositWithdrawalContract>> GetAllDepositWithdrawals(DepositWithdrawalRequest request)
        {
            SoCBanking.Business.Banking.DepositWithdrawal depositWithdrawalBusiness = new Business.Banking.DepositWithdrawal();
            var response = depositWithdrawalBusiness.GetAllDepositWithdrawals(request);
            return response;
        }

        public GenericResponse<List<DepositWithdrawalContract>> FilterDepositWithdrawalsByProperties(DepositWithdrawalRequest request)
        {
            SoCBanking.Business.Banking.DepositWithdrawal depositWithdrawalBusiness = new Business.Banking.DepositWithdrawal();
            var response = depositWithdrawalBusiness.FilterDepositWithdrawalsByProperties(request);
            return response;
        }
    }
}
