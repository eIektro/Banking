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
    /// Interaction logic for CustomerComponent.xaml
    /// </summary>
    public partial class CustomerComponent : UserControl, INotifyPropertyChanged
    {
        public CustomerComponent(int customerid)
        {
            Customer = GetCustomerById(new CustomerContract() { CustomerId = customerid }); //Customer Null olduğu durum için mantıklı bir kapatma çözümü bul.

            if (Customer != null)
            {
                InitializeComponent(); 
            }
            else
            {
                ReturnNull();
            }

            
        }

        public int? ReturnNull()
        {
            return null;
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

        private CustomerContract customer;

        public CustomerContract Customer
        {
            get { return this.customer; }
            set
            {
                this.customer = value;
                OnPropertyChanged("Customer");
            }
        }

        #region Event Handling
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        
        private void btnKapat_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.Close();
        }

        private void btnTamam_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
