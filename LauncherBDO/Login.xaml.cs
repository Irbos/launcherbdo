using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace LauncherBDO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public static string key = "123456"; // Change on your api key
        public Login()
        {
            InitializeComponent();
            CloseFirstWindow.Click += (sender, e) => { foreach (Window w in App.Current.Windows) w.Close(); }; //close window
        }


        /// <summary>
        /// Auth functions 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        { 
            
            //1 method - for connect to site DesertCore
            MainWindow taskWindow = new MainWindow();
            if (UsernameText.Text != "")
            {
                string url = $"http://sitename/api/login.php?key={key}&username={UsernameText.Text}&password={FloatingPasswordBox.Password}"; //api requery 
                try
                {
                    using (var webClient = new WebClient())
                    {
                        var response = webClient.DownloadString(url);
                        if (response.Contains("200"))
                        {
                            this.Hide();
                            taskWindow.ProfileName.Content = UsernameText.Text;
                            taskWindow.loginT = UsernameText.Text;
                            taskWindow.passwordT = FloatingPasswordBox.Password;
                            taskWindow.Show();
                        }
                    }
                }
                catch (WebException ex)
                {
                    string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    MessageBox.Show("Account not found");
                }

            }
            //2 method - without checking and connecting to the site           
            //MainWindow taskWindow = new MainWindow();
            //Hide();
            //taskWindow.ProfileName.Content = UsernameText.Text;
            //taskWindow.loginT = UsernameText.Text;
            //taskWindow.passwordT = FloatingPasswordBox.Password;
            //taskWindow.Show();   
            
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
