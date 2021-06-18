using SoCBanking.Types.Banking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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

namespace SoCBanking.UI.Banking.AccountList
{
    /// <summary>
    /// Interaction logic for AccountListUC.xaml
    /// </summary>
    public partial class AccountListUC
    {
        public TabControl MainScreenTabControl { get; set; }

        public AccountListUC(TabControl tabcontrol)
        {
            MainScreenTabControl = tabcontrol;

            #region Responses
            AllAccounts = GetAllAccounts();
            Currencies = GetAllCurrencies();
            Branches = GetAllBranchs();
            Users = GetAllUsers();
            #endregion

            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FilterContract = new AccountContract();
        }

        #region Getters and Setters

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

        private List<LoginContract> users;
        public List<LoginContract> Users
        {
            get { return this.users; }
            set
            {
                this.users = value;
                OnPropertyChanged("Users");
            }
        }

        private List<CurrencyContract> currencies;
        public List<CurrencyContract> Currencies
        {
            get { return this.currencies; }
            set
            {
                this.currencies = value;
                OnPropertyChanged("Currencies");
            }
        }

        private AccountContract selectedaccount;
        public AccountContract SelectedAccount
        {
            get { return this.selectedaccount; }
            set
            {
                this.selectedaccount = value;
                OnPropertyChanged("SelectedAccount");
            }
        }

        private List<AccountContract> allaccounts;
        public List<AccountContract> AllAccounts
        {
            get { return this.allaccounts; }
            set
            {
                this.allaccounts = value;
                OnPropertyChanged("AllAccounts");
                
            }
        }

        private AccountContract filtercontract;
        public AccountContract FilterContract
        {
            get
            {
                return this.filtercontract;
            }
            set
            {
                this.filtercontract = value;
                OnPropertyChanged("FilterContract");
            }
        }

        #endregion

        #region Db Operations
        private List<AccountContract> GetAllAccounts()
        {
            var connect = new Connector.Banking.Connect<GenericResponse<List<AccountContract>>>();
            var request = new AccountRequest();
            request.MethodName = "GetAllAccounts";
            var response = connect.Execute(request);
            if (!response.IsSuccess)
            {
                MessageBox.Show($"{response.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return response.Value;
        }

        private List<LoginContract> GetAllUsers()
        {
            var connect = new Connector.Banking.Connect<GenericResponse<List<LoginContract>>>();
            var request = new LoginRequest();
            request.MethodName = "GetAllUsers";
            var response = connect.Execute(request);
            if (!response.IsSuccess)
            {
                MessageBox.Show($"{response.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return response.Value;
        }

        private List<BranchContract> GetAllBranchs()
        {
            var connect = new Connector.Banking.Connect<GenericResponse< List<BranchContract>>>();
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

        private List<CurrencyContract> GetAllCurrencies()
        {
            var connect = new Connector.Banking.Connect<GenericResponse<List<CurrencyContract>>>();
            var request = new AccountRequest();
            request.MethodName = "GetAllCurrencies";
            var response = connect.Execute(request);
            if (!response.IsSuccess)
            {
                MessageBox.Show($"{response.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return response.Value;
        }

        private List<AccountContract> FilterEngine(AccountContract _contract)
        {
            var connect = new Connector.Banking.Connect<GenericResponse<List<AccountContract>>>();
            var request = new AccountRequest();
            request.MethodName = "FilterAccountsByProperties";
            request.DataContract = _contract;
            var response = connect.Execute(request);
            if (!response.IsSuccess)
            {
                MessageBox.Show($"{response.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return response.Value;
        }
        #endregion

        #region Regex Operations
        private void tbFilterbyId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbFilterbyBranchId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbFilterbyCustomerId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbFilterbyBalance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbFilterbyFormedUserId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion

        #region Button Operations
        private void btnCustomerComponent_Click(object sender, RoutedEventArgs e)
        {
            //if (tbFilterbyCustomerId.Text != "")
            //{
            //    //CustomerDetailsComponent.CustomerDetailsComponent customerComponent = new CustomerDetailsComponent.CustomerDetailsComponent(Convert.ToInt32(tbFilterbyCustomerId.Text));
            //    //if (customerComponent.Content == null) return;
            //    //CustomerDetailsComponent.CusComponentWindow customerComponentWindow = new CustomerDetailsComponent.CusComponentWindow();
            //    //customerComponentWindow.Content = customerComponent;
            //    //customerComponentWindow.ShowDialog();

            //}
        }

        private void tbFilterbyCustomerId_LostFocus(object sender, RoutedEventArgs e)
        {
            //int customerid;
            //Int32.TryParse(tbFilterbyCustomerId.Text, out customerid);

            //ccCusCom.Customer = new CustomerContract() { CustomerId = customerid };
        }

        private void btnHesapDetay_Click(object sender, RoutedEventArgs e)
        {
            if (dgAccountList.SelectedItem == null) return;
            if (MainScreenTabControl == null) return;

            SelectedAccount = (AccountContract)dgAccountList.SelectedItem;
            AccountAdd.AccountAddUC accountAddUC = new AccountAdd.AccountAddUC(SelectedAccount);

            CloseableTab.CloseableTab theTabItem = new CloseableTab.CloseableTab();
            theTabItem.Title = $"Hesap Detayları - MüşteriId: {SelectedAccount.CustomerId}";
            theTabItem.Content = accountAddUC;
            MainScreenTabControl.Items.Add(theTabItem);
            theTabItem.Focus();

        }

        private void btnFiltrele_Click(object sender, RoutedEventArgs e)
        {
            if (ccCusCom.Customer.CustomerId != null)
            {
                FilterContract.CustomerId = ccCusCom.Customer.CustomerId;
            }
            AllAccounts = FilterEngine(FilterContract);
        }

        private void btnHesapSil_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTemizle_Click(object sender, RoutedEventArgs e)
        {
            FilterContract = new AccountContract();
            ccCusCom.Customer = new CustomerContract();
        }
        #endregion
    }
}
