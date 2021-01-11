using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    public class AccountContract
    {

        public int? Id { get; set; }
        public int? BranchId { get; set; }
        public int? CustomerId { get; set; }
        public int? AdditionNo { get; set; }
        public int? CurrencyId { get; set; }
        public Decimal? Balance { get; set; }

        public DateTime? DateOfFormation { get; set; }
        public string IBAN { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DateOfDeactivation { get; set; }

        public int? FormedUserId { get; set; }

        public DateTime? DateOfLastTrasaction { get; set; }

        public string FormedUserName { get; set; }

        public string BranchName { get; set; }

        public string CurrencyCode { get; set; }

    }
}
