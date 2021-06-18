using SoCBanking.Types.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoCBanking.Process.Banking
{
    public class Remittance
    {
        public GenericResponse<RemittanceContract> DoNewRemittanceTransaction(RemittanceRequest request)
        {
            if(request.DataContract.TransactionDate.GetValueOrDefault() < new DateTime(1753, 01, 01))
            {
                DateTime sqlRange = new DateTime(1753, 01, 01);
                request.DataContract.TransactionDate = sqlRange;
            }

            SoCBanking.Business.Banking.Remittance remittranceBusiness = new Business.Banking.Remittance();
            var response = remittranceBusiness.DoNewRemittanceTransaction(request);
            return response;

        }

        public GenericResponse<List<RemittanceContract>> GetAllRemittances(RemittanceRequest request)
        {
            SoCBanking.Business.Banking.Remittance remittranceBusiness = new Business.Banking.Remittance();
            var response = remittranceBusiness.GetAllRemittances(request);
            return response;
        }

        public GenericResponse<List<RemittanceContract>> FilterRemittancesByProperties(RemittanceRequest request)
        {
            SoCBanking.Business.Banking.Remittance remittranceBusiness = new Business.Banking.Remittance();
            var response = remittranceBusiness.FilterRemittancesByProperties(request);
            return response;
        }

    }
}
