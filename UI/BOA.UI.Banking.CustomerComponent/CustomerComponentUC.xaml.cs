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
    public partial class CustomerComponentUC : UserControl, INotifyPropertyChanged
    {
        public CustomerComponentUC()
        {
            #region responses
            //Customer = new CustomerContract();
            #endregion

            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //SelectedAccount = new AccountContract();
        }

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


        #region event handling
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


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

        
    }
}
