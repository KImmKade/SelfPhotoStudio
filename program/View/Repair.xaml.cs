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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Threading;
using Google.Protobuf.WellKnownTypes;
using System.Windows.Threading;

namespace wpfTest.View
{
    /// <summary>
    /// Repair.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Repair : Page
    {
        // 로그 업로드 타이머

        DispatcherTimer timer = new DispatcherTimer();

        public Repair()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(MainWindow.uipath + @"\RePair.png", UriKind.RelativeOrAbsolute);
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();

                backgroundimg.Source = bi;

                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += new EventHandler(Timer_Tick);
                timer.Start();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (MainWindow.checkrepair == 0)
                {
                    timer.Tick -= new EventHandler(Timer_Tick);
                    timer = null;
                    backgroundimg.Source = null;
                    NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }
    }
}
