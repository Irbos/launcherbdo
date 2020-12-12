using Hardcodet.Wpf.TaskbarNotification;
using LauncherBDO.Showcase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LauncherBDO
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string loginT;
        public string passwordT;
        WindowState prevState;

        public MainWindow()
        {
            InitializeComponent();
	    CheckServerStatus();
            DiscordChannelBtn.Click += (sender, e) => { Process.Start("explorer.exe", "https://discord.gg/sKNHsWawsJ"); }; //open discord group
            CloseSecondWindow.Click += (sender, e) => { foreach (Window w in App.Current.Windows) w.Close(); }; //close window
            MinimizeSecondWindow.Click += (sender, e) => { this.WindowState = WindowState.Minimized; if (WindowState == WindowState.Minimized) Hide(); else prevState = WindowState; var balloon = new WelcomeTrey(); tb.ShowCustomBalloon(balloon, PopupAnimation.Slide, 12000);
            };
        }

        /// <summary>
        /// this checked server status (he check when the program starts)
        /// </summary>
        public void CheckServerStatus()
        {
            string host = "127.0.0.1"; //change server ip
            int port = 8889; //change server port
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            bool flag = socket.BeginConnect(host, port, null, null).AsyncWaitHandle.WaitOne(100, true);
            if (flag)
            {
                ServerStatusL.Content = "Online";
                ServerStatusL.Foreground = Brushes.Lime;
                socket.Close();
            }
            else
            {
                if (flag)
                    return;
                ServerStatusL.Content = "Offline";
                ServerStatusL.Foreground = Brushes.Red;
                socket.Close();
            }
        }

        public void FirstPostEvent(object sender, RoutedEventArgs e) { }
        public void SecondPostEvent(object sender, RoutedEventArgs e) { }


        /// <summary>
        /// play button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PlayGameBtn(object sender, RoutedEventArgs e)
        {
            Loading load = new Loading();
            this.Hide();
            load.Show();
            Process.Start(new ProcessStartInfo()
            {
                UseShellExecute = true,
                WorkingDirectory = Directory.GetCurrentDirectory() + @"\bin64\",
                Verb = "runas",
                FileName = "BlackDesert64.exe",
                Arguments = " " + loginT + "," + passwordT,
                CreateNoWindow = true
            });
        }

        /// <summary>
        /// Open window from taskbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskbarIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
        {
            this.Show();
            WindowState = prevState;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
