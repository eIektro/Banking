using BOA.Types.Banking.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    public class BranchContract : ContractBase
    {

        public int? Id
        {
            get => GetProperty<int?>();
            set => SetProperty<int?>(value);
        }

        public string BranchName
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

        public int? CityId
        {
            get => GetProperty<int?>();
            set => SetProperty<int?>(value);
        }

        public DateTime? DateOfLaunch
        {
            get => GetProperty<DateTime?>();
            set => SetProperty<DateTime?>(value);
        }

        public string Adress
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

        public string PhoneNumber
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

        public string MailAdress
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

        public string City
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

    }
}
