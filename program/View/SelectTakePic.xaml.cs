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
using wpfTest;
using System.Windows.Threading;
using OpenCvSharp;
using wpfTest.View;

namespace Onecut.View
{
    /// <summary>
    /// SelectTakePic.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SelectTakePic : Page
    {

        public static int checkversion = 0;
        BitmapImage backgroundimg = new BitmapImage();
        BitmapImage solo_on = new BitmapImage();
        BitmapImage solo_off = new BitmapImage();
        BitmapImage together_on = new BitmapImage();
        BitmapImage together_off = new BitmapImage();

        BitmapImage Backbtnimg = new BitmapImage();
        BitmapImage Nextbtnimgoff = new BitmapImage();
        BitmapImage nextbtnimgon = new BitmapImage();
        int checkduration = Convert.ToInt32(MainWindow.count);

        DispatcherTimer timer = new DispatcherTimer();

        public SelectTakePic()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                checkversion = 0;
                backgroundimg = new BitmapImage();
                backgroundimg.BeginInit();
                backgroundimg.UriSource = new Uri(MainWindow.uipath + @"\SelectTakePic.png", UriKind.RelativeOrAbsolute);
                backgroundimg.CacheOption = BitmapCacheOption.OnLoad;
                backgroundimg.EndInit();

                solo_off = new BitmapImage();
                solo_off.BeginInit();
                solo_off.UriSource = new Uri(MainWindow.uipath + @"\solo_off.png", UriKind.RelativeOrAbsolute);
                solo_off.CacheOption = BitmapCacheOption.OnLoad;
                solo_off.EndInit();

                solo_on = new BitmapImage();
                solo_on.BeginInit();
                solo_on.UriSource = new Uri(MainWindow.uipath + @"\solo_on.png", UriKind.RelativeOrAbsolute);
                solo_on.CacheOption = BitmapCacheOption.OnLoad;
                solo_on.EndInit();

                together_on = new BitmapImage();
                together_on.BeginInit();
                together_on.UriSource = new Uri(MainWindow.uipath + @"\together_on.png", UriKind.RelativeOrAbsolute);
                together_on.CacheOption = BitmapCacheOption.OnLoad;
                together_on.EndInit();

                together_off = new BitmapImage();
                together_off.BeginInit();
                together_off.UriSource = new Uri(MainWindow.uipath + @"\together_off.png", UriKind.RelativeOrAbsolute);
                together_off .CacheOption = BitmapCacheOption.OnLoad;
                together_off .EndInit();

                Backgroundimg.Source = backgroundimg;
                BackImg.Source = MainWindow.Backimg;
                NextImg.Source = MainWindow.nextbtn;
                SoloVersion.Source = solo_off;
                TogetherVersion.Source = together_off;

                Timer.Foreground = MainWindow.textbrush;

                if (MainWindow.inifoldername.Contains("mediagram"))
                {
                    Timer.Margin = new Thickness(1720, 41, 0, 0);

                    nextbtnimgon = new BitmapImage();
                    nextbtnimgon.BeginInit();
                    nextbtnimgon.UriSource = MainWindow.nextbtn.UriSource;
                    nextbtnimgon.CacheOption = BitmapCacheOption.OnLoad;
                    nextbtnimgon.EndInit();

                    Nextbtnimgoff = new BitmapImage();
                    Nextbtnimgoff.BeginInit();
                    Nextbtnimgoff.UriSource = new Uri(MainWindow.uipath + @"\Next_off.png", UriKind.RelativeOrAbsolute);
                    Nextbtnimgoff.CacheOption = BitmapCacheOption.OnLoad;
                    Nextbtnimgoff.EndInit();

                    BackImg.Margin = new Thickness(569, 927, 1920 - 370 - 569, 1080 - 927 - 100);
                    NextImg.Margin = new Thickness(979, 927, 1920 - 370 - 979, 1080 - 927 - 100);

                    BackImg.Width = 370;
                    BackImg.Height = 100;
                    NextImg.Width = 370;
                    NextImg.Height = 100;

                    NextImg.Source = Nextbtnimgoff;
                }


                if (MainWindow.timerlocation == "left")
                {
                    Timer.Margin = new Thickness(0, 130, 1720, 0);
                }
                else
                {
                    
                }

                if (MainWindow.Timer.ToString() == "Use")
                {
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += new EventHandler(Timer_Tick);
                    timer.Start();
                    Timer.Text = checkduration.ToString();
                    wpfTest.Source.Log.log.Info("타이머 시작");
                }
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                checkduration--;
                if (checkduration >= 0)
                {
                    Timer.Text = checkduration.ToString();
                }
                if (checkduration < 0)
                {
                    Timer.Text = "0";
                }
                if (checkduration == 0)
                {
                    SoloVersion.IsEnabled = false;
                    TogetherVersion.IsEnabled = false;
                    BackImg.IsEnabled = false;
                    NextImg.IsEnabled = false;
                }
                if (checkduration == -1)
                {
                    timer.Stop();
                    wpfTest.Source.Log.log.Info("타이머 종료됨");
                    timer.Tick -= new EventHandler(Timer_Tick);
                    timer = null;
                    NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void SoloVersion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                checkversion = 1;

                SoloVersion.IsEnabled = false;
                TogetherVersion.IsEnabled = true;
                SoloVersion.Source = solo_on;
                TogetherVersion.Source = together_off;

                NextImg.Source = MainWindow.nextbtn;
                NextImg.IsEnabled = true;
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod());
            }
        }

        private void TogetherVersion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                checkversion = 2;

                SoloVersion.IsEnabled = true;
                TogetherVersion.IsEnabled = false;
                SoloVersion.Source = solo_off;
                TogetherVersion.Source = together_on;

                NextImg.Source = MainWindow.nextbtn;
                NextImg.IsEnabled = true;
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod());
            }
        }

        private void BackImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                wpfTest.Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.Timer.ToString() == "Use")
                {
                    timer.Stop();
                    wpfTest.Source.Log.log.Info("타이머 종료됨");
                    timer.Tick -= new EventHandler(Timer_Tick);
                    timer = null;
                }
                Payment.Pm_Global.Pm_Insert = 0;

                NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod());
            }
        }

        private void NextImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                wpfTest.Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                if (MainWindow.Timer.ToString() == "Use")
                {
                    timer.Stop();
                    wpfTest.Source.Log.log.Info("타이머 종료됨");
                    timer.Tick -= new EventHandler(Timer_Tick);
                    timer = null;
                }
                if (checkversion == 1)
                {
                    NavigationService.Navigate(new Uri("View/TempSelect.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    NavigationService.Navigate(new Uri("View/SelectPlayer.xaml", UriKind.RelativeOrAbsolute));
                }
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod());
            }
        }
    }
}
