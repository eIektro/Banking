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

namespace BOA.UI.Banking.BranchList
{
    /// <summary>
    /// Interaction logic for BranchListUC.xaml
    /// </summary>
    public partial class BranchListUC : UserControl
    {
        List<BranchContract> branchList;
        BranchContract selectedBranch;

        public BranchListUC()
        {
            InitializeComponent();
            BindGrid();
            BindCities();
        }

        private void btnSubeEkle_Click(object sender, RoutedEventArgs e)
        {
            BranchAdd.BranchAdd branchAdd = new BranchAdd.BranchAdd();
            branchAdd.ShowDialog();
        }

        private void BindGrid()
        {
            var connect = new BOA.Connector.Banking.Connect();
            var request = new BOA.Types.Banking.BranchRequest();

            request.MethodName = "GetAllBranches";

            var response = (ResponseBase)connect.Execute(request);


            if (response.IsSuccess)
            {
                branchList = (List<BranchContract>)response.DataContract;
                dgBranchList.ItemsSource = branchList;//sp den like ile - ui'da filtreleme olmayacak
            }
            else
            {

            }
        }

        private void BindCities()
        {
            Connector.Banking.Connect connect = new Connector.Banking.Connect();
            BranchRequest request = new BranchRequest();

            request.MethodName = "getAllCities";

            var response = connect.Execute(request);

            if (response.IsSuccess)
            {
                var cities = (List<CityContract>)response.DataContract;
                foreach (CityContract x in cities)
                {
                    cbFilterbyCityId.Items.Add(x.name);
                }
            }
        }

        private void btnSubeDetay_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBranch != null)
            {
                BranchAdd.BranchAdd branchAdd = new BranchAdd.BranchAdd(selectedBranch);
                branchAdd.ShowDialog();
                BindGrid();

            }
        }

        private void dgBranchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedBranch = (BranchContract)dgBranchList.SelectedItem;
        }

        private void btnFiltrele_Click(object sender, RoutedEventArgs e)
        {
            if (tbFilterbyId.Text != "")
            {
                int id = Convert.ToInt32(tbFilterbyId.Text);
                dgBranchList.ItemsSource = SearchEngine(id);
            }
            else
            {
                dgBranchList.ItemsSource = SearchEngine(tbFilterbyName.Text, tbFilterbyEmail.Text, tbFilterbyPhoneNumber.Text, cbFilterbyCityId.SelectedIndex, dpFilterbyDateOfLaunch.SelectedDate.GetValueOrDefault());
            }
        }

        private List<BranchContract> SearchEngine(string name = "", string email = "", string phonenumber = "", int cityid = default, DateTime dateoflaunch = default)
        {
            if (cityid != -1)
            {
                var search = branchList.FindAll(x => x.BranchName.ToLower().Contains(name.ToLower()) && x.PhoneNumber.ToLower().Contains(phonenumber.ToLower()) && x.MailAdress.ToLower().Contains(email.ToLower()) && x.CityId == cityid
                && x.DateOfLaunch >= dateoflaunch);
                return search;
            }

            var result = branchList.FindAll(x => x.BranchName.ToLower().Contains(name.ToLower()) && x.PhoneNumber.ToLower().Contains(phonenumber.ToLower())
            && x.MailAdress.ToLower().Contains(email.ToLower())/*x.CitizenshipId.ToLower().Contains(citizenshipid.ToLower()) && x.DateOfBirth >= dateofbirth*/ /* && x.DateOfBirth.ToString("dd.MM.yyyy").Contains(dateofbirth)*/);
            return result;
        }
        private List<BranchContract> SearchEngine(int id) //TO-DO: refactor edilecek, zaten id primary key olduğundan tek item dönüyor
        {
            var result = branchList.FindAll(x => x.Id == id);
            return result;
        }

        private void btnSubeSil_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBranch != null)
            {
                if (MessageBox.Show($"{selectedBranch.BranchName} ({selectedBranch.Id}) bilgilerine sahip şube veritabanından silinsin mi? \n (BU ŞUBEYE BAĞLI BÜTÜN HESAPLAR PASİF OLACAKTIR.)", "Silme Uyarısı", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {

                    BOA.Connector.Banking.Connect connect = new Connector.Banking.Connect();
                    BranchRequest request = new BranchRequest();
                    request.MethodName = "DeleteBranchById";
                    request.DataContract = selectedBranch;
                    var response = connect.Execute(request);

                    if (response.IsSuccess)
                    {
                        MessageBox.Show("Silme işlemi başarılı", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                        BindGrid();
                    }


                }
            }
        }


    }
}
