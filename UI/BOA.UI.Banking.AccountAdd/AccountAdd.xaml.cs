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

namespace BOA.UI.Banking.AccountAdd
{
    /// <summary>
    /// Interaction logic for AccountAdd.xaml
    /// </summary>
    public partial class AccountAdd : Window
    {
        public AccountContract EditingAccount;
        public bool IsEditing;

        public AccountAdd()
        {
            InitializeComponent();
            BindCurrencyCB();
            FillBranchAutoCompleteBox();

        }

        public AccountAdd(AccountContract _editingAccount)
        {
            InitializeComponent();
            BindCurrencyCB();
            EditingAccount = _editingAccount;
            IsEditing = true;
            tbAdditionNo.Text = EditingAccount.AdditionNo.ToString();
            tbBalance.Text = EditingAccount.Balance.ToString();
            tbBranchID.Text = EditingAccount.BranchId.ToString();
            tbCustomerId.Text = EditingAccount.CustomerId.ToString();
            tbIBAN.Text = EditingAccount.IBAN.ToString();
            cbCurrencyId.SelectedIndex = EditingAccount.CurrencyId;
            cbIsActive.IsChecked = EditingAccount.IsActive;
            DisableUserInputs(true);
            btnDuzenle.Visibility = Visibility.Visible;
            btnKaydet.Visibility = Visibility.Hidden;
            btnVazgec.Visibility = Visibility.Hidden;
            


        }

        public void DisableUserInputs(bool WannaDisable)
        {
            tbAdditionNo.IsEnabled = !WannaDisable;
            tbBalance.IsEnabled = !WannaDisable;
            tbBranchID.IsEnabled = !WannaDisable;
            tbCustomerId.IsEnabled = !WannaDisable;
            tbIBAN.IsEnabled = !WannaDisable;
            cbCurrencyId.IsEnabled = !WannaDisable;
            cbIsActive.IsEnabled = !WannaDisable;
        }

        public void RetrieveDetailsAfterGiveUp()
        {
            tbAdditionNo.Text = EditingAccount.AdditionNo.ToString();
            tbBalance.Text = EditingAccount.Balance.ToString();
            tbBranchID.Text = EditingAccount.BranchId.ToString();
            tbCustomerId.Text = EditingAccount.CustomerId.ToString();
            tbIBAN.Text = EditingAccount.IBAN.ToString();
            cbCurrencyId.SelectedIndex = EditingAccount.CurrencyId;
            cbIsActive.IsChecked = EditingAccount.IsActive;
        }

        public void BindCurrencyCB()
        {
            var connect = new Connector.Banking.Connect();
            var request = new AccountRequest();

            request.MethodName = "GetAllCurrencies";

            var response = (ResponseBase)connect.Execute(request);

            if (response.IsSuccess)
            {
                var currencies = (List<CurrencyContract>)response.DataContract;

                foreach(CurrencyContract x in currencies)
                {
                    cbCurrencyId.Items.Add(x.code);
                }
            }
        }

        //public interface IYourViewModel
        //{
        //    IEnumerable<string> Names { get; set; }
        //    string SelectedName { get; set; }
        //}

        //public class myViewModel /*: IYourViewModel*/
        //{
        //    public IEnumerable<string> Names { get; set; }


        //    public string SelectedName { get; set; }


        //}

        public class BranchesViewModel
        {
            public List<BranchContract> Branches;
            public BranchContract SelectedBranch;
        }

        

        public void FillBranchAutoCompleteBox() {

            BranchesViewModel branchesViewModel = new BranchesViewModel();

            var connect = new BOA.Connector.Banking.Connect();
            var request = new BOA.Types.Banking.BranchRequest();

            request.MethodName = "GetAllBranches";

            var response = (ResponseBase)connect.Execute(request);


            if (response.IsSuccess)
            {
                var branches = (List<BranchContract>)response.DataContract;
                branchesViewModel.Branches = branches;
                acbBranchId.ItemsSource = branchesViewModel.Branches;
            }
            else
            {

            }

            //myViewModel myViewModel = new myViewModel();
            //string emre = "Emre";
            //string ezgi = "Ezgi";
            //string ferdi = "Ferdi";
            //myViewModel.Names = new List<string> { emre,ezgi,ferdi };
            //acbBranchId.ItemsSource = myViewModel.Names;

        }

        private void btnKaydet_Click(object sender, RoutedEventArgs e)
        {

            if (IsEditing)
            {
                if (MessageBox.Show("Yaptığınız değişlikler hesaba yansısın mı?", "Tasdik", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    decimal balance;
                    bool parsedOk = decimal.TryParse(tbBalance.Text, out balance);

                    var connect = new Connector.Banking.Connect();
                    var request = new AccountRequest();
                    request.MethodName = "UpdateAccountDetailsById";
                    request.DataContract = new AccountContract()
                    {
                        Id = EditingAccount.Id,
                        CustomerId = Convert.ToInt32(tbCustomerId.Text),
                        AdditionNo = Convert.ToInt32(tbAdditionNo.Text),
                        CurrencyId = cbCurrencyId.SelectedIndex,
                        Balance = balance,
                        BranchId = (acbBranchId.SelectedItem as BranchContract).Id,
                        IBAN = tbIBAN.Text,
                        IsActive = (bool)cbIsActive.IsChecked,

                    };

                    if ((bool)!cbIsActive.IsChecked)
                    {
                        request.DataContract.DateOfDeactivation = DateTime.Now;
                        
                    }                   

                    var response = connect.Execute(request);

                    if (response.IsSuccess)
                    {
                        MessageBox.Show("Detaylar güncellendi.", "Bilgilendirme", MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();
                    }
                }
            }

            if (IsEditing == false)
            {
                if (MessageBox.Show("Bilgileri girmiş olduğunuz hesap oluşturulsun mu?", "Uyarı", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    decimal balance;
                    bool parsedOk = decimal.TryParse(tbBalance.Text, out balance);

                    var connect = new Connector.Banking.Connect();
                    var request = new AccountRequest();
                    request.MethodName = "AddNewAccount";
                    request.DataContract = new AccountContract()
                    {
                        FormedUserId = Login.LoginScreen._userId,
                        DateOfFormation = DateTime.Now,
                        AdditionNo = Convert.ToInt32(tbAdditionNo.Text),
                        IBAN = tbIBAN.Text,
                        Balance = balance,
                        BranchId = (acbBranchId.SelectedItem as BranchContract).Id,
                        CurrencyId = cbCurrencyId.SelectedIndex,
                        CustomerId = Convert.ToInt32(tbCustomerId.Text),
                        IsActive = (bool)cbIsActive.IsChecked,

                    };

                    var response = connect.Execute(request);

                    if (response.IsSuccess)
                    {
                        MessageBox.Show("Hesap kaydetme işlemi başarılı", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();

                    }
                } 
            }

        }

        private void tbBalance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnDuzenle_Click(object sender, RoutedEventArgs e)
        {
            btnVazgec.Visibility = Visibility.Visible;
            btnKaydet.Visibility = Visibility.Visible;
            btnDuzenle.Visibility = Visibility.Hidden;
            DisableUserInputs(false);
        }

        private void btnVazgec_Click(object sender, RoutedEventArgs e)
        {
            btnVazgec.Visibility = Visibility.Hidden;
            btnDuzenle.Visibility = Visibility.Visible;
            btnKaydet.Visibility = Visibility.Hidden;
            DisableUserInputs(true);
            RetrieveDetailsAfterGiveUp();
        }
    }
}
