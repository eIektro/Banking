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

namespace BOA.UI.Banking.BranchAdd
{
    /// <summary>
    /// Interaction logic for BranchAddUC.xaml
    /// </summary>
    public partial class BranchAddUC : UserControl, INotifyPropertyChanged
    {
        public BranchAddUC()
        {
            #region responses
            var _AllCitiesResponse = GetAllCities();
            if (_AllCitiesResponse.IsSuccess)
            {
                Cities = (List<CityContract>)_AllCitiesResponse.DataContract;
            }
            else
            {
                MessageBox.Show($"{_AllCitiesResponse.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
            #endregion

            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Branch = new BranchContract();
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
        private List<CityContract> _Cities;
        public List<CityContract> Cities
        {
            get { return this._Cities; }
            set
            {
                this._Cities = value;
                OnPropertyChanged("Cities");
            }
        }

        private BranchContract _Branch;
        public BranchContract Branch
        {
            get { return this._Branch; }
            set
            {
                this._Branch = value;
                OnPropertyChanged("Branch");
            }
        }
        #endregion

        #region db operations
        private ResponseBase AddBranch(BranchContract _contract)
        {
            var connect = new Connector.Banking.Connect();
            var request = new BranchRequest();

            request.MethodName = "AddNewBranch";
            request.DataContract = _contract;

            var response = connect.Execute(request);

            return response;
        }

        private ResponseBase GetAllCities()
        {
            var connect = new Connector.Banking.Connect();
            var request = new BranchRequest();
            request.MethodName = "getAllCities";
            var response = connect.Execute(request);
            return response;
        }
        #endregion

        #region regex
        private void tbBranchPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion

        #region button operations
        private void btnKaydet_Click(object sender, RoutedEventArgs e)
        {
            var response = AddBranch(Branch);
            if (response.IsSuccess)
            {
                MessageBox.Show($"Şube eklendi!", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
                Branch = new BranchContract();
            }
            else { MessageBox.Show($"{response.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error); }
        } 
        #endregion

        //public void disableUserInputs(bool wannaDisable)
        //{
        //    tbBranchName.IsEnabled = !wannaDisable;
        //    tbBranchAdress.IsEnabled = !wannaDisable;
        //    cbCityId.IsEnabled = !wannaDisable;
        //    tbBranchEmailAdress.IsEnabled = !wannaDisable;
        //    tbBranchPhoneNumber.IsEnabled = !wannaDisable;
        //}
    }
}
