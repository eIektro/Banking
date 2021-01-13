using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

namespace BOA.UI.Banking.CustomerList
{
    /// <summary>
    /// Interaction logic for CustomerListUC.xaml
    /// </summary>
    public partial class CustomerListUC : UserControl, INotifyPropertyChanged
    {
        private TabControl MainScreenTabControl { get; set; }

        public CustomerListUC(TabControl tabcontrol)
        {
            MainScreenTabControl = tabcontrol;

            #region responses
            var _EducationLevelsResponse = GetAllEducationLevels();
            if (_EducationLevelsResponse.IsSuccess)
            {
                EducationLevels = (List<EducationLevelContract>)_EducationLevelsResponse.DataContract;
            }
            else
            {
                MessageBox.Show($"{_EducationLevelsResponse.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            var _JobsResponse = GetAllJobs();
            if (_JobsResponse.IsSuccess)
            {
                Jobs = (List<JobContract>)_JobsResponse.DataContract;
            }
            else
            {
                MessageBox.Show($"{_JobsResponse.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            var _CustomersResponse = GetAllCustomers();
            if (_CustomersResponse.IsSuccess)
            {
                AllCustomers = (List<CustomerContract>)_CustomersResponse.DataContract;
            }
            else
            {
                MessageBox.Show($"{_CustomersResponse.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
            #endregion

            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FilterContract = new CustomerContract();
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
        private CustomerContract _FilterContract;
        public CustomerContract FilterContract
        {
            get { return this._FilterContract; }
            set
            {
                this._FilterContract = value;
                OnPropertyChanged("FilterContract");
            }
        }

        private CustomerContract _SelectedCustomer;
        public CustomerContract SelectedCustomer
        {
            get { return this._SelectedCustomer; }
            set
            {
                this._SelectedCustomer = value;
                OnPropertyChanged("SelectedCustomer");
            }
        }

        private List<EducationLevelContract> _EducationLevels;
        public List<EducationLevelContract> EducationLevels
        {
            get { return this._EducationLevels; }
            set
            {
                this._EducationLevels = value;
                OnPropertyChanged("EducationLevels");
            }
        }

        private List<JobContract> _Jobs;
        public List<JobContract> Jobs
        {
            get { return this._Jobs; }
            set
            {
                this._Jobs = value;
                OnPropertyChanged("Jobs");
            }
        }

        private List<CustomerContract> _AllCustomers;
        public List<CustomerContract> AllCustomers
        {
            get { return this._AllCustomers; }
            set
            {
                this._AllCustomers = value;
                OnPropertyChanged("AllCustomers");
            }
        }
        #endregion

        #region Db Operations
        private ResponseBase GetAllEducationLevels()
        {
            var connect = new Connector.Banking.Connect();
            var request = new CustomerRequest();
            request.MethodName = "getAllEducationLevels";
            var response = connect.Execute(request);
            return response;
        }
        private ResponseBase GetAllJobs()
        {
            var connect = new Connector.Banking.Connect();
            var request = new CustomerRequest();
            request.MethodName = "getAllJobs";
            var response = connect.Execute(request);
            return response;
        }
        private ResponseBase GetAllCustomers()
        {
            var connect = new Connector.Banking.Connect();
            var request = new CustomerRequest();
            request.MethodName = "GetAllCustomers";
            var response = connect.Execute(request);
            return response;
        }

        private ResponseBase FilterEngine(CustomerContract _contract)
        {
            var connect = new Connector.Banking.Connect();
            var request = new CustomerRequest();

            request.MethodName = "FilterCustomersByProperties";
            request.DataContract = _contract;

            var response = connect.Execute(request);

            return response;
        }
        #endregion

        #region Button Operations
        private void btnMusteriSil_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMusteriDetay_Click(object sender, RoutedEventArgs e)
        {
            if (dgMusteriListesi.SelectedItem == null) return;
            if (MainScreenTabControl == null) return;


            SelectedCustomer = (CustomerContract)dgMusteriListesi.SelectedItem;
            CustomerAdd.CustomerAddUC customerAddUC = new CustomerAdd.CustomerAddUC(SelectedCustomer);

            CloseableTab.CloseableTab theTabItem = new CloseableTab.CloseableTab();
            theTabItem.Title = $"Müşteri Detayları - ({SelectedCustomer.CustomerId}) {SelectedCustomer.CustomerName} {SelectedCustomer.CustomerLastName}";
            theTabItem.Content = customerAddUC;
            MainScreenTabControl.Items.Add(theTabItem);
            theTabItem.Focus();

        }

        private void btnFiltrele_Click(object sender, RoutedEventArgs e)
        {
            var response = FilterEngine(FilterContract);
            if (response.IsSuccess)
            {
                var responseCustomers = (List<CustomerContract>)response.DataContract;
                AllCustomers = responseCustomers;
            }
            else { MessageBox.Show($"{response.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void btnTemizle_Click(object sender, RoutedEventArgs e)
        {
            FilterContract = new CustomerContract();
        }
        #endregion

        #region regex operations
        private void tbFilterbyId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        } 
        #endregion
    }
}
