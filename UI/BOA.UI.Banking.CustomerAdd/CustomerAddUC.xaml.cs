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
        public CustomerContract editingCustomer { get; set; }
        public CustomerContract _kaydedilecekMusteri { get; set; }
        public List<CustomerPhoneContract> _customerPhones { get; set; }
        public List<CustomerEmailContract> _customerEmails { get; set; }

        public Boolean IsCustomerEditWindow = false;

        public CustomerAddUC()
        {
            InitializeComponent();
            BindAllJobsToCombobox();
            BindAllEducationLevelsToCombobox();
        }

        //public CustomerAdd(CustomerContract _selectedCustomer, bool _isCustomerEditWindow)
        //{
        //    editingCustomer = _selectedCustomer;
        //    IsCustomerEditWindow = _isCustomerEditWindow;
        //    /* TO-DO: Müşteri ekleme ile aynı method kullanılacak şekilde REFACTOR EDİLECEK. Şuan _selectedcustomer ve _kaydedilecekmusteri isimli iki ayrı liste kullanılıyor.  */
        //    InitializeComponent();
        //    BindAllJobsToCombobox();
        //    cbJobId.SelectedIndex = _selectedCustomer.JobId.GetValueOrDefault();
        //    BindAllEducationLevelsToCombobox();
        //    cbEducationLvId.SelectedIndex = _selectedCustomer.EducationLvId.GetValueOrDefault();
        //    tbCitizenshipId.Text = _selectedCustomer.CitizenshipId;
        //    tbCustomerLastName.Text = _selectedCustomer.CustomerLastName;
        //    tbCustomerName.Text = _selectedCustomer.CustomerName;
        //    tbFatherName.Text = _selectedCustomer.FatherName;
        //    tbMotherName.Text = _selectedCustomer.MotherName;
        //    tbPlaceOfBirth.Text = _selectedCustomer.PlaceOfBirth;
        //    dpDateOfBirth.SelectedDate = _selectedCustomer.DateOfBirth;
        //    _customerPhones = (List<CustomerPhoneContract>)_selectedCustomer.PhoneNumbers;
        //    _customerEmails = (List<CustomerEmailContract>)_selectedCustomer.Emails;
        //    foreach (CustomerEmailContract x in _customerEmails)
        //    {
        //        dgEmails.Items.Add(x);
        //    }
        //    foreach (CustomerPhoneContract x in _customerPhones)
        //    {
        //        dgPhoneNumbers.Items.Add(x);
        //    }
        //    btnEmailPopOpen.Content = "Mail Adresleri";
        //    btnPhonePopOpen.Content = "Telefon Numaraları";
        //    SetUserInputsEditableFunction(true);
        //    btnDuzenle.Visibility = Visibility.Visible;
        //    btnKaydet.Visibility = Visibility.Hidden;
        //    this.Title = "Müşteri Bilgisi Düzenle";

        //}

        public void SetUserInputsEditableFunction(Boolean IsNotEditable)
        {

            cbJobId.IsEnabled = !IsNotEditable;
            cbEducationLvId.IsEnabled = !IsNotEditable;
            tbCustomerName.IsReadOnly = IsNotEditable;
            tbCitizenshipId.IsReadOnly = IsNotEditable;
            tbCustomerLastName.IsReadOnly = IsNotEditable;
            tbFatherName.IsReadOnly = IsNotEditable;
            tbMotherName.IsReadOnly = IsNotEditable;
            tbPlaceOfBirth.IsReadOnly = IsNotEditable;
            dpDateOfBirth.IsEnabled = !IsNotEditable;
            btnEmailAddToDataGrid.IsEnabled = !IsNotEditable;
            btnPhoneAddToDataGrid.IsEnabled = !IsNotEditable;
            cbPhoneType.IsEnabled = !IsNotEditable;
            tbPhoneNumber.IsReadOnly = IsNotEditable;
            tbEmail.IsReadOnly = IsNotEditable;
            cbPhoneType.IsEnabled = !IsNotEditable;
            btnEmailDeleteFromDataGrid.IsEnabled = !IsNotEditable;
            btnPhoneDeleteFromDataGrid.IsEnabled = !IsNotEditable;
            cbEmailType.IsEnabled = !IsNotEditable;
            //dpDateOfBirth
            //btnEmailPopOpen.IsEnabled = false;
            //btnPhonePopOpen.IsEnabled = false;
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
            if (IsCustomerEditWindow)
            {
                /* TO-DO:Farklı yerlerde tekraren yapılan bir işlem refactor edilecek. */
                editingCustomer.JobId = cbJobId.SelectedIndex;
                editingCustomer.EducationLvId = cbEducationLvId.SelectedIndex;
                editingCustomer.CitizenshipId = tbCitizenshipId.Text;
                editingCustomer.CustomerLastName = tbCustomerLastName.Text;
                editingCustomer.CustomerName = tbCustomerName.Text;
                editingCustomer.FatherName = tbFatherName.Text;
                editingCustomer.MotherName = tbMotherName.Text;
                editingCustomer.PlaceOfBirth = tbPlaceOfBirth.Text;
                editingCustomer.DateOfBirth = (DateTime)dpDateOfBirth.SelectedDate;
                editingCustomer.PhoneNumbers = _customerPhones;
                editingCustomer.Emails = _customerEmails;

                var connect = new Connector.Banking.Connect();
                var request = new CustomerRequest();
                request.MethodName = "UpdateCustomerbyId";
                request.DataContract = editingCustomer;

                var response = (ResponseBase)connect.Execute(request);

                if (response.IsSuccess)
                {
                    btnKaydet.Visibility = Visibility.Hidden;
                    btnVazgec.Visibility = Visibility.Hidden;
                    btnDuzenle.Visibility = Visibility.Visible;
                    MessageBox.Show("Seçilen müşteri için bilgiler güncellendi", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                }
                else
                {
                    MessageBox.Show("Bilgiler güncellenemedi. " + response.ErrorMessage, "Başarısız", MessageBoxButton.OK, MessageBoxImage.Error);
                    
                }
            }

            if (IsCustomerEditWindow == false)
            {
                if (tbCitizenshipId.Text != "" && tbCustomerLastName.Text != "" && tbCustomerName.Text != null)
                {
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


                        request.MethodName = "CustomerAdd";
                        request.DataContract = contract;


                        var response = (ResponseBase)connect.Execute(request);



                        if (response.IsSuccess)
                        {
                            var addedCustomer = (CustomerContract)response.DataContract;
                            MessageBox.Show($"Müşteri {addedCustomer.CustomerId} id'si ile oluşturuldu.");
                            ClearInputs();
                        }
                        else
                        {
                            MessageBox.Show(response.ErrorMessage);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Gerekli alanları doldurunuz", "Bilgilendirme", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
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
            var request = new BOA.Types.Banking.CustomerRequest();
            //var contract = new BOA.Types.Banking.EducationLevelContract();

            request.MethodName = "getAllEducationLevels";


            var response = (ResponseBase)connect.Execute(request);

            if (response.IsSuccess)
            {
                var educationLevels = (List<EducationLevelContract>)response.DataContract;
                foreach (EducationLevelContract item in educationLevels)
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
            var request = new BOA.Types.Banking.CustomerRequest();
            //var contract = new BOA.Types.Banking.JobContract();

            request.MethodName = "getAllJobs";


            var response = (ResponseBase)connect.Execute(request);

            if (response.IsSuccess)
            {
                var jobs = (List<JobContract>)response.DataContract;

                foreach (JobContract item in jobs)
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
            dpDateOfBirth.SelectedDate = default;

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

        private void btnEmailPopOpen_Click(object sender, RoutedEventArgs e)
        {
            popEmailAdd.IsOpen = true;

            //if (popEmailAdd.IsOpen == false)
            //{
            //    popEmailAdd.IsOpen = true; 
            //}
            //popEmailAdd.IsOpen = true;
            //popEmailAdd.IsOpen = true;
            //if (_customerEmails == null)
            //{
            //    _customerEmails = new List<CustomerEmailContract>();
            //}
        }

        private void btnEmailPopClose_Click(object sender, RoutedEventArgs e)
        {
            popEmailAdd.IsOpen = false;
        }

        private void btnPhoneAddToDataGrid_Click(object sender, RoutedEventArgs e)
        {
            if (tbPhoneNumber.Text != "" && cbPhoneType.SelectedItem != null)
            {
                dgPhoneNumbers.Items.Add(new CustomerPhoneContract() { PhoneNumber = tbPhoneNumber.Text, PhoneType = cbPhoneType.SelectedIndex });

            }
        }

        private void btnPhoneDeleteFromDataGrid_Click(object sender, RoutedEventArgs e)
        {
            if (dgPhoneNumbers.SelectedIndex != -1)
            {



            }
        }

        private void btnEmailAddToDataGrid_Click(object sender, RoutedEventArgs e)
        {
            if (tbEmail.Text != "" && cbEmailType.SelectedItem != null) //SelectedIndex -1 yazmak gerekiyor olabilir ikisine de bak
            {
                dgEmails.Items.Add(new CustomerEmailContract() { EmailType = cbEmailType.SelectedIndex, MailAdress = tbEmail.Text });

            }
        }

        private void btnEmailDeleteFromDataGrid_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmails.SelectedIndex != -1)
            {


            }
        }

        private void btnDuzenle_Click(object sender, RoutedEventArgs e)
        {
            btnKaydet.Visibility = Visibility.Visible;
            btnVazgec.Visibility = Visibility.Visible;
            btnDuzenle.Visibility = Visibility.Hidden;
            SetUserInputsEditableFunction(false);
        }

        private void btnVazgec_Click(object sender, RoutedEventArgs e)
        {
            btnKaydet.Visibility = Visibility.Hidden;
            btnDuzenle.Visibility = Visibility.Visible;
            btnVazgec.Visibility = Visibility.Hidden;
            SetUserInputsEditableFunction(true);

        }


        //public CustomerContract _kaydedilecekMusteri { get; set; }
        //public List<CustomerPhoneContract> _customerPhones { get; set; }
        //public List<CustomerEmailContract> _customerEmails { get; set; }

        //public CustomerAdd()
        //{
        //    InitializeComponent();
        //    BindAllJobsToCombobox();
        //    BindAllEducationLevelsToCombobox();

        //}

        //private void tbCitizenshipId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    Regex regex = new Regex("[^0-9]+");
        //    e.Handled = regex.IsMatch(e.Text);
        //}

        //private void dpDateOfBirth_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    Regex regex = new Regex("[^0-9.-/]+");
        //    e.Handled = regex.IsMatch(e.Text);
        //}

        //private void btnKaydet_Click(object sender, RoutedEventArgs e)
        //{
        //    //Boş alan varsa. Lütfen gerekli alanları doldurunuz mesajı gelecek.
        //    if (MessageBox.Show("Bilgilerini girdiğiniz müşteri veritabanına kaydedilsin mi?","Kayıt",MessageBoxButton.YesNoCancel,MessageBoxImage.Question) == MessageBoxResult.Yes)
        //    {
        //        _kaydedilecekMusteri = new CustomerContract();
        //        _kaydedilecekMusteri.CustomerId = null; //Kayıttan sonra oluşacak
        //        _kaydedilecekMusteri.CustomerName = tbCustomerName.Text;
        //        _kaydedilecekMusteri.CustomerLastName = tbCustomerLastName.Text;
        //        _kaydedilecekMusteri.CitizenshipId = tbCitizenshipId.Text;
        //        _kaydedilecekMusteri.JobId = cbJobId.SelectedIndex;
        //        _kaydedilecekMusteri.EducationLvId = cbEducationLvId.SelectedIndex;
        //        _kaydedilecekMusteri.FatherName = tbFatherName.Text;
        //        _kaydedilecekMusteri.MotherName = tbMotherName.Text;
        //        _kaydedilecekMusteri.PlaceOfBirth = tbPlaceOfBirth.Text;
        //        _kaydedilecekMusteri.DateOfBirth = (DateTime)dpDateOfBirth.SelectedDate;

        //        if(_customerPhones != null)
        //            _kaydedilecekMusteri.PhoneNumbers = _customerPhones;
        //        if(_customerEmails != null)
        //            _kaydedilecekMusteri.Emails = _customerEmails;


        //        var connect = new BOA.Connector.Banking.Connect();
        //        var request = new BOA.Types.Banking.CustomerRequest();
        //        var contract = _kaydedilecekMusteri;


        //        request.MethodName = "CustomerAdd";
        //        request.DataContract = contract;


        //        var response = (ResponseBase)connect.Execute(request);

        //        if (response.IsSuccess)
        //        {
        //            var kaydedilenMusteri = (CustomerContract)response.DataContract;
        //            MessageBox.Show($"Müşteri {kaydedilenMusteri.CustomerId} id'si ile oluşturuldu.");
        //            ClearInputs();
        //        }
        //        else
        //        {
        //            MessageBox.Show(response.ErrorMessage);
        //        }


        //    }
        //}

        //private void btnPhonePopClose_Click(object sender, RoutedEventArgs e)
        //{
        //    popPhoneAdd.IsOpen = false;
        //}

        //private void btnPhonePopOpen_Click(object sender, RoutedEventArgs e)
        //{
        //    popPhoneAdd.IsOpen = true;
        //    if(_customerPhones == null)
        //    {
        //        _customerPhones = new List<CustomerPhoneContract>();
        //    }
        //}

        //private void tbPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    Regex regex = new Regex("[^0-9]+");
        //    e.Handled = regex.IsMatch(e.Text);
        //}

        //public void BindAllEducationLevelsToCombobox()
        //{
        //    var connect = new BOA.Connector.Banking.Connect();
        //    var request = new BOA.Types.Banking.EducationLevelRequest();
        //    //var contract = new BOA.Types.Banking.EducationLevelContract();


        //    request.MethodName = "getAllEducationLevels";


        //    var response = (ResponseBase)connect.Execute(request);

        //    if (response.IsSuccess)
        //    {
        //        var educationLevels = (List<EducationLevelContract>)response.DataContract;

        //        foreach (EducationLevelContract item in educationLevels)
        //        {
        //            cbEducationLvId.Items.Add(item.EducationLevel);
        //        }

        //    }
        //    else
        //    {
        //        MessageBox.Show(response.ErrorMessage);
        //    }
        //}

        //void BindAllJobsToCombobox()
        //{
        //    var connect = new BOA.Connector.Banking.Connect();
        //    var request = new BOA.Types.Banking.JobRequest();
        //    //var contract = new BOA.Types.Banking.JobContract();

        //    request.MethodName = "getAllJobs";


        //    var response = (ResponseBase)connect.Execute(request);

        //    if (response.IsSuccess)
        //    {
        //        var jobContracts = (List<JobContract>)response.DataContract;

        //        foreach(JobContract item in jobContracts)
        //        {
        //            cbJobId.Items.Add(item.JobName);
        //        }

        //    }
        //    else
        //    {
        //        MessageBox.Show(response.ErrorMessage);
        //    }
        //}

        //void ClearInputs()
        //{
        //    tbCustomerName.Clear();
        //    tbCitizenshipId.Clear();
        //    tbCustomerLastName.Clear();
        //    tbFatherName.Clear();
        //    tbMotherName.Clear();
        //    tbPlaceOfBirth.Clear();
        //    cbEducationLvId.Text = "";
        //    cbJobId.Text = "";
        //    //dpDateOfBirth.SelectedDate = new DateTime(null);

        //}

        //private void btnEmailPopOpen_Click(object sender, RoutedEventArgs e)
        //{
        //    popEmailAdd.IsOpen = true;
        //    if(_customerEmails == null)
        //    {
        //        _customerEmails = new List<CustomerEmailContract>();
        //    }
        //}

        //private void btnEmailPopClose_Click(object sender, RoutedEventArgs e)
        //{
        //    popEmailAdd.IsOpen = false;
        //}

        //private void btnPhoneAddToListbox_Click(object sender, RoutedEventArgs e)
        //{
        //    if(tbPhoneNumber.Text != "" && cbPhoneType.SelectedItem != null)
        //    {
        //        string phoneAndType = $"{tbPhoneNumber.Text} ( {cbPhoneType.SelectedItem} ) ";
        //        lbPhoneNumbers.Items.Add(phoneAndType);
        //        _customerPhones.Add(new CustomerPhoneContract() { CustomerId = null, CustomerPhoneId = null, PhoneNumber = tbPhoneNumber.Text, PhoneType = cbPhoneType.SelectedIndex });
        //        tbPhoneNumber.Text = "";
        //        cbPhoneType.SelectedIndex = -1;

        //    }
        //}

        //private void btnPhoneDeleteFromListbox_Click(object sender, RoutedEventArgs e)
        //{
        //    if (lbPhoneNumbers.SelectedIndex != -1) 
        //    {
        //        _customerPhones.RemoveAt(lbPhoneNumbers.SelectedIndex);
        //        lbPhoneNumbers.Items.RemoveAt(lbPhoneNumbers.SelectedIndex);

        //    }
        //}

        //private void btnEmailAddToListbox_Click(object sender, RoutedEventArgs e)
        //{
        //    if (tbEmail.Text != "" && cbEmailType.SelectedItem != null) //SelectedIndex -1 yazmak gerekiyor olabilir ikisine de bak
        //    {
        //        string emailAndType = $"{tbEmail.Text} ( {cbEmailType.SelectedItem} ) ";
        //        lbEmails.Items.Add(emailAndType);
        //        _customerEmails.Add(new CustomerEmailContract() { CustomerId = null, CustomerMailId = null, MailAdress = tbEmail.Text, EmailType = cbEmailType.SelectedIndex });
        //        tbEmail.Text = "";
        //        cbEmailType.SelectedIndex = -1;

        //    }
        //}

        //private void btnEmailDeleteFromListbox_Click(object sender, RoutedEventArgs e)
        //{
        //    if (lbEmails.SelectedIndex != -1)
        //    {
        //        _customerEmails.RemoveAt(lbEmails.SelectedIndex);
        //        lbEmails.Items.RemoveAt(lbEmails.SelectedIndex);

        //    }
        //}
    }
}
