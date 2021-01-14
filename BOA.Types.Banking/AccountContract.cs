using BOA.Types.Banking.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    public class AccountContract : ContractBase
    {

        public int? Id
        {
            get => GetProperty<int?>();
            set => SetProperty<int?>(value);
        }
        public int? BranchId
        {
            get => GetProperty<int?>();
            set => SetProperty<int?>(value);
        }
        public int? CustomerId
        {
            get => GetProperty<int?>();
            set => SetProperty<int?>(value);
        }
        public int? AdditionNo
        {
            get => GetProperty<int?>();
            set => SetProperty<int?>(value);
        }
        public int? CurrencyId
        {
            get => GetProperty<int?>();
            set => SetProperty<int?>(value);
        }
        public Decimal? Balance
        {
            get => GetProperty<decimal?>();
            set => SetProperty<decimal?>(value);
        }

        public DateTime? DateOfFormation
        {
            get => GetProperty<DateTime?>();
            set => SetProperty<DateTime?>(value);
        }
        public string IBAN
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

        public bool IsActive
        {
            get => GetProperty<bool>();
            set => SetProperty<bool>(value);
        }

        public DateTime? DateOfDeactivation
        {
            get => GetProperty<DateTime?>();
            set => SetProperty<DateTime?>(value);
        }

        public int? FormedUserId
        {
            get => GetProperty<int?>();
            set => SetProperty<int?>(value);
        }

        public DateTime? DateOfLastTrasaction
        {
            get => GetProperty<DateTime?>();
            set => SetProperty<DateTime?>(value);
        }

        public string FormedUserName
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

        public string BranchName
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

        public string CurrencyCode
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

    }
}
