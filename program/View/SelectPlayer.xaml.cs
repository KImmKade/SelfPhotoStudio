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
using System.Windows.Threading;
using System.IO;
using wpfTest;
using OpenCvSharp;

namespace Onecut.View
{
    /// <summary>
    /// SelectPlayer.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SelectPlayer : Page
    {
        public static int playerselect = 0;
        DispatcherTimer timer = new DispatcherTimer();
        public static bool checkfifth = false;

        BitmapImage backgroundimg = new BitmapImage();
        BitmapImage temp1_on = new BitmapImage();
        BitmapImage temp2_on = new BitmapImage();
        BitmapImage temp3_on = new BitmapImage();
        BitmapImage temp4_on = new BitmapImage();
        BitmapImage temp5_on = new BitmapImage();
        BitmapImage temp1_off = new BitmapImage();
        BitmapImage temp2_off = new BitmapImage();
        BitmapImage temp3_off = new BitmapImage();
        BitmapImage temp4_off = new BitmapImage();
        BitmapImage temp5_off = new BitmapImage();
        int tsduration = Convert.ToInt32(MainWindow.count);


        BitmapImage Nextbtnimgoff = new BitmapImage();

        public SelectPlayer()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                checkfifth = false;
                playerselect = 1;

                backgroundimg = new BitmapImage();
                backgroundimg.BeginInit();
                backgroundimg.UriSource = new Uri(MainWindow.uipath + @"\PlayerSelect.png", UriKind.RelativeOrAbsolute);
                backgroundimg.CacheOption = BitmapCacheOption.OnLoad;
                backgroundimg.EndInit();

                Backgroundimg.Source = backgroundimg;

                Nextbtnimgoff.BeginInit();
                Nextbtnimgoff.UriSource = new Uri(MainWindow.uipath + @"\Next_off.png", UriKind.RelativeOrAbsolute);
                Nextbtnimgoff.CacheOption = BitmapCacheOption.OnLoad;
                Nextbtnimgoff.EndInit();

                BackImg.Source = MainWindow.Backimg;
                NextImg.Source = Nextbtnimgoff;
                NextImg.IsEnabled = false;

                if (File.Exists(MainWindow.uipath + @"\Player5.png"))
                {
                    temp1_off = new BitmapImage();
                    temp1_off.BeginInit();
                    temp1_off.UriSource = new Uri(MainWindow.uipath + @"\Player1.png", UriKind.RelativeOrAbsolute);
                    temp1_off.CacheOption = BitmapCacheOption.OnLoad;
                    temp1_off.DecodePixelWidth = 261;
                    temp1_off.DecodePixelHeight = 441;
                    temp1_off.EndInit();

                    temp2_off = new BitmapImage();
                    temp2_off.BeginInit();
                    temp2_off.UriSource = new Uri(MainWindow.uipath + @"\Player2.png", UriKind.RelativeOrAbsolute);
                    temp2_off.CacheOption = BitmapCacheOption.OnLoad;
                    temp2_off.DecodePixelWidth = 261;
                    temp2_off.DecodePixelHeight = 441;
                    temp2_off.EndInit();

                    temp3_off = new BitmapImage();
                    temp3_off.BeginInit();
                    temp3_off.UriSource = new Uri(MainWindow.uipath + @"\Player3.png", UriKind.RelativeOrAbsolute);
                    temp3_off.CacheOption = BitmapCacheOption.OnLoad;
                    temp3_off.DecodePixelWidth = 261;
                    temp3_off.DecodePixelHeight = 441;
                    temp3_off.EndInit();

                    temp4_off = new BitmapImage();
                    temp4_off.BeginInit();
                    temp4_off.UriSource = new Uri(MainWindow.uipath + @"\Player4.png", UriKind.RelativeOrAbsolute);
                    temp4_off.CacheOption = BitmapCacheOption.OnLoad;
                    temp4_off.DecodePixelWidth = 261;
                    temp4_off.DecodePixelHeight = 441;
                    temp4_off.EndInit();

                    temp5_off = new BitmapImage();
                    temp5_off.BeginInit();
                    temp5_off.UriSource = new Uri(MainWindow.uipath + @"\Player5.png", UriKind.RelativeOrAbsolute);
                    temp5_off.CacheOption = BitmapCacheOption.OnLoad;
                    temp5_off.DecodePixelWidth = 261;
                    temp5_off.DecodePixelHeight = 441;
                    temp5_off.EndInit();

                    temp1_on = new BitmapImage();
                    temp1_on.BeginInit();
                    temp1_on.UriSource = new Uri(MainWindow.uipath + @"\Player1_Pick.png", UriKind.RelativeOrAbsolute);
                    temp1_on.CacheOption = BitmapCacheOption.OnLoad;
                    temp1_on.DecodePixelWidth = 261;
                    temp1_on.DecodePixelHeight = 441;
                    temp1_on.EndInit();

                    temp2_on = new BitmapImage();
                    temp2_on.BeginInit();
                    temp2_on.UriSource = new Uri(MainWindow.uipath + @"\Player2_Pick.png", UriKind.RelativeOrAbsolute);
                    temp2_on.CacheOption = BitmapCacheOption.OnLoad;
                    temp2_on.DecodePixelWidth = 261;
                    temp2_on.DecodePixelHeight = 441;
                    temp2_on.EndInit();

                    temp3_on = new BitmapImage();
                    temp3_on.BeginInit();
                    temp3_on.UriSource = new Uri(MainWindow.uipath + @"\Player3_Pick.png", UriKind.RelativeOrAbsolute);
                    temp3_on.CacheOption = BitmapCacheOption.OnLoad;
                    temp3_on.DecodePixelWidth = 261;
                    temp3_on.DecodePixelHeight = 441;
                    temp3_on.EndInit();

                    temp4_on = new BitmapImage();
                    temp4_on.BeginInit();
                    temp4_on.UriSource = new Uri(MainWindow.uipath + @"\Player4_Pick.png", UriKind.RelativeOrAbsolute);
                    temp4_on.CacheOption = BitmapCacheOption.OnLoad;
                    temp4_on.DecodePixelWidth = 261;
                    temp4_on.DecodePixelHeight = 441;
                    temp4_on.EndInit();

                    temp5_on = new BitmapImage();
                    temp5_on.BeginInit();
                    temp5_on.UriSource = new Uri(MainWindow.uipath + @"\Player5_Pick.png", UriKind.RelativeOrAbsolute);
                    temp5_on.CacheOption = BitmapCacheOption.OnLoad;
                    temp5_on.DecodePixelWidth = 261;
                    temp5_on.DecodePixelHeight = 441;
                    temp5_on.EndInit();

                    FirstPlayer.Source = temp1_off;
                    SecondPlayer.Source = temp2_off;
                    ThirdPlayer.Source = temp3_off;
                    FourthPlayer.Source = temp4_off;
                    FifthPlayer.Source = temp5_off;

                    FirstPlayer.IsEnabled = true;
                    SecondPlayer.IsEnabled = true;
                    ThirdPlayer.IsEnabled = true;
                    FourthPlayer.IsEnabled = true;
                    FifthPlayer.IsEnabled = true;

                    FirstPlayer.Margin = new Thickness(20, 317, 1540, 263);
                    SecondPlayer.Margin = new Thickness(400, 317, 1160, 263);
                    ThirdPlayer.Margin = new Thickness(780, 317, 780, 263);
                    FourthPlayer.Margin = new Thickness(1160, 317, 400, 263);
                    FifthPlayer.Margin = new Thickness(1540, 317, 20, 263);

                    checkfifth = true;
                }
                else
                {
                    temp1_off = new BitmapImage();
                    temp1_off.BeginInit();
                    temp1_off.UriSource = new Uri(MainWindow.uipath + @"\Player1.png", UriKind.RelativeOrAbsolute);
                    temp1_off.CacheOption = BitmapCacheOption.OnLoad;
                    temp1_off.DecodePixelWidth = 261;
                    temp1_off.DecodePixelHeight = 441;
                    temp1_off.EndInit();

                    temp2_off = new BitmapImage();
                    temp2_off.BeginInit();
                    temp2_off.UriSource = new Uri(MainWindow.uipath + @"\Player2.png", UriKind.RelativeOrAbsolute);
                    temp2_off.CacheOption = BitmapCacheOption.OnLoad;
                    temp2_off.DecodePixelWidth = 261;
                    temp2_off.DecodePixelHeight = 441;
                    temp2_off.EndInit();

                    temp3_off = new BitmapImage();
                    temp3_off.BeginInit();
                    temp3_off.UriSource = new Uri(MainWindow.uipath + @"\Player3.png", UriKind.RelativeOrAbsolute);
                    temp3_off.CacheOption = BitmapCacheOption.OnLoad;
                    temp3_off.DecodePixelWidth = 261;
                    temp3_off.DecodePixelHeight = 441;
                    temp3_off.EndInit();

                    temp4_off = new BitmapImage();
                    temp4_off.BeginInit();
                    temp4_off.UriSource = new Uri(MainWindow.uipath + @"\Player4.png", UriKind.RelativeOrAbsolute);
                    temp4_off.CacheOption = BitmapCacheOption.OnLoad;
                    temp4_off.DecodePixelWidth = 261;
                    temp4_off.DecodePixelHeight = 441;
                    temp4_off.EndInit();

                    temp1_on = new BitmapImage();
                    temp1_on.BeginInit();
                    temp1_on.UriSource = new Uri(MainWindow.uipath + @"\Player1_Pick.png", UriKind.RelativeOrAbsolute);
                    temp1_on.CacheOption = BitmapCacheOption.OnLoad;
                    temp1_on.DecodePixelWidth = 261;
                    temp1_on.DecodePixelHeight = 441;
                    temp1_on.EndInit();

                    temp2_on = new BitmapImage();
                    temp2_on.BeginInit();
                    temp2_on.UriSource = new Uri(MainWindow.uipath + @"\Player2_Pick.png", UriKind.RelativeOrAbsolute);
                    temp2_on.CacheOption = BitmapCacheOption.OnLoad;
                    temp2_on.DecodePixelWidth = 261;
                    temp2_on.DecodePixelHeight = 441;
                    temp2_on.EndInit();

                    temp3_on = new BitmapImage();
                    temp3_on.BeginInit();
                    temp3_on.UriSource = new Uri(MainWindow.uipath + @"\Player3_Pick.png", UriKind.RelativeOrAbsolute);
                    temp3_on.CacheOption = BitmapCacheOption.OnLoad;
                    temp3_on.DecodePixelWidth = 261;
                    temp3_on.DecodePixelHeight = 441;
                    temp3_on.EndInit();

                    temp4_on = new BitmapImage();
                    temp4_on.BeginInit();
                    temp4_on.UriSource = new Uri(MainWindow.uipath + @"\Player4_Pick.png", UriKind.RelativeOrAbsolute);
                    temp4_on.CacheOption = BitmapCacheOption.OnLoad;
                    temp4_on.DecodePixelWidth = 261;
                    temp4_on.DecodePixelHeight = 441;
                    temp4_on.EndInit();

                    FirstPlayer.Source = temp1_off;
                    SecondPlayer.Source = temp2_off;
                    ThirdPlayer.Source = temp3_off;
                    FourthPlayer.Source = temp4_off;

                    FirstPlayer.IsEnabled = true;
                    SecondPlayer.IsEnabled = true;
                    ThirdPlayer.IsEnabled = true;
                    FourthPlayer.IsEnabled = true;

                    FifthPlayer.Margin = new Thickness(0, 0, 1920, 1080);

                    checkfifth = false;
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
                    Timer.Text = tsduration.ToString();
                    timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += new EventHandler(Timer_Tick);
                    timer.Start();
                }
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void BackImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                wpfTest.Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                BackImg.Source = null;

                timer.Stop();
                Dispose();
                NavigationService.Navigate(new Uri("View/SelectTakePic.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void NextImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                wpfTest.Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                NextImg.IsEnabled = false;

                timer.Stop();
                Dispose();
                NavigationService.Navigate(new Uri("View/TempSelect.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void FirstPlayer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                playerselect = 1;
                if (checkfifth)
                {
                    FirstPlayer.Source = temp1_on;
                    SecondPlayer.Source = temp2_off;
                    ThirdPlayer.Source = temp3_off;
                    FourthPlayer.Source = temp4_off;
                    FifthPlayer.Source = temp5_off;
                }
                else
                {
                    FirstPlayer.Source = temp1_on;
                    SecondPlayer.Source = temp2_off;
                    ThirdPlayer.Source = temp3_off;
                    FourthPlayer.Source = temp4_off;
                }

                FirstPlayer.IsEnabled = false;
                SecondPlayer.IsEnabled = true;
                ThirdPlayer.IsEnabled = true;
                FourthPlayer.IsEnabled = true;
                FifthPlayer.IsEnabled = true;

                NextImg.IsEnabled = true;
                NextImg.Source = MainWindow.nextbtn;
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void SecondPlayer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                playerselect = 2;
                if (checkfifth)
                {
                    FirstPlayer.Source = temp1_off;
                    SecondPlayer.Source = temp2_on;
                    ThirdPlayer.Source = temp3_off;
                    FourthPlayer.Source = temp4_off;
                    FifthPlayer.Source = temp5_off;
                }
                else
                {
                    FirstPlayer.Source = temp1_off;
                    SecondPlayer.Source = temp2_on;
                    ThirdPlayer.Source = temp3_off;
                    FourthPlayer.Source = temp4_off;
                }

                FirstPlayer.IsEnabled = true;
                SecondPlayer.IsEnabled = false;
                ThirdPlayer.IsEnabled = true;
                FourthPlayer.IsEnabled = true;
                FifthPlayer.IsEnabled = true;

                NextImg.IsEnabled = true;
                NextImg.Source = MainWindow.nextbtn;
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void ThirdPlayer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                playerselect = 3;
                if (checkfifth)
                {
                    FirstPlayer.Source = temp1_off;
                    SecondPlayer.Source = temp2_off;
                    ThirdPlayer.Source = temp3_on;
                    FourthPlayer.Source = temp4_off;
                    FifthPlayer.Source = temp5_off;
                }
                else
                {
                    FirstPlayer.Source = temp1_off;
                    SecondPlayer.Source = temp2_off;
                    ThirdPlayer.Source = temp3_on;
                    FourthPlayer.Source = temp4_off;
                }

                FirstPlayer.IsEnabled = true;
                SecondPlayer.IsEnabled = true;
                ThirdPlayer.IsEnabled = false;
                FourthPlayer.IsEnabled = true;
                FifthPlayer.IsEnabled = true;

                NextImg.IsEnabled = true;
                NextImg.Source = MainWindow.nextbtn;
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void FourthPlayer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                playerselect = 4;
                if (checkfifth)
                {
                    FirstPlayer.Source = temp1_off;
                    SecondPlayer.Source = temp2_off;
                    ThirdPlayer.Source = temp3_off;
                    FourthPlayer.Source = temp4_on;
                    FifthPlayer.Source = temp5_off;
                }
                else
                {
                    FirstPlayer.Source = temp1_off;
                    SecondPlayer.Source = temp2_off;
                    ThirdPlayer.Source = temp3_off;
                    FourthPlayer.Source = temp4_on;
                }

                FirstPlayer.IsEnabled = true;
                SecondPlayer.IsEnabled = true;
                ThirdPlayer.IsEnabled = true;
                FourthPlayer.IsEnabled = false;
                FifthPlayer.IsEnabled = true;

                NextImg.IsEnabled = true;
                NextImg.Source = MainWindow.nextbtn;
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void FifthPlayer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                playerselect = 5;

                FirstPlayer.Source = temp1_off;
                SecondPlayer.Source = temp2_off;
                ThirdPlayer.Source = temp3_off;
                FourthPlayer.Source = temp4_off;
                FifthPlayer.Source = temp5_on;

                FirstPlayer.IsEnabled = true;
                SecondPlayer.IsEnabled = true;
                ThirdPlayer.IsEnabled = true;
                FourthPlayer.IsEnabled = true;
                FifthPlayer.IsEnabled = false;

                NextImg.IsEnabled = true;
                NextImg.Source = MainWindow.nextbtn;
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        #region///타이머///

        private void Timer_Tick(object sender, EventArgs e)
        {

            tsduration--;
            if (tsduration < 0)
            {
                Timer.Text = "0";
            }
            else
            {
                Timer.Text = tsduration.ToString();
            }

            if (tsduration == 0)
            {
                NextImg.IsEnabled = false;
                FirstPlayer.IsEnabled = false;
                SecondPlayer.IsEnabled = false;
                ThirdPlayer.IsEnabled = false;
                FourthPlayer.IsEnabled = false;
                FifthPlayer.IsEnabled=false;
            }

            if (tsduration == -1)
            {
                timer.Stop();
                Dispose();
                NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        #endregion

        private void Dispose()
        {
            try
            {
                wpfTest.Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                temp1_off = null;
                temp2_off = null;
                temp3_off = null;
                temp4_off = null;
                temp5_off = null;
                temp1_on = null;
                temp2_on = null;
                temp3_on = null;
                temp4_on = null;
                temp5_on = null;

                FirstPlayer.Source = null;
                SecondPlayer.Source = null;
                ThirdPlayer.Source = null;
                FourthPlayer.Source = null;
                FifthPlayer.Source = null;
                FirstPlayer = null;
                SecondPlayer = null;
                ThirdPlayer = null;
                FourthPlayer = null;
                FifthPlayer = null;
                Backgroundimg.Source = null;
                Backgroundimg = null;
                BackImg.Source = null;
                BackImg = null;

                if (MainWindow.Timer.ToString() == "Use")
                {
                    timer.Tick -= new EventHandler(Timer_Tick);
                    Timer.Text = null;
                    Timer = null;
                }
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }
    }
}
