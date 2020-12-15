using BOA.Types.Banking;
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

namespace BOA.UI.Banking.CustomerAdd
{
    /// <summary>
    /// Interaction logic for CustomerAdd.xaml
    /// </summary>
    public partial class CustomerAdd : Window
    {

        public CustomerContract _kaydedilecekMusteri { get; set; }
        
        public CustomerAdd()
        {
            InitializeComponent();
        }

        private void tbCitizenshipId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void dpDateOfBirth_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-/]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bilgilerini girdiğiniz müşteri veritabanına kaydedilsin mi?","Kayıt",MessageBoxButton.YesNoCancel,MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _kaydedilecekMusteri = new CustomerContract();
                _kaydedilecekMusteri.CustomerId = 0;
                _kaydedilecekMusteri.CustomerName = tbCustomerName.Text;
                _kaydedilecekMusteri.CustomerLastName = tbCustomerLastName.Text;
                _kaydedilecekMusteri.JobId = cbJobId.SelectedIndex;
                _kaydedilecekMusteri.EducationLvId = cbEducationLvId.SelectedIndex;
                _kaydedilecekMusteri.FatherName = tbFatherName.Text;
                _kaydedilecekMusteri.MotherName = tbMotherName.Text;
                _kaydedilecekMusteri.PlaceOfBirth = tbPlaceOfBirth.Text;
                _kaydedilecekMusteri.DateOfBirth = (DateTime)dpDateOfBirth.SelectedDate;
                _kaydedilecekMusteri.PhoneNumbers = new List<CustomePhoneContract>();
                _kaydedilecekMusteri.Emails = new List<CustomerEmailContract>();



            }
        }

        private void btnPhonePopClose_Click(object sender, RoutedEventArgs e)
        {
            popPhoneAdd.IsOpen = false;
        }

        private void btnPhoneAdd_Click(object sender, RoutedEventArgs e)
        {
            popPhoneAdd.IsOpen = true;
            //lbPhoneNumbers.Items.Add("05073430022"+" "+"(Kişisel)");
            //lbPhoneNumbers.Items.Add("05073430022" + " " + "(İş)");
            //lbPhoneNumbers.Items.Add("02164243068" + " " + "(Ev)");
        }

        private void tbPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
