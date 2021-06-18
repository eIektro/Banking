using SoCBanking.Types.Banking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace SoCBanking.UI.Banking.Remittance
{
    /// <summary>
    /// Interaction logic for RemittanceScreenUC.xaml
    /// </summary>
    public partial class RemittanceScreenUC
    {
        public RemittanceScreenUC()
        {
            InitializeComponent();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Transaction = new RemittanceContract();
        }

        #region getters and setters
        public AccountContract WithdrawalAccount
        {
            get { return ccWitdrawalAccount.SelectedAccount; }

        }
        public AccountContract DepositAccount
        {
            get { return ccDepositAccount.SelectedAccount; }
        }

        private RemittanceContract transaction;
        public RemittanceContract Transaction
        {
            get
            {
                return transaction;
            }
            set
            {
                transaction = value;
                OnPropertyChanged("Transaction");
            }
        }

               

        #endregion

        #region database operations

        public RemittanceContract DoTransaction(RemittanceContract contract)
        {
            var connect = new Connector.Banking.Connect<GenericResponse<RemittanceContract>>();
            var request = new RemittanceRequest();
            request.MethodName = "DoNewRemittanceTransaction";
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
            if (WithdrawalAccount == null)
            {
                MessageBox.Show("Kaynak hesap seçilmeden işlem yapılamaz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else if (DepositAccount == null)
            {
                MessageBox.Show("Hedef hesap seçilmeden işlem yapılamaz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else if (WithdrawalAccount.IsActive == false || DepositAccount.IsActive == false)
            {
                MessageBox.Show("Pasif hesap ile işlem yapılamaz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else if(WithdrawalAccount.Balance <=0)
            {
                MessageBox.Show("Kaynak hesabın bakiyesi <=0 olamaz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else if(WithdrawalAccount.Id == DepositAccount.Id)
            {
                MessageBox.Show("Kaynak hesap ile hedef hesap aynı olamaz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else if(WithdrawalAccount.CurrencyId != DepositAccount.CurrencyId)
            {
                MessageBox.Show("Kaynak hesap ile hedef hesabın para birimleri aynı olmalıdır!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else if(Transaction.TransferAmount > WithdrawalAccount.Balance)
            {
                MessageBox.Show("Havale edilmek istenen tutar kaynak hesabın bakiyesinden büyük olamaz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else if(Transaction.TransferAmount == null)
            {
                MessageBox.Show("Tutarı doldurunuz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (Transaction.TransactionDate == default ) //Tarih seçilmediyse anın tarihi ve saati yazılır
            {
                Transaction.TransactionDate = DateTime.Now;
            }


            Transaction.SenderAccountNumber = $"{WithdrawalAccount.CustomerId}";
            Transaction.ReceiverAccountNumber = $"{DepositAccount.CustomerId}";
            Transaction.SenderAccountSuffix = $"{WithdrawalAccount.AdditionNo}";
            Transaction.ReceiverAccountSuffix = $"{DepositAccount.AdditionNo}";
            Transaction.TransactionStatus = (int?)SoCBanking.Types.Banking.Enums.TransactionStatus.Active;
            if (DoTransaction(Transaction) != null)
            {
                MessageBox.Show("Havale işlemi gerçekleştirilmiştir.", "Bilgilendirme", MessageBoxButton.OK, MessageBoxImage.Information);
                btnClearControls_Click(new object(), new RoutedEventArgs());
            }
            
        }

        private void btnClearControls_Click(object sender, RoutedEventArgs e)
        {

            ccWitdrawalAccount.Customer = new CustomerContract();
            ccDepositAccount.Customer = new CustomerContract();
            ccWitdrawalAccount.CustomerAccounts = new List<AccountContract>();
            ccDepositAccount.CustomerAccounts = new List<AccountContract>();
            Transaction = new RemittanceContract();
        }

       
    }
}
