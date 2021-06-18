using SoCBanking.Types.Banking.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoCBanking.Types.Banking
{
    [Serializable]
    public partial class RemittanceContract : ContractBase
    {
        public RemittanceContract()
        {

        }

        private int? id;
        private string senderAccountNumber;
        private string receiverAccountNumber;
        private string senderAccountSuffix;
        private string receiverAccountSuffix;
        private decimal? transferAmount;
        private DateTime? transactionDate;
        private int? transactionStatus;
        private string transactionDescription;
        private string senderName;
        private string senderLastName;
        private string receiverName;
        private string receiverLastName;
        private string senderBranchName;
        private string receiverBranchName;
        private DateTime? startingDate;
        private DateTime? endingDate;
        private int? currencyId;
        private decimal? startingBalance;
        private decimal? endingBalance;
        private string currencyCode;

        public int? Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string SenderName
        {
            get { return senderName; }
            set { senderName = value;
                OnPropertyChanged("SenderName");
            }
        }

        public string SenderLastName
        {
            get { return senderLastName; }
            set
            {
                senderLastName = value;
                OnPropertyChanged("SenderLastName");
            }
        }

        public string ReceiverName
        {
            get { return receiverName; }
            set
            {
                receiverName = value;
                OnPropertyChanged("ReceiverName");
            }
        }

        public string ReceiverLastName
        {
            get { return receiverLastName; }
            set
            {
                receiverLastName = value;
                OnPropertyChanged("ReceiverLastName");
            }
        }

        public string SenderBranchName
        {
            get { return senderBranchName; }
            set
            {
                senderBranchName = value;
                OnPropertyChanged("SenderBranchName");
            }
        }

        public string ReceiverBranchName
        {
            get { return receiverBranchName; }
            set
            {
                receiverBranchName = value;
                OnPropertyChanged("ReceiverBranchName");
            }
        }

        public string SenderAccountNumber
        {
            get { return senderAccountNumber; }
            set { senderAccountNumber = value;
                OnPropertyChanged("SenderAccountNumber");
            }
        }

        public string ReceiverAccountNumber
        {
            get { return receiverAccountNumber; }
            set
            {
                receiverAccountNumber = value;
                OnPropertyChanged("ReceiverAccountNumber");
            }
        }

        public string SenderAccountSuffix
        {
            get { return senderAccountSuffix; }
            set
            {
                senderAccountSuffix = value;
                OnPropertyChanged("SenderAccountSuffix");
            }
        }

        public string ReceiverAccountSuffix
        {
            get { return receiverAccountSuffix; }
            set
            {
                receiverAccountSuffix = value;
                OnPropertyChanged("ReceiverAccountSuffix");
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

        public DateTime? StartingDate
        {
            get { return startingDate; }
            set { startingDate = value;
                OnPropertyChanged("StartingDate");
            }
        }

        public DateTime? EndingDate
        {
            get { return endingDate; }
            set
            {
                endingDate = value;
                OnPropertyChanged("EndingDate");
            }
        }

        public int? CurrencyId
        {
            get { return currencyId; }
            set
            {
                currencyId = value;
                OnPropertyChanged("CurrencyId");
            }
        }

        public decimal? StartingBalance
        {
            get { return startingBalance; }
            set { startingBalance = value;
                OnPropertyChanged("StartingBalance");
            }
        }

        public decimal? EndingBalance
        {
            get { return endingBalance; }
            set
            {
                endingBalance = value;
                OnPropertyChanged("EndingBalance");
            }
        }

        public string CurrencyCode
        {
            get { return currencyCode; }
            set
            {
                currencyCode = value;
                OnPropertyChanged("CurrencyCode");
            }
        }

    }
}
