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
using System.Diagnostics.Eventing.Reader;

namespace wpfTest.View
{
    /// <summary>
    /// ChoicePayment.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ChoicePayment : Page
    {

        int checkskip = 0;
        int check = 0;
        int checkduration = Convert.ToInt32(MainWindow.count);
        DispatcherTimer timer = new DispatcherTimer();
        public static int checkpayment = 1;

        BitmapImage coupon_on = new BitmapImage();
        BitmapImage coupon_off = new BitmapImage();

        public ChoicePayment()
        {
            Source.Log.log.Info("ChoicePayment 페이지 진입");
            InitializeComponent();
        }

        private void cardimg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (MainWindow.inifoldername == "animalhospital")
                {
                    cardimg.Source = MainWindow.card_on;
                    cashimg.Source = coupon_off;
                }
                else
                {
                    switch (MainWindow.paymentway)
                    {
                        case "cardcash":
                            cardimg.Source = MainWindow.card_on;
                            cashimg.Source = MainWindow.cash_off;
                            break;
                        case "cashcoupon":
                            cardimg.Source = coupon_on;
                            cashimg.Source = MainWindow.cash_off;
                            break;
                        case "cardcoupon":
                            cardimg.Source = MainWindow.card_on;
                            cashimg.Source = coupon_on;
                            break;
                    }
                }

                cardimg.IsEnabled = false;
                cashimg.IsEnabled = true;

                checkpayment = 2;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void cashimg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername == "animalhospital")
                {
                    cardimg.Source = MainWindow.card_off;
                    cashimg.Source = coupon_on;
                }
                else
                {
                    switch (MainWindow.paymentway)
                    {
                        case "cardcash":
                            cardimg.Source = MainWindow.card_off;
                            cashimg.Source = MainWindow.cash_on;
                            break;
                        case "cashcoupon":
                            cardimg.Source = coupon_off;
                            cashimg.Source = MainWindow.cash_on;
                            break;
                        case "cardcoupon":
                            cardimg.Source = MainWindow.card_off;
                            cashimg.Source = coupon_off;
                            break;
                    }
                }

                cardimg.IsEnabled = true;
                cashimg.IsEnabled = false;

                checkpayment = 1;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void NextImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                if (MainWindow.Timer.ToString() == "Use")
                {
                    timer.Stop();
                    Source.Log.log.Info("타이머 종료됨");
                    timer.Tick -= new EventHandler(Timer_Tick);
                    timer = null;
                }
                if (!cardimg.IsEnabled)
                {
                    switch (MainWindow.paymentway)
                    {
                        case "cardcash":
                            NavigationService.Navigate(new Uri("View/CreditPayment.xaml", UriKind.RelativeOrAbsolute));
                            break;
                        case "cashcoupon":
                            NavigationService.Navigate(new Uri("View/CouponPage.xaml", UriKind.RelativeOrAbsolute));
                            break;
                        case "cardcoupon":
                            NavigationService.Navigate(new Uri("View/CreditPayment.xaml", UriKind.RelativeOrAbsolute));
                            break;
                    }
                }
                else if (!cashimg.IsEnabled)
                {
                    switch (MainWindow.paymentway)
                    {
                        case "cardcash":
                            NavigationService.Navigate(new Uri("View/Payment.xaml", UriKind.RelativeOrAbsolute));
                            break;
                        case "cashcoupon":
                            NavigationService.Navigate(new Uri("View/Payment.xaml", UriKind.RelativeOrAbsolute));
                            break;
                        case "cardcoupon":
                            NavigationService.Navigate(new Uri("View/CouponPage.xaml", UriKind.RelativeOrAbsolute));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void BackImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.Timer.ToString() == "Use")
                {
                    timer.Stop();
                    Source.Log.log.Info("타이머 종료됨");
                    timer.Tick -= new EventHandler(Timer_Tick);
                    timer = null;
                }
                Payment.Pm_Global.Pm_Insert = 0;

                if (MainWindow.pagenum == "1")
                {
                    NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    NavigationService.Navigate(new Uri("View/PageSelect.xaml", UriKind.RelativeOrAbsolute));
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                checkpayment = 1;

                backimg.Source = MainWindow.choicepayment;
                if (MainWindow.inifoldername == "animalhospital")
                {
                    coupon_on = new BitmapImage();
                    coupon_on.BeginInit();
                    coupon_on.UriSource = new Uri(MainWindow.uipath + @"\coupon_on.png", UriKind.RelativeOrAbsolute);
                    coupon_on.CacheOption = BitmapCacheOption.OnLoad;
                    coupon_on.EndInit();
                    
                    coupon_off = new BitmapImage();
                    coupon_off.BeginInit();
                    coupon_off.UriSource = new Uri(MainWindow.uipath + @"\coupon_off.png", UriKind.RelativeOrAbsolute);
                    coupon_off.CacheOption = BitmapCacheOption.OnLoad;
                    coupon_off.EndInit();

                    cashimg.Source = coupon_on;
                }
                else
                {
                    if (MainWindow.paymentway == "cashcoupon")
                    {
                        cashimg.Source = MainWindow.cash_on;

                        coupon_on = new BitmapImage();
                        coupon_on.BeginInit();
                        coupon_on.UriSource = new Uri(MainWindow.uipath + @"\coupon_on.png", UriKind.RelativeOrAbsolute);
                        coupon_on.CacheOption = BitmapCacheOption.OnLoad;
                        coupon_on.EndInit();


                        coupon_off = new BitmapImage();
                        coupon_off.BeginInit();
                        coupon_off.UriSource = new Uri(MainWindow.uipath + @"\coupon_off.png", UriKind.RelativeOrAbsolute);
                        coupon_off.CacheOption = BitmapCacheOption.OnLoad;
                        coupon_off.EndInit();

                        cardimg.Source = coupon_off;
                    }
                    else if (MainWindow.paymentway == "cardcoupon")
                    {
                        coupon_on = new BitmapImage();
                        coupon_on.BeginInit();
                        coupon_on.UriSource = new Uri(MainWindow.uipath + @"\coupon_on.png", UriKind.RelativeOrAbsolute);
                        coupon_on.CacheOption = BitmapCacheOption.OnLoad;
                        coupon_on.EndInit();


                        coupon_off = new BitmapImage();
                        coupon_off.BeginInit();
                        coupon_off.UriSource = new Uri(MainWindow.uipath + @"\coupon_off.png", UriKind.RelativeOrAbsolute);
                        coupon_off.CacheOption = BitmapCacheOption.OnLoad;
                        coupon_off.EndInit();

                        cashimg.Source = coupon_on;

                        cardimg.Source = MainWindow.card_off;
                    }
                    else if (MainWindow.paymentway == "cardcash")
                    {
                        cashimg.Source = MainWindow.cash_on;
                        cardimg.Source = MainWindow.card_off;
                    }
                }
                BackImg.Source = MainWindow.Backimg;
                NextImg.Source = MainWindow.nextbtn;

                Timer.Foreground = MainWindow.textbrush;

                if (MainWindow.timerlocation == "left")
                {
                    Timer.Margin = new Thickness(0, 130, 1720, 0);
                }

                if (MainWindow.Timer.ToString() == "Use")
                {
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += new EventHandler(Timer_Tick);
                    timer.Start();
                    Timer.Text = checkduration.ToString();
                    Source.Log.log.Info("타이머 시작");
                }
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
                    cardimg.IsEnabled = false;
                    cashimg.IsEnabled = false;
                    BackImg.IsEnabled = false;
                    NextImg.IsEnabled = false;
                }

                if (checkduration == -1)
                {
                    timer.Stop();
                    Source.Log.log.Info("타이머 종료됨");
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
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }
    }
}
