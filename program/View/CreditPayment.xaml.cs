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
using System.Runtime.InteropServices;
using System.Windows.Threading;
using System.Threading;
using System.Windows.Shapes;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Interop;
using System.IO;
using System.Net;
using K4os.Compression.LZ4.Streams;

namespace wpfTest.View
{
    /// <summary>
    /// CreditPayment.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CreditPayment : Page
    {

        public static IntPtr ProcessHandle = default(IntPtr);
        static DispatcherTimer timer = new DispatcherTimer();
        static DispatcherTimer creditresulttimer = new DispatcherTimer();
        static DispatcherTimer endtimer = new DispatcherTimer();
        int creditresultduration = 1;
        int creditduration = 90;
        public static int fail = 0;

        int endcount = 3;

        #region///DllImport///

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string IpClassName, string IpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bYk, byte bScan, uint dwFlags, UIntPtr dwExtrainfo);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern void BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern void SetForegroundWindow(IntPtr hWnd);

        #endregion

        public CreditPayment()
        {
            Source.Log.log.Info("카드결제페이지 진입");
            InitializeComponent();
        }

        private void BackImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BackImg.IsEnabled = false;
            PressKey(27);
            fail = 0;
            timer.Stop();
            creditresulttimer.Stop();
            if (creditresulttimer.IsEnabled == true)
            {
                creditresulttimer.Stop();
            }
            timer.Tick -= new EventHandler(timer_Tick);
            creditresulttimer.Tick -= new EventHandler(creditresulttimer_Tick);
            Source.Log.log.Info("이전페이지 진입버튼 클릭");
            if (MainWindow.paymentini.ToString() == "cardcash" || MainWindow.paymentini.ToString() == "cashcoupon" || MainWindow.paymentini.ToString() == "cardcoupon")
            {
                NavigationService.Navigate(new Uri("View/ChoicePayment.xaml", UriKind.RelativeOrAbsolute));
            }
            else
            {
                if (MainWindow.inifoldername.Contains("mediagram"))
                {
                    NavigationService.Navigate(new Uri("View/PageSelect.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
                }
            }
        }

        static bool PressKey(byte key, bool IsUpper = false)
        {
            FocusWindow();
            keybd_event(key, 0, 1u, (UIntPtr)0uL);
            return true;
        }

        public static void FocusWindow()
        {
            ShowWindow(ProcessHandle, 1);
            BringWindowToTop(ProcessHandle);
            SetForegroundWindow(ProcessHandle);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                creditimg.Source = MainWindow.credit;
                Cancleimg.Source = MainWindow.creditfail;
                
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

                TotalPay.Foreground = MainWindow.textbrush;
                pagenum.Foreground = MainWindow.textbrush;
                Timer.Foreground = MainWindow.textbrush;

                if (MainWindow.timerlocation == "left")
                {
                    Timer.Margin = new Thickness(0, 130, 1720, 0);
                }
                else
                {
                    if (MainWindow.inifoldername.Contains("mediagram"))
                    {
                        Timer.Margin = new Thickness(1720, 41, 0, 0);
                    }
                }

                fail = 0;
                if (MainWindow.pagenum == "1")
                {
                    TotalPay.Text = "\\ " + Convert.ToInt32(MainWindow.moneyset).ToString("#,##0");
                    pagenum.Text = "2장";
                }
                else
                {
                    if (MainWindow.printerratio == 1)
                    {
                        TotalPay.Text = "\\ " + Convert.ToInt32(PageSelect.totalmoney).ToString("#,##0");
                        if (MainWindow.SorR == "R")
                        {
                            if (MainWindow.pagenum == "1")
                            {
                                pagenum.Text = "2장";
                            }
                            else
                            {
                                pagenum.Text = PageSelect.PaperCount * 2 + "장";
                            }
                        }
                        else
                        {
                            if (MainWindow.inifoldername == "dearpic4" || MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic2")
                            {
                                pagenum.Text = PageSelect.PaperCount + "장";
                            }
                            else if (MainWindow.inifoldername == "r80")
                            {
                                switch (TempSelect.temp)
                                {
                                    case 1:
                                        pagenum.Text = PageSelect.PaperCount + "장";
                                        break;
                                    case 2:
                                    case 3:
                                    case 4:
                                    case 5:
                                    case 6:
                                        pagenum.Text = PageSelect.PaperCount * 2 + "장";
                                        break;
                                }
                            }
                            else if (MainWindow.inifoldername == "hhh")
                            {
                                pagenum.Text = PageSelect.PaperCount + "장";
                            }
                            else
                            {
                                switch (TempSelect.temp)
                                {
                                    case 1:
                                        pagenum.Text = PageSelect.PaperCount * 2 + "장";
                                        break;
                                    case 2:
                                        pagenum.Text = PageSelect.PaperCount + "장";
                                        break;
                                    case 3:
                                        pagenum.Text = PageSelect.PaperCount + "장";
                                        break;
                                }
                            }
                        }
                    }
                }

                if (MainWindow.inifoldername.Contains("mediagram"))
                {
                    pagenum.Height = 96;
                    TotalPay.Height = 96;

                    pagenum.Margin = new Thickness(1299, 712, 360, 272);
                    TotalPay.Margin = new Thickness(1245, 805, 360, 179);

                    pagenum.FontSize = 60;
                    TotalPay.FontSize = 60;
                    pagenum.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    TotalPay.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;

                    BackImg.Width = 370;
                    BackImg.Height = 100;
                    BackImg.Margin = new Thickness(775, 917, 1920 - 370 - 775, 1080 - 100 - 917);

                    switch (TempSelect.temp)
                    {
                        case 1:
                            pagenum.Text = PageSelect.PaperCount * 2 + "매";
                            break;
                        case 2:
                        case 3:
                            pagenum.Text = PageSelect.PaperCount + "매";
                            break;
                    }
                    TotalPay.Text = Convert.ToInt32(PageSelect.totalmoney).ToString("#,##0") + "원";
                }

                Timer.Text = creditduration.ToString();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += new EventHandler(timer_Tick);
                timer.Start();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            creditresultduration--;

            if (creditresultduration == 0)
            {
                creditresulttimer.Interval = TimeSpan.FromSeconds(1);
                creditresulttimer.Tick += new EventHandler(creditresulttimer_Tick);
                creditresulttimer.Start();


                IntPtr Handle;
                string text = "";
                Handle = FindWindow(null, "MainWindow");
                Source.CreditCard credit = new Source.CreditCard();
                if (MainWindow.pagenum == "1")
                {
                    text = credit.Request(Convert.ToInt32(MainWindow.moneyset), Handle, MainWindow.cardnum.ToString());
                }
                else
                {
                    text = credit.Request(Convert.ToInt32(PageSelect.totalmoney), Handle, MainWindow.cardnum.ToString());
                }
                if (text.Contains("OK!"))
                {
                    BackImg.IsEnabled = false;
                    timer.Stop();
                    creditresulttimer.Stop();
                    timer.Tick -= new EventHandler(timer_Tick);
                    creditresulttimer.Tick -= new EventHandler(creditresulttimer_Tick);
                    string[] array = text.Split('|');
                    if (array.Length < 4)
                    {
                        Source.Log.log.Error(text + "Split Error");
                    }
                    else
                    {
                        DateTime nowdate = DateTime.Now;
                        int nowyear = nowdate.Year;
                        int nowmonth = nowdate.Month;
                        int nowday = nowdate.Day;
                        string now = DateTime.Now.ToString("yyyy.MM.dd.HH:mm");

                        if (File.Exists(MainWindow.savePath + @"Data\BankBookData\cardrecord" + nowyear + "." + nowmonth + ".txt"))
                        {
                            string filePath = MainWindow.savePath + @"Data\BankBookData\cardrecord" + nowyear + "." + nowmonth + ".txt";
                            string existingContent = File.ReadAllText(filePath);
                            string newContent = "";
                            if (MainWindow.pagenum != "1")
                            {
                                newContent = now + "," + array[4] + "," + array[1] + "," + Convert.ToInt32(PageSelect.totalmoney) + "," + array[2] + "," + array[3] + "\n" + existingContent;
                            }
                            else
                            {
                                newContent = now + "," + array[4] + "," + array[1] + "," + Convert.ToInt32(MainWindow.moneyset) + "," + array[2] + "," + array[3] + "\n" + existingContent;
                            }
                            File.WriteAllText(filePath, newContent);
                        }
                        else
                        {
                            StreamWriter writer2;
                            writer2 = File.AppendText(MainWindow.savePath + @"Data\BankBookData\cardrecord" + nowyear + "." + nowmonth + ".txt");
                            if (MainWindow.pagenum != "1")
                            {
                                writer2.WriteLine(now + "," + array[4] + "," + array[1] + "," + Convert.ToInt32(PageSelect.totalmoney) + "," + array[2] + "," + array[3]);
                            }
                            else
                            {
                                writer2.WriteLine(now + "," + array[4] + "," + array[1] + "," + Convert.ToInt32(MainWindow.moneyset) + "," + array[2] + "," + array[3]);
                            }
                            writer2.Close();
                        }
                    }
                    Adddata();
                    bool isconnected = MainWindow.CheckInternetConnection();
                    if (isconnected)
                    {
                        SendTotalData();
                    }
                    NavigationService.Navigate(new Uri("View/paymentcomplete.xaml", UriKind.RelativeOrAbsolute));
                }
                else if (text.Contains("Error"))
                {
                    if (MainWindow.inifoldername.Contains("mediagram"))
                    {
                        timer.Stop();
                        creditresulttimer.Stop();
                        if (creditresulttimer.IsEnabled == true)
                        {
                            creditresulttimer.Stop();
                        }
                        timer.Tick -= new EventHandler(timer_Tick);
                        creditresulttimer.Tick -= new EventHandler(creditresulttimer_Tick);
                        Source.Log.log.Info("타이머 멈춤");
                        Cancleimg.Opacity = 1;
                        Cancleimg.IsEnabled = true;
                        BackImg.IsEnabled = false;
                        BackImg.Opacity = 0;

                        endtimer.Interval = TimeSpan.FromSeconds(1);
                        endtimer.Tick += new EventHandler(endtimer_tick);
                        endtimer.Start();
                    }
                    else
                    {
                        timer.Stop();
                        creditresulttimer.Stop();
                        if (creditresulttimer.IsEnabled == true)
                        {
                            creditresulttimer.Stop();
                        }
                        timer.Tick -= new EventHandler(timer_Tick);
                        creditresulttimer.Tick -= new EventHandler(creditresulttimer_Tick);
                        Source.Log.log.Info("타이머 멈춤");
                        Cancleimg.Opacity = 1;
                        Cancleimg.IsEnabled = true;
                        BackImg.IsEnabled = false;
                    }
                    return;
                }
                else if (text.Contains("TimeOut"))
                {
                    if (MainWindow.inifoldername.Contains("mediagram"))
                    {
                        timer.Stop();
                        creditresulttimer.Stop();
                        if (creditresulttimer.IsEnabled == true)
                        {
                            creditresulttimer.Stop();
                        }
                        timer.Tick -= new EventHandler(timer_Tick);
                        creditresulttimer.Tick -= new EventHandler(creditresulttimer_Tick);
                        Source.Log.log.Info("타이머 멈춤");
                        if (File.Exists(MainWindow.uipath + @"\credittimeout.png"))
                        {
                            Cancleimg.Source = MainWindow.credittimeout;
                        }
                        Cancleimg.Opacity = 1;
                        Cancleimg.IsEnabled = true;
                        BackImg.IsEnabled = false;
                        BackImg.Opacity = 0;

                        endtimer.Interval = TimeSpan.FromSeconds(1);
                        endtimer.Tick += new EventHandler(endtimer_tick);
                        endtimer.Start();
                    }
                    else
                    {
                        timer.Stop();
                        creditresulttimer.Stop();
                        if (creditresulttimer.IsEnabled == true)
                        {
                            creditresulttimer.Stop();
                        }
                        timer.Tick -= new EventHandler(timer_Tick);
                        creditresulttimer.Tick -= new EventHandler(creditresulttimer_Tick);
                        Source.Log.log.Info("타이머 멈춤");
                        if (File.Exists(MainWindow.uipath + @"\credittimeout.png"))
                        {
                            Cancleimg.Source = MainWindow.credittimeout;
                        }
                        Cancleimg.Opacity = 1;
                        Cancleimg.IsEnabled = true;
                        BackImg.IsEnabled = false;
                    }
                    return;
                }
                else if (text.Contains("Retry"))
                {
                    if (MainWindow.inifoldername.Contains("mediagram"))
                    {
                        timer.Stop();
                        creditresulttimer.Stop();
                        if (creditresulttimer.IsEnabled == true)
                        {
                            creditresulttimer.Stop();
                        }
                        timer.Tick -= new EventHandler(timer_Tick);
                        creditresulttimer.Tick -= new EventHandler(creditresulttimer_Tick);
                        Source.Log.log.Info("타이머 멈춤");
                        Cancleimg.Opacity = 1;
                        Cancleimg.IsEnabled = true;
                        BackImg.IsEnabled = false;
                        BackImg.Opacity = 0;

                        endtimer.Interval = TimeSpan.FromSeconds(1);
                        endtimer.Tick += new EventHandler(endtimer_tick);
                        endtimer.Start();
                    }
                    else
                    {
                        timer.Stop();
                        creditresulttimer.Stop();
                        if (creditresulttimer.IsEnabled == true)
                        {
                            creditresulttimer.Stop();
                        }
                        timer.Tick -= new EventHandler(timer_Tick);
                        creditresulttimer.Tick -= new EventHandler(creditresulttimer_Tick);
                        Source.Log.log.Info("타이머 멈춤");
                        Cancleimg.Opacity = 1;
                        Cancleimg.IsEnabled = true;
                        BackImg.IsEnabled = false;
                    }
                    return;
                }
                else if (text.Contains("Fail"))
                {
                    if (MainWindow.inifoldername.Contains("mediagram"))
                    {
                        timer.Stop();
                        creditresulttimer.Stop();
                        if (creditresulttimer.IsEnabled == true)
                        {
                            creditresulttimer.Stop();
                        }
                        timer.Tick -= new EventHandler(timer_Tick);
                        creditresulttimer.Tick -= new EventHandler(creditresulttimer_Tick);
                        Source.Log.log.Info("타이머 멈춤");
                        Cancleimg.Opacity = 1;
                        Cancleimg.IsEnabled = true;
                        BackImg.IsEnabled = false;
                        BackImg.Opacity = 0;

                        endtimer.Interval = TimeSpan.FromSeconds(1);
                        endtimer.Tick += new EventHandler(endtimer_tick);
                        endtimer.Start();
                    }
                    else
                    {
                        timer.Stop();
                        creditresulttimer.Stop();
                        if (creditresulttimer.IsEnabled == true)
                        {
                            creditresulttimer.Stop();
                        }
                        timer.Tick -= new EventHandler(timer_Tick);
                        creditresulttimer.Tick -= new EventHandler(creditresulttimer_Tick);
                        Source.Log.log.Info("타이머 멈춤");
                        Cancleimg.Opacity = 1;
                        Cancleimg.IsEnabled = true;
                        BackImg.IsEnabled = false;
                    }
                    return;
                }
                else if (text.Contains("Cancelled"))
                {
                    if (MainWindow.inifoldername.Contains("mediagrma"))
                    {
                        timer.Stop();
                        creditresulttimer.Stop();
                        if (creditresulttimer.IsEnabled == true)
                        {
                            creditresulttimer.Stop();
                        }
                        timer.Tick -= new EventHandler(timer_Tick);
                        creditresulttimer.Tick -= new EventHandler(creditresulttimer_Tick);
                        Source.Log.log.Info("타이머 멈춤");
                        Cancleimg.Opacity = 1;
                        Cancleimg.IsEnabled = true;
                        BackImg.IsEnabled = false;
                        BackImg.Opacity = 0;

                        endtimer.Interval = TimeSpan.FromSeconds(1);
                        endtimer.Tick += new EventHandler(endtimer_tick);
                        endtimer.Start();
                    }
                    else
                    {
                        timer.Stop();
                        creditresulttimer.Stop();
                        if (creditresulttimer.IsEnabled == true)
                        {
                            creditresulttimer.Stop();
                        }
                        timer.Tick -= new EventHandler(timer_Tick);
                        creditresulttimer.Tick -= new EventHandler(creditresulttimer_Tick);
                        Source.Log.log.Info("타이머 멈춤");
                        Cancleimg.Opacity = 1;
                        Cancleimg.IsEnabled = true;
                        BackImg.IsEnabled = false;
                    }
                    return;
                }
                else if (text == "")
                {
                    if (MainWindow.inifoldername.Contains("mediagram"))
                    {
                        timer.Stop();
                        creditresulttimer.Stop();
                        if (creditresulttimer.IsEnabled == true)
                        {
                            creditresulttimer.Stop();
                        }

                        timer.Tick -= new EventHandler(timer_Tick);
                        creditresulttimer.Tick -= new EventHandler(creditresulttimer_Tick);
                        Source.Log.log.Info("타이머 멈춤");
                        Cancleimg.Opacity = 1;
                        Cancleimg.IsEnabled = true;
                        BackImg.IsEnabled = false;
                        BackImg.Opacity = 0;

                        endtimer.Interval = TimeSpan.FromSeconds(1);
                        endtimer.Tick += new EventHandler(endtimer_tick);
                        endtimer.Start();
                    }
                    else
                    {
                        timer.Stop();
                        creditresulttimer.Stop();
                        if (creditresulttimer.IsEnabled == true)
                        {
                            creditresulttimer.Stop();
                        }

                        timer.Tick -= new EventHandler(timer_Tick);
                        creditresulttimer.Tick -= new EventHandler(creditresulttimer_Tick);
                        Source.Log.log.Info("타이머 멈춤");
                        Cancleimg.Opacity = 1;
                        Cancleimg.IsEnabled = true;
                        BackImg.IsEnabled = false;
                    }
                    return;
                }
            }
        }

        private void creditresulttimer_Tick(object sender, EventArgs e)
        {
            creditduration--;

            if (creditduration < 0)
            {
                Timer.Text = "0";
            }
            else
            {
                Timer.Text = creditduration.ToString();
            }

            if (creditduration == 88)
            {
                BackImg.IsEnabled = true;
            }

            if (creditduration == -4)
            {
                BackImg.IsEnabled = false;
            }

            if (creditduration == -5)
            {
                if (MainWindow.inifoldername.Contains("mediagram"))
                {
                    BackImg.IsEnabled = false;
                    PressKey(27);
                    timer.Stop();
                    creditresulttimer.Stop();
                    if (creditresulttimer.IsEnabled == true)
                    {
                        creditresulttimer.Stop();
                    }
                    timer.Tick -= new EventHandler(timer_Tick);
                    creditresulttimer.Tick -= new EventHandler(creditresulttimer_Tick);
                    Source.Log.log.Info("타이머 멈춤");
                    if (File.Exists(MainWindow.uipath + @"\credittimeout.png"))
                    {
                        Cancleimg.Source = MainWindow.credittimeout;
                    }
                    Cancleimg.Opacity = 1;
                    Cancleimg.IsEnabled = true;
                    BackImg.IsEnabled = false;
                    BackImg.Opacity = 0;

                    endtimer.Interval = TimeSpan.FromSeconds(1);
                    endtimer.Tick += new EventHandler(endtimer_tick);
                    endtimer.Start();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                else
                {
                    BackImg.IsEnabled = false;
                    PressKey(27);
                    Source.Log.log.Info("메인페이지 진입");
                    NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }

            if (fail == 1327)
            {
                PressKey(27);
                if (File.Exists(MainWindow.uipath + @"\crediterror.png"))
                {
                    Cancleimg.Source = MainWindow.crediterror;
                }
                Cancleimg.Opacity = 1;
                BackImg.IsEnabled = false;

                if (MainWindow.inifoldername.Contains("mediagram"))
                {
                    BackImg.Opacity = 0;

                    endtimer.Interval = TimeSpan.FromSeconds(1);
                    endtimer.Tick += new EventHandler(endtimer_tick);
                    endtimer.Start();
                }
            }
        }

        private static DateTime Delay(int MS)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 딜레이 시작 딜레이 시간(단위 : ms) : " + MS);

            DateTime thisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime afterMoment = thisMoment.Add(duration);

            while (afterMoment >= thisMoment)
            {
                if (System.Windows.Application.Current != null)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new Action(delegate { }));
                }
                thisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }

        public static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr IParam, ref bool handled)
        {
            if (msg == 1327)
            {
                fail = 1327;
            }

            return IntPtr.Zero;
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
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 1 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + Convert.ToInt32(MainWindow.moneyset) + "&CARD_AMT=" + 0;
                            break;
                        case "4":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 1 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + MainWindow.moneyset;
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
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + PageSelect.PaperCount + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + Convert.ToInt32(PageSelect.totalmoney) + "&CARD_AMT=" + 0;
                            break;
                        case "4":
                            sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + PageSelect.PaperCount + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + PageSelect.totalmoney;
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

        private void Adddata()
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");


                string day = DateTime.Today.ToString("dd");
                int allintmoney = 0;
                int chargedshotnumber = 0;
                Delay(200);
                if (MainWindow.couponshot.ToString() == "0")
                {
                    MainWindow.couponshot.Clear();
                    if (MainWindow.pagenum == "1")
                    {
                        MainWindow.couponshot.Append(MainWindow.moneyset);
                    }
                    else
                    {
                        MainWindow.couponshot.Append(PageSelect.totalmoney);
                    }
                    WritePrivateProfileString("CouponShot", day, Convert.ToString(MainWindow.couponshot), MainWindow.bankbookinipath);
                }
                else
                {
                    if (MainWindow.day == day)
                    {
                        allintmoney = Convert.ToInt32(MainWindow.couponshot.ToString());
                        MainWindow.couponshot.Clear();
                        if (MainWindow.pagenum == "1")
                        {
                            MainWindow.couponshot.Append(Convert.ToString(allintmoney + MainWindow.moneyset));
                        }
                        else
                        {
                            MainWindow.couponshot.Append(Convert.ToString(allintmoney + PageSelect.totalmoney));
                        }
                        WritePrivateProfileString("CouponShot", day, Convert.ToString(MainWindow.couponshot), MainWindow.bankbookinipath);
                    }
                    else
                    {
                        MainWindow.couponshot.Clear();
                        allintmoney = Convert.ToInt32(MainWindow.couponshot.ToString());
                        if (MainWindow.pagenum == "1")
                        {
                            MainWindow.couponshot.Append(Convert.ToString(allintmoney + MainWindow.moneyset));
                        }
                        else
                        {
                            MainWindow.couponshot.Append(Convert.ToString(allintmoney + PageSelect.totalmoney));
                        }
                        WritePrivateProfileString("CouponShot", day, Convert.ToString(MainWindow.couponshot), MainWindow.bankbookinipath);
                    }
                }
                if (MainWindow.couponshot.ToString() == "0")
                {
                    chargedshotnumber = 0;
                    chargedshotnumber++;
                    MainWindow.couponshot.Append(chargedshotnumber);
                    WritePrivateProfileString("ChargedShot", day, Convert.ToString(MainWindow.couponshot), MainWindow.bankbookinipath);
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
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Cancleimg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() | Check 이미지 버튼 클릭");
                fail = 0;

                if (MainWindow.paymentini.ToString() == "cardcash")
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

        private void endtimer_tick(object sender, EventArgs e)
        {
            endcount--;

            if (endcount <= 0)
            {
                endtimer.Stop();
                endtimer.Tick -= new EventHandler(endtimer_tick);

                Cancleimg.IsEnabled = false;
                NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
            }
        }
    }
}
