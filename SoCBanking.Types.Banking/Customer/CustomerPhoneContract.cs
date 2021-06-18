using SoCBanking.Types.Banking.Base;
using System;

namespace SoCBanking.Types.Banking
{
    [Serializable]
    public partial class CustomerPhoneContract : ContractBase
    {
        public CustomerPhoneContract()
        {

        }

        private int? customerPhoneId;
        private int? customerId;
        private int phoneType;
        private string phoneNumber;

        public int? CustomerPhoneId
        {
            get { return customerPhoneId; }
            set { customerPhoneId = value;
                OnPropertyChanged("CustomerPhoneId");
            }
        }

        public int? CustomerId
        {
            get { return customerId; }
            set { customerId = value;
                OnPropertyChanged("CustomerId");
            }
        }

        public int PhoneType
        {
            get { return phoneType; }
            set { phoneType = value;
                OnPropertyChanged("PhoneType");
            }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
    }
}