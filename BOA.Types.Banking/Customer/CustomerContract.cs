using BOA.Types.Banking.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    public class CustomerContract : ContractBase //Domain class
    {
        public int? CustomerId {
            get => GetProperty<int?>();
            set => SetProperty<int?>(value);
        }

        public string CustomerName
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

        public string CustomerLastName
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

        public string CitizenshipId
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

        public string MotherName
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

        public string FatherName
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

        public string PlaceOfBirth
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

        public int?  JobId
        {
            get => GetProperty<int?>();
            set => SetProperty<int?>(value);
        }

        public int? EducationLvId
        {
            get => GetProperty<int?>();
            set => SetProperty<int?>(value);
        }

        public DateTime? DateOfBirth
        {
            get => GetProperty<DateTime?>();
            set => SetProperty<DateTime?>(value);
        }

        public List<CustomerPhoneContract> PhoneNumbers
        {
            get => GetProperty<List<CustomerPhoneContract>>();
            set => SetProperty<List<CustomerPhoneContract>>(value);
        }

        public List<CustomerEmailContract> Emails
        {
            get => GetProperty<List<CustomerEmailContract>>();
            set => SetProperty<List<CustomerEmailContract>>(value);
        }

        public string JobName
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

        public string EducationLevelName
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }

    }
}
