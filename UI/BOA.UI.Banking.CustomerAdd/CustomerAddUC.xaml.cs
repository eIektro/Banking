using BOA.Types.Banking;
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
    /// Interaction logic for CustomerAddUC.xaml
    /// </summary>
    public partial class CustomerAddUC : UserControl, INotifyPropertyChanged
    {
        private bool IsEditingOption = false;

        public CustomerAddUC()
        {

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
            
        }

        public CustomerAddUC(CustomerContract contract)
        {
            customerContract = contract;
            IsEditingOption = true;

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

            SetUserInputsEditableFunction(true);
            SetVisibilitiesForDetailOption();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsEditingOption)
            {
                customerContract = new CustomerContract(); 
            }
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

        private CustomerContract customercontract;
        public CustomerContract customerContract
        {
            get { return this.customercontract; }
            set
            {
                this.customercontract = value;
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

        private ResponseBase AddCustomer(CustomerContract _contract)
        {
            var connect = new Connector.Banking.Connect();
            var request = new CustomerRequest();

            request.MethodName = "CustomerAdd";
            request.DataContract = _contract;

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

        #region regex operations
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
            if (IsEditingOption)
            {
                if (MessageBox.Show("Yaptığınız değişlikler müşteri bilgilerine yansısın mı?", "Onay", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var responseUpdate = UpdateCustomer(customerContract);
                    if (responseUpdate.IsSuccess)
                    {
                        MessageBox.Show("Değişiklikler uygulandı!", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
                        btnVazgec_Click(new object(), new RoutedEventArgs());

                    }
                    else
                    {
                        MessageBox.Show($"{responseUpdate.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                return;
            }



            if (MessageBox.Show("Bilgilerini girdiğiniz müşteri veritabanına kaydedilsin mi?", "Kayıt", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var response = AddCustomer(customerContract);
                if (response.IsSuccess)
                {
                    MessageBox.Show($"Müşteri ekleme işlemi başarılı", "Bilgilendirme", MessageBoxButton.OK, MessageBoxImage.Information);
                    customerContract = new CustomerContract();
                }
                else { MessageBox.Show($"{response.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error); }
            }

            else
            {
                MessageBox.Show("Gerekli alanları doldurunuz", "Bilgilendirme", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            if (customerContract.PhoneNumbers == null) customerContract.PhoneNumbers = new List<CustomerPhoneContract>();
            customerContract.PhoneNumbers.Add(new CustomerPhoneContract() { CustomerId = customerContract.CustomerId, PhoneNumber = tbPhoneNumber.Text, PhoneType = cbPhoneType.SelectedIndex });

        }

        private void btnPhoneDeleteFromDataGrid_Click(object sender, RoutedEventArgs e)
        {
            if (dgPhoneNumbers.SelectedItem != null)
            {
                customerContract.PhoneNumbers.Remove((CustomerPhoneContract)dgPhoneNumbers.SelectedItem);
            }
        }

        private void btnEmailAddToDataGrid_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEmailDeleteFromDataGrid_Click(object sender, RoutedEventArgs e)
        {

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

        public void SetVisibilitiesForDetailOption()
        {
            btnKaydet.Visibility = Visibility.Hidden;
            btnVazgec.Visibility = Visibility.Hidden;
            btnDuzenle.Visibility = Visibility.Visible;
        }

    }
}
