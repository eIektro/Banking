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

namespace BOA.UI.Banking.AccountList
{
    /// <summary>
    /// Interaction logic for AccountListUC.xaml
    /// </summary>
    public partial class AccountListUC : UserControl
    {
        public AccountContract SelectedAccount;
        List<AccountContract> accounts;

        public AccountListUC()
        {
            InitializeComponent();
            BindCurrencyCB();
            BindAccountsToGrid();
        }

        private void btnHesapEkle_Click(object sender, RoutedEventArgs e)
        {
            AccountAdd.AccountAdd accountAdd = new AccountAdd.AccountAdd();
            accountAdd.ShowDialog();
            BindAccountsToGrid();
        }

        public void BindAccountsToGrid()
        {
            var connect = new Connector.Banking.Connect();
            var request = new AccountRequest();

            request.MethodName = "GetAllAccounts";
            var response = connect.Execute(request);

            if (response.IsSuccess)
            {
                accounts = (List<AccountContract>)response.DataContract;

                dgAccountList.ItemsSource = accounts;
            }
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

                foreach (CurrencyContract x in currencies)
                {
                    cbFilterbyCurrencyId.Items.Add(x.code);
                }
            }
        }

        private List<AccountContract> SearchEngine(object [] _parameters)
        {

            if(Convert.ToInt32(_parameters[3].ToString()) != -1)
            {
                var resultWithSpesificParameter = (List<AccountContract>)accounts.Where(x => x.CurrencyId == Convert.ToInt32(_parameters[3].ToString())).ToList();
                return resultWithSpesificParameter;
            }

            if(_parameters[0] == "" && _parameters[1] == "" && _parameters[2] == "" && _parameters[5] == "" && _parameters[7] == "")
            {
                var resultsNotFiltered = (List<AccountContract>)accounts;
                return resultsNotFiltered;
            }

            Func<AccountContract, bool> predicate = x => x.CustomerId.ToString() == _parameters[1].ToString() || x.BranchId.ToString() == _parameters[0].ToString();
            var result = (List<AccountContract>)accounts.Where(predicate)
                                                        .ToList();
            return result;
            

            //var result = accounts.FindAll(x =>
            
            //x.BranchId.ToString().Contains(_subeid.ToString()) &&
            //x.CustomerId.ToString().Contains( _musteriid.ToString()) &&
            //x.DateOfDeactivation >= (DateTime)_olusturulmatarihi &&
            //x.IBAN.Contains(_IBAN.ToString()));

            //return result;


        //    int subeid = -1;
        //    int musteriid = -1;
        //    int dovizcinsi = (int)_dovizcinsi;
        //    int olusturanid = -1;
        //    decimal bakiye = -1;
        //    DateTime olusturulmatarihi = (DateTime)_olusturulmatarihi;
        //    string IBAN = (string)_IBAN;
        //    bool aktifMi = (bool)_aktifMi;

        //    if ((string)_subeid != "")
        //    {
        //        subeid = Convert.ToInt32(_subeid); 
        //    }
        //    if ((string)_musteriid != "")
        //    {
        //        musteriid = Convert.ToInt32(_musteriid); 
        //    }
        //    if ((string)_bakiye != "")
        //    {
        //        bakiye = Convert.ToDecimal(_bakiye); 
        //    }
        //    if ((string)_olusturanid != "")
        //    {
        //        olusturanid = Convert.ToInt32(_olusturanid); 
        //    }

        //    if(olusturulmatarihi == default)
        //    {
        //        var datedResult = accounts.FindAll(x => x.BranchId == subeid || x.CustomerId == musteriid || x.Balance == bakiye || x.CurrencyId == dovizcinsi ||
        //                                  x.IBAN.ToLower().Contains(IBAN.ToLower()) || x.IsActive == aktifMi || x.FormedUserId == olusturanid);
        //        return datedResult;
        //    }

        //    var result = accounts.FindAll(x => x.BranchId == subeid || x.CustomerId == musteriid || x.Balance == bakiye || x.CurrencyId == dovizcinsi || x.DateOfFormation >= olusturulmatarihi ||
        //                                  x.IBAN.ToLower().Contains(IBAN.ToLower()) || x.IsActive == aktifMi || x.FormedUserId == olusturanid);


        //    return result;
        }
        private List<AccountContract> SearchEngine(int id) //TO-DO: refactor edilecek, zaten id primary key olduğundan tek item dönüyor
        {
            var result = accounts.FindAll(x => x.Id == id);
            return result;
        }
        private void dgAccountList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedAccount = (AccountContract)dgAccountList.SelectedItem;
        }

        private void btnHesapDetay_Click(object sender, RoutedEventArgs e)
        {
            AccountAdd.AccountAdd accountAdd = new AccountAdd.AccountAdd(SelectedAccount);
            accountAdd.ShowDialog();
            BindAccountsToGrid();
        }

        private void btnFiltrele_Click(object sender, RoutedEventArgs e)
        {
            if (tbFilterbyId.Text != "")
            {
                int id = Convert.ToInt32(tbFilterbyId.Text);
                dgAccountList.ItemsSource = SearchEngine(id);
            }
            else
            {
                dgAccountList.ItemsSource = SearchEngine( new object[] {tbFilterbyBranchId.Text,tbFilterbyCustomerId.Text, tbFilterbyBalance.Text,
                    cbFilterbyCurrencyId.SelectedIndex, dpFilterbyDateOfFormation.SelectedDate.GetValueOrDefault(),tbFilterbyIban.Text,(bool)cbIsActive.IsChecked,tbFilterbyFormedUserId.Text});
            }
        }

        private void tbFilterbyId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbFilterbyBranchId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbFilterbyCustomerId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbFilterbyBalance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbFilterbyFormedUserId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnHesapSil_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
