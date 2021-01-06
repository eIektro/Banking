using BOA.Types.Banking;
using BOA.Types.Banking.Customer;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace BOA.UI.Banking.CustomerList
{
    /// <summary>
    /// Interaction logic for CustomerListUC.xaml
    /// </summary>
    public partial class CustomerListUC : UserControl
    {
        CustomerContract selectedCustomer;

        List<CustomerContract> customersList;

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
            var request = new BOA.Types.Banking.CustomerRequest();

            request.MethodName = "GetAllCustomers";

            var response = (ResponseBase)connect.Execute(request);

            customersList = (List<CustomerContract>)response.DataContract;


            if (response.IsSuccess)
            {
                
                dgMusteriListesi.ItemsSource = customersList;
            }
            else
            { 
                
            }
        }

        
        private List<CustomerContract> SearchEngine(string name="", string surname="", string placeofbirh="", string citizenshipid="", DateTime dateofbirth=default)
        {
            var result = customersList.FindAll(x => x.CustomerName.ToLower().Contains(name.ToLower()) && x.CustomerLastName.ToLower().Contains(surname.ToLower()) 
            && x.PlaceOfBirth.ToLower().Contains(placeofbirh.ToLower()) && x.CitizenshipId.ToLower().Contains(citizenshipid.ToLower()) && x.DateOfBirth >= dateofbirth /* && x.DateOfBirth.ToString("dd.MM.yyyy").Contains(dateofbirth)*/);
            return result;
        }
        private List<CustomerContract> SearchEngine(int id) //TO-DO: refactor edilecek, zaten id primary key olduğundan tek item dönüyor
        {
            var result = customersList.FindAll(x => x.CustomerId == id);
            return result;
        }

        private void btnMusteriSil_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCustomer != null)
            {
                if (MessageBox.Show($"{selectedCustomer.CustomerName} {selectedCustomer.CustomerLastName} ({selectedCustomer.CustomerId}) isimli müşteriyi veritabanından silmeye emin misiniz?", "Silme Uyarısı", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {

                    BOA.Connector.Banking.Connect connect = new Connector.Banking.Connect();
                    CustomerRequest request = new CustomerRequest();
                    request.MethodName = "CustomerDelete";
                    request.DataContract = selectedCustomer;
                    var response = connect.Execute(request);

                    if (response.IsSuccess)
                    {
                        MessageBox.Show("Silme işlemi başarılı","Bilgi",MessageBoxButton.OK,MessageBoxImage.Information);
                        BindGrid();
                    }


                } 
            }
        }

        private void btnMusteriDetay_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCustomer != null)
            {
                CustomerAdd.CustomerAdd customerDetails = new CustomerAdd.CustomerAdd(selectedCustomer,true);
                customerDetails.ShowDialog();
                BindGrid();
            }
        }

       
        private void dgMusteriListesi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCustomer = (CustomerContract)dgMusteriListesi.SelectedItem;
            
        }

        private void btnFiltrele_Click(object sender, RoutedEventArgs e)
        {
            if (tbFilterbyId.Text != "")
            {
                int id = Convert.ToInt32(tbFilterbyId.Text);
                dgMusteriListesi.ItemsSource = SearchEngine(id);
            }
            else
            {
                dgMusteriListesi.ItemsSource = SearchEngine(tbFilterbyName.Text, tbFilterbySurname.Text, tbFilterbyPlaceOfBirth.Text, tbFilterbyCitizenshipId.Text, dpFilterbyDateOfBirth.SelectedDate.GetValueOrDefault());
            }
        }

        private void btnMusteriEkle_Click(object sender, RoutedEventArgs e)
        {
            CustomerAdd.CustomerAdd addNewCustomer = new CustomerAdd.CustomerAdd();
            addNewCustomer.ShowDialog();
            BindGrid();
        }

        private void tbFilterbyId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
