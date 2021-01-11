using BOA.Types.Banking;
using BOA.Types.Banking.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BOA.UI.Banking.CustomerAdd
{
    /// <summary>
    /// Interaction logic for CustomerAdd.xaml
    /// </summary>
    public partial class CustomerAdd : Window,INotifyPropertyChanged
    {
        
        //public CustomerAdd()
        //{
        //    InitializeComponent();
        //    BindAllJobsToCombobox();
        //    BindAllEducationLevelsToCombobox();
        //}

        public CustomerAdd(CustomerContract _selectedCustomer)
        {
            customerContract = _selectedCustomer;

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
            #endregion

            InitializeComponent();
            
            btnEmailPopOpen.Content = "Mail Adresleri";
            btnPhonePopOpen.Content = "Telefon Numaraları";
            SetUserInputsEditableFunction(true);
            btnDuzenle.Visibility = Visibility.Visible;
            btnKaydet.Visibility = Visibility.Hidden;
            this.Title = "Müşteri Bilgisi Düzenle";
            
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

        private CustomerContract _customerContract;
        public CustomerContract customerContract
        {
            get { return this._customerContract; }
            set
            {
                this._customerContract = value;
                OnPropertyChanged("customerContract");
            }
        }
        #endregion

        #region database operations
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

        private ResponseBase UpdateCustomer(CustomerContract _contract)
        {
            var connect = new Connector.Banking.Connect();
            var request = new CustomerRequest();

            request.MethodName = "UpdateCustomerbyId";
            request.DataContract = _contract;

            var response = connect.Execute(request);

            return response;
        }
        #endregion

        #region regex
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

        private void tbPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        } 
        #endregion

        #region button operations
        private void btnKaydet_Click(object sender, RoutedEventArgs e)
        {
            var response = UpdateCustomer(customerContract);
            if (response.IsSuccess)
            {
                btnKaydet.Visibility = Visibility.Hidden;
                btnVazgec.Visibility = Visibility.Hidden;
                btnDuzenle.Visibility = Visibility.Visible;
                MessageBox.Show("Seçilen müşteri için bilgiler güncellendi", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            else
            {
                MessageBox.Show($"Bilgiler güncellenemedi. {response.ErrorMessage} ", "Başarısız", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private void btnPhonePopClose_Click(object sender, RoutedEventArgs e)
        {
            popPhoneAdd.IsOpen = false;
        }

        private void btnPhonePopOpen_Click(object sender, RoutedEventArgs e)
        {
            popPhoneAdd.IsOpen = true;

        }

        private void btnEmailPopOpen_Click(object sender, RoutedEventArgs e)
        {
            popEmailAdd.IsOpen = true;
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
        #endregion

        #region user inputs
        public void SetUserInputsEditableFunction(Boolean WannaDisable)
        {

            cbJobId.IsHitTestVisible = !WannaDisable;
            cbEducationLvId.IsHitTestVisible = !WannaDisable;
            tbCustomerName.IsReadOnly = WannaDisable;
            tbCitizenshipId.IsReadOnly = WannaDisable;
            tbCustomerLastName.IsReadOnly = WannaDisable;
            tbFatherName.IsReadOnly = WannaDisable;
            tbMotherName.IsReadOnly = WannaDisable;
            tbPlaceOfBirth.IsReadOnly = WannaDisable;
            dpDateOfBirth.IsHitTestVisible = !WannaDisable;
            btnEmailAddToDataGrid.IsHitTestVisible = !WannaDisable;
            btnPhoneAddToDataGrid.IsHitTestVisible = !WannaDisable;
            cbPhoneType.IsHitTestVisible = !WannaDisable;
            tbPhoneNumber.IsReadOnly = WannaDisable;
            tbEmail.IsReadOnly = WannaDisable;
            cbPhoneType.IsHitTestVisible = !WannaDisable;
            btnEmailDeleteFromDataGrid.IsHitTestVisible = !WannaDisable;
            btnPhoneDeleteFromDataGrid.IsHitTestVisible = !WannaDisable;
            cbEmailType.IsHitTestVisible = !WannaDisable;

        } 
        #endregion
    }
}
