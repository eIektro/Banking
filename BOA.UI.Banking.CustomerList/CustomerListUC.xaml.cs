using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace BOA.UI.Banking.CustomerList
{
    /// <summary>
    /// Interaction logic for CustomerListUC.xaml
    /// </summary>
    public partial class CustomerListUC : UserControl
    {
        public CustomerListUC()
        {
            InitializeComponent();
            
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            var connect = new BOA.Connector.Banking.Connect();
            var request = new BOA.Types.Banking.GetAllCustomersRequest();
            var contract = new BOA.Types.Banking.CustomerContract();
            var response = new BOA.Types.Banking.GetAllCustomersResponse();

            request.MethodName = "GetAllCustomers";


            response = (GetAllCustomersResponse)connect.Execute(request);

            

            if (response.IsSuccess)
            {
                MusteriGrid.ItemsSource = response.CustomersList;
            }
            else
            {
                
            }
        }
    }
}
