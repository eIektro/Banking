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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BOA.Types.Banking;
using BOA.UI.Banking.Login;


namespace BOA.UI.Banking.MainScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainScreen : Window
    {
        LoginScreen _loginScreen;
        //public object obje { get; set; }

        public MainScreen()
        {
            _loginScreen = new LoginScreen();

            InitializeComponent();

            if (/*!is logged in*/true)
            {
                Application.Current.MainWindow.Hide();
                _loginScreen.Show();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //obje = (CustomerResponse)new CustomerResponse() { IsSuccess = false };
            //var response = (CustomerResponse)obje;
            
            
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void tvMusteriEkle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var theCustomerAddTab = tabControl1.Items.OfType<CloseableTab>().SingleOrDefault(x => x.Title == "Müşteri Ekle");
            if (theCustomerAddTab != null)
            {
                theCustomerAddTab.Focus();
            }
            else
            {
                CustomerAdd.CustomerAddUC customerAdd = new CustomerAdd.CustomerAddUC();
                CloseableTab theTabItem = new CloseableTab();
                theTabItem.Title = "Müşteri Ekle";
                theTabItem.Content = customerAdd;
                tabControl1.Items.Add(theTabItem);
                theTabItem.Focus();
            }
        }

        private void tvMusteriListele_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
            var theCustomerTab = tabControl1.Items.OfType<CloseableTab>().SingleOrDefault(x => x.Title == "Müşteri Listele");
            if (theCustomerTab != null) {
                theCustomerTab.Focus();
            }
            
            else
            {
                CustomerList.CustomerListUC customerList = new CustomerList.CustomerListUC(tabControl1);
                CloseableTab theTabItem = new CloseableTab();
                theTabItem.Title = "Müşteri Listele";
                theTabItem.Content = customerList;
                tabControl1.Items.Add(theTabItem);
                theTabItem.Focus();
            }
        }

        private void tvSubeEkle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {


            var theBranchAddTab = tabControl1.Items.OfType<CloseableTab>().SingleOrDefault(x => x.Title == "Şube Ekle");
            if (theBranchAddTab != null)
            {
                theBranchAddTab.Focus();
            }

            BranchAdd.BranchAddUC branchAdd = new BranchAdd.BranchAddUC();
            CloseableTab theTabItem = new CloseableTab();
            theTabItem.Title = "Şube Ekle";
            theTabItem.Content = branchAdd;
            tabControl1.Items.Add(theTabItem);
            theTabItem.Focus();
        }

        private void tvSubeListesi_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var theBranchesTab = tabControl1.Items.OfType<CloseableTab>().SingleOrDefault(x => x.Title == "Şube Listesi");
            if (theBranchesTab != null)
            {
                theBranchesTab.Focus(); 
            }
            
            else
            {
                BranchList.BranchListUC branchListUc = new BranchList.BranchListUC(tabControl1);
                CloseableTab theTabItem = new CloseableTab();
                theTabItem.Title = "Şube Listesi";
                theTabItem.Content = branchListUc;
                tabControl1.Items.Add(theTabItem);
                theTabItem.Focus();
            }
        }

        private void tvHesapListesi_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
                var theAccountsTab = tabControl1.Items.OfType<CloseableTab>().SingleOrDefault(x => x.Title == "Hesap Listesi");
            if (theAccountsTab != null)
            {
                theAccountsTab.Focus();
            }

            else
            {

                AccountList.AccountListUC accountListUC = new AccountList.AccountListUC(tabControl1);
                CloseableTab theTabItem = new CloseableTab();
                theTabItem.Title = "Hesap Listesi";
                theTabItem.Content = accountListUC;
                tabControl1.Items.Add(theTabItem);
                theTabItem.Focus();
            }
        }

        private void tvHesapEkle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var theAccountAddTab = tabControl1.Items.OfType<CloseableTab>().SingleOrDefault(x => x.Title == "Hesap Ekle");
            if (theAccountAddTab != null)
            {
                theAccountAddTab.Focus();
            }

            else
            {

                AccountAdd.AccountAddUC accountAddUC = new AccountAdd.AccountAddUC();
                CloseableTab theTabItem = new CloseableTab();
                theTabItem.Title = "Hesap Ekle";
                theTabItem.Content = accountAddUC;
                tabControl1.Items.Add(theTabItem);
                theTabItem.Focus();
            }
        }

        /* ↓↓ STORYBOARD KODLARI ↓↓ */

        /**  
        
        btnshow.Click += Btnshow_Click;
        btnclose.Click += Btnclose_Click;

        private void Btnclose_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = Resources["CloseMenu"] as Storyboard;
            sb.Begin(LeftMenu);
        }

        private void Btnshow_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = Resources["OpenMenu"] as Storyboard;
            sb.Begin(LeftMenu);
        }

        **/
    }
}
