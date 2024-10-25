using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Net;

namespace wpfTest.View
{
    /// <summary>
    /// Payment.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Payment : Page
    {

        #region///INI Import///

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #endregion

        #region///가상 키보드///

        [DllImport("User32.DLL")]
        public static extern Boolean PostMessage(Int32 hWnd, Int32 Msg, Int32 wParam, Int32 lParam);
        public const Int32 WM_USER = 1024;
        public const Int32 WM_CSKEYBOARD = WM_USER + 192;
        public const Int32 WM_CSKEYBOARDMOVE = WM_USER + 193;
        public const Int32 WM_CSKEYBOARDRESIZE = WM_USER + 197;

        static Process keyboardPs;

        #endregion

        #region///변수///

        public static SerialPort port = new SerialPort();
        int totalpay = 0;
        int insertmoney = 0;
        int leftmoney = 0;
        int ts_money = 0;
        int payduration = 120;
        int allintmoney;
        int chargedshotnumber;
        int Couponshotnumber;
        int cp_total;
        int cp;
        string recievedata;
        DispatcherTimer timer = new DispatcherTimer();

        #endregion

        public Payment()
        {
            Source.Log.log.Info("Payment(결제페이지) Level 진입");
            InitializeComponent();
            port.DataReceived += Recieve_Send_Data;
        }

        #region///이미지 로드///

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 결제페이지 셋팅 로드");
            try
            {
                keyboardPs = null;

                PaymentImg.Source = MainWindow.payment;
                if (File.Exists(MainWindow.uipath + @"\Cancle.png"))
                {
                    BitmapImage cancleimg = new BitmapImage();
                    cancleimg.BeginInit();
                    cancleimg.UriSource = new Uri(MainWindow.uipath + @"\Cancle.png", UriKind.RelativeOrAbsolute);
                    cancleimg.CacheOption = BitmapCacheOption.OnLoad;
                    cancleimg.EndInit();
                    BackImg.Source = cancleimg;
                    BackImg.Width = cancleimg.Width;
                    BackImg.Height = cancleimg.Height;
                }
                else
                {
                    BackImg.Source = MainWindow.Backimg;
                    BackImg.Width = MainWindow.Backimg.Width;
                    BackImg.Height = MainWindow.Backimg.Height;
                }

                Timer.Foreground = MainWindow.textbrush;
                TotalPay.Foreground = MainWindow.textbrush;
                InsertMoney.Foreground = MainWindow.textbrush;

                if (MainWindow.timerlocation == "left")
                {
                    Timer.Margin = new Thickness(0, 130, 1720, 0);
                }

                if (MainWindow.Timer.ToString() == "Use")
                {
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += new EventHandler(timer_Tick);
                    timer.Start();
                    Source.Log.log.Info("타이머 시작");
                    Timer.Text = payduration.ToString();
                }
                if (MainWindow.pagenum != "1")
                {
                    ts_money = Convert.ToInt32(PageSelect.totalmoney);
                }
                else
                {
                    ts_money = Convert.ToInt32(MainWindow.moneyset.ToString());
                }
                totalpay = ts_money;
                Pm_Global.Pm_Total = totalpay;
                cp_total = totalpay;
                TotalPay.Text = "\\ " + totalpay.ToString("#,##0");
                InsertMoney.Text = insertmoney.ToString("#,##0");
                SerialDataRecieve();
            }
            catch(Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        #endregion

        #region///지폐기 동작///

        private void SerialDataRecieve() // 지폐기 돈계산 함수
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 지폐기 돈 셋팅");
            try
            {
                if (totalpay >= 10000)
                {
                    DataSend('S', 'C', '7');
                    Source.Log.log.Info("DataSend('S', 'C', '7')");

                }
                else if (totalpay < 10000 && totalpay >= 5000)
                {
                    DataSend('S', 'C', '3');
                    Source.Log.log.Info("DataSend('S', 'C', '3')");
                }
                else if (totalpay < 5000)
                {
                    DataSend('S', 'C', '1');
                    Source.Log.log.Info("DataSend('S', 'C', '1')");
                }
                Delay(200);
                DataSend('S', 'A', '\u000d');
                Source.Log.log.Info("DataSend('S', 'A', '\u000d')");
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }


        private void Recieve_Send_Data(object sender, SerialDataReceivedEventArgs e) // 시리얼 통신 데이터 받는값, 반환값
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 시리얼 통신 값 받아오기");
            try
            {
                byte b2;
                recievedata = port.ReadExisting();
                //Delay(100);
                if (recievedata.Length < 5 || recievedata[0] != '$')
                {
                    Source.Log.log.Error("ERROR : " + recievedata);
                }
                else if (recievedata.Substring(0, 3) == "$gb")
                {
                    Source.Log.log.Info("MoneyChecked");
                    b2 = (byte)recievedata[3];
                    if ((uint)b2 <= 1u)
                    {
                        if (b2 != 0)
                        {
                            if (b2 != 1)
                            {
                                Source.Log.log.Error("ERROR : MoneyChecked");
                            }
                            insertmoney += 1000;
                            Source.Log.log.Info("1000원 받음");
                            bool isconnected = MainWindow.CheckInternetConnection();
                            if (isconnected)
                            {
                                SendTotalData1();
                            }
                            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(delegate { InsertMoney.Text = "\\" + insertmoney.ToString("#,##0"); BackImg.IsEnabled = false; })); // 다른 스레드에서 UI작업
                            leftmoney = totalpay - insertmoney;
                            if (insertmoney < totalpay)
                            {
                                if (leftmoney >= 10000)
                                {
                                    DataSend('S', 'C', '7');
                                    Source.Log.log.Info("DataSend('S', 'C', '7')");
                                }
                                else if (leftmoney < 10000 && leftmoney >= 5000)
                                {
                                    DataSend('S', 'C', '3');
                                    Source.Log.log.Info("DataSend('S', 'C', '3')");
                                }
                                else if (leftmoney < 5000)
                                {
                                    DataSend('S', 'C', '1');
                                    Source.Log.log.Info("DataSend('S', 'C', '1')");
                                }
                                Delay(100);
                                DataSend('S', 'A', '\u000d');
                                Source.Log.log.Info("DataSend('S', 'A', '\u000d')");
                                Source.Log.log.Info("지폐기 켜짐");
                            }
                            else if (leftmoney == 0)
                            {
                                return;
                            }
                        }
                    }
                    else if (b2 != 5)
                    {
                        if (b2 != 10)
                        {
                            if (b2 != 50)
                            {
                                Source.Log.log.Error("ERROR : MoneyChecked");
                            }
                            insertmoney += 50000;
                            Source.Log.log.Info("50000원 받음");
                            bool isconnected = MainWindow.CheckInternetConnection();
                            if (isconnected)
                            {
                                SendTotalData4();
                            }
                            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(delegate { InsertMoney.Text = "\\" + insertmoney.ToString("#,##0"); BackImg.IsEnabled = false; })); // 다른 스레드에서 UI작업
                            leftmoney = totalpay - insertmoney;
                            if (insertmoney < totalpay)
                            {
                                if (leftmoney >= 10000)
                                {
                                    DataSend('S', 'C', '7');
                                    Source.Log.log.Info("DataSend('S', 'C', '7')");
                                }
                                else if (leftmoney < 10000 && leftmoney >= 5000)
                                {
                                    DataSend('S', 'C', '3');
                                    Source.Log.log.Info("DataSend('S', 'C', '3')");
                                }
                                else if (leftmoney < 5000)
                                {
                                    DataSend('S', 'C', '1');
                                    Source.Log.log.Info("DataSend('S', 'C', '1')");
                                }
                                Delay(100);
                                DataSend('S', 'A', '\u000d');
                                Source.Log.log.Info("DataSend('S', 'A', '\u000d')");
                                Source.Log.log.Info("지폐기 켜짐");
                            }
                            else if (leftmoney == 0)
                            {
                                return;
                            }
                        }
                        else
                        {
                            insertmoney += 10000;
                            Source.Log.log.Info("10000원 받음");
                            bool isconnected = MainWindow.CheckInternetConnection();
                            if (isconnected)
                            {
                                SendTotalData3();
                            }
                            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(delegate { InsertMoney.Text = "\\" + insertmoney.ToString("#,##0"); BackImg.IsEnabled = false; })); // 다른 스레드에서 UI작업
                            leftmoney = totalpay - insertmoney;
                            if (insertmoney < totalpay)
                            {
                                if (leftmoney >= 10000)
                                {
                                    DataSend('S', 'C', '7');
                                    Source.Log.log.Info("DataSend('S', 'C', '7')");
                                }
                                else if (leftmoney < 10000 && leftmoney >= 5000)
                                {
                                    DataSend('S', 'C', '3');
                                    Source.Log.log.Info("DataSend('S', 'C', '3')");
                                }
                                else if (leftmoney < 5000)
                                {
                                    DataSend('S', 'C', '1');
                                    Source.Log.log.Info("DataSend('S', 'C', '1')");
                                }
                                Delay(100);
                                DataSend('S', 'A', '\u000d');
                                Source.Log.log.Info("DataSend('S', 'A', '\u000d')");
                                Source.Log.log.Info("지폐기 켜짐");
                            }
                            else if (leftmoney == 0)
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        insertmoney += 5000;
                        Source.Log.log.Info("5000원 받음");
                        bool isconnected = MainWindow.CheckInternetConnection();
                        if (isconnected)
                        {
                            SendTotalData2();
                        }
                        Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(delegate { InsertMoney.Text = "\\" + insertmoney.ToString("#,##0"); BackImg.IsEnabled = false; })); // 다른 스레드에서 UI작업
                        leftmoney = totalpay - insertmoney;
                        if (insertmoney < totalpay)
                        {
                            if (leftmoney >= 10000)
                            {
                                DataSend('S', 'C', '7');
                                Source.Log.log.Info("DataSend('S', 'C', '7')");
                            }
                            else if (leftmoney < 10000 && leftmoney >= 5000)
                            {
                                DataSend('S', 'C', '3');
                                Source.Log.log.Info("DataSend('S', 'C', '3')");
                            }
                            else if (leftmoney < 5000)
                            {
                                DataSend('S', 'C', '1');
                                Source.Log.log.Info("DataSend('S', 'C', '1')");
                            }
                            Delay(100);
                            DataSend('S', 'A', '\u000d');
                            Source.Log.log.Info("DataSend('S', 'A', '\u000d')");
                            Source.Log.log.Info("지폐기 켜짐");
                        }
                        else if (leftmoney == 0)
                        {
                            return;
                        }
                    }
                }
                else if (recievedata.Substring(0, 3) == "$ES")
                {
                    switch ((byte)recievedata[3])
                    {
                        case 1:
                            Source.Log.log.Info("InitialOff");
                            DataSend('e', 's', '\u0001');
                            Source.Log.log.Info("DataSend('e', 's', '\u0001')");
                            break;
                        case 2:
                            Source.Log.log.Info("Waiting");
                            DataSend('e', 's', '\u0002');
                            Source.Log.log.Info("DataSend('e', 's', '\u0002')");
                            break;
                        case 4:
                            Source.Log.log.Info("MoneyDeposited");
                            DataSend('e', 's', '\u0004');
                            Source.Log.log.Info("DataSend('e', 's', '\u0004')");
                            break;
                        case 11:
                            Source.Log.log.Info("DepositCompoleted");
                            DataSend('e', 's', '\v');
                            Source.Log.log.Info("DataSend('e', 's', '\v')");
                            DataSend('G', 'B', '?');
                            Source.Log.log.Info("DataSend('G', 'B', '?')");
                            break;
                    }
                }
                return;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
                return;
            }
        }

        public static void DataSend(char byte2, char byte3, char byte4) //지폐기로 데이터 보내기
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 지폐기에 데이터 보냄. 데이터 : " + byte2 + byte3 + byte4 + ((byte)(byte2 + byte3 + byte4)));
            try
            {
                byte[] array = new byte[5]
                {
                    36,
                    (byte)byte2,
                    (byte)byte3,
                    (byte)byte4,
                    (byte)(byte2 + byte3 + byte4)
                };
                port.Write(array, 0, array.Length);
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        #endregion

        #region///버튼클릭 이벤트///
        private void BackImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 뒤로 페이지 이동 이미지 클릭");
            try
            {
                BackImg.IsEnabled = false;
                timer.Stop();
                Source.Log.log.Info("타이머 꺼짐");
                totalpay = 0;
                insertmoney = 0;
                leftmoney = 0;
                DataSend('S', 'A', '\u000e');
                Delay(100);
                port.DataReceived -= Recieve_Send_Data;
                Dispose();
                if (MainWindow.paymentini.ToString() == "cardcash" || MainWindow.paymentini.ToString() == "cashcoupon" || MainWindow.paymentini.ToString() == "cardcoupon")
                {
                    NavigationService.Navigate(new Uri("View/ChoicePayment.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch(Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void NextPage()
        {
            try
            {
                timer.Stop();
                Source.Log.log.Info("타이머 정지");
                insertmoney = 0;
                leftmoney = 0;
                Pm_Global.Pm_Insert = 0;
                cp = 0;
                port.DataReceived -= Recieve_Send_Data;
                Dispose();
                NavigationService.Navigate(new Uri("View/paymentcomplete.xaml", UriKind.RelativeOrAbsolute));
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch(Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void timer_Tick(object sender, EventArgs e) // 타이머S
        {
            try
            {
                if (MainWindow.Timer.ToString() == "Use")
                {
                    Timer.Text = payduration.ToString();
                    payduration--;
                    if (payduration == -1)
                    {
                        timer.Stop();
                        Source.Log.log.Info("타이머 정지");
                        totalpay = 0;
                        insertmoney = 0;
                        leftmoney = 0;
                        DataSend('S', 'A', '\u000e');
                        Dispose();
                        port.DataReceived -= Recieve_Send_Data;
                        Main.port.Close();
                        if (Main.port.IsOpen)
                        {
                            Main.port.Close();
                        }
                        NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
                    }
                }
                if (cp == cp_total)
                {
                    timer.Stop();
                    Source.Log.log.Info("타이머 정지");
                    DataSend('S', 'A', '\u000e');
                    string day = DateTime.Today.ToString("dd");
                    Delay(200);
                    if (MainWindow.couponshot.ToString() == "")
                    {
                        Couponshotnumber = 0;
                        Couponshotnumber++;
                        MainWindow.couponshot.Append(Couponshotnumber);
                        WritePrivateProfileString("CouponShot", day, Convert.ToString(Couponshotnumber), MainWindow.bankbookinipath);
                    }
                    else
                    {
                        if (MainWindow.day == day)
                        {
                            Couponshotnumber = Convert.ToInt32(MainWindow.couponshot.ToString());
                            Couponshotnumber++;
                            MainWindow.couponshot.Clear();
                            MainWindow.couponshot.Append(Couponshotnumber);
                            WritePrivateProfileString("CouponShot", day, Convert.ToString(MainWindow.couponshot), MainWindow.bankbookinipath);
                        }
                        else
                        {
                            MainWindow.couponshot.Clear();
                            MainWindow.couponshot.Append(0);
                            Couponshotnumber = Convert.ToInt32(MainWindow.couponshot.ToString());
                            Couponshotnumber++;
                            MainWindow.couponshot.Clear();
                            MainWindow.couponshot.Append(Couponshotnumber);
                            WritePrivateProfileString("CouponShot", day, Convert.ToString(MainWindow.couponshot), MainWindow.bankbookinipath);
                        }
                    }
                    port.DataReceived -= Recieve_Send_Data;
                    Main.port.Close();
                    if (Main.port.IsOpen)
                    {
                        Main.port.Close();
                    }
                    NextPage();
                }
                if (Pm_Global.Pm_Insert == Pm_Global.Pm_Total)
                {
                    timer.Stop();
                    Source.Log.log.Info("타이머 정지");
                    DataSend('S', 'A', '\u000e');
                    string day = DateTime.Today.ToString("dd");
                    Delay(200);
                    port.DataReceived -= Recieve_Send_Data;
                    Main.port.Close();
                    if (Main.port.IsOpen)
                    {
                        Main.port.Close();
                    }
                    NextPage();
                }
                if (insertmoney == totalpay)
                {
                    timer.Stop();
                    Source.Log.log.Info("타이머 정지");
                    port.DataReceived -= Recieve_Send_Data;
                    Main.port.Close();
                    if (Main.port.IsOpen)
                    {
                        Main.port.Close();
                    }
                    string day = DateTime.Today.ToString("dd");
                    Delay(200);
                    if (MainWindow.collectmoney.ToString() == "0")
                    {
                        MainWindow.collectmoney.Clear();
                        MainWindow.collectmoney.Append(insertmoney);
                        WritePrivateProfileString("Money", day, Convert.ToString(MainWindow.collectmoney), MainWindow.bankbookinipath);
                    }
                    else
                    {
                        if (MainWindow.day == day)
                        {
                            allintmoney = Convert.ToInt32(MainWindow.collectmoney.ToString());
                            MainWindow.collectmoney.Clear();
                            MainWindow.collectmoney.Append(Convert.ToString(allintmoney + insertmoney));
                            WritePrivateProfileString("Money", day, Convert.ToString(MainWindow.collectmoney), MainWindow.bankbookinipath);
                        }
                        else
                        {
                            MainWindow.collectmoney.Clear();
                            MainWindow.collectmoney.Append(0);
                            allintmoney = Convert.ToInt32(MainWindow.collectmoney.ToString());
                            MainWindow.collectmoney.Append(Convert.ToString(allintmoney + insertmoney));
                            WritePrivateProfileString("Money", day, Convert.ToString(MainWindow.collectmoney), MainWindow.bankbookinipath);
                        }
                    }
                    if (MainWindow.chargedshot.ToString() == "0")
                    {
                        chargedshotnumber = 0;
                        chargedshotnumber++;
                        MainWindow.chargedshot.Append(chargedshotnumber);
                        WritePrivateProfileString("ChargedShot", day, Convert.ToString(MainWindow.chargedshot), MainWindow.bankbookinipath);
                    }
                    else
                    {
                        if (MainWindow.day == day)
                        {
                            chargedshotnumber = Convert.ToInt32(MainWindow.chargedshot.ToString());
                            chargedshotnumber++;
                            MainWindow.chargedshot.Clear();
                            MainWindow.chargedshot.Append(chargedshotnumber);
                            WritePrivateProfileString("ChargedShot", day, Convert.ToString(MainWindow.chargedshot), MainWindow.bankbookinipath);
                        }
                        else
                        {
                            MainWindow.chargedshot.Clear();
                            MainWindow.chargedshot.Append(0);
                            chargedshotnumber = Convert.ToInt32(MainWindow.chargedshot.ToString());
                            chargedshotnumber++;
                            MainWindow.chargedshot.Clear();
                            MainWindow.chargedshot.Append(chargedshotnumber);
                            WritePrivateProfileString("ChargedShot", day, Convert.ToString(MainWindow.chargedshot), MainWindow.bankbookinipath);
                        }
                    }
                    bool isconnected = MainWindow.CheckInternetConnection();
                    if (isconnected)
                    {
                        SendTotalData();
                    }
                    NextPage();
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }
        #endregion

        private static DateTime Delay(int MS)
        {
            DateTime thisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime afterMoment = thisMoment.Add(duration);

            while(afterMoment >= thisMoment)
            {
                if (System.Windows.Application.Current != null)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new Action(delegate { }));
                }
                thisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }

        public static class Pm_Global
        {
            public static int Pm_Total;
            public static int Pm_Insert = 0;
        }

        private void Dispose()
        {
            timer.Tick -= new EventHandler(timer_Tick);
            PaymentImg.Source = null;
            PaymentImg = null;
            BackImg.Source = null; ;
            BackImg = null;
            TotalPay.Text = null;
            TotalPay = null;
            InsertMoney.Text = null;
            InsertMoney = null;
            Timer.Text = null;
            Timer = null;
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
        private void SendTotalData1()
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
                    switch (MainWindow.Way.ToString())
                    {
                        case "1":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                            break;
                        case "2":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                            break;
                        case "3":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 1000 + "&CARD_AMT=" + 0;
                            break;
                        case "4":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 1000 + "&CARD_AMT=" + 0;
                            break;
                    }
                }
                else
                {
                    switch (MainWindow.Way.ToString())
                    {
                        case "1":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                            break;
                        case "2":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                            break;
                        case "3":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 1000 + "&CARD_AMT=" + 0;
                            break;
                        case "4":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 1000 + "&CARD_AMT=" + 0;
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

        private void SendTotalData2()
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
                    switch (MainWindow.Way.ToString())
                    {
                        case "1":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                            break;
                        case "2":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                            break;
                        case "3":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 5000 + "&CARD_AMT=" + 0;
                            break;
                        case "4":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 5000 + "&CARD_AMT=" + 0;
                            break;
                    }
                }
                else
                {
                    switch (MainWindow.Way.ToString())
                    {
                        case "1":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                            break;
                        case "2":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                            break;
                        case "3":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 5000 + "&CARD_AMT=" + 0;
                            break;
                        case "4":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 5000 + "&CARD_AMT=" + 0;
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

        private void SendTotalData3()
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
                    switch (MainWindow.Way.ToString())
                    {
                        case "1":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                            break;
                        case "2":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                            break;
                        case "3":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 10000 + "&CARD_AMT=" + 0;
                            break;
                        case "4":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 10000 + "&CARD_AMT=" + 0;
                            break;
                    }
                }
                else
                {
                    switch (MainWindow.Way.ToString())
                    {
                        case "1":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                            break;
                        case "2":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                            break;
                        case "3":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 10000 + "&CARD_AMT=" + 0;
                            break;
                        case "4":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 10000 + "&CARD_AMT=" + 0;
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

        private void SendTotalData4()
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
                    switch (MainWindow.Way.ToString())
                    {
                        case "1":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                            break;
                        case "2":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                            break;
                        case "3":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 50000 + "&CARD_AMT=" + 0;
                            break;
                        case "4":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 50000 + "&CARD_AMT=" + 0;
                            break;
                    }
                }
                else
                {
                    switch (MainWindow.Way.ToString())
                    {
                        case "1":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                            break;
                        case "2":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                            break;
                        case "3":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 50000 + "&CARD_AMT=" + 0;
                            break;
                        case "4":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 50000 + "&CARD_AMT=" + 0;
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