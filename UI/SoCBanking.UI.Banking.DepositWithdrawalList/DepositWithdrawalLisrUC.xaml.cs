using SoCBanking.Types.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SoCBanking.UI.Banking.DepositWithdrawalList
{
    /// <summary>
    /// Interaction logic for DepositWithdrawalLisrUC.xaml
    /// </summary>
    public partial class DepositWithdrawalLisrUC
    {
        public DepositWithdrawalLisrUC()
        {
            Currencies = GetAllCurrencies();
            Users = GetAllUsers();
            Branches = GetAllBranchs();
            AllDepositWithdrawals = GetAllDepositWithdrawals();
            InitializeComponent();
        }

        private void UCBase_Loaded(object sender, RoutedEventArgs e)
        {
            FilterContract = new DepositWithdrawalContract();
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


        private List<DepositWithdrawalContract> GetAllDepositWithdrawals()
        {
            var connect = new Connector.Banking.Connect<GenericResponse<List<DepositWithdrawalContract>>>();
            var request = new DepositWithdrawalRequest();
            request.MethodName = "GetAllDepositWithdrawals";
            var response = connect.Execute(request);
            if (!response.IsSuccess)
            {
                MessageBox.Show($"{response.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return response.Value;
        }

        private List<DepositWithdrawalContract> FilterEngine(DepositWithdrawalContract _contract)
        {
            var connect = new Connector.Banking.Connect<GenericResponse<List<DepositWithdrawalContract>>>();
            var request = new DepositWithdrawalRequest();
            request.MethodName = "FilterDepositWithdrawalsByProperties";
            request.DataContract = _contract;
            var response = connect.Execute(request);
            if (!response.IsSuccess)
            {
                MessageBox.Show($"{response.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return response.Value;
        }

        private List<DepositWithdrawalContract> allDepositWithdrawals;

        public List<DepositWithdrawalContract> AllDepositWithdrawals
        {
            get => allDepositWithdrawals; set
            {
                allDepositWithdrawals = value;
                OnPropertyChanged("AllDepositWithdrawals");
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

        private DepositWithdrawalContract filtercontract;
        public DepositWithdrawalContract FilterContract
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

        

        private void tbFilterbyId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnTemizle_Click(object sender, RoutedEventArgs e)
        {
            FilterContract = new DepositWithdrawalContract();
            ccCusCom.Customer = new CustomerContract();
        }

        private void btnFiltrele_Click(object sender, RoutedEventArgs e)
        {
            if (ccCusCom.Customer.CustomerId != null)
            {
                FilterContract.AccountNumber = ccCusCom.Customer.CustomerId.ToString();
            }
            AllDepositWithdrawals = FilterEngine(FilterContract);
        }
    }
}
