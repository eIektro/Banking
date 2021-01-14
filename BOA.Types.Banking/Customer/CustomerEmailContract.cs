using BOA.Types.Banking.Base;

namespace BOA.Types.Banking
{
    public class CustomerEmailContract : ContractBase
    {
        public int? CustomerMailId
        {
            get => GetProperty<int?>();
            set => SetProperty<int?>(value);
        }

        public int? CustomerId
        {
            get => GetProperty<int?>();
            set => SetProperty<int?>(value);
        }

        public int EmailType
        {
            get => GetProperty<int>();
            set => SetProperty<int>(value);
        }

        public string MailAdress
        {
            get => GetProperty<string>();
            set => SetProperty<string>(value);
        }
    }
}