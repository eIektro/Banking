using SoCBanking.Types.Banking;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoCBanking.Business.Banking
{
    public class Remittance
    {
        public GenericResponse<RemittanceContract> DoNewRemittanceTransaction(RemittanceRequest request)
        {
            DbOperation dbOperation = new DbOperation();
            SqlParameter[] sqlParameters = new SqlParameter[] {
                new SqlParameter("@SenderAccountNumber",request.DataContract.SenderAccountNumber),
                new SqlParameter("@SenderAccountSuffix",request.DataContract.SenderAccountSuffix),
                new SqlParameter("@ReceiverAccountNumber",request.DataContract.ReceiverAccountNumber),
                new SqlParameter("@ReceiverAccountSuffix",request.DataContract.ReceiverAccountSuffix),
                new SqlParameter("@TransferAmount",request.DataContract.TransferAmount),
                new SqlParameter("@TransactionDate",request.DataContract.TransactionDate),
                new SqlParameter("@TransactionStatus",request.DataContract.TransactionStatus),
                new SqlParameter("@TransactionDescription",request.DataContract.TransactionDescription),
            };

            try
            {
                var response = dbOperation.spExecuteScalar("TRN.ins_DoNewRemittance", sqlParameters);
                return new GenericResponse<RemittanceContract>() { IsSuccess = true, Value = new RemittanceContract() };
            }
            catch (Exception)
            {
                return new GenericResponse<RemittanceContract>() { IsSuccess = false, ErrorMessage = "DoNewRemittanceTransaction operasyonu başarısız!", Value = null };
                throw;
            }



        }

        public GenericResponse<List<RemittanceContract>> GetAllRemittances(RemittanceRequest request)
        {
            DbOperation dbOperation = new DbOperation();

            try
            {
                List<RemittanceContract> remittancesList = new List<RemittanceContract>();
                SqlDataReader reader = dbOperation.GetData("TRN.sel_GelAllRemittances");
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        remittancesList.Add(new RemittanceContract()
                        {
                            Id = (int)reader["Id"],
                            ReceiverAccountNumber = reader["ReceiverAccountNumber"].ToString(),
                            ReceiverAccountSuffix = reader["ReceiverAccountSuffix"].ToString(),
                            SenderAccountNumber = reader["SenderAccountNumber"].ToString(),
                            SenderAccountSuffix = reader["SenderAccountSuffix"].ToString(),
                            TransactionStatus = (int?)reader["TransactionStatus"],
                            TransactionDate = (DateTime?)reader["TransactionDate"],
                            TransactionDescription = reader["TransactionDescription"].ToString(),
                            TransferAmount = (decimal?)reader["TransferAmount"],
                            ReceiverBranchName = reader["ReceiverBranchName"].ToString(),
                            SenderBranchName = reader["SenderBranchName"].ToString(),
                            SenderLastName = reader["SenderLastName"].ToString(),
                            SenderName = reader["SenderName"].ToString(),
                            ReceiverLastName = reader["ReceiverLastName"].ToString(),
                            ReceiverName = reader["ReceiverName"].ToString(),
                            CurrencyId = (int?)reader["CurrencyId"],
                            CurrencyCode = reader["CurrencyCode"].ToString()

                        });
                    }

                    return new GenericResponse<List<RemittanceContract>>() { IsSuccess = true, Value = remittancesList };
                }

                return new GenericResponse<List<RemittanceContract>>() { IsSuccess = false, ErrorMessage = "Herhangi bir havale kaydı bulunamadı." };
            }
            catch(Exception ex)
            {
                return new GenericResponse<List<RemittanceContract>>() { Value = null, IsSuccess = false, ErrorMessage = "GetAllRemittances işlemi başarısız!" };
                throw ex;
            }
        }

        public GenericResponse<List<RemittanceContract>> FilterRemittancesByProperties(RemittanceRequest request)
        {
            DbOperation dbOperation = new DbOperation();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@StartingDate",request.DataContract.StartingDate),
                new SqlParameter("@EndingDate",request.DataContract.EndingDate),
                new SqlParameter("@CurrencyId",request.DataContract.CurrencyId),
                new SqlParameter("@StartingBalance",request.DataContract.StartingBalance),
                new SqlParameter("@EndingBalance",request.DataContract.EndingBalance),
                new SqlParameter("SenderAccountNumber",request.DataContract.SenderAccountNumber),
                new SqlParameter("ReceiverAccountNumber",request.DataContract.ReceiverAccountNumber)
            };
            try
            {
                List<RemittanceContract> remittancesList = new List<RemittanceContract>();
                SqlDataReader reader = dbOperation.GetData("TRN.sel_FilterRemittancesByProperties", parameters);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        remittancesList.Add(new RemittanceContract()
                        {
                            Id = (int)reader["Id"],
                            ReceiverAccountNumber = reader["ReceiverAccountNumber"].ToString(),
                            ReceiverAccountSuffix = reader["ReceiverAccountSuffix"].ToString(),
                            SenderAccountNumber = reader["SenderAccountNumber"].ToString(),
                            SenderAccountSuffix = reader["SenderAccountSuffix"].ToString(),
                            TransactionStatus = (int?)reader["TransactionStatus"],
                            TransactionDate = (DateTime?)reader["TransactionDate"],
                            TransactionDescription = reader["TransactionDescription"].ToString(),
                            TransferAmount = (decimal?)reader["TransferAmount"],
                            ReceiverBranchName = reader["ReceiverBranchName"].ToString(),
                            SenderBranchName = reader["SenderBranchName"].ToString(),
                            SenderLastName = reader["SenderLastName"].ToString(),
                            SenderName = reader["SenderName"].ToString(),
                            ReceiverLastName = reader["ReceiverLastName"].ToString(),
                            ReceiverName = reader["ReceiverName"].ToString(),
                            CurrencyId = (int?)reader["CurrencyId"],
                            CurrencyCode = reader["CurrencyCode"].ToString()

                        });
                    }

                    return new GenericResponse<List<RemittanceContract>>() { IsSuccess = true, Value = remittancesList };
                }
                return new GenericResponse<List<RemittanceContract>>() { IsSuccess = false, ErrorMessage = "Herhangi bir havale kaydı bulunamadı." };
            }
            
            catch (Exception ex)
            {
                return new GenericResponse<List<RemittanceContract>> { ErrorMessage = "FilterRemittancesByProperties işlemi başarısız!", IsSuccess = false, Value = null };
                throw ex;
            }
            
            
        }


    }
}
