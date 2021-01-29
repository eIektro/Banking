using BOA.Types.Banking.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    [Serializable]
    public partial class RemittanceContract : ContractBase
    {
        public RemittanceContract()
        {

        }

        private int? id;
        private string withdrawalAccountNumber;
        private string depositAccountNumber;
        private decimal? transferAmount;
        private DateTime? transactionDate;
        private int? transactionStatus;
        private string transactionDescription;

        public int? Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string WithdrawalAccountNumber
        {
            get { return withdrawalAccountNumber; }
            set { withdrawalAccountNumber = value;
                OnPropertyChanged("WithdrawalAccountNumber");
            }
        }

        public string DepositAccountNumber
        {
            get { return depositAccountNumber; }
            set
            {
                depositAccountNumber = value;
                OnPropertyChanged("DepositAccountNumber");
            }
        }

        public string TransactionDescription
        {
            get { return transactionDescription; }
            set
            {
                transactionDescription = value;
                OnPropertyChanged("TransactionDescription");
            }
        }

        public DateTime? TransactionDate
        {
            get { return transactionDate; }
            set
            {
                transactionDate = value;
                OnPropertyChanged("RransactionDate");
            }
        }

        public decimal? TransferAmount
        {
            get { return transferAmount; }
            set
            {
                transferAmount = value;
                OnPropertyChanged("TransferAmount");
            }
        }

        public int? TransactionStatus
        {
            get { return transactionStatus; }
            set
            {
                transactionStatus = value;
                OnPropertyChanged("TransactionStatus");
            }
        }

    }
}
