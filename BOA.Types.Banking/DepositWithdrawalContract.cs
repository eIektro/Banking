using BOA.Types.Banking.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    [Serializable]
    public class DepositWithdrawalContract : ContractBase
    {
        public DepositWithdrawalContract()
        {

        }

        private int? id;
        private int? transferType;
        private int? transferBranchId;
        private string accountNumber;
        private string accountSuffix;
        private DateTime? transferDate;
        private Decimal? transferAmount;
        private string transferDescription;
        private int? formedUserId;
        private int? currencyId;

        public int? Id
        {
            get => id; set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public int? TransferType
        {
            get => transferType; set
            {
                transferType = value;
                OnPropertyChanged("TransferType");
            }
        }
        public int? TransferBranchId
        {
            get => transferBranchId; set
            {
                transferBranchId = value;
                OnPropertyChanged("TransferBranchId");
            }
        }
        public string AccountNumber
        {
            get => accountNumber; set
            {
                accountNumber = value;
                OnPropertyChanged("AccountNumber");
            }
        }
        public string AccountSuffix 
        { 
            get => accountSuffix; set
            {
                accountSuffix = value;
                OnPropertyChanged("AccountSuffix");

            } 
        }
        public DateTime? TransferDate 
        { 
            get => transferDate; set
            {
                transferDate = value;
                OnPropertyChanged("TransferDate");
            } 
        }
        public decimal? TransferAmount 
        { 
            get => transferAmount; set 
            {
                transferAmount = value;
                OnPropertyChanged("TransferAmount");
            } 
        }
        public string TransferDescription 
        {
            get => transferDescription; set 
            {
                transferDescription = value;
                OnPropertyChanged("TransferDescription");
            }
        }
        public int? FormedUserId 
        { 
            get => formedUserId; set 
            {
                formedUserId = value;
                OnPropertyChanged("FormedUserId");
            } 
        }

        public int? CurrencyId
        {
            get => currencyId;
            set
            {
                currencyId = value;
                OnPropertyChanged("CurrencyId");
            }
        }
    }
}
