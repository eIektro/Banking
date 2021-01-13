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

namespace BOA.UI.Banking.AccountAdd
{
    /// <summary>
    /// Interaction logic for AccountAddUC.xaml
    /// </summary>
    public partial class AccountAddUC : UserControl,INotifyPropertyChanged
    {
        private bool isEditingOption = false;
        //public AccountContract editingAccount { get; set; }

        public AccountAddUC()
        {
            #region responses
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
            #endregion

            InitializeComponent();
        }

        public AccountAddUC(AccountContract contract)
        {
            #region responses
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
            #endregion

            Account = contract;
            isEditingOption = true;
            InitializeComponent();
            DisableUserInputs(true);
            SetVisibilitiesForDetail();
        }
        
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(!isEditingOption){
                Account = new AccountContract();
            }
            
        }

        #region event handling
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region getters and setters
        private AccountContract account; //küçük harfler
        public AccountContract Account
        {
            get
            {
                return this.account;
            }
            set
            {
                this.account = value;
                OnPropertyChanged("Account");
            }
        }

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
        #endregion

        #region db operations
        private ResponseBase GetAllCurrencies()
        {
            var connect = new Connector.Banking.Connect();
            var request = new AccountRequest();
            request.MethodName = "GetAllCurrencies";
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

        private ResponseBase AddAccount(AccountContract contract)
        {
            var connect = new Connector.Banking.Connect();
            var request = new AccountRequest();
            request.MethodName = "AddNewAccount";
            request.DataContract = contract;
            var response = connect.Execute(request);
            return response;
        }

        private ResponseBase UpdateAccount(AccountContract contract)
        {
            var connect = new Connector.Banking.Connect();
            var request = new AccountRequest();
            request.MethodName = "UpdateAccountDetailsById";
            request.DataContract = contract;
            var response = connect.Execute(request);
            return response;
        }
        #endregion

        #region regex
        private void tbBalance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion

        #region button operations
        private void btnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (isEditingOption)
            {
                if (MessageBox.Show("Yaptığınız değişlikler hesaba yansısın mı?", "Onay", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var responseUpdate = UpdateAccount(Account);
                    if (responseUpdate.IsSuccess)
                    {
                        MessageBox.Show("Değişiklikler uygulandı!", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
                        btnVazgec_Click(new object(), new RoutedEventArgs());

                    }
                    else
                    {
                        MessageBox.Show($"{responseUpdate.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                return;
            }


            Account.FormedUserId = Login.LoginScreen._userId;
            var response = AddAccount(Account);
            if (response.IsSuccess)
            {
                MessageBox.Show("Hesap eklendi!", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
                Account = new AccountContract();
            }
            else
            {
                MessageBox.Show($"{response.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void btnVazgec_Click(object sender, RoutedEventArgs e)
        {
            DisableUserInputs(true);
            btnVazgec.Visibility = Visibility.Hidden;
            btnDuzenle.Visibility = Visibility.Visible;
            btnKaydet.Visibility = Visibility.Hidden;
        }

        private void btnDuzenle_Click(object sender, RoutedEventArgs e)
        {
            btnVazgec.Visibility = Visibility.Visible;
            btnKaydet.Visibility = Visibility.Visible;
            btnDuzenle.Visibility = Visibility.Hidden;
            DisableUserInputs(false);
        }


        #endregion

        public void SetVisibilitiesForDetail()
        {
            btnDuzenle.Visibility = Visibility.Visible;
            btnKaydet.Visibility = Visibility.Hidden;
            btnVazgec.Visibility = Visibility.Hidden;
        }

        public void DisableUserInputs(bool WannaDisable)
        {
            tbAdditionNo.IsReadOnly = WannaDisable;
            tbBalance.IsReadOnly = WannaDisable;
            cbBranchId.IsHitTestVisible = !WannaDisable;
            tbCustomerId.IsReadOnly = WannaDisable;
            tbIBAN.IsReadOnly = WannaDisable;
            cbCurrencyId.IsHitTestVisible = !WannaDisable;
            cbIsActive.IsHitTestVisible = !WannaDisable;
        }


        //private void ClearInputs()
        //{
        //    tbAdditionNo.Text = "";
        //    tbCustomerId.Text = "";
        //    cbBranchId.SelectedIndex = -1;
        //    cbCurrencyId.SelectedIndex = -1;
        //    tbBalance.Text = "";
        //    tbIBAN.Text = "";
        //    cbIsActive.IsChecked = false;
        //}
    }
}
