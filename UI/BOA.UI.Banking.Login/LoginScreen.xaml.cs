using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using BOA.Connector.Banking;
using BOA.Types.Banking;

namespace BOA.UI.Banking.Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public static string _userName {get;set;}
        public static int _userId { get; set; }

        public LoginScreen()
        {
            InitializeComponent();

            ProgramVersionTextBlock.Text = ProgramVersionTextBlock.Text + " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserName.Text != "" && Password.Password != "")
            {
                var connect = new BOA.Connector.Banking.Connect<GenericResponse<LoginContract>>();
                var request = new BOA.Types.Banking.LoginRequest();
                var contract = new BOA.Types.Banking.LoginContract();
                
                contract.LoginName = UserName.Text;
                contract.Password = Password.Password;

                request.DataContract = contract;
                request.MethodName = "UserLogin";


                 var response = connect.Execute(request);


                if (response.IsSuccess)
                {
                    var user = response.Value;
                    _userName = user.LoginName;
                    _userId = (int)user.Id;
                    
                    Application.Current.MainWindow.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show(response.ErrorMessage);
                    UserName.Text = "";
                    Password.Password = "";
                } 
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch
            {

            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(UserName);
        }
    }
}
