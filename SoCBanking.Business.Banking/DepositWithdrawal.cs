using SoCBanking.Types.Banking;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoCBanking.Business.Banking
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

        public GenericResponse<List<DepositWithdrawalContract>> GetAllDepositWithdrawals(DepositWithdrawalRequest request)
        {
            DbOperation dbOperation = new DbOperation();

            try
            {
                List<DepositWithdrawalContract> depositWithdrawalsList = new List<DepositWithdrawalContract>();
                SqlDataReader reader = dbOperation.GetData("TRN.sel_GelAllDepositWithDrawals");
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        depositWithdrawalsList.Add(new DepositWithdrawalContract()
                        {
                            Id = (int)reader["Id"],
                            AccountNumber = reader["AccountNumber"].ToString(),
                            AccountSuffix = reader["AccountSuffix"].ToString(),
                            FormedUserId = (int?)reader["FormedUserId"],
                            TransferBranchId = (int?)reader["TransferBranchId"],
                            TransferDate = (DateTime?)reader["TransferDate"],
                            TransferDescription = reader["TransferDescription"].ToString(),
                            TransferType = (int?)reader["TransferType"],
                            CurrencyId = (int?)reader["CurrencyId"],
                            TransferAmount = (decimal)reader["TransferAmount"],
                            CurrencyCode = reader["CurrencyCode"].ToString(),
                            UserName = reader["UserName"].ToString(),
                            BranchName = reader["BranchName"].ToString()
                           
                        });
                    }

                    return new GenericResponse<List<DepositWithdrawalContract>>() { IsSuccess = true, Value = depositWithdrawalsList };
                }

                return new GenericResponse<List<DepositWithdrawalContract>>() { IsSuccess = false, ErrorMessage = "Herhangi bir havale kaydı bulunamadı.", Value = null };
            }
            catch (Exception ex)
            {
                return new GenericResponse<List<DepositWithdrawalContract>>() { Value = null, IsSuccess = false, ErrorMessage = "GetAllDepositWithdrawals işlemi başarısız!" };
                throw ex;
            }
        }

        public GenericResponse<List<DepositWithdrawalContract>> FilterDepositWithdrawalsByProperties(DepositWithdrawalRequest request)
        {
            DbOperation dbOperation = new DbOperation();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@StartingDate",request.DataContract.StartingDate),
                new SqlParameter("@TransferBranchId",request.DataContract.TransferBranchId),
                new SqlParameter("@EndingDate",request.DataContract.EndingDate),
                new SqlParameter("@CurrencyId",request.DataContract.CurrencyId),
                new SqlParameter("@AccountNumber",request.DataContract.AccountNumber),
                new SqlParameter("@AccountSuffix",request.DataContract.AccountSuffix),
                new SqlParameter("@StartingAmount",request.DataContract.StartingAmount),
                new SqlParameter("@EndingAmount",request.DataContract.EndingAmount),
                new SqlParameter("@FormedUserId",request.DataContract.FormedUserId),
                new SqlParameter("@Id",request.DataContract.Id),
                new SqlParameter("@TransferDate",request.DataContract.TransferDate),
                new SqlParameter("@TransferDescription",request.DataContract.TransferDescription),
                new SqlParameter("@TransferType",request.DataContract.TransferType)
                
            };
            try
            {
                List<DepositWithdrawalContract> depositWithdrawalsList = new List<DepositWithdrawalContract>();
                SqlDataReader reader = dbOperation.GetData("TRN.sel_FilterDepositWithdrawalsByProperties", parameters);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        depositWithdrawalsList.Add(new DepositWithdrawalContract()
                        {
                            Id = (int)reader["Id"],
                            AccountNumber = reader["AccountNumber"].ToString(),
                            AccountSuffix = reader["AccountSuffix"].ToString(),
                            FormedUserId = (int?)reader["FormedUserId"],
                            TransferBranchId = (int?)reader["TransferBranchId"],
                            TransferDate = (DateTime?)reader["TransferDate"],
                            TransferDescription = reader["TransferDescription"].ToString(),
                            TransferType = (int?)reader["TransferType"],
                            CurrencyId = (int?)reader["CurrencyId"],
                            TransferAmount = (decimal)reader["TransferAmount"],
                            CurrencyCode = reader["CurrencyCode"].ToString(),
                            UserName = reader["UserName"].ToString(),
                            BranchName = reader["BranchName"].ToString()

                        });
                    }

                    return new GenericResponse<List<DepositWithdrawalContract>>() { IsSuccess = true, Value = depositWithdrawalsList };
                }
                return new GenericResponse<List<DepositWithdrawalContract>>() { IsSuccess = false, ErrorMessage = "Herhangi bir havale kaydı bulunamadı.", Value = null };
            }

            catch (Exception ex)
            {
                return new GenericResponse<List<DepositWithdrawalContract>> { ErrorMessage = "FilterDepositWithdrawalsByProperties işlemi başarısız!", IsSuccess = false, Value = null };
                throw ex;
            }
        }
    }
}
