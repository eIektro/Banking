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
        private string senderAccountNumber;
        private string receiverAccountNumber;
        private string senderAccountSuffix;
        private string receiverAccountSuffix;
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

    }
}
