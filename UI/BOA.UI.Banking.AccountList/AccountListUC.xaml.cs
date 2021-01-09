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
        List<AccountContract> AllAccounts;

        public AccountListUC()
        {
            InitializeComponent();
            
           
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BindAccountsToGrid();
            BindCurrencyCB();
            BindBranchsCB();
            BindUsersCB();
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
                AllAccounts = (List<AccountContract>)response.DataContract;

                dgAccountList.ItemsSource = AllAccounts;
            }
        }

        public void BindUsersCB()
        {
            var connect = new Connector.Banking.Connect();
            var request = new LoginRequest();

            request.MethodName = "GetAllUsers";

            var response = (ResponseBase)connect.Execute(request);

            if (response.IsSuccess)
            {
                var users = (List<LoginContract>)response.DataContract;

                dgccFormedUser.ItemsSource = users;

                foreach (LoginContract x in users)
                {
                    cbFilterbyFormedUserId.Items.Add(x.LoginName);
                }
            }
        }

        public void BindBranchsCB()
        {
            var connect = new Connector.Banking.Connect();
            var request = new BranchRequest();

            request.MethodName = "GetAllBranches";

            var response = (ResponseBase)connect.Execute(request);

            if (response.IsSuccess)
            {
                var branches = (List<BranchContract>)response.DataContract;

                dgccBranch.ItemsSource = branches;

                foreach (BranchContract x in branches)
                {
                    cbFilterByBranchId.Items.Add(x.BranchName); 
                }
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

                dgccCurrency.ItemsSource = currencies;

                foreach (CurrencyContract x in currencies)
                {
                    cbFilterbyCurrencyId.Items.Add(x.code);
                }
            }
        }

        //private List<AccountContract> SearchEngine(object [] _parameters)
        //{

        //    if(Convert.ToInt32(_parameters[3].ToString()) != -1)
        //    {
        //        var resultWithSpesificParameter = (List<AccountContract>)accounts.Where(x => x.CurrencyId == Convert.ToInt32(_parameters[3].ToString())).ToList();
        //        return resultWithSpesificParameter;
        //    }

        //    if(_parameters[0] == "" && _parameters[1] == "" && _parameters[2] == "" && _parameters[5] == "" && _parameters[7] == "")
        //    {
        //        var resultsNotFiltered = (List<AccountContract>)accounts;
        //        return resultsNotFiltered;
        //    }

        //    Func<AccountContract, bool> predicate = x => x.CustomerId.ToString() == _parameters[1].ToString() || x.BranchId.ToString() == _parameters[0].ToString();
        //    var result = (List<AccountContract>)accounts.Where(predicate)
        //                                                .ToList();
        //    return result;
            

        //    //var result = accounts.FindAll(x =>
            
        //    //x.BranchId.ToString().Contains(_subeid.ToString()) &&
        //    //x.CustomerId.ToString().Contains( _musteriid.ToString()) &&
        //    //x.DateOfDeactivation >= (DateTime)_olusturulmatarihi &&
        //    //x.IBAN.Contains(_IBAN.ToString()));

        //    //return result;


        ////    int subeid = -1;
        ////    int musteriid = -1;
        ////    int dovizcinsi = (int)_dovizcinsi;
        ////    int olusturanid = -1;
        ////    decimal bakiye = -1;
        ////    DateTime olusturulmatarihi = (DateTime)_olusturulmatarihi;
        ////    string IBAN = (string)_IBAN;
        ////    bool aktifMi = (bool)_aktifMi;

        ////    if ((string)_subeid != "")
        ////    {
        ////        subeid = Convert.ToInt32(_subeid); 
        ////    }
        ////    if ((string)_musteriid != "")
        ////    {
        ////        musteriid = Convert.ToInt32(_musteriid); 
        ////    }
        ////    if ((string)_bakiye != "")
        ////    {
        ////        bakiye = Convert.ToDecimal(_bakiye); 
        ////    }
        ////    if ((string)_olusturanid != "")
        ////    {
        ////        olusturanid = Convert.ToInt32(_olusturanid); 
        ////    }

        ////    if(olusturulmatarihi == default)
        ////    {
        ////        var datedResult = accounts.FindAll(x => x.BranchId == subeid || x.CustomerId == musteriid || x.Balance == bakiye || x.CurrencyId == dovizcinsi ||
        ////                                  x.IBAN.ToLower().Contains(IBAN.ToLower()) || x.IsActive == aktifMi || x.FormedUserId == olusturanid);
        ////        return datedResult;
        ////    }

        ////    var result = accounts.FindAll(x => x.BranchId == subeid || x.CustomerId == musteriid || x.Balance == bakiye || x.CurrencyId == dovizcinsi || x.DateOfFormation >= olusturulmatarihi ||
        ////                                  x.IBAN.ToLower().Contains(IBAN.ToLower()) || x.IsActive == aktifMi || x.FormedUserId == olusturanid);


        ////    return result;
        //}
        //private List<AccountContract> SearchEngine(int id) //TO-DO: refactor edilecek, zaten id primary key olduğundan tek item dönüyor
        //{
        //    var result = accounts.FindAll(x => x.Id == id);
        //    return result;
        //}
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
            int? id = null;
            int? branchid = null;
            int? currencyid = null;
            int? formeduserid = null;
            int? customerid = null;
            int? additionno = null;
            decimal balance = 0;

            if (tbFilterbyId.Text != "")
            {
                id = Convert.ToInt32(tbFilterbyId.Text);
            }

            if (cbFilterByBranchId.SelectedIndex != -1)
            {
                branchid = cbFilterByBranchId.SelectedIndex;
            }

            if (cbFilterbyCurrencyId.SelectedIndex != -1)
            {
                currencyid = cbFilterbyCurrencyId.SelectedIndex;
            }

            if(tbFilterbyCustomerId.Text != "")
            {
                customerid = Convert.ToInt32(tbFilterbyCustomerId.Text);
            }
            if(tbFilterByAdditionNo.Text != "")
            {
                additionno = Convert.ToInt32(tbFilterByAdditionNo.Text);
            }
            if(tbFilterbyBalance.Text != "")
            {
                Decimal.TryParse(tbFilterbyBalance.Text,out balance);
            }

            AccountContract accountProperties = new AccountContract()
            {
                IBAN = tbFilterbyIban.Text,
                Balance = balance,
                BranchId = branchid,
                CurrencyId = currencyid,
                CustomerId = customerid,
                DateOfFormation = dpFilterbyDateOfFormation.SelectedDate.GetValueOrDefault(),
                FormedUserId = formeduserid,
                Id = id,
                IsActive = (bool)cbIsActive.IsChecked,
                AdditionNo = additionno,
                
                //Kapanış tarihi
                //Last transaction
                //Ek no

            };

            dgAccountList.ItemsSource = FilterEngine(accountProperties);

            //if (tbFilterbyId.Text != "")
            //{
            //    int id = Convert.ToInt32(tbFilterbyId.Text);
            //    dgAccountList.ItemsSource = SearchEngine(id);
            //}
            //else
            //{
            //    dgAccountList.ItemsSource = SearchEngine( new object[] {tbFilterbyBranchId.Text,tbFilterbyCustomerId.Text, tbFilterbyBalance.Text,
            //        cbFilterbyCurrencyId.SelectedIndex, dpFilterbyDateOfFormation.SelectedDate.GetValueOrDefault(),tbFilterbyIban.Text,(bool)cbIsActive.IsChecked,tbFilterbyFormedUserId.Text});
            //}
        }

        private List<AccountContract> FilterEngine(AccountContract _contract)
        {
            var connect = new Connector.Banking.Connect();
            var request = new AccountRequest();

            request.MethodName = "FilterAccountsByProperties";
            request.DataContract = _contract;

            var response = connect.Execute(request);

            if (response.IsSuccess)
            {
                var _accountsList = (List<AccountContract>)response.DataContract;
                return _accountsList;
            }
            return new List<AccountContract>();
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
            Regex regex = new Regex("[^0-9,]+");
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

        private void btnTemizle_Click(object sender, RoutedEventArgs e)
        {
            tbFilterbyId.Text = "";
            cbFilterByBranchId.SelectedIndex = -1;
            tbFilterbyCustomerId.Text = "";
            tbFilterByAdditionNo.Text = "";
            tbFilterbyBalance.Text = "";
            cbFilterbyCurrencyId.SelectedIndex = -1;
            dpFilterbyDateOfFormation.SelectedDate = default;
            tbFilterbyIban.Text = "";
            cbIsActive.IsChecked = true;
            cbFilterbyFormedUserId.SelectedIndex = -1;
        }
    }
}
