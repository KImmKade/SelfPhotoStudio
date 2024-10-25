using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Threading;
using Npgsql;

namespace wpfTest.View
{
    /// <summary>
    /// paymentcomplete.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class paymentcomplete : Page
    {

        DispatcherTimer timer = new DispatcherTimer();
        int timesecond;

        BitmapImage paymentcompleteimg = new BitmapImage();

        public paymentcomplete()
        {
            Source.Log.log.Info("PaymentComplete 페이지 진입");
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작 | PaymentComplete 페이지");

                if (MainWindow.inifoldername.Contains("mediagram"))
                {

                    if (MainWindow.inifoldername == "mediagram9" || MainWindow.inifoldername == "mediagram10" || MainWindow.inifoldername == "mediagram11")
                    {
                        Timer.Foreground = Brushes.Black;
                    }
                    paymentcompleteimg = new BitmapImage();
                    paymentcompleteimg.BeginInit();
                    switch (TempSelect.temp)
                    {
                        case 1:
                            paymentcompleteimg.UriSource = new Uri(MainWindow.uipath + @"\PaymentComplete1.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 2:
                            paymentcompleteimg.UriSource = new Uri(MainWindow.uipath + @"\PaymentComplete2.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 3:
                            Timer.Margin = new Thickness(621, 264, 1034, 368);
                            paymentcompleteimg.UriSource = new Uri(MainWindow.uipath + @"\PaymentComplete3.png", UriKind.RelativeOrAbsolute);
                            break;
                    }
                    paymentcompleteimg.CacheOption = BitmapCacheOption.OnLoad;
                    paymentcompleteimg.EndInit();
                    backgroundimg.Source = paymentcompleteimg;
                }
                else
                {
                    backgroundimg.Source = MainWindow.paymentcomplete;
                }

                timesecond = 5;
                if (MainWindow.inifoldername.Contains("mediagram"))
                {
                    Timer.Text = timesecond.ToString();
                }
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += new EventHandler(Timer_TikTok);
                timer.Start();
                Source.Log.log.Info("PaymentComplete 타이머 시작");
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Timer_TikTok(object sender, EventArgs e)
        {
            try
            {
                timesecond--;
                if (MainWindow.inifoldername.Contains("mediagram"))
                {
                    Timer.Text = timesecond.ToString();
                }

                if (timesecond == 0)
                {
                    timer.Stop();
                    Source.Log.log.Info("PaymentComplete 타이머 종료");

                    backgroundimg.Source = null;
                    backgroundimg = null;
                    timer.Tick -= new EventHandler(Timer_TikTok);
                    timer = null;

                    if (MainWindow.inifoldername == "dearpic1" || MainWindow.inifoldername == "dearpic4" || MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic2")
                    {
                        NavigationService.Navigate(new Uri("View/PriewView.xaml", UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        if (MainWindow.camnumber.ToString() == "1")
                        {
                            NavigationService.Navigate(new Uri("View/TakePic.xaml", UriKind.RelativeOrAbsolute));
                        }
                        else
                        {
                            NavigationService.Navigate(new Uri("View/WebCam.xaml", UriKind.RelativeOrAbsolute));
                        }
                    }

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }
    }
}
