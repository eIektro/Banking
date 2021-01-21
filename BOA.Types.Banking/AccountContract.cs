using BOA.Types.Banking.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    [Serializable]
    public partial class AccountContract : ContractBase
    {
        public AccountContract()
        {

        }


        private int? id;
        private int? branchId;
        private int? customerId;
        private int? additionNo;
        private int? currencyId;
        private Decimal? balance;
        private DateTime? dateOfFormation;
        private string iban;
        private bool isActive;
        private DateTime? dateOfDeactivation;
        private int? formedUserId;
        private DateTime? dateOfLastTrasaction;
        private string formedUserName;
        private string branchName;
        private string currencyCode;


        public int? Id
        {
            get { return id; }
            set { id = value;
                OnPropertyChanged("Id");
            }

        }
        public int? BranchId
        {
            get { return branchId; }
            set { branchId = value;
                OnPropertyChanged("BranchId");
            }
        }
        public int? CustomerId
        {
            get { return customerId; }
            set { customerId = value;
                OnPropertyChanged("CustomerId");
            }
        }
        public int? AdditionNo
        {
            get { return additionNo; }
            set { additionNo = value;
                OnPropertyChanged("AdditionNo");
            }
        }
        public int? CurrencyId
        {
            get { return currencyId; }
            set { currencyId = value;
                OnPropertyChanged("CurrencyId");
            }
        }
        public Decimal? Balance
        {
            get { return balance; }
            set { balance = value;
                OnPropertyChanged("Balance");
            }
        }

        public DateTime? DateOfFormation
        {
            get { return dateOfFormation; }
            set { dateOfFormation = value;
                OnPropertyChanged("DateOfFormation");
            }
        }
        public string IBAN
        {
            get { return iban; }
            set { iban = value;
                OnPropertyChanged("IBAN");
            }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value;
                OnPropertyChanged("IsActive");
            }
        }

        public DateTime? DateOfDeactivation
        {
            get { return dateOfDeactivation; }
            set { dateOfDeactivation = value;
                OnPropertyChanged("DateOfDeactivation");
            }
        }

        public int? FormedUserId
        {
            get { return formedUserId; }
            set { formedUserId = value;
                OnPropertyChanged("FormedUserId");
            }
        }

        public DateTime? DateOfLastTrasaction
        {
            get { return dateOfLastTrasaction; }
            set { dateOfLastTrasaction = value;
                OnPropertyChanged("DateOfLastTrasaction");
            }
        }

        public string FormedUserName
        {
            get { return formedUserName; }
            set { formedUserName = value;
                OnPropertyChanged("FormedUserName");
            }
        }

        public string BranchName
        {
            get { return branchName; }
            set { branchName = value;
                OnPropertyChanged("BranchName");
            }
        }

        public string CurrencyCode
        {
            get { return currencyCode; }
            set { currencyCode = value;
                OnPropertyChanged("CurrencyCode");
            }
        }

    }
}
