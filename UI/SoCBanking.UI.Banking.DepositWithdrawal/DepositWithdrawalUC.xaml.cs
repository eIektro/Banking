using SoCBanking.Types.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoCBanking.UI.Banking.DepositWithdrawal
{
    /// <summary>
    /// Interaction logic for DepositWithdrawalUC.xaml
    /// </summary>
    public partial class DepositWithdrawalUC
    {
        public DepositWithdrawalUC()
        {
            #region responses
            Branches = GetAllBranchs();
            #endregion
            InitializeComponent();
        }

        private void UCBase_Loaded(object sender, RoutedEventArgs e)
        {
            Transfer = new DepositWithdrawalContract();
        }

        #region getters and setters
        private List<BranchContract> branches;
        public List<BranchContract> Branches
        {
            get { return this.branches; }
            set
            {
                this.branches = value;
                OnPropertyChanged("Branches");
            }
        }

        private DepositWithdrawalContract transfer;
        public DepositWithdrawalContract Transfer
        {
            get => transfer; set
            {
                transfer = value;
                OnPropertyChanged("Transfer");
            }
        }

        public AccountContract Account
        {
            get { return ccAccount.SelectedAccount; }
        }

        #endregion
        #region database operations
        private List<BranchContract> GetAllBranchs()
        {
            var connect = new Connector.Banking.Connect<GenericResponse<List<BranchContract>>>();
            var request = new BranchRequest();
            request.MethodName = "GetAllBranches";
            var response = connect.Execute(request);
            if (!response.IsSuccess)
            {
                MessageBox.Show($"{response.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return response.Value;
        }

        private DepositWithdrawalContract DoTransfer(DepositWithdrawalContract contract)
        {
            var connect = new Connector.Banking.Connect<GenericResponse<DepositWithdrawalContract>>();
            var request = new DepositWithdrawalRequest();
            request.MethodName = "DoNewDepositWithdrawalTransfer";
            request.DataContract = contract;
            var response = connect.Execute(request);
            if (!response.IsSuccess)
            {
                MessageBox.Show($"{response.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return response.Value;
        }

        #endregion

        private void btnAprove_Click(object sender, RoutedEventArgs e)
        {
            if(Transfer == null || Account == null)
            {
                MessageBox.Show("Lütfen gerekli bilgileri seçiniz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if(Transfer.TransferAmount > Account.Balance && Transfer.TransferType == 2)
            {
                MessageBox.Show("Transfer tutarı kaynak hesabın bakiyesinden büyük olamaz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if(Account.IsActive == false)
            {
                MessageBox.Show("Pasif hesap ile işlem yapılamaz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (Transfer.TransferAmount == null)
            {
                MessageBox.Show("Transfer tutarını doldurunuz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if(Account.Balance <= 0 && Transfer.TransferType == 2)
            {
                MessageBox.Show("Bakiyesi olmayan hesaptan para çekme işlemi yapılamaz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if(Transfer.TransferType == null)
            {
                MessageBox.Show("Lütfen işlem tipiniz seçiniz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (Transfer.TransferDate == default)
            {
                Transfer.TransferDate = DateTime.Now;
            }

            Transfer.AccountNumber = $"{Account.CustomerId}";
            Transfer.AccountSuffix = $"{Account.AdditionNo}";
            Transfer.FormedUserId = Login.LoginScreen._userId;
            Transfer.CurrencyId = Account.CurrencyId;

            if(DoTransfer(Transfer) != null)
            {
                MessageBox.Show("Para transferi gerçekleştirilmiştir.", "Bilgilendirme", MessageBoxButton.OK, MessageBoxImage.Information);
                btnClearControls_Click(new object(), new RoutedEventArgs());
            }


        }

        private void btnClearControls_Click(object sender, RoutedEventArgs e)
        {
            ccAccount.Customer = new CustomerContract();
            ccAccount.CustomerAccounts = new List<AccountContract>();
            Transfer = new DepositWithdrawalContract();
        }
    }
}
