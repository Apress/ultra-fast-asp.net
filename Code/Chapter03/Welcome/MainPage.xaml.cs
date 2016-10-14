using System;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using Welcome.LoginReference;

namespace Welcome
{
    public partial class MainPage : UserControl
    {
        public const string WELCOME = "welcome";

        public MainPage()
        {
            this.Loaded += new RoutedEventHandler(Page_Loaded);
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string welcome = null;
            IsolatedStorageSettings.SiteSettings.TryGetValue(WELCOME, out welcome);
            LoggedIn(welcome);
        }

        private void LoggedIn(string welcome)
        {
            if (!String.IsNullOrEmpty(welcome))
            {
                this.info.Text = welcome;
                this.UserName.Visibility = Visibility.Collapsed;
                this.LoginButton.Content = "Logout";
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.UserName.Visibility == Visibility.Collapsed)
            {
                this.info.Text = "Please Login";
                this.UserName.Visibility = Visibility.Visible;
                this.LoginButton.Content = "Login";
                IsolatedStorageSettings.SiteSettings[WELCOME] = null;
            }
            else
            {
                string name = this.UserName.Text;
                if (!String.IsNullOrEmpty(name))
                {
                    LoginServiceClient loginService = new LoginServiceClient();
                    loginService.LoginCompleted += new EventHandler<LoginCompletedEventArgs>(loginService_LoginCompleted);
                    loginService.LoginAsync(name);
                }
            }
        }

        private void loginService_LoginCompleted(object sender, LoginCompletedEventArgs e)
        {
            IsolatedStorageSettings.SiteSettings[WELCOME] = e.Result;
            LoggedIn(e.Result);
        }
    }
}
