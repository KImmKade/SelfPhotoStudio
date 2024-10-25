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
using System.Windows.Threading;
using Google.Protobuf.WellKnownTypes;
using Npgsql;
using Accord.Collections;
using System.IO;
using System.Net;

namespace wpfTest.View
{
    /// <summary>
    /// CouponPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CouponPage : Page
    {

        DispatcherTimer timer = new DispatcherTimer();
        int cpduration = Convert.ToInt32(MainWindow.count);
        string couponnum = "";
        string dbcouponnum = "";
        int dbexpired = 0;
        int dbremaincount;
        bool checkcoupon = false;
        string tbname = "";
        bool checkpass = false;
        

        BitmapImage btn1;
        BitmapImage btn2;
        BitmapImage btn3;
        BitmapImage btn4;
        BitmapImage btn5;
        BitmapImage btn6;
        BitmapImage btn7;
        BitmapImage btn8;
        BitmapImage btn9;
        BitmapImage btn0;
        BitmapImage btnA;
        BitmapImage btnB;
        BitmapImage btnC;
        BitmapImage btnD;
        BitmapImage btnE;
        BitmapImage btnF;
        BitmapImage btnDel;
        BitmapImage btnOK;

        BitmapImage couponpaymentimg = new BitmapImage();

        public CouponPage()
        {
            Source.Log.log.Info("CouponPage 진입");
            InitializeComponent();
        }

        private void Key_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername.Contains("animalhospital"))
                {
                    if (couponnum.Length < 8)
                    {
                        couponnum += "1";
                    }
                    else
                    { }
                }
                else if (MainWindow.inifoldername.Contains("tech"))
                {
                    if (couponnum.Length < 6)
                    {
                        couponnum += "1";
                    }
                    else
                    { }
                }
                else
                {
                    if (couponnum.Length < 11)
                    {
                        couponnum += "1";
                    }
                    else
                    { }
                }

                CouponNumber.Text = couponnum;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Key_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername.Contains("animalhospital"))
                {
                    if (couponnum.Length < 8)
                    {
                        couponnum += "2";
                    }
                    else
                    { }
                }
                else if (MainWindow.inifoldername.Contains("tech"))
                {
                    if (couponnum.Length < 6)
                    {
                        couponnum += "2";
                    }
                    else
                    { }
                }
                else
                {
                    if (couponnum.Length < 11)
                    {
                        couponnum += "2";
                    }
                    else
                    { }
                }

                CouponNumber.Text = couponnum;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Key_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername.Contains("animalhospital"))
                {
                    if (couponnum.Length < 8)
                    {
                        couponnum += "3";
                    }
                    else
                    { }
                }
                else if (MainWindow.inifoldername.Contains("tech"))
                {
                    if (couponnum.Length < 6)
                    {
                        couponnum += "3";
                    }
                    else
                    { }
                }
                else
                {
                    if (couponnum.Length < 11)
                    {
                        couponnum += "3";
                    }
                    else
                    { }
                }

                CouponNumber.Text = couponnum;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Key_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername.Contains("animalhospital"))
                {
                    if (couponnum.Length < 8)
                    {
                        couponnum += "4";
                    }
                    else
                    { }
                }
                else if (MainWindow.inifoldername.Contains("tech"))
                {
                    if (couponnum.Length < 6)
                    {
                        couponnum += "4";
                    }
                    else
                    { }
                }
                else
                {
                    if (couponnum.Length < 11)
                    {
                        couponnum += "4";
                    }
                    else
                    { }
                }

                CouponNumber.Text = couponnum;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Key_5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername.Contains("animalhospital"))
                {
                    if (couponnum.Length < 8)
                    {
                        couponnum += "5";
                    }
                    else
                    { }
                }
                else if (MainWindow.inifoldername.Contains("tech"))
                {
                    if (couponnum.Length < 6)
                    {
                        couponnum += "5";
                    }
                    else
                    { }
                }
                else
                {
                    if (couponnum.Length < 11)
                    {
                        couponnum += "5";
                    }
                    else
                    { }
                }

                CouponNumber.Text = couponnum;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Key_6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername.Contains("animalhospital"))
                {
                    if (couponnum.Length < 8)
                    {
                        couponnum += "6";
                    }
                    else
                    { }
                }
                else if (MainWindow.inifoldername.Contains("tech"))
                {
                    if (couponnum.Length < 6)
                    {
                        couponnum += "6";
                    }
                    else
                    { }
                }
                else
                {
                    if (couponnum.Length < 11)
                    {
                        couponnum += "6";
                    }
                    else
                    { }
                }

                CouponNumber.Text = couponnum;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Key_7_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername.Contains("animalhospital"))
                {
                    if (couponnum.Length < 8)
                    {
                        couponnum += "7";
                    }
                    else
                    { }
                }
                else if (MainWindow.inifoldername.Contains("tech"))
                {
                    if (couponnum.Length < 6)
                    {
                        couponnum += "7";
                    }
                    else
                    { }
                }
                else
                {
                    if (couponnum.Length < 11)
                    {
                        couponnum += "7";
                    }
                    else
                    { }
                }

                CouponNumber.Text = couponnum;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Key_8_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername.Contains("animalhospital"))
                {
                    if (couponnum.Length < 8)
                    {
                        couponnum += "8";
                    }
                    else
                    { }
                }
                else if (MainWindow.inifoldername.Contains("tech"))
                {
                    if (couponnum.Length < 6)
                    {
                        couponnum += "8";
                    }
                    else
                    { }
                }
                else
                {
                    if (couponnum.Length < 11)
                    {
                        couponnum += "8";
                    }
                    else
                    { }
                }

                CouponNumber.Text = couponnum;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Key_9_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername.Contains("animalhospital"))
                {
                    if (couponnum.Length < 8)
                    {
                        couponnum += "9";
                    }
                    else
                    { }
                }
                else if (MainWindow.inifoldername.Contains("tech"))
                {
                    if (couponnum.Length < 6)
                    {
                        couponnum += "9";
                    }
                    else
                    { }
                }
                else
                {
                    if (couponnum.Length < 11)
                    {
                        couponnum += "9";
                    }
                    else
                    { }
                }

                CouponNumber.Text = couponnum;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Key_0_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername.Contains("animalhospital"))
                {
                    if (couponnum.Length < 8)
                    {
                        couponnum += "0";
                    }
                    else
                    { }
                }
                else if (MainWindow.inifoldername.Contains("tech"))
                {
                    if (couponnum.Length < 6)
                    {
                        couponnum += "0";
                    }
                    else
                    { }
                }
                else
                {
                    if (couponnum.Length < 11)
                    {
                        couponnum += "0";
                    }
                    else
                    { }
                }

                CouponNumber.Text = couponnum;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void keyA_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername.Contains("animalhospital"))
                {
                    if (couponnum.Length < 8)
                    {
                        couponnum += "A";
                    }
                    else
                    { }
                }
                else if (MainWindow.inifoldername.Contains("tech"))
                {
                    if (couponnum.Length < 6)
                    {
                        couponnum += "A";
                    }
                    else
                    { }
                }
                else
                {
                    if (couponnum.Length < 11)
                    {
                        couponnum += "A";
                    }
                    else
                    { }
                }

                CouponNumber.Text = couponnum;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void keyB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername.Contains("animalhospital"))
                {
                    if (couponnum.Length < 8)
                    {
                        couponnum += "B";
                    }
                    else
                    { }
                }
                else if (MainWindow.inifoldername.Contains("tech"))
                {
                    if (couponnum.Length < 6)
                    {
                        couponnum += "B";
                    }
                    else
                    { }
                }
                else
                {
                    if (couponnum.Length < 11)
                    {
                        couponnum += "B";
                    }
                    else
                    { }
                }

                CouponNumber.Text = couponnum;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void keyC_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername.Contains("animalhospital"))
                {
                    if (couponnum.Length < 8)
                    {
                        couponnum += "C";
                    }
                    else
                    { }
                }
                else if (MainWindow.inifoldername.Contains("tech"))
                {
                    if (couponnum.Length < 6)
                    {
                        couponnum += "C";
                    }
                    else
                    { }
                }
                else
                {
                    if (couponnum.Length < 11)
                    {
                        couponnum += "C";
                    }
                    else
                    { }
                }

                CouponNumber.Text = couponnum;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void keyD_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername.Contains("animalhospital"))
                {
                    if (couponnum.Length < 8)
                    {
                        couponnum += "D";
                    }
                    else
                    { }
                }
                else if (MainWindow.inifoldername.Contains("tech"))
                {
                    if (couponnum.Length < 6)
                    {
                        couponnum += "D";
                    }
                    else
                    { }
                }
                else
                {
                    if (couponnum.Length < 11)
                    {
                        couponnum += "D";
                    }
                    else
                    { }
                }

                CouponNumber.Text = couponnum;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void keyE_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername.Contains("animalhospital"))
                {
                    if (couponnum.Length < 8)
                    {
                        couponnum += "E";
                    }
                    else
                    { }
                }
                else if (MainWindow.inifoldername.Contains("tech"))
                {
                    if (couponnum.Length < 6)
                    {
                        couponnum += "E";
                    }
                    else
                    { }
                }
                else
                {
                    if (couponnum.Length < 11)
                    {
                        couponnum += "E";
                    }
                    else
                    { }
                }

                CouponNumber.Text = couponnum;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void keyF_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername.Contains("animalhospital"))
                {
                    if (couponnum.Length < 8)
                    {
                        couponnum += "F";
                    }
                    else
                    { }
                }
                else if (MainWindow.inifoldername.Contains("tech"))
                {
                    if (couponnum.Length < 6)
                    {
                        couponnum += "F";
                    }
                    else
                    { }
                }
                else
                {
                    if (couponnum.Length < 11)
                    {
                        couponnum += "F";
                    }
                    else
                    { }
                }

                CouponNumber.Text = couponnum;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Key_Del_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                checkpass = false;
                remain.Text = "";

                if (checkcoupon)
                {
                    checkcoupon = false;
                }
                if (couponnum.Length != 0)
                {
                    couponnum = RemoveLastChar(couponnum);
                }
                CouponNumber.Text = couponnum;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        public string RemoveLastChar(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }

        #region///타이머///

        private void Timer_Tick(object sender, EventArgs e)
        {

            cpduration--;
            if (cpduration < 0)
            {
                Timer.Text = "0";
            }
            else
            {
                Timer.Text = cpduration.ToString();
            }

            if (cpduration == 0)
            {
                NextImg.IsEnabled = false;
            }

            if (cpduration == -1)
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
                backgroundimg.Source = null;
                backgroundimg = null;

                Key_0.Source = null;
                Key_0 = null;
                Key_1.Source = null;
                Key_1 = null;
                Key_2.Source = null;
                Key_2 = null;
                Key_3.Source = null;
                Key_3 = null;
                Key_4.Source = null;
                Key_4 = null;
                Key_5.Source = null;
                Key_5 = null;
                Key_6.Source = null;
                Key_6 = null;
                Key_7.Source = null;
                Key_7 = null;
                Key_8.Source = null;
                Key_8 = null;
                Key_9.Source = null;
                Key_9 = null;
                keyA.Source = null;
                keyA = null;
                keyB.Source = null;
                keyB = null;
                keyC.Source = null;
                keyC = null;
                keyD.Source = null;
                keyD = null;
                keyE.Source = null;
                keyE = null;
                keyF.Source = null;
                keyF = null;
                Key_Del.Source = null;
                Key_Del = null;
                BackBtn.Source = null;
                BackBtn = null;
                NextImg.Source = null;
                NextImg = null;

                if (MainWindow.Timer.ToString() == "Use")
                {
                    timer.Stop();
                    timer.Tick -= new EventHandler(Timer_Tick);
                    Timer.Text = null;
                    Timer = null;
                }
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
                if (Source.OneClick.One_Click())
                {
                    Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                    string connectionString = "Host=175.125.92.65;Port=5432;Database=onecut;User Id=onecut;Password=one6677";
                    tbname = "coupons_" + MainWindow.inifoldername;

                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();

                        string sqlQuery = "SELECT remaining_count, expired From " + tbname + " WHERE coupon_number = @couponnum";

                        using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                        {
                            command.Parameters.AddWithValue("@couponnum", couponnum);

                            using (NpgsqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    dbremaincount = reader.GetInt32(0);
                                    dbexpired = Convert.ToInt32(reader.GetString(1));
                                    checkcoupon = true;
                                }
                            }
                        }

                        connection.Close();
                    }

                    if (dbexpired == 0 && dbremaincount != 0 && checkcoupon)
                    {
                        connectionString = "Host=175.125.92.65;Port=5432;Database=onecut;User Id=onecut;Password=one6677";
                        string userid = MainWindow.inifoldername;
                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                        {
                            connection.Open();

                            string sql = "UPDATE " + tbname + " SET remaining_count = @remaincount ,expired = 1 WHERE coupon_number = @couponnum";

                            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                            {
                                command.Parameters.AddWithValue("remaincount", dbremaincount - 1);
                                // @temp1count 매개변수 추가
                                command.Parameters.AddWithValue("@expired", 1); // temp1count는 실제 값으로 변경해야 합니다.

                                // @userid 매개변수 추가
                                command.Parameters.AddWithValue("@couponnum", couponnum); // userid는 실제 사용자 ID 값으로 변경해야 합니다.

                                command.ExecuteNonQuery();
                            }

                            connection.Close();
                        }
                        SendTotalData();
                        Dispose();
                        NavigationService.Navigate(new Uri("View/paymentcomplete.xaml", UriKind.RelativeOrAbsolute));
                        checkpass = true;
                    }
                    else
                    {
                        remain.Text = "쿠폰번호를 확인해주세요.";
                        checkpass = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void BackBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                Dispose();

                if (MainWindow.paymentini.ToString() == "cashcoupon" || MainWindow.paymentini.ToString() == "cardcoupon")
                {
                    NavigationService.Navigate(new Uri("View/ChoicePayment.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
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

                checkcoupon = false;
                checkpass = false;

                couponpaymentimg = new BitmapImage();
                couponpaymentimg.BeginInit();
                couponpaymentimg.UriSource = new Uri(MainWindow.uipath + @"\CouponPaymentimg.png", UriKind.RelativeOrAbsolute);
                couponpaymentimg.CacheOption = BitmapCacheOption.OnLoad;
                couponpaymentimg.EndInit();

                backgroundimg.Source = couponpaymentimg;

                NextImg.Source = MainWindow.nextbtn;
                BackBtn.Source = MainWindow.Backimg;

                if (MainWindow.couponkind == "num")
                {
                    btn0 = new BitmapImage();
                    btn0.BeginInit();
                    btn0.UriSource = new Uri(MainWindow.uipath + @"\coupon_0.png", UriKind.RelativeOrAbsolute);
                    btn0.CacheOption = BitmapCacheOption.OnLoad;
                    btn0.EndInit();

                    Key_0.Source = btn0;

                    btn1 = new BitmapImage();
                    btn1.BeginInit();
                    btn1.UriSource = new Uri(MainWindow.uipath + @"\coupon_1.png", UriKind.RelativeOrAbsolute);
                    btn1.CacheOption = BitmapCacheOption.OnLoad;
                    btn1.EndInit();

                    Key_1.Source = btn1;

                    btn2 = new BitmapImage();
                    btn2.BeginInit();
                    btn2.UriSource = new Uri(MainWindow.uipath + @"\coupon_2.png", UriKind.RelativeOrAbsolute);
                    btn2.CacheOption = BitmapCacheOption.OnLoad;
                    btn2.EndInit();

                    Key_2.Source = btn2;

                    btn3 = new BitmapImage();
                    btn3.BeginInit();
                    btn3.UriSource = new Uri(MainWindow.uipath + @"\coupon_3.png", UriKind.RelativeOrAbsolute);
                    btn3.CacheOption = BitmapCacheOption.OnLoad;
                    btn3.EndInit();

                    Key_3.Source = btn3;

                    btn4 = new BitmapImage();
                    btn4.BeginInit();
                    btn4.UriSource = new Uri(MainWindow.uipath + @"\coupon_4.png", UriKind.RelativeOrAbsolute);
                    btn4.CacheOption = BitmapCacheOption.OnLoad;
                    btn4.EndInit();

                    Key_4.Source = btn4;

                    btn5 = new BitmapImage();
                    btn5.BeginInit();
                    btn5.UriSource = new Uri(MainWindow.uipath + @"\coupon_5.png", UriKind.RelativeOrAbsolute);
                    btn5.CacheOption = BitmapCacheOption.OnLoad;
                    btn5.EndInit();

                    Key_5.Source = btn5;

                    btn6 = new BitmapImage();
                    btn6.BeginInit();
                    btn6.UriSource = new Uri(MainWindow.uipath + @"\coupon_6.png", UriKind.RelativeOrAbsolute);
                    btn6.CacheOption = BitmapCacheOption.OnLoad;
                    btn6.EndInit();

                    Key_6.Source = btn6;

                    btn7 = new BitmapImage();
                    btn7.BeginInit();
                    btn7.UriSource = new Uri(MainWindow.uipath + @"\coupon_7.png", UriKind.RelativeOrAbsolute);
                    btn7.CacheOption = BitmapCacheOption.OnLoad;
                    btn7.EndInit();

                    Key_7.Source = btn7;

                    btn8 = new BitmapImage();
                    btn8.BeginInit();
                    btn8.UriSource = new Uri(MainWindow.uipath + @"\coupon_8.png", UriKind.RelativeOrAbsolute);
                    btn8.CacheOption = BitmapCacheOption.OnLoad;
                    btn8.EndInit();

                    Key_8.Source = btn8;

                    btn9 = new BitmapImage();
                    btn9.BeginInit();
                    btn9.UriSource = new Uri(MainWindow.uipath + @"\coupon_9.png", UriKind.RelativeOrAbsolute);
                    btn9.CacheOption = BitmapCacheOption.OnLoad;
                    btn9.EndInit();

                    Key_9.Source = btn9;

                    btnDel = new BitmapImage();
                    btnDel.BeginInit();
                    btnDel.UriSource = new Uri(MainWindow.uipath + @"\coupon_Del.png", UriKind.RelativeOrAbsolute);
                    btnDel.CacheOption = BitmapCacheOption.OnLoad;
                    btnDel.EndInit();

                    Key_Del.Source = btnDel;
                }
                else if (MainWindow.couponkind == "numchar")
                {
                    btn0 = new BitmapImage();
                    btn0.BeginInit();
                    btn0.UriSource = new Uri(MainWindow.uipath + @"\coupon_0.png", UriKind.RelativeOrAbsolute);
                    btn0.CacheOption = BitmapCacheOption.OnLoad;
                    btn0.EndInit();

                    Key_0.Source = btn0;

                    btn1 = new BitmapImage();
                    btn1.BeginInit();
                    btn1.UriSource = new Uri(MainWindow.uipath + @"\coupon_1.png", UriKind.RelativeOrAbsolute);
                    btn1.CacheOption = BitmapCacheOption.OnLoad;
                    btn1.EndInit();

                    Key_1.Source = btn1;

                    btn2 = new BitmapImage();
                    btn2.BeginInit();
                    btn2.UriSource = new Uri(MainWindow.uipath + @"\coupon_2.png", UriKind.RelativeOrAbsolute);
                    btn2.CacheOption = BitmapCacheOption.OnLoad;
                    btn2.EndInit();

                    Key_2.Source = btn2;

                    btn3 = new BitmapImage();
                    btn3.BeginInit();
                    btn3.UriSource = new Uri(MainWindow.uipath + @"\coupon_3.png", UriKind.RelativeOrAbsolute);
                    btn3.CacheOption = BitmapCacheOption.OnLoad;
                    btn3.EndInit();

                    Key_3.Source = btn3;

                    btn4 = new BitmapImage();
                    btn4.BeginInit();
                    btn4.UriSource = new Uri(MainWindow.uipath + @"\coupon_4.png", UriKind.RelativeOrAbsolute);
                    btn4.CacheOption = BitmapCacheOption.OnLoad;
                    btn4.EndInit();

                    Key_4.Source = btn4;

                    btn5 = new BitmapImage();
                    btn5.BeginInit();
                    btn5.UriSource = new Uri(MainWindow.uipath + @"\coupon_5.png", UriKind.RelativeOrAbsolute);
                    btn5.CacheOption = BitmapCacheOption.OnLoad;
                    btn5.EndInit();

                    Key_5.Source = btn5;

                    btn6 = new BitmapImage();
                    btn6.BeginInit();
                    btn6.UriSource = new Uri(MainWindow.uipath + @"\coupon_6.png", UriKind.RelativeOrAbsolute);
                    btn6.CacheOption = BitmapCacheOption.OnLoad;
                    btn6.EndInit();

                    Key_6.Source = btn6;

                    btn7 = new BitmapImage();
                    btn7.BeginInit();
                    btn7.UriSource = new Uri(MainWindow.uipath + @"\coupon_7.png", UriKind.RelativeOrAbsolute);
                    btn7.CacheOption = BitmapCacheOption.OnLoad;
                    btn7.EndInit();

                    Key_7.Source = btn7;

                    btn8 = new BitmapImage();
                    btn8.BeginInit();
                    btn8.UriSource = new Uri(MainWindow.uipath + @"\coupon_8.png", UriKind.RelativeOrAbsolute);
                    btn8.CacheOption = BitmapCacheOption.OnLoad;
                    btn8.EndInit();

                    Key_8.Source = btn8;

                    btn9 = new BitmapImage();
                    btn9.BeginInit();
                    btn9.UriSource = new Uri(MainWindow.uipath + @"\coupon_9.png", UriKind.RelativeOrAbsolute);
                    btn9.CacheOption = BitmapCacheOption.OnLoad;
                    btn9.EndInit();

                    Key_9.Source = btn9;

                    btnDel = new BitmapImage();
                    btnDel.BeginInit();
                    btnDel.UriSource = new Uri(MainWindow.uipath + @"\coupon_Del.png", UriKind.RelativeOrAbsolute);
                    btnDel.CacheOption = BitmapCacheOption.OnLoad;
                    btnDel.EndInit();

                    Key_Del.Source = btnDel;

                    btnA = new BitmapImage();
                    btnA.BeginInit();
                    btnA.UriSource = new Uri(MainWindow.uipath + @"\coupon_a.png", UriKind.RelativeOrAbsolute);
                    btnA.CacheOption = BitmapCacheOption.OnLoad;
                    btnA.EndInit();

                    keyA.Source = btnA;

                    btnB = new BitmapImage();
                    btnB.BeginInit();
                    btnB.UriSource = new Uri(MainWindow.uipath + @"\coupon_b.png", UriKind.RelativeOrAbsolute);
                    btnB.CacheOption = BitmapCacheOption.OnLoad;
                    btnB.EndInit();

                    keyB.Source = btnB;

                    btnC = new BitmapImage();
                    btnC.BeginInit();
                    btnC.UriSource = new Uri(MainWindow.uipath + @"\coupon_c.png", UriKind.RelativeOrAbsolute);
                    btnC.CacheOption = BitmapCacheOption.OnLoad;
                    btnC.EndInit();

                    keyC.Source = btnC;

                    btnD = new BitmapImage();
                    btnD.BeginInit();
                    btnD.UriSource = new Uri(MainWindow.uipath + @"\coupon_d.png", UriKind.RelativeOrAbsolute);
                    btnD.CacheOption = BitmapCacheOption.OnLoad;
                    btnD.EndInit();

                    keyD.Source = btnD;

                    btnE = new BitmapImage();
                    btnE.BeginInit();
                    btnE.UriSource = new Uri(MainWindow.uipath + @"\coupon_e.png", UriKind.RelativeOrAbsolute);
                    btnE.CacheOption = BitmapCacheOption.OnLoad;
                    btnE.EndInit();

                    keyE.Source = btnE;

                    btnF = new BitmapImage();
                    btnF.BeginInit();
                    btnF.UriSource = new Uri(MainWindow.uipath + @"\coupon_f.png", UriKind.RelativeOrAbsolute);
                    btnF.CacheOption = BitmapCacheOption.OnLoad;
                    btnF.EndInit();

                    keyF.Source = btnF;

                    Key_1.Margin = new Thickness(478, 570, 1920 - 478 - 98, 1080 - 570 - 98);
                    Key_2.Margin = new Thickness(586, 570, 1920 - 586 - 98, 1080 - 570 - 98);
                    Key_3.Margin = new Thickness(694, 570, 1920 - 694 - 98, 1080 - 570 - 98);
                    Key_4.Margin = new Thickness(802, 570, 1920 - 802 - 98, 1080 - 570 - 98);
                    Key_5.Margin = new Thickness(910, 570, 1920 - 910 - 98, 1080 - 570 - 98);
                    Key_6.Margin = new Thickness(1018, 570, 1920 - 1018 - 98, 1080 - 570 - 98);
                    Key_7.Margin = new Thickness(1126, 570, 1920 - 1126 - 98, 1080 - 570 - 98);
                    Key_8.Margin = new Thickness(1234, 570, 1920 - 1234 - 98, 1080 - 570 - 98);
                    Key_9.Margin = new Thickness(1342, 570, 1920 - 1342 - 98, 1080 - 570 - 98);
                    Key_0.Margin = new Thickness(478, 680, 1920 - 478 - 98, 1080 - 680 - 98);
                    keyA.Margin = new Thickness(586, 680, 1920 - 586 - 98, 1080 - 680 - 98);
                    keyB.Margin = new Thickness(694, 680, 1920 - 694 - 98, 1080 - 680 - 98);
                    keyC.Margin = new Thickness(802, 680, 1920 - 802 - 98, 1080 - 680 - 98);
                    keyD.Margin = new Thickness(910, 680, 1920 - 910 - 98, 1080 - 680 - 98);
                    keyE.Margin = new Thickness(1018, 680, 1920 - 1018 - 98, 1080 - 680 - 98);
                    keyF.Margin = new Thickness(1126, 680, 1920 - 1126 - 98, 1080 - 680 - 98);
                    Key_Del.Margin = new Thickness(1234, 680, 1920 - 1234 - 206, 1080 - 680 - 98);
                }

                Timer.Foreground = MainWindow.textbrush;

                if (MainWindow.timerlocation == "left")
                {
                    Timer.Margin = new Thickness(0, 130, 1720, 0);
                }

                if (MainWindow.Timer.ToString() == "Use")
                {
                    Timer.Text = cpduration.ToString();
                    timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += new EventHandler(Timer_Tick);
                    timer.Start();
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void SendTotalData()
        {
            try
            {
                string url = "";

                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                string day = DateTime.Now.ToString("yyyy-MM-dd");

                string sendData = null;

                if (MainWindow.pagenum == "1")
                {
                    if (MainWindow.inifoldername == "animalhospital")
                    {
                        switch(TempSelect.temp)
                        {
                            case 1:
                                switch (MainWindow.Way.ToString())
                                {
                                    case "1":
                                        sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 1 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                                        break;
                                    case "2":
                                        sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 1 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                                        break;
                                    case "3":
                                        sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 1 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                                        break;
                                    case "4":
                                        sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 1 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                                        break;
                                }
                                break;
                            case 2:
                                switch (MainWindow.Way.ToString())
                                {
                                    case "1":
                                        sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 2 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                                        break;
                                    case "2":
                                        sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 2 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                                        break;
                                    case "3":
                                        sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 2 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                                        break;
                                    case "4":
                                        sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 2 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                                        break;
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (MainWindow.Way.ToString())
                        {
                            case "1":
                                sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 1 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                                break;
                            case "2":
                                sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 1 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                                break;
                            case "3":
                                sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 1 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                                break;
                            case "4":
                                sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 1 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                                break;
                        }
                    }
                }
                else
                {
                    switch (MainWindow.Way.ToString())
                    {
                        case "1":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + PageSelect.PaperCount + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                            break;
                        case "2":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + PageSelect.PaperCount + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                            break;
                        case "3":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + PageSelect.PaperCount + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                            break;
                        case "4":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + PageSelect.PaperCount + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                            break;
                    }
                }

                Source.Log.log.Info("매출 데이터 값 : " + sendData);

                byte[] buffer;
                buffer = Encoding.Default.GetBytes(sendData);
                request.ContentLength = buffer.Length;

                Stream sendStream = request.GetRequestStream();
                sendStream.Write(buffer, 0, buffer.Length);
                sendStream.Close();

                WebResponse response = request.GetResponse();

                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);

                string responseMsg = reader.ReadToEnd();

                Source.Log.log.Info(responseMsg);

                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }
    }
}
