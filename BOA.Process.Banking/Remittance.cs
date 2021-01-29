using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Process.Banking
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

            BOA.Business.Banking.Remittance remittranceBusiness = new Business.Banking.Remittance();
            var response = remittranceBusiness.DoNewRemittanceTransaction(request);
            return response;

        }
    }
}
