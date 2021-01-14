using BOA.Types.Banking.Base;

namespace BOA.Types.Banking
{
    public class CustomerPhoneContract : ContractBase
    {
        public int? CustomerPhoneId
        {
            get => GetProperty<int?>();
            set => SetProperty<int?>(value);
        }

        public int? CustomerId
        {
            get => GetProperty<int?>();
            set => SetProperty<int?>(value);
        }

        public int PhoneType
        {
            get => GetProperty<int>();
            set => SetProperty<int>(value);
        }

        public string PhoneNumber
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }
    }
}