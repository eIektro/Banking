using BOA.Types.Banking;
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

namespace BOA.UI.Banking.CustomerComponent
{
    /// <summary>
    /// Interaction logic for CustomerComponentUC.xaml
    /// </summary>
    public partial class CustomerComponentUC
    {
        public CustomerComponentUC()
        {
            #region responses
            //Customer = new CustomerContract();
            #endregion

            InitializeComponent();

            string parentucname = ParentUcName;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(ParentUcName == "AccountAddUC")
            {
                tbCustomerId.Visibility = Visibility.Collapsed;
                tbBalance.Visibility = Visibility.Collapsed;
                tbBlockage.Visibility = Visibility.Collapsed;
                tbIban.Visibility = Visibility.Collapsed;
                tbUsableBalance.Visibility = Visibility.Collapsed;
                cbSuffixes.Visibility = Visibility.Collapsed;
                tbCurrencySymbol.Visibility = Visibility.Collapsed;
                lblIban.Visibility = Visibility.Collapsed;
                lblBalance.Visibility = Visibility.Collapsed;
                lblUsableBalance.Visibility = Visibility.Collapsed;
                lblBlockage.Visibility = Visibility.Collapsed;
                lblCitizenshipTaxNumber.Visibility = Visibility.Collapsed;
                btnFind.Visibility = Visibility.Collapsed;


                tbBranchName.Width = 150;
                tbBranchName.Height = 25;
                tbBranchName.Margin = new Thickness(10, 10, 0, 0);
                tbCustomerName.Width = 150;
                tbCustomerName.Height = 25;
                tbCustomerName.Margin = new Thickness(10, 10, 0, 0);
                tbCitizenshipTaxNumber.Width = 150;
                tbCitizenshipTaxNumber.Height = 25;
                tbCitizenshipTaxNumber.Margin = new Thickness(10, 5, 0, 0);


            }

            if(ParentUcName == "AccountListUC")
            {
                tbCustomerId.Visibility = Visibility.Collapsed;
                tbBalance.Visibility = Visibility.Collapsed;
                tbBlockage.Visibility = Visibility.Collapsed;
                tbIban.Visibility = Visibility.Collapsed;
                tbUsableBalance.Visibility = Visibility.Collapsed;
                cbSuffixes.Visibility = Visibility.Collapsed;
                tbCurrencySymbol.Visibility = Visibility.Collapsed;
                lblIban.Visibility = Visibility.Collapsed;
                lblBalance.Visibility = Visibility.Collapsed;
                lblUsableBalance.Visibility = Visibility.Collapsed;
                lblBlockage.Visibility = Visibility.Collapsed;
                lblCitizenshipTaxNumber.Visibility = Visibility.Collapsed;
                btnFind.Visibility = Visibility.Collapsed;


                tbBranchName.Width = 120;
                tbBranchName.Height = 25;
                tbBranchName.Margin = new Thickness(10, 5, 0, 0);
                tbCustomerName.Width = 120;
                tbCustomerName.Height = 25;
                tbCustomerName.Margin = new Thickness(10, 10, 0, 0);
                tbCitizenshipTaxNumber.Width = 120;
                tbCitizenshipTaxNumber.Height = 25;
                tbCitizenshipTaxNumber.Margin = new Thickness(10, 0, 0, 0);
                
            }
            //SelectedAccount = new AccountContract();
        }

        private string parentUcName;
        public string ParentUcName
        {
            get { return parentUcName; }
            set { parentUcName = value;
                OnPropertyChanged("ParentUcName");
            }
        }

        //public string MyParentUC
        //{
        //    get { return (string)GetValue(MyParentUCProperty); }
        //    set { SetValue(MyParentUCProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyVarIconX.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty MyParentUCProperty =
        //DependencyProperty.Register("ParentUC", typeof(string), typeof(CustomerComponentUC), new UIPropertyMetadata(0));

        private void tbCustomerId_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbCustomerId.Text != "" && tbCustomerId.Text != null) {
                Customer = GetCustomerById(new CustomerContract() { CustomerId = Convert.ToInt32(tbCustomerId.Text) });
                if(Customer != null)
                {
                    CustomerAccounts = GetAccountsByCustomerId(new AccountContract() { CustomerId = Customer.CustomerId });
                }
            }
        }

        #region getters and setters

        private CustomerContract customer;
        public CustomerContract Customer
        {
            get
            {
                return customer;
            }
            set
            {
                customer = value;
                OnPropertyChanged("Customer");
            }
        }

        
        public String ComTbCustomerId
        {
            get
            {
                return tbCustomerId.Text;
                
            }
            set
            {
                tbCustomerId.Text = value;
                OnPropertyChanged("ComTbCustomerId");
            }
        }
        
        public void ComTbCustomerId_LostFocus()
        {
            tbCustomerId_LostFocus(new object(), new RoutedEventArgs());
        }

        
        private AccountContract selectedAccount;
        public AccountContract SelectedAccount
        {
            get
            {
                return selectedAccount;
            }
            set
            {
                selectedAccount = value;
                OnPropertyChanged("SelectedAccount");
            }
        }

        private List<AccountContract> customerAccounts;
        public List<AccountContract> CustomerAccounts
        {
            get { return this.customerAccounts; }
            set
            {
                this.customerAccounts = value;
                OnPropertyChanged("CustomerAccounts");
            }
        }
        #endregion

        #region database operations
        private List<AccountContract> GetAccountsByCustomerId(AccountContract _contract)
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

        public CustomerContract GetCustomerById(CustomerContract contract)
        {

            var connect = new Connector.Banking.Connect<GenericResponse<CustomerContract>>();
            var request = new CustomerRequest();

            request.MethodName = "GetCustomerDetailsById";
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

        private void tbCustomerId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
