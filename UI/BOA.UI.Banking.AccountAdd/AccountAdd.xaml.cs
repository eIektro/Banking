using BOA.Types.Banking;
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
    /// Interaction logic for AccountAdd.xaml
    /// </summary>
    public partial class AccountAdd : Window,INotifyPropertyChanged
    {
        public bool IsEditing;


        public AccountAdd(AccountContract _editingAccount)
        {
            Account = _editingAccount;

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

            DisableUserInputs(true);
            btnDuzenle.Visibility = Visibility.Visible;
            btnKaydet.Visibility = Visibility.Hidden;
            btnVazgec.Visibility = Visibility.Hidden;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Account = new AccountContract();
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
        private AccountContract _Account;
        public AccountContract Account
        {
            get
            {
                return this._Account;
            }
            set
            {
                this._Account = value;
                OnPropertyChanged("Account");
            }
        }

        private List<BranchContract> _Branches;
        public List<BranchContract> Branches
        {
            get { return this._Branches; }
            set
            {
                this._Branches = value;
                OnPropertyChanged("Branches");
            }
        }

        private List<CurrencyContract> _Currencies;
        public List<CurrencyContract> Currencies
        {
            get { return this._Currencies; }
            set
            {
                this._Currencies = value;
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

            if (IsEditing)
            {
                if (MessageBox.Show("Yaptığınız değişlikler hesaba yansısın mı?", "Tasdik", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var response = UpdateAccount(Account);
                    if (response.IsSuccess)
                    {
                        MessageBox.Show("Değişiklikler uygulandı!", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show($"{response.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

        }

        private void btnDuzenle_Click(object sender, RoutedEventArgs e)
        {
            btnVazgec.Visibility = Visibility.Visible;
            btnKaydet.Visibility = Visibility.Visible;
            btnDuzenle.Visibility = Visibility.Hidden;
            DisableUserInputs(false);
        }

        private void btnVazgec_Click(object sender, RoutedEventArgs e)
        {
            Account = new AccountContract();
            DisableUserInputs(true);
            btnVazgec.Visibility = Visibility.Hidden;
            btnDuzenle.Visibility = Visibility.Visible;
            btnKaydet.Visibility = Visibility.Hidden;
        }
        #endregion

        #region user inputs
        public void DisableUserInputs(bool WannaDisable)
        {
            tbAdditionNo.IsReadOnly = WannaDisable;
            tbBalance.IsReadOnly = WannaDisable;
            cbBranchId.IsReadOnly = WannaDisable;
            tbCustomerId.IsReadOnly = WannaDisable;
            tbIBAN.IsReadOnly = WannaDisable;
            cbCurrencyId.IsReadOnly = WannaDisable;
            cbIsActive.IsHitTestVisible = !WannaDisable;
        } 
        #endregion
    }
}
