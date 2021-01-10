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

namespace BOA.UI.Banking.BranchList
{
    /// <summary>
    /// Interaction logic for BranchListUC.xaml
    /// </summary>
    public partial class BranchListUC : UserControl, INotifyPropertyChanged
    {
        public BranchListUC()
        {
            var _AllBranchesResponse = GetAllBranchs();
            if (_AllBranchesResponse.IsSuccess)
            {
                Branches = (List<BranchContract>)_AllBranchesResponse.DataContract;
            }
            else
            {
                MessageBox.Show($"{_AllBranchesResponse.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            var _AllCitiesResponse = GetAllCities();
            if (_AllBranchesResponse.IsSuccess)
            {
                Cities = (List<CityContract>)_AllCitiesResponse.DataContract;
            }
            else
            {
                MessageBox.Show($"{_AllCitiesResponse.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            InitializeComponent();
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FilterContract = new BranchContract();
        }

        private BranchContract _FilterContract;
        public BranchContract FilterContract
        {
            get { return this._FilterContract; }
            set
            {
                this._FilterContract = value;
                OnPropertyChanged("FilterContract");
            }
        }

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

        private BranchContract _SelectedBranch;
        public BranchContract SelectedBranch
        {
            get { return this._SelectedBranch; }
            set
            {
                this._SelectedBranch = value;
                OnPropertyChanged("SelectedBranch");
            }
        }

        private ResponseBase GetAllCities()
        {
            var connect = new Connector.Banking.Connect();
            var request = new BranchRequest();
            request.MethodName = "getAllCities";
            var response = connect.Execute(request);
            return response;
        }

        private List<BranchContract> _Branches;
        public List<BranchContract> Branches
        {
            get { return this._Branches; }
            set
            {
                this._Branches = value;
                OnPropertyChanged("Branches");
            }
        }

        private ResponseBase GetAllBranchs()
        {
            var connect = new Connector.Banking.Connect();
            var request = new BranchRequest();
            request.MethodName = "GetAllBranches";
            var response = connect.Execute(request);
            return response;
        }

        #region Event Handling
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private void btnSubeEkle_Click(object sender, RoutedEventArgs e)
        {
            BranchAdd.BranchAdd branchAdd = new BranchAdd.BranchAdd();
            branchAdd.ShowDialog();
        }

        private void btnSubeDetay_Click(object sender, RoutedEventArgs e)
        {
            if (dgBranchList.SelectedItem == null) return;
            SelectedBranch = (BranchContract)dgBranchList.SelectedItem;
            BranchAdd.BranchAdd branchAdd = new BranchAdd.BranchAdd(SelectedBranch);
            branchAdd.ShowDialog();
        }

        private void btnFiltrele_Click(object sender, RoutedEventArgs e)
        {
            var response = FilterEngine(FilterContract);
            if (response.IsSuccess)
            {
                var responseBranches = (List<BranchContract>)response.DataContract;
                Branches = responseBranches;
            }
            else { MessageBox.Show($"{response.ErrorMessage}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error); }
            //int? id = null;
            //int? cityid = null;


            //if (tbFilterbyId.Text != "")
            //{
            //    id = Convert.ToInt32(tbFilterbyId.Text);
            //}

            //if (cbFilterbyCityId.SelectedIndex != -1)
            //{
            //    cityid = cbFilterbyCityId.SelectedIndex;
            //}

            //BranchContract branchProperties = new BranchContract()
            //{
            //    Id = id,
            //    CityId = cityid,
            //    BranchName = tbFilterbyName.Text,
            //    DateOfLaunch = dpFilterbyDateOfLaunch.SelectedDate.GetValueOrDefault(),
            //    MailAdress = tbFilterbyEmail.Text,
            //    PhoneNumber = tbFilterbyPhoneNumber.Text,
            //    Adress = tbFilterbyAdress.Text
            //};

            //dgBranchList.ItemsSource = FilterEngine(branchProperties);

            //if (tbFilterbyId.Text != "")
            //{
            //    int id = Convert.ToInt32(tbFilterbyId.Text);
            //    dgBranchList.ItemsSource = SearchEngine(id);
            //}
            //else
            //{
            //    dgBranchList.ItemsSource = SearchEngine(tbFilterbyName.Text, tbFilterbyEmail.Text, tbFilterbyPhoneNumber.Text, cbFilterbyCityId.SelectedIndex, dpFilterbyDateOfLaunch.SelectedDate.GetValueOrDefault());
            //}
        }


        private ResponseBase FilterEngine(BranchContract _contract)
        {
            var connect = new Connector.Banking.Connect();
            var request = new BranchRequest();

            request.MethodName = "FilterBranchsByProperties";
            request.DataContract = _contract;

            var response = connect.Execute(request);
            return response;
        }

        //private List<BranchContract> SearchEngine(string name = "", string email = "", string phonenumber = "", int cityid = default, DateTime dateoflaunch = default)
        //{
        //    if (cityid != -1)
        //    {
        //        var search = branchList.FindAll(x => x.BranchName.ToLower().Contains(name.ToLower()) && x.PhoneNumber.ToLower().Contains(phonenumber.ToLower()) && x.MailAdress.ToLower().Contains(email.ToLower()) && x.CityId == cityid
        //        && x.DateOfLaunch >= dateoflaunch);
        //        return search;
        //    }

        //    var result = branchList.FindAll(x => x.BranchName.ToLower().Contains(name.ToLower()) && x.PhoneNumber.ToLower().Contains(phonenumber.ToLower())
        //    && x.MailAdress.ToLower().Contains(email.ToLower())/*x.CitizenshipId.ToLower().Contains(citizenshipid.ToLower()) && x.DateOfBirth >= dateofbirth*/ /* && x.DateOfBirth.ToString("dd.MM.yyyy").Contains(dateofbirth)*/);
        //    return result;
        //}
        //private List<BranchContract> SearchEngine(int id) //TO-DO: refactor edilecek, zaten id primary key olduğundan tek item dönüyor
        //{
        //    var result = branchList.FindAll(x => x.Id == id);
        //    return result;
        //}

        private void btnSubeSil_Click(object sender, RoutedEventArgs e)
        {
            //if (selectedBranch != null)
            //{
            //    if (MessageBox.Show($"{selectedBranch.BranchName} ({selectedBranch.Id}) bilgilerine sahip şube veritabanından silinsin mi? \n (BU ŞUBEYE BAĞLI BÜTÜN HESAPLAR PASİF OLACAKTIR.)", "Silme Uyarısı", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            //    {

            //        BOA.Connector.Banking.Connect connect = new Connector.Banking.Connect();
            //        BranchRequest request = new BranchRequest();
            //        request.MethodName = "DeleteBranchById";
            //        request.DataContract = selectedBranch;
            //        var response = connect.Execute(request);

            //        if (response.IsSuccess)
            //        {
            //            MessageBox.Show("Silme işlemi başarılı", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
            //            //BindGrid();
            //        }


            //    }
            //}
        }

        private void btnTemizle_Click(object sender, RoutedEventArgs e)
        {
            FilterContract = new BranchContract();
        }

    }
}
