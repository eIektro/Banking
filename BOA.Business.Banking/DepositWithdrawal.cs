using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Business.Banking
{
    public class DepositWithdrawal
    {
        public GenericResponse<DepositWithdrawalContract> DoNewDepositWithdrawalTransfer(DepositWithdrawalRequest request)
        {
            DbOperation dbOperation = new DbOperation();
            SqlParameter[] sqlParameters = new SqlParameter[] {
                new SqlParameter("@TransferType",request.DataContract.TransferType),
                new SqlParameter("@TransferBranchId",request.DataContract.TransferBranchId),
                new SqlParameter("@AccountNumber",request.DataContract.AccountNumber),
                new SqlParameter("@AccountSuffix",request.DataContract.AccountSuffix),
                new SqlParameter("@TransferDate",request.DataContract.TransferDate),
                new SqlParameter("@TransferAmount",request.DataContract.TransferAmount),
                new SqlParameter("@TransferDescription",request.DataContract.TransferDescription),
                new SqlParameter("@FormedUserId",request.DataContract.FormedUserId),
                new SqlParameter("@CurrencyId",request.DataContract.CurrencyId),
            };

            try
            {
                var response = dbOperation.spExecuteScalar("TRN.ins_AddNewDepositWithdrawal", sqlParameters);
                
                return new GenericResponse<DepositWithdrawalContract>() { IsSuccess = true, Value = new DepositWithdrawalContract() };
            }
            catch (Exception)
            {
                return new GenericResponse<DepositWithdrawalContract>() { IsSuccess = false, ErrorMessage = "DoNewRemittanceTransaction operasyonu başarısız!", Value = null };
                throw;
            }



        }
    }
}
