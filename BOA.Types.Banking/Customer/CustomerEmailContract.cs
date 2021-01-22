using BOA.Types.Banking.Base;
using System;

namespace BOA.Types.Banking
{
    [Serializable]
    public partial class CustomerEmailContract : ContractBase
    {
        public CustomerEmailContract()
        {

        }

        private int? customerMailId;
        private int? customerId;
        private int emailType;
        private string mailAdress;


        public int? CustomerMailId
        {
            get { return customerMailId; }
            set { customerMailId = value;
                OnPropertyChanged("CustomerMailId");
            }
        }

        public int? CustomerId
        {
            get { return customerId; }
            set { customerId = value;
                OnPropertyChanged("CustomerId");
            }
        }

        public int EmailType
        {
            get { return emailType; }
            set { emailType = value;
                OnPropertyChanged("EmailType");
            }
        }

        public string MailAdress
        {
            get { return mailAdress; }
            set { mailAdress = value;
                OnPropertyChanged("MailAdress");
            }
        }
    }
}