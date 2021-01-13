﻿using BOA.Types.Banking;
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

namespace BOA.UI.Banking.AccountList
{
    /// <summary>
    /// Interaction logic for AccountListUC.xaml
    /// </summary>
    public partial class AccountListUC : UserControl,INotifyPropertyChanged //contract base e atılacak, method overload tab ui şeklinde
    {
        public TabControl MainScreenTabControl { get; set; }

        public AccountListUC(TabControl tabcontrol)
        {
            MainScreenTabControl = tabcontrol;

            #region Responses
            var _AllAccountsResponse = GetAllAccounts();
            if (_AllAccountsResponse.IsSuccess)
            {
                AllAccounts = (List<AccountContract>)_AllAccountsResponse.DataContract;
            }
            else
            {
                MessageBox.Show($"{_AllAccountsResponse.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            var _AllCurrenciesResponse = GetAllCurrencies();
            if (_AllCurrenciesResponse.IsSuccess)
            {
                Currencies = (List<CurrencyContract>)_AllCurrenciesResponse.DataContract;
            }
            else
            {
                MessageBox.Show($"{_AllCurrenciesResponse.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            var _AllBranchesResponse = GetAllBranchs();
            if (_AllBranchesResponse.IsSuccess)
            {
                Branches = (List<BranchContract>)_AllBranchesResponse.DataContract;
            }
            else
            {
                MessageBox.Show($"{_AllBranchesResponse.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            var _AllUsersResponse = GetAllUsers();
            if (_AllUsersResponse.IsSuccess)
            {
                Users = (List<LoginContract>)_AllUsersResponse.DataContract;
            }
            else
            {
                MessageBox.Show($"{_AllUsersResponse.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
            #endregion

            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FilterContract = new AccountContract();
        }

        #region Event Handling
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion

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
        private ResponseBase GetAllAccounts()
        {
            var connect = new Connector.Banking.Connect();
            var request = new AccountRequest();
            request.MethodName = "GetAllAccounts";
            var response = connect.Execute(request);
            return response;
        }

        private ResponseBase GetAllUsers()
        {
            var connect = new Connector.Banking.Connect();
            var request = new LoginRequest();
            request.MethodName = "GetAllUsers";
            var response = connect.Execute(request);
            return response;
        }

        private ResponseBase GetAllBranchs()
        {
            var connect = new Connector.Banking.Connect();
            var request = new BranchRequest();
            request.MethodName = "GetAllBranches";
            var response = connect.Execute(request);
            return response;
        }

        private ResponseBase GetAllCurrencies()
        {
            var connect = new Connector.Banking.Connect();
            var request = new AccountRequest();
            request.MethodName = "GetAllCurrencies";
            var response = connect.Execute(request);
            return response;
        }

        private ResponseBase FilterEngine(AccountContract _contract)
        {
            var connect = new Connector.Banking.Connect();
            var request = new AccountRequest();
            request.MethodName = "FilterAccountsByProperties";
            request.DataContract = _contract;
            var response = connect.Execute(request);
            return response;
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
            var response = FilterEngine(FilterContract);
            if (response.IsSuccess)
            {
                var responseAccounts = (List<AccountContract>)response.DataContract;
                AllAccounts = responseAccounts;
            }
            else { MessageBox.Show($"{response.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void btnHesapSil_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTemizle_Click(object sender, RoutedEventArgs e)
        {
            FilterContract = new AccountContract();
        } 
        #endregion
    }
}
