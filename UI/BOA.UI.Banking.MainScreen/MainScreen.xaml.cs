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
        CustomerAdd.CustomerAdd customerAdd;

        public MainScreen()
        {
            _loginScreen = new LoginScreen();

            InitializeComponent();

            btnshow.Click += Btnshow_Click;
            btnclose.Click += Btnclose_Click;

            if (/*!is logged in*/true)
            {
                Application.Current.MainWindow.Hide();
                _loginScreen.Show();
            }
        }

        
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            customerAdd = new CustomerAdd.CustomerAdd();
            customerAdd.Show();
        }

        private void btnListCustomer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
