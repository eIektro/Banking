using SoCBanking.Types.Banking;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace SoCBanking.UI.Banking.RemittanceList
{
    /// <summary>
    /// Interaction logic for RemittanceListUC.xaml
    /// </summary>
    public partial class RemittanceListUC
    {
        public RemittanceListUC()
        {
            #region
            Remittances = GetAllRemittances();
            Currencies = GetAllCurrencies();
            #endregion
            InitializeComponent();
        }

        private void UCBase_Loaded(object sender, RoutedEventArgs e)
        {
            FilterContract = new RemittanceContract();
        }


        #region getters and setter
        private List<RemittanceContract> remittances;
        public List<RemittanceContract> Remittances
        {
            get { return remittances; }
            set { remittances = value;
                OnPropertyChanged("Remittances");
            }
        }

        private RemittanceContract filterContract;
        public RemittanceContract FilterContract
        {
            get { return filterContract; }
            set
            {
                filterContract = value;
                OnPropertyChanged("FilterContract");
            }
        }

        private List<CurrencyContract> currencies;
        public List<CurrencyContract> Currencies
        {
            get { return currencies; }
            set { currencies = value;
                OnPropertyChanged("Currencies");
            }
        }
        #endregion

        #region db operations
        public List<RemittanceContract> GetAllRemittances()
        {
            Connector.Banking.Connect<GenericResponse<List<RemittanceContract>>> connect = new Connector.Banking.Connect<GenericResponse<List<RemittanceContract>>>();
            RemittanceRequest request = new RemittanceRequest();
            request.MethodName = "GetAllRemittances";
            var response = connect.Execute(request);
            if (!response.IsSuccess)
            {
                MessageBox.Show($"{response.ErrorMessage}","Hata",MessageBoxButton.OK,MessageBoxImage.Error);
                return null;
            }
            return response.Value;
        }


        public List<RemittanceContract> FilterRemittancesByProperties(RemittanceContract contract)
        {
            Connector.Banking.Connect<GenericResponse<List<RemittanceContract>>> connect = new Connector.Banking.Connect<GenericResponse<List<RemittanceContract>>>();
            RemittanceRequest request = new RemittanceRequest();
            request.MethodName = "FilterRemittancesByProperties";
            request.DataContract = contract;
            var response = connect.Execute(request);
            if (!response.IsSuccess)
            {
                MessageBox.Show($"{response.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return response.Value;
        }

        public List<CurrencyContract> GetAllCurrencies()
        {
            Connector.Banking.Connect<GenericResponse<List<CurrencyContract>>> connect = new Connector.Banking.Connect<GenericResponse<List<CurrencyContract>>>();
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


        #endregion

        private void tbEndingBalance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbStartingBalance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnFiltrele_Click(object sender, RoutedEventArgs e)
        {
            if (cusComSender.Customer.CustomerId != null)
            {
                FilterContract.SenderAccountNumber = cusComSender.Customer.CustomerId.ToString(); 
            }
            if (cusComReceiver.Customer.CustomerId != null)
            {
                FilterContract.ReceiverAccountNumber = cusComReceiver.Customer.CustomerId.ToString(); 
            }
            Remittances = FilterRemittancesByProperties(FilterContract);
        }

        private void btnTemizle_Click(object sender, RoutedEventArgs e)
        {
            cusComSender.Customer = new CustomerContract();
            cusComReceiver.Customer = new CustomerContract();
            FilterContract = new RemittanceContract();
        }

        
    }
}
