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
        private TabControl MainWindowTabControl { get; set; }

        public BranchListUC(TabControl tabControl)
        {
            MainWindowTabControl = tabControl;

            #region responses
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
            #endregion

            InitializeComponent();
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FilterContract = new BranchContract();
        }

        #region Event Handling
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Getters and Setters
        private BranchContract filtercontract;
        public BranchContract FilterContract
        {
            get { return this.filtercontract; }
            set
            {
                this.filtercontract = value;
                OnPropertyChanged("FilterContract");
            }
        }

        private List<CityContract> cities;
        public List<CityContract> Cities
        {
            get { return this.cities; }
            set
            {
                this.cities = value;
                OnPropertyChanged("Cities");
            }
        }

        private BranchContract selectedbranch;
        public BranchContract SelectedBranch
        {
            get { return this.selectedbranch; }
            set
            {
                this.selectedbranch = value;
                OnPropertyChanged("SelectedBranch");
            }
        }

        private List<BranchContract> branches;
        public List<BranchContract> Branches
        {
            get { return this.branches; }
            set
            {
                this.branches = value;
                OnPropertyChanged("Branches");
            }
        }
        #endregion

        #region Db Operations
        private ResponseBase GetAllBranchs()
        {
            var connect = new Connector.Banking.Connect();
            var request = new BranchRequest();
            request.MethodName = "GetAllBranches";
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

        private ResponseBase FilterEngine(BranchContract _contract)
        {
            var connect = new Connector.Banking.Connect();
            var request = new BranchRequest();

            request.MethodName = "FilterBranchsByProperties";
            request.DataContract = _contract;

            var response = connect.Execute(request);
            return response;
        } 
        #endregion

        #region Button Operations
        
        private void btnSubeDetay_Click(object sender, RoutedEventArgs e)
        {
            if (dgBranchList.SelectedItem == null) return;
            if (MainWindowTabControl == null) return;

            SelectedBranch = (BranchContract)dgBranchList.SelectedItem;

            BranchAdd.BranchAddUC branchAdd = new BranchAdd.BranchAddUC(SelectedBranch);
            CloseableTab.CloseableTab theTabItem = new CloseableTab.CloseableTab();
            theTabItem.Title = $"Şube Detayları - {SelectedBranch.BranchName}";
            theTabItem.Content = branchAdd;
            MainWindowTabControl.Items.Add(theTabItem);
            theTabItem.Focus();
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
        }

        private void btnSubeSil_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTemizle_Click(object sender, RoutedEventArgs e)
        {
            FilterContract = new BranchContract();
        } 
        #endregion

    }
}
