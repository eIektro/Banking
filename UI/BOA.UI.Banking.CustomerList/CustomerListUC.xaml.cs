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
            EducationLevels = GetAllEducationLevels();
            Jobs = GetAllJobs();
            AllCustomers = GetAllCustomers();
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
        private CustomerContract filtercontract;
        public CustomerContract FilterContract
        {
            get { return this.filtercontract; }
            set
            {
                this.filtercontract = value;
                OnPropertyChanged("FilterContract");
            }
        }

        private CustomerContract selectedcustomer;
        public CustomerContract SelectedCustomer
        {
            get { return this.selectedcustomer; }
            set
            {
                this.selectedcustomer = value;
                OnPropertyChanged("SelectedCustomer");
            }
        }

        private List<EducationLevelContract> educationlevels;
        public List<EducationLevelContract> EducationLevels
        {
            get { return this.educationlevels; }
            set
            {
                this.educationlevels = value;
                OnPropertyChanged("EducationLevels");
            }
        }

        private List<JobContract> jobs;
        public List<JobContract> Jobs
        {
            get { return this.jobs; }
            set
            {
                this.jobs = value;
                OnPropertyChanged("Jobs");
            }
        }

        private List<CustomerContract> allcustomers;
        public List<CustomerContract> AllCustomers
        {
            get { return this.allcustomers; }
            set
            {
                this.allcustomers = value;
                OnPropertyChanged("AllCustomers");
            }
        }
        #endregion

        #region Db Operations
        private List<EducationLevelContract> GetAllEducationLevels()
        {
            var connect = new Connector.Banking.Connect<GenericResponse<List<EducationLevelContract>>>();
            var request = new CustomerRequest();
            request.MethodName = "getAllEducationLevels";
            var response = connect.Execute(request);
            if (!response.IsSuccess)
            {
                MessageBox.Show($"{response.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return response.Value;
        }
        private List<JobContract> GetAllJobs()
        {
            var connect = new Connector.Banking.Connect<GenericResponse<List<JobContract>>>();
            var request = new CustomerRequest();
            request.MethodName = "getAllJobs";
            var response = connect.Execute(request);
            if (!response.IsSuccess)
            {
                MessageBox.Show($"{response.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return response.Value;
        }
        private List<CustomerContract> GetAllCustomers()
        {
            var connect = new Connector.Banking.Connect<GenericResponse<List<CustomerContract>>>();
            var request = new CustomerRequest();
            request.MethodName = "GetAllCustomers";
            var response = connect.Execute(request);
            if (!response.IsSuccess)
            {
                MessageBox.Show($"{response.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return response.Value;
        }

        private List<CustomerContract> FilterEngine(CustomerContract _contract)
        {
            var connect = new Connector.Banking.Connect<GenericResponse<List<CustomerContract>>>();
            var request = new CustomerRequest();

            request.MethodName = "GetCustomerDetailsById";
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

        #region Button Operations
        private void btnCustomerComponent_Click(object sender, RoutedEventArgs e)
        {
            if (tbFilterbyId.Text != "")
            {
                CustomerComponent.CustomerComponent customerComponent = new CustomerComponent.CustomerComponent(Convert.ToInt32(tbFilterbyId.Text));
                CustomerComponent.CusComponentWindow customerComponentWindow = new CustomerComponent.CusComponentWindow();
                customerComponentWindow.Content = customerComponent;
                customerComponentWindow.ShowDialog();

            }
        }

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
            AllCustomers = FilterEngine(FilterContract);
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
