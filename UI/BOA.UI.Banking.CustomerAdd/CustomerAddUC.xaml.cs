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
    /// Interaction logic for CustomerAddUC.xaml
    /// </summary>
    public partial class CustomerAddUC : UserControl
    {
        public CustomerAddUC()
        {
            InitializeComponent();
            BindAllJobsToCombobox();
            BindAllEducationLevelsToCombobox();
        }

        public CustomerContract _kaydedilecekMusteri { get; set; }
        public List<CustomerPhoneContract> _customerPhones { get; set; }
        public List<CustomerEmailContract> _customerEmails { get; set; }

        

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
            //Boş alan varsa. Lütfen gerekli alanları doldurunuz mesajı gelecek.
            if (MessageBox.Show("Bilgilerini girdiğiniz müşteri veritabanına kaydedilsin mi?", "Kayıt", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _kaydedilecekMusteri = new CustomerContract();
                _kaydedilecekMusteri.CustomerId = null; //Kayıttan sonra oluşacak
                _kaydedilecekMusteri.CustomerName = tbCustomerName.Text;
                _kaydedilecekMusteri.CustomerLastName = tbCustomerLastName.Text;
                _kaydedilecekMusteri.CitizenshipId = tbCitizenshipId.Text;
                _kaydedilecekMusteri.JobId = cbJobId.SelectedIndex;
                _kaydedilecekMusteri.EducationLvId = cbEducationLvId.SelectedIndex;
                _kaydedilecekMusteri.FatherName = tbFatherName.Text;
                _kaydedilecekMusteri.MotherName = tbMotherName.Text;
                _kaydedilecekMusteri.PlaceOfBirth = tbPlaceOfBirth.Text;
                _kaydedilecekMusteri.DateOfBirth = (DateTime)dpDateOfBirth.SelectedDate;

                if (_customerPhones != null)
                    _kaydedilecekMusteri.PhoneNumbers = _customerPhones;
                if (_customerEmails != null)
                    _kaydedilecekMusteri.Emails = _customerEmails;


                var connect = new BOA.Connector.Banking.Connect();
                var request = new BOA.Types.Banking.CustomerRequest();
                var contract = _kaydedilecekMusteri;
                var response = new BOA.Types.Banking.CustomerResponse();

                request.MethodName = "CustomerAdd";
                request.DataContract = contract;


                response = (CustomerResponse)connect.Execute(request);

                if (response.IsSuccess)
                {
                    MessageBox.Show($"Müşteri {response.DataContract.CustomerId} id'si ile oluşturuldu.");
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show(response.ErrorMessage);
                }


            }
        }

        private void btnPhonePopClose_Click(object sender, RoutedEventArgs e)
        {
            popPhoneAdd.IsOpen = false;
        }

        private void btnPhonePopOpen_Click(object sender, RoutedEventArgs e)
        {
            popPhoneAdd.IsOpen = true;
            if (_customerPhones == null)
            {
                _customerPhones = new List<CustomerPhoneContract>();
            }
        }

        private void tbPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public void BindAllEducationLevelsToCombobox()
        {
            var connect = new BOA.Connector.Banking.Connect();
            var request = new BOA.Types.Banking.EducationLevelRequest();
            //var contract = new BOA.Types.Banking.EducationLevelContract();
            var response = new BOA.Types.Banking.GetAllEducationLevelsResponse();

            request.MethodName = "getAllEducationLevels";


            response = (GetAllEducationLevelsResponse)connect.Execute(request);

            if (response.IsSuccess)
            {

                foreach (EducationLevelContract item in response.DataContract)
                {
                    cbEducationLvId.Items.Add(item.EducationLevel);
                }

            }
            else
            {
                MessageBox.Show(response.ErrorMessage);
            }
        }

        void BindAllJobsToCombobox()
        {
            var connect = new BOA.Connector.Banking.Connect();
            var request = new BOA.Types.Banking.JobRequest();
            //var contract = new BOA.Types.Banking.JobContract();
            var response = new BOA.Types.Banking.GetAllJobsResponse();

            request.MethodName = "getAllJobs";


            response = (GetAllJobsResponse)connect.Execute(request);

            if (response.IsSuccess)
            {

                foreach (JobContract item in response.DataContract)
                {
                    cbJobId.Items.Add(item.JobName);
                }

            }
            else
            {
                MessageBox.Show(response.ErrorMessage);
            }
        }

        void ClearInputs()
        {
            tbCustomerName.Clear();
            tbCitizenshipId.Clear();
            tbCustomerLastName.Clear();
            tbFatherName.Clear();
            tbMotherName.Clear();
            tbPlaceOfBirth.Clear();
            cbEducationLvId.Text = "";
            cbJobId.Text = "";
            //dpDateOfBirth.SelectedDate = new DateTime(null);

        }

        private void btnEmailPopOpen_Click(object sender, RoutedEventArgs e)
        {
            popEmailAdd.IsOpen = true;
            if (_customerEmails == null)
            {
                _customerEmails = new List<CustomerEmailContract>();
            }
        }

        private void btnEmailPopClose_Click(object sender, RoutedEventArgs e)
        {
            popEmailAdd.IsOpen = false;
        }

        private void btnPhoneAddToListbox_Click(object sender, RoutedEventArgs e)
        {
            if (tbPhoneNumber.Text != "" && cbPhoneType.SelectedItem != null)
            {
                string phoneAndType = $"{tbPhoneNumber.Text} ( {cbPhoneType.SelectedItem} ) ";
                lbPhoneNumbers.Items.Add(phoneAndType);
                _customerPhones.Add(new CustomerPhoneContract() { CustomerId = null, CustomerPhoneId = null, PhoneNumber = tbPhoneNumber.Text, PhoneType = cbPhoneType.SelectedIndex });
                tbPhoneNumber.Text = "";
                cbPhoneType.SelectedIndex = -1;

            }
        }

        private void btnPhoneDeleteFromListbox_Click(object sender, RoutedEventArgs e)
        {
            if (lbPhoneNumbers.SelectedIndex != -1)
            {
                _customerPhones.RemoveAt(lbPhoneNumbers.SelectedIndex);
                lbPhoneNumbers.Items.RemoveAt(lbPhoneNumbers.SelectedIndex);

            }
        }

        private void btnEmailAddToListbox_Click(object sender, RoutedEventArgs e)
        {
            if (tbEmail.Text != "" && cbEmailType.SelectedItem != null) //SelectedIndex -1 yazmak gerekiyor olabilir ikisine de bak
            {
                string emailAndType = $"{tbEmail.Text} ( {cbEmailType.SelectedItem} ) ";
                lbEmails.Items.Add(emailAndType);
                _customerEmails.Add(new CustomerEmailContract() { CustomerId = null, CustomerMailId = null, MailAdress = tbEmail.Text, EmailType = cbEmailType.SelectedIndex });
                tbEmail.Text = "";
                cbEmailType.SelectedIndex = -1;

            }
        }

        private void btnEmailDeleteFromListbox_Click(object sender, RoutedEventArgs e)
        {
            if (lbEmails.SelectedIndex != -1)
            {
                _customerEmails.RemoveAt(lbEmails.SelectedIndex);
                lbEmails.Items.RemoveAt(lbEmails.SelectedIndex);

            }
        }
    }
}
