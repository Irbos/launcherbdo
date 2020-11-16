using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
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
    /// Логика взаимодействия для Loading.xaml
    /// </summary>
    public partial class Loading : Window
    {
        public Loading()
        {
            InitializeComponent();
            TimerClose();
        }

        /// <summary>
        /// This timer for close window 
        /// </summary>
        public void TimerClose()
        {
            DispatcherTimer t = new DispatcherTimer();
            t.Interval = new TimeSpan(0, 0, 3);
            t.Tick += (s, e) =>
            {
                this.Close();
            };
            t.Start();
        }
    }
}
