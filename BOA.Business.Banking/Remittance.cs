using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Business.Banking
{
    public class Remittance
    {
        public GenericResponse<RemittanceContract> DoNewRemittanceTransaction(RemittanceRequest request)
        {
            DbOperation dbOperation = new DbOperation();
            SqlParameter[] sqlParameters = new SqlParameter[] {
                new SqlParameter("@WithdrawalAccountNumber",request.DataContract.WithdrawalAccountNumber),
                new SqlParameter("@DepositAccountNumber",request.DataContract.DepositAccountNumber),
                new SqlParameter("@TransferAmount",request.DataContract.TransferAmount),
                new SqlParameter("@TransactionDate",request.DataContract.TransactionDate),
                new SqlParameter("@TransactionStatus",request.DataContract.TransactionStatus),
                new SqlParameter("@TransactionDescription",request.DataContract.TransactionDescription),
            };

            try
            {
                var response = dbOperation.spExecuteScalar("CUS.ins_DoNewRemittance", sqlParameters);
                return new GenericResponse<RemittanceContract>() { IsSuccess = true, Value = new RemittanceContract() };
            }
            catch (Exception)
            {
                return new GenericResponse<RemittanceContract>() { IsSuccess = false, ErrorMessage = "DoNewRemittanceTransaction operasyonu başarısız!" };
                throw;
            }



        }
    }
}
