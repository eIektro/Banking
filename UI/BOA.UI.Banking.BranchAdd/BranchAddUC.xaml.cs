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

namespace BOA.UI.Banking.BranchAdd
{
    /// <summary>
    /// Interaction logic for BranchAddUC.xaml
    /// </summary>
    public partial class BranchAddUC : UserControl
    {
        public Boolean isEditing = false;
        public BranchContract editingBranch;

        public BranchAddUC()
        {
            InitializeComponent();
            BindCbbox();
        }

        //public BranchAdd(BranchContract _editingBranch)
        //{
        //    InitializeComponent();
        //    isEditing = true;
        //    editingBranch = _editingBranch;
        //    tbBranchName.Text = editingBranch.BranchName;
        //    tbBranchAdress.Text = editingBranch.Adress;
        //    BindCbbox();
        //    cbCityId.SelectedIndex = (int)editingBranch.CityId;
        //    tbBranchEmailAdress.Text = editingBranch.MailAdress;
        //    tbBranchPhoneNumber.Text = editingBranch.PhoneNumber;
        //    disableUserInputs(true);
        //    btnDuzenle.Visibility = Visibility.Visible;
        //    btnKaydet.Visibility = Visibility.Hidden;
        //}

        public void disableUserInputs(bool wannaDisable)
        {
            tbBranchName.IsEnabled = !wannaDisable;
            tbBranchAdress.IsEnabled = !wannaDisable;
            cbCityId.IsEnabled = !wannaDisable;
            tbBranchEmailAdress.IsEnabled = !wannaDisable;
            tbBranchPhoneNumber.IsEnabled = !wannaDisable;
        }

        public void retrieveInputsAfterGiveUp()
        {
            tbBranchName.Text = editingBranch.BranchName;
            tbBranchAdress.Text = editingBranch.Adress;
            cbCityId.SelectedIndex = (int)editingBranch.CityId;
            tbBranchEmailAdress.Text = editingBranch.MailAdress;
            tbBranchPhoneNumber.Text = editingBranch.PhoneNumber;
            btnDuzenle.Visibility = Visibility.Visible;
            btnKaydet.Visibility = Visibility.Hidden;
            btnVazgec.Visibility = Visibility.Hidden;
        }

        public void BindCbbox()
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
                    cbCityId.Items.Add(x.name);
                }
            }
        }

        private void btnKaydet_Click(object sender, RoutedEventArgs e)
        {

            if (isEditing == true)
            {
                if (MessageBox.Show("Şube bilgileri güncellensin mi?", "Uyarı", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Connector.Banking.Connect connect = new Connector.Banking.Connect();
                    BranchRequest request = new BranchRequest();
                    request.DataContract = new BranchContract()
                    {
                        Id = editingBranch.Id,
                        Adress = tbBranchAdress.Text,
                        MailAdress = tbBranchEmailAdress.Text,
                        BranchName = tbBranchName.Text,
                        CityId = cbCityId.SelectedIndex,
                        PhoneNumber = tbBranchPhoneNumber.Text
                    };
                    request.MethodName = "UpdateBranchDetailsById";

                    var response = connect.Execute(request);

                    if (response.IsSuccess)
                    {
                        MessageBox.Show("Şube güncellenmiştir");
                        
                    }
                }
            }

            if (isEditing == false)
            {
                if (MessageBox.Show("Şube veri tabanına kaydedilsin mi?", "Uyarı", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    BranchContract yeniSube = new BranchContract()
                    {
                        BranchName = tbBranchName.Text,
                        Adress = tbBranchAdress.Text,
                        CityId = cbCityId.SelectedIndex,
                        DateOfLaunch = DateTime.Now,
                        MailAdress = tbBranchEmailAdress.Text,
                        PhoneNumber = tbBranchPhoneNumber.Text,

                    };


                    Connector.Banking.Connect connect = new Connector.Banking.Connect();
                    BranchRequest request = new BranchRequest();

                    request.DataContract = yeniSube;
                    request.MethodName = "AddNewBranch";

                    var response = connect.Execute(request);

                    if (response.IsSuccess)
                    {
                        var id = response.DataContract;

                        MessageBox.Show($"Şube {id} id'si ile oluşturuldu.");

                        ClearUserInputs();
                    }


                }
            }
        }

        private void ClearUserInputs()
        {
            tbBranchName.Text = "";
            tbBranchAdress.Text = "";
            cbCityId.SelectedIndex = -1;

            tbBranchEmailAdress.Text = "";
            tbBranchPhoneNumber.Text = "";
        }

        private void tbBranchPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnDuzenle_Click(object sender, RoutedEventArgs e)
        {
            disableUserInputs(false);
            btnKaydet.Visibility = Visibility.Visible;
            btnVazgec.Visibility = Visibility.Visible;
            btnDuzenle.Visibility = Visibility.Hidden;
        }

        private void btnVazgec_Click(object sender, RoutedEventArgs e)
        {
            disableUserInputs(true);
            retrieveInputsAfterGiveUp();
            btnVazgec.Visibility = Visibility.Hidden;
            btnKaydet.Visibility = Visibility.Hidden;
            btnDuzenle.Visibility = Visibility.Visible;
        }
    }
}
