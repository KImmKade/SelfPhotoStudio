using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;
using K4os.Compression.LZ4.Streams;

namespace wpfTest.View
{
    /// <summary>
    /// PageSelect.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PageSelect : Page
    {
        public static int PaperCount;
        public static int totalmoney;

        public static int checkskip = 0;

        BitmapImage page2on = new BitmapImage();
        BitmapImage page2off = new BitmapImage();
        BitmapImage page4on = new BitmapImage();
        BitmapImage page4off = new BitmapImage();
        BitmapImage page6on = new BitmapImage();
        BitmapImage page6off = new BitmapImage();
        BitmapImage page8on = new BitmapImage();
        BitmapImage page8off = new BitmapImage();
        BitmapImage page10on = new BitmapImage();
        BitmapImage page10off = new BitmapImage();

        BitmapImage Nextbtnimgoff = new BitmapImage();
        BitmapImage nextbtnimgon = new BitmapImage();

        int pagenum;
        int pgduration = Convert.ToInt32(MainWindow.count);
        DispatcherTimer timer = new DispatcherTimer();

        #region INI import

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        #endregion

        public PageSelect()
        {
            Source.Log.log.Info("PageSelect Level 진입");
            InitializeComponent();
        }

        #region///버튼클릭 이벤트///

        private void NextBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 다음 버튼 클릭");
            try
            {
                NextBtn.IsEnabled = false;

                timer.Stop();

                if (MainWindow.SorR == "R")
                {
                    if (MainWindow.Timer.ToString() == "Use")
                    {
                        timer.IsEnabled = false;
                    }
                    if (MainWindow.checkfreeornot == "0")
                    {
                        if (MainWindow.printerratio == 1)
                        {
                            PaperCount /= 2;
                        }
                        totalmoney = Convert.ToInt32(MainWindow.moneyset) * PaperCount;
                    }
                    else
                    {
                        if (MainWindow.printerratio == 1)
                        {
                            PaperCount /= 2;
                        }
                    }
                }
                else
                {
                    if (MainWindow.Timer.ToString() == "Use")
                    {
                        timer.IsEnabled = false;
                    }
                    if (MainWindow.checkfreeornot == "0")
                    {
                        if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4" || MainWindow.inifoldername == "dearpic2")
                        {

                        }
                        else if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4" || MainWindow.inifoldername == "dearpic2")
                        {
                            switch (TempSelect.temp)
                            {
                                case 1:
                                    totalmoney = Convert.ToInt32(MainWindow.moneyset) * PaperCount / 2;
                                    break;
                                case 2:
                                    totalmoney = Convert.ToInt32(MainWindow.temp2fee) * PaperCount / 2;
                                    break;
                                case 3:
                                    totalmoney = Convert.ToInt32(MainWindow.temp3fee) * PaperCount / 2;
                                    break;
                            }
                        }
                        else if (MainWindow.inifoldername == "tech")
                        {
                            switch (TempSelect.temp)
                            {
                                case 1:
                                    PaperCount /= 2;
                                    totalmoney = Convert.ToInt32(MainWindow.moneyset) * PaperCount;
                                    break;
                                case 2:
                                    totalmoney = Convert.ToInt32(MainWindow.temp2fee) * PaperCount / 2;
                                    break;
                                case 3:
                                    totalmoney = Convert.ToInt32(MainWindow.temp3fee) * PaperCount / 2;
                                    break;
                                case 4:
                                    totalmoney = Convert.ToInt32(MainWindow.temp3fee) * PaperCount / 2;
                                    break;
                            }
                        }
                        else if (MainWindow.inifoldername == "r80")
                        {
                            switch (TempSelect.temp)
                            {
                                case 1:
                                    PaperCount /= 2;
                                    totalmoney = Convert.ToInt32(MainWindow.moneyset) * PaperCount;
                                    break;
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                    totalmoney = Convert.ToInt32(MainWindow.moneyset) * PaperCount / 2;
                                    break;
                            }
                        }
                        else if (MainWindow.inifoldername == "r81" || MainWindow.inifoldername == "r83")
                        {
                            totalmoney = Convert.ToInt32(MainWindow.moneyset) * PaperCount / 2;
                        }
                        else if (MainWindow.inifoldername == "hhh")
                        {
                            totalmoney = Convert.ToInt32(MainWindow.moneyset) * PaperCount;
                        }
                        else if (MainWindow.inifoldername == "harim")
                        {
                            switch (TempSelect.temp)
                            {
                                case 1:
                                    PaperCount /= 2;
                                    totalmoney = Convert.ToInt32(MainWindow.moneyset) * PaperCount;
                                    break;
                                case 2:
                                    PaperCount /= 2;
                                    totalmoney = Convert.ToInt32(MainWindow.moneyset) * PaperCount;
                                    break;
                                case 3:
                                    PaperCount /= 2;
                                    totalmoney = Convert.ToInt32(MainWindow.moneyset) * PaperCount;
                                    break;
                            }
                        }
                        else
                        {
                            switch (TempSelect.temp)
                            {
                                case 1:
                                    PaperCount /= 2;
                                    totalmoney = Convert.ToInt32(MainWindow.moneyset) * PaperCount;
                                    break;
                                case 2:
                                    totalmoney = Convert.ToInt32(MainWindow.temp2fee) * PaperCount / 2;
                                    break;
                                case 3:
                                    totalmoney = Convert.ToInt32(MainWindow.temp3fee) * PaperCount / 2;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (MainWindow.inifoldername == "r80")
                        {
                            switch (TempSelect.temp)
                            {
                                case 1:
                                    PaperCount /= 2;
                                    break;
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                    break;
                            }
                        }
                        else if (MainWindow.inifoldername == "r30")
                        {
                            PaperCount /= 2;
                        }
                        else if (MainWindow.inifoldername == "r81" || MainWindow.inifoldername == "r83")
                        {

                        }
                        else if (MainWindow.inifoldername == "r28")
                        {

                        }
                        else
                        {
                            if (TempSelect.temp == 1)
                            {
                                if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4" || MainWindow.inifoldername == "dearpic2")
                                { }
                                else
                                {
                                    PaperCount /= 2;
                                }
                            }
                        }
                    }
                }
                Dispose();
                if (MainWindow.checkfreeornot == "0")
                {
                    switch (MainWindow.paymentini.ToString())
                    {
                        case "cash":
                            NavigationService.Navigate(new Uri("View/Payment.xaml", UriKind.RelativeOrAbsolute));
                            break;
                        case "card":
                            NavigationService.Navigate(new Uri("View/CreditPayment.xaml", UriKind.RelativeOrAbsolute));
                            break;
                        case "cardcash":
                        case "cashcoupon":
                        case "cardcoupon":
                            NavigationService.Navigate(new Uri("View/ChoicePayment.xaml", UriKind.RelativeOrAbsolute));
                            break;
                        case "coupon":
                            NavigationService.Navigate(new Uri("View/CouponPage.xaml", UriKind.RelativeOrAbsolute));
                            break;
                    }
                }
                else
                {
                    if (MainWindow.camnumber.ToString() == "1")
                    {
                        if (MainWindow.inifoldername == "dearpic1" || MainWindow.inifoldername == "dearpic4" || MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic2")
                        {
                            NavigationService.Navigate(new Uri("View/PriewView.xaml", UriKind.RelativeOrAbsolute));
                        }
                        else
                        {
                            NavigationService.Navigate(new Uri("View/TakePic.xaml", UriKind.RelativeOrAbsolute));
                        }
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
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        #endregion

        #region///타이머///

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                pgduration--;
                Timer.Text = pgduration.ToString();

                if (checkskip == 1)
                {
                    timer.Stop();
                    Source.Log.log.Info("타이머 종료");

                    if (MainWindow.Timer.ToString() == "Use")
                    {
                        timer.IsEnabled = false;
                    }
                    if (MainWindow.checkfreeornot == "0")
                    {
                        if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4" || MainWindow.inifoldername == "dearpic2")
                        {

                        }
                        else if (MainWindow.inifoldername == "hhh")
                        {

                        }
                        else if (MainWindow.inifoldername == "harim")
                        {
                            PaperCount /= 2;
                        }
                        else
                        {
                            if (TempSelect.temp == 1)
                            {
                                PaperCount /= 2;
                            }
                        }
                        if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4" || MainWindow.inifoldername == "dearpic2")
                        {
                            switch (TempSelect.temp)
                            {
                                case 1:
                                    totalmoney = Convert.ToInt32(MainWindow.moneyset) * PaperCount / 2;
                                    break;
                                case 2:
                                    totalmoney = Convert.ToInt32(MainWindow.temp2fee) * PaperCount / 2;
                                    break;
                                case 3:
                                    totalmoney = Convert.ToInt32(MainWindow.temp3fee) * PaperCount / 2;
                                    break;
                            }
                        }
                        else if (MainWindow.inifoldername == "hhh")
                        {
                            totalmoney = Convert.ToInt32(MainWindow.moneyset) * PaperCount;
                        }
                        else if (MainWindow.inifoldername == "harim")
                        {
                            totalmoney = Convert.ToInt32(MainWindow.moneyset) * PaperCount;
                        }
                        else
                        {
                            switch (TempSelect.temp)
                            {
                                case 1:
                                    totalmoney = Convert.ToInt32(MainWindow.moneyset) * PaperCount;
                                    break;
                                case 2:
                                    totalmoney = Convert.ToInt32(MainWindow.temp2fee) * PaperCount / 2;
                                    break;
                                case 3:
                                    totalmoney = Convert.ToInt32(MainWindow.temp3fee) * PaperCount / 2;
                                    break;
                            }
                        }

                        if (MainWindow.inifoldername.Contains("mediagram"))
                        {
                            if (PaperCount == 0)
                            {
                                switch(TempSelect.temp)
                                {
                                    case 1:
                                        PaperCount = 1;
                                        break;
                                    case 2:
                                        PaperCount = 2;
                                        break;
                                    case 3:
                                        PaperCount = 2;
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (TempSelect.temp == 1)
                        {
                            if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4" || MainWindow.inifoldername == "dearpic2")
                            { }
                            else
                            {
                                PaperCount /= 2;
                            }
                        }
                    }
                    Dispose();
                    checkskip = 0;
                    if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4" || MainWindow.inifoldername == "dearpic2")
                    {
                        NavigationService.Navigate(new Uri("View/PriewView.xaml", UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        if (MainWindow.camnumber.ToString() == "1")
                        {
                            if (MainWindow.inifoldername.Contains("mediagram"))
                            {
                                NavigationService.Navigate(new Uri("View/TakePic.xaml", UriKind.RelativeOrAbsolute));
                            }
                            NavigationService.Navigate(new Uri("View/paymentcomplete.xaml", UriKind.RelativeOrAbsolute));
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

                if (pgduration == -1)
                {
                    timer.Stop();
                    Source.Log.log.Info("타이머 종료");
                    Dispose();
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

        #endregion

        private void Dispose()
        {
            timer.Tick -= new EventHandler(Timer_Tick);
            timer = null;
            BackgroundImg.Source = null;
            NextBtn.Source = null;
            Timer.Text = null;
            BackgroundImg = null;
            NextBtn = null;
            Timer = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void BackBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 뒷 페이지 넘어가기 이미지 클릭");
            try
            {
                BackBtn.IsEnabled = false;
                timer.Stop();
                Source.Log.log.Info("타이머 멈춤");
                Dispose();
                if (MainWindow.tempoption == 1)
                {
                    if (MainWindow.inifoldername == "dearpic2" || MainWindow.inifoldername == "harim")
                    {
                        NavigationService.Navigate(new Uri("View/AISelect.xaml", UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        NavigationService.Navigate(new Uri("View/TempSelect.xaml", UriKind.RelativeOrAbsolute));
                    }
                }
                else
                {
                    NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
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

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | PageSelect 세팅 로딩");
            try
            {
                BackgroundImg.Source = MainWindow.pageselect;
                NextBtn.Source = MainWindow.nextbtn;
                BackBtn.Source = MainWindow.Backimg;
                nextpagenum.Source = MainWindow.nextcolor;
                backpagenum.Source = MainWindow.backcolor;
                NextBtn.Source = MainWindow.nextbtn;
                NextBtn.Width = MainWindow.nextbtn.Width;
                NextBtn.Height = MainWindow.nextbtn.Height;
                BackBtn.Source = MainWindow.Backimg;
                BackBtn.Width = MainWindow.Backimg.Width;
                BackBtn.Height = MainWindow.Backimg.Height;

                Timer.Foreground = MainWindow.textbrush;
                Pagenumber.Foreground = MainWindow.textbrush;
                money.Foreground = MainWindow.textbrush;

                checkskip = 0;

                if (MainWindow.SorR == "R")
                {
                    if (MainWindow.printerratio == 1)
                    {
                        pagenum = 2;
                        PaperCount = 2;
                    }
                    else
                    {
                        pagenum = 1;
                        PaperCount = 1;
                    }
                }
                else
                {
                    if (MainWindow.inifoldername == "hhh")
                    {
                        Timer.Margin = new Thickness(1613, 130, 107, 850);

                        Pagenumber.Foreground = Brushes.Black;

                        pagenum = 1;
                        PaperCount = 1;
                    }
                    else if (MainWindow.inifoldername == "r28")
                    {
                        pagenum = 2;
                        PaperCount = 2;
                    }
                    else
                    {
                        pagenum = 2;
                        PaperCount = 2;
                    }

                    if (MainWindow.inifoldername.Contains("mediagram"))
                    {
                        PaperCount = 0;
                        backpagenum.Opacity = 0;
                        nextpagenum.Opacity = 0;
                        backpagenum.IsEnabled = false;
                        nextpagenum.IsEnabled = false;
                        backpagenum.Margin = new Thickness(0, 0, 1920, 1080);
                        nextpagenum.Margin = new Thickness(0, 0, 1920, 1080);

                        page2on = new BitmapImage();
                        page2on.BeginInit();
                        page2on.UriSource = new Uri(MainWindow.uipath + @"\page2on.png", UriKind.RelativeOrAbsolute);
                        page2on.CacheOption = BitmapCacheOption.OnLoad;
                        page2on.EndInit();

                        page2off = new BitmapImage();
                        page2off.BeginInit();
                        page2off.UriSource = new Uri(MainWindow.uipath + @"\page2off.png", UriKind.RelativeOrAbsolute);
                        page2off.CacheOption = BitmapCacheOption.OnLoad;
                        page2off.EndInit();

                        page4on = new BitmapImage();
                        page4on.BeginInit();
                        page4on.UriSource = new Uri(MainWindow.uipath + @"\page4on.png", UriKind.RelativeOrAbsolute);
                        page4on.CacheOption = BitmapCacheOption.OnLoad;
                        page4on.EndInit();

                        page4off = new BitmapImage();
                        page4off.BeginInit();
                        page4off.UriSource = new Uri(MainWindow.uipath + @"\page4off.png", UriKind.RelativeOrAbsolute);
                        page4off.CacheOption = BitmapCacheOption.OnLoad;
                        page4off.EndInit();

                        page6on = new BitmapImage();
                        page6on.BeginInit();
                        page6on.UriSource = new Uri(MainWindow.uipath + @"\page6on.png", UriKind.RelativeOrAbsolute);
                        page6on.CacheOption = BitmapCacheOption.OnLoad;
                        page6on.EndInit();

                        page6off = new BitmapImage();
                        page6off.BeginInit();
                        page6off.UriSource = new Uri(MainWindow.uipath + @"\page6off.png", UriKind.RelativeOrAbsolute);
                        page6off.CacheOption = BitmapCacheOption.OnLoad;
                        page6off.EndInit();

                        page8on = new BitmapImage();
                        page8on.BeginInit();
                        page8on.UriSource = new Uri(MainWindow.uipath + @"\page8on.png", UriKind.RelativeOrAbsolute);
                        page8on.CacheOption = BitmapCacheOption.OnLoad;
                        page8on.EndInit();

                        page8off = new BitmapImage();
                        page8off.BeginInit();
                        page8off.UriSource = new Uri(MainWindow.uipath + @"\page8off.png", UriKind.RelativeOrAbsolute);
                        page8off.CacheOption = BitmapCacheOption.OnLoad;
                        page8off.EndInit();

                        page10on = new BitmapImage();
                        page10on.BeginInit();
                        page10on.UriSource = new Uri(MainWindow.uipath + @"\page10on.png", UriKind.RelativeOrAbsolute);
                        page10on.CacheOption = BitmapCacheOption.OnLoad;
                        page10on.EndInit();

                        page10off = new BitmapImage();
                        page10off.BeginInit();
                        page10off.UriSource = new Uri(MainWindow.uipath + @"\page10off.png", UriKind.RelativeOrAbsolute);
                        page10off.CacheOption = BitmapCacheOption.OnLoad;
                        page10off.EndInit();

                        page2.Margin = new Thickness(465, 329, 1920 - 180 - 465, 1080 - 180 - 329);
                        page4.Margin = new Thickness(680, 329, 1920 - 180 - 680, 1080 - 180 - 329);
                        page6.Margin = new Thickness(896, 329, 1920 - 180 - 896, 1080 - 180 - 329);
                        page8.Margin = new Thickness(1112, 329, 1920 - 180 - 1112, 1080 - 180 - 329);
                        page10.Margin = new Thickness(1328, 329, 1920 - 180 - 1328, 1080 - 180 - 329);

                        page2.Source = page2off;
                        page4.Source = page4off;
                        page6.Source = page6off;
                        page8.Source = page8off;
                        page10.Source = page10off;

                        BackBtn.Width = 370;
                        BackBtn.Height = 100;
                        NextBtn.Width = 370;
                        NextBtn.Height = 100;
                        BackBtn.Margin = new Thickness(569, 927, 1920 - 370 - 569, 1080 - 927 - 100);
                        NextBtn.Margin = new Thickness(979, 927, 1920 - 370 - 979, 1080 - 927 - 100);

                        nextbtnimgon.BeginInit();
                        nextbtnimgon.UriSource = new Uri(MainWindow.uipath + @"\Payment_on.png", UriKind.RelativeOrAbsolute);
                        nextbtnimgon.CacheOption = BitmapCacheOption.OnLoad;
                        nextbtnimgon.EndInit();

                        Nextbtnimgoff.BeginInit();
                        Nextbtnimgoff.UriSource = new Uri(MainWindow.uipath + @"\Payment_off.png", UriKind.RelativeOrAbsolute);
                        Nextbtnimgoff.CacheOption = BitmapCacheOption.OnLoad;
                        Nextbtnimgoff.EndInit();

                        NextBtn.IsEnabled = false;

                        NextBtn.Source = Nextbtnimgoff;

                        Pagenumber.Height = 96;
                        Pagenumber.Margin = new Thickness(1244, 640, 361, 1080 - 96 - 640);
                        Pagenumber.Text = "0매";

                        money.Height = 96;
                        money.Margin = new Thickness(1920 - 361 - 315, 773, 361, 1080 - 96 - 773);

                        money.Text = "0 원";

                        Pagenumber.HorizontalAlignment = HorizontalAlignment.Right;
                        money.HorizontalAlignment = HorizontalAlignment.Right;

                    }
                }
                backpagenum.IsEnabled = false;
                if (!MainWindow.inifoldername.Contains("mediagram"))
                {
                    pagetextshow();
                }

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

                if (MainWindow.Timer.ToString() == "Use")
                {
                    Timer.Text = pgduration.ToString();
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += new EventHandler(Timer_Tick);
                    timer.Start();
                    Source.Log.log.Info("타이머 시작");
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void backpagenum_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 페이지 줄이기 이미지 클릭");
            try
            {
                if (MainWindow.SorR == "R")
                {
                    if (MainWindow.printerratio == 1)
                    {
                        if (PaperCount == Convert.ToInt32(MainWindow.pagenum) * 2)
                        {
                            nextpagenum.IsEnabled = true;
                        }
                        if (PaperCount == 2)
                        {
                            return;
                        }
                        else
                        {
                            PaperCount -= 2;
                            if (PaperCount == 2)
                            {
                                backpagenum.IsEnabled = false;
                            }
                        }
                    }
                    else
                    {
                        if (PaperCount == Convert.ToInt32(MainWindow.pagenum))
                        {
                            nextpagenum.IsEnabled = true;
                        }
                        if (PaperCount == 1)
                        {
                            return;
                        }
                        else
                        {
                            PaperCount -= 1;
                            if (PaperCount == 1)
                            {
                                backpagenum.IsEnabled = false;
                            }
                        }
                    }
                }
                else
                {
                    if (MainWindow.inifoldername == "hhh")
                    {
                        if (PaperCount == Convert.ToInt32(MainWindow.pagenum) * 1)
                        {
                            nextpagenum.IsEnabled = true;
                        }
                        if (PaperCount == 1)
                        {
                            return;
                        }
                        else
                        {
                            PaperCount -= 1;
                            if (PaperCount == 1)
                            {
                                backpagenum.IsEnabled = false;
                            }
                        }
                    }
                    else
                    {
                        if (PaperCount <= Convert.ToInt32(MainWindow.pagenum) * 2)
                        {
                            nextpagenum.IsEnabled = true;
                        }
                        if (PaperCount == 2 || PaperCount == 1)
                        {
                            return;
                        }
                        else
                        {
                            PaperCount -= 2;
                            if (PaperCount == 2 || PaperCount == 1)
                            {
                                backpagenum.IsEnabled = false;
                            }
                        }
                    }
                }

                pagetextshow();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void nextpagenum_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 페이지 증가 이미지 클릭");
            try
            {
                if (MainWindow.SorR == "R")
                {
                    if (MainWindow.printerratio == 1)
                    {
                        if (PaperCount == 2)
                        {
                            backpagenum.IsEnabled = true;
                        }
                        if (PaperCount == Convert.ToInt32(MainWindow.pagenum) * 2)
                        {
                            return;
                        }
                        else
                        {
                            PaperCount += 2;
                            if (PaperCount == Convert.ToInt32(MainWindow.pagenum) * 2)
                            {
                                nextpagenum.IsEnabled = false;
                            }
                        }
                    }
                    else
                    {
                        if (PaperCount == 1)
                        {
                            backpagenum.IsEnabled = true;
                        }
                        if (PaperCount == Convert.ToInt32(MainWindow.pagenum))
                        {
                            return;
                        }
                        else
                        {
                            PaperCount += 1;
                            if (PaperCount == Convert.ToInt32(MainWindow.pagenum))
                            {
                                nextpagenum.IsEnabled = false;
                            }
                        }
                    }
                }
                else
                {
                    if (MainWindow.inifoldername == "hhh")
                    {
                        if (PaperCount == 1)
                        {
                            backpagenum.IsEnabled = true;
                        }
                        if (PaperCount == Convert.ToInt32(MainWindow.pagenum) * 1)
                        {
                            return;
                        }
                        else
                        {
                            PaperCount += 1;
                            if (PaperCount == Convert.ToInt32(MainWindow.pagenum) * 1)
                            {
                                nextpagenum.IsEnabled = false;
                            }
                        }
                    }
                    else
                    {
                        if (PaperCount == 2 || PaperCount == 1)
                        {
                            backpagenum.IsEnabled = true;
                        }
                        if (PaperCount >= Convert.ToInt32(MainWindow.pagenum) * 2)
                        {
                            return;
                        }
                        else
                        {
                            PaperCount += 2;
                            if (PaperCount >= Convert.ToInt32(MainWindow.pagenum) * 2)
                            {
                                nextpagenum.IsEnabled = false;
                            }
                        }
                    }
                }
                pagetextshow();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pagetextshow()
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 페이지 장수 확인");
            try
            {
                if (MainWindow.SorR == "R")
                {
                    if (MainWindow.checkfreeornot == "0")
                    {
                        money.Text = (Convert.ToInt32(MainWindow.moneyset) * PaperCount / 2).ToString("#,##0") + "원";
                    }
                }
                else
                {
                    if (MainWindow.inifoldername == "tech")
                    {
                        if (MainWindow.checkfreeornot == "0")
                        {
                            switch (TempSelect.temp)
                            {
                                case 1:
                                    if (MainWindow.moneyset != "0")
                                    {
                                        money.Text = (Convert.ToInt32(MainWindow.moneyset) * PaperCount / 2).ToString("#,##0") + "원";
                                    }
                                    else
                                    {
                                        money.Opacity = 0;
                                    }
                                    break;
                                case 2:
                                    if (MainWindow.temp2fee != "0")
                                    {
                                        money.Text = (Convert.ToInt32(MainWindow.temp2fee) * PaperCount / 2).ToString("#,##0") + "원";
                                    }
                                    else
                                    {
                                        money.Opacity = 0;
                                    }
                                    break;
                                case 3:
                                    if (MainWindow.temp3fee != "0")
                                    {
                                        money.Text = (Convert.ToInt32(MainWindow.temp3fee) * PaperCount / 2).ToString("#,##0") + "원";
                                    }
                                    else
                                    {
                                        money.Opacity = 0;
                                    }
                                    break;
                                case 4:
                                    if (MainWindow.temp3fee != "0")
                                    {
                                        money.Text = (Convert.ToInt32(MainWindow.temp3fee) * PaperCount / 2).ToString("#,##0") + "원";
                                    }
                                    else
                                    {
                                        money.Opacity = 0;
                                    }
                                    break;
                            }
                        }
                    }
                    else if (MainWindow.inifoldername == "r80")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (MainWindow.moneyset != "0")
                                {
                                    money.Text = (Convert.ToInt32(MainWindow.moneyset) * PaperCount / 2).ToString("#,##0") + "원";
                                }
                                else
                                {
                                    money.Opacity = 0;
                                }
                                break;
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                                if (MainWindow.moneyset != "0")
                                {
                                    money.Text = (Convert.ToInt32(MainWindow.moneyset) * PaperCount / 2).ToString("#,##0") + "원";
                                }
                                else
                                {
                                    money.Opacity = 0;
                                }
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "r81" || MainWindow.inifoldername == "83")
                    {
                        if (MainWindow.moneyset != "0")
                        {
                            money.Text = (Convert.ToInt32(MainWindow.moneyset) * PaperCount / 2).ToString("#,##0") + "원";
                        }
                        else
                        {
                            money.Opacity = 0;
                        }
                    }
                    else if (MainWindow.inifoldername == "hhh")
                    {
                        money.Text = (Convert.ToInt32(MainWindow.moneyset) * PaperCount).ToString("#,##0") + "원";
                    }
                    else if (MainWindow.inifoldername == "harim")
                    {
                        if (MainWindow.checkfreeornot == "0")
                        {
                            switch (TempSelect.temp)
                            {
                                case 1:
                                    if (MainWindow.moneyset != "0")
                                    {
                                        money.Text = (Convert.ToInt32(MainWindow.moneyset) * PaperCount / 2).ToString("#,##0") + "원";
                                    }
                                    else
                                    {
                                        money.Opacity = 0;
                                    }
                                    break;
                                case 2:
                                    if (MainWindow.temp2fee != "0")
                                    {
                                        money.Text = (Convert.ToInt32(MainWindow.temp2fee) * PaperCount / 2).ToString("#,##0") + "원";
                                    }
                                    else
                                    {
                                        money.Opacity = 0;
                                    }
                                    break;
                                case 3:
                                    if (MainWindow.temp3fee != "0")
                                    {
                                        money.Text = (Convert.ToInt32(MainWindow.temp3fee) * PaperCount / 2).ToString("#,##0") + "원";
                                    }
                                    else
                                    {
                                        money.Opacity = 0;
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (MainWindow.checkfreeornot == "0")
                        {
                            switch (TempSelect.temp)
                            {
                                case 1:
                                    if (MainWindow.moneyset != "0")
                                    {
                                        money.Text = (Convert.ToInt32(MainWindow.moneyset) * PaperCount / 2).ToString("#,##0") + "원";
                                    }
                                    else
                                    {
                                        money.Opacity = 0;
                                    }
                                    break;
                                case 2:
                                    if (MainWindow.temp2fee != "0")
                                    {
                                        money.Text = (Convert.ToInt32(MainWindow.temp2fee) * PaperCount / 2).ToString("#,##0") + "원";
                                    }
                                    else
                                    {
                                        money.Opacity = 0;
                                    }
                                    break;
                                case 3:
                                    if (MainWindow.temp3fee != "0")
                                    {
                                        money.Text = (Convert.ToInt32(MainWindow.temp3fee) * PaperCount / 2).ToString("#,##0") + "원";
                                    }
                                    else
                                    {
                                        money.Opacity = 0;
                                    }
                                    break;
                            }
                        }
                    }

                    if (MainWindow.inifoldername.Contains("mediagram"))
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (MainWindow.moneyset != "0")
                                {
                                    money.Text = (Convert.ToInt32(MainWindow.moneyset) * PaperCount / 2).ToString("#,##0") + "원";
                                }
                                else
                                {
                                    money.Opacity = 0;
                                }
                                break;
                            case 2:
                                if (MainWindow.temp2fee != "0")
                                {
                                    money.Text = (Convert.ToInt32(MainWindow.temp2fee) * PaperCount / 2).ToString("#,##0") + "원";
                                }
                                else
                                {
                                    money.Opacity = 0;
                                }
                                break;
                            case 3:
                                if (MainWindow.temp3fee != "0")
                                {
                                    money.Text = (Convert.ToInt32(MainWindow.temp3fee) * PaperCount / 2).ToString("#,##0") + "원";
                                }
                                else
                                {
                                    money.Opacity = 0;
                                }
                                break;
                        }
                    }
                }
                if (MainWindow.inifoldername == "harim")
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            Pagenumber.Text = Convert.ToString(PaperCount) + "장";
                            break;
                        case 2:
                        case 3:
                            Pagenumber.Text = Convert.ToString(PaperCount / 2) + "장";
                            break;
                    }
                }
                else
                {
                    if (MainWindow.inifoldername.Contains("mediagram"))
                    {
                        Pagenumber.Text = Convert.ToString(PaperCount) + "매";
                    }
                    else
                    {
                        Pagenumber.Text = Convert.ToString(PaperCount) + "장";
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void page2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info("2장 선택");

                PaperCount = 2;

                page2.Source = page2on;
                page4.Source = page4off;
                page6.Source = page6off;
                page8.Source = page8off;
                page10.Source = page10off;

                NextBtn.Source = nextbtnimgon;
                NextBtn.IsEnabled = true;

                pagetextshow();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void page4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info("4장 선택");

                PaperCount = 4;

                page2.Source = page2off;
                page4.Source = page4on;
                page6.Source = page6off;
                page8.Source = page8off;
                page10.Source = page10off;

                NextBtn.Source = nextbtnimgon;
                NextBtn.IsEnabled = true;

                pagetextshow();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void page6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info("6장 선택");

                PaperCount = 6;

                page2.Source = page2off;
                page4.Source = page4off;
                page6.Source = page6on;
                page8.Source = page8off;
                page10.Source = page10off;

                NextBtn.Source = nextbtnimgon;
                NextBtn.IsEnabled = true;

                pagetextshow();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void page8_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info("8장 선택");

                PaperCount = 8;

                page2.Source = page2off;
                page4.Source = page4off;
                page6.Source = page6off;
                page8.Source = page8on;
                page10.Source = page10off;

                NextBtn.Source = nextbtnimgon;
                NextBtn.IsEnabled = true;

                pagetextshow();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void page10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info("10장 선택");

                PaperCount = 10;

                page2.Source = page2off;
                page4.Source = page4off;
                page6.Source = page6off;
                page8.Source = page8off;
                page10.Source = page10on;

                NextBtn.Source = nextbtnimgon;
                NextBtn.IsEnabled = true;

                pagetextshow();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }
    }
}
