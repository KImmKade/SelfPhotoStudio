using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MNWiseKakaoTalk;
using MNWiseKakaoTalk.Models;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing.Printing;

namespace wpfTest.View
{
    /// <summary>
    /// Kakao.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class coupon : Page
    {
        #region///변수///

        string[] phonenumber1 = new string[3];
        string[] phonenumber2 = new string[4];
        string[] phonenumber3 = new string[4];
        int kduration = 60;
        DispatcherTimer timer = new DispatcherTimer();

        bool uploadqr = false;
        BitmapImage QRCodeimg = new BitmapImage();
        int papercount;
        int pagecount;
        System.Drawing.Image qrcodeimg;

        public string correctedPrintername { get; private set; }

        #endregion

        #region///ini import///

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #endregion

        public coupon()
        {
            Source.Log.log.Info("KaKaoPage진입");
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                MainWindow.phonenumber = "";
                Backgroundimg.Source = MainWindow.couponpage;

                Timer.Foreground = MainWindow.textbrush;

                if (MainWindow.timerlocation == "left")
                {
                    Timer.Margin = new Thickness(0, 130, 1720, 0);
                }

                if (MainWindow.checkkakao == "0" && MainWindow.qroption == 0)
                {
                    Kakaobtn.Source = MainWindow.kakaobtn;
                    NoKakaobtn.Source = MainWindow.nokakaobtn;
                    Key_1.Source = MainWindow.number1;
                    Key_2.Source = MainWindow.number2;
                    Key_3.Source = MainWindow.number3;
                    Key_4.Source = MainWindow.number4;
                    Key_5.Source = MainWindow.number5;
                    Key_6.Source = MainWindow.number6;
                    Key_7.Source = MainWindow.number7;
                    Key_8.Source = MainWindow.number8;
                    Key_9.Source = MainWindow.number9;
                    Key_0.Source = MainWindow.number0;
                    Key_Del.Source = MainWindow.delbtn;

                    Phonenumbertext.Foreground = MainWindow.fontbrush;

                    if (MainWindow.Kakaothread == null)
                    { }
                    else
                    {
                        try
                        {
                            if (MainWindow.Kakaothread.IsAlive)
                            {
                                Source.Log.log.Info("kakaothread살아있음");
                                MainWindow.Kakaothread = null;
                            }
                        }
                        catch (ThreadAbortException threadex)
                        {
                            Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - " + threadex.Message);
                            Source.Log.log.Info("SendKakao thread 종료");
                        }
                    }
                }
                if ((MainWindow.checkkakao.ToString() == "1" && MainWindow.qroption == 1) || MainWindow.checkvideo == "1")
                {
                    Backgroundimg.Source = MainWindow.QRpage;

                    Kakaobtn.IsEnabled = false;
                    NoKakaobtn.IsEnabled = false;
                    Key_1.IsEnabled = false;
                    Key_2.IsEnabled = false;
                    Key_3.IsEnabled = false;
                    Key_4.IsEnabled = false;
                    Key_5.IsEnabled = false;
                    Key_6.IsEnabled = false;
                    Key_7.IsEnabled = false;
                    Key_8.IsEnabled = false;
                    Key_9.IsEnabled = false;
                    Key_0.IsEnabled = false;
                    Key_Del.IsEnabled = false;

                    Kakaobtn.Opacity = 0;
                    NoKakaobtn.Opacity = 0;
                    Key_1.Opacity = 0;
                    Key_2.Opacity = 0;
                    Key_3.Opacity = 0;
                    Key_4.Opacity = 0;
                    Key_5.Opacity = 0;
                    Key_6.Opacity = 0;
                    Key_7.Opacity = 0;
                    Key_8.Opacity = 0;
                    Key_9.Opacity = 0;
                    Key_0.Opacity = 0;
                    Key_Del.Opacity = 0;

                    QRCodegif.Opacity = 1;
                    QRCode.Opacity = 1;
                }
                Timer.Text = kduration.ToString();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += new EventHandler(Timer_Tick);
                timer.Start();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        #region 버튼 클릭 이벤트

        private void Key_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (phonenumber1[0] == null)
            {
                phonenumber1[0] = Convert.ToString(1);
            }
            else if (phonenumber1[0] != null && phonenumber1[1] == null)
            {
                phonenumber1[1] = Convert.ToString(1);
            }
            else if (phonenumber1[1] != null && phonenumber1[2] == null)
            {
                phonenumber1[2] = Convert.ToString(1);
            }
            else if (phonenumber1[2] != null && phonenumber2[0] == null)
            {
                phonenumber2[0] = Convert.ToString(1);
            }
            else if (phonenumber2[0] != null && phonenumber2[1] == null)
            {
                phonenumber2[1] = Convert.ToString(1);
            }
            else if (phonenumber2[1] != null && phonenumber2[2] == null)
            {
                phonenumber2[2] = Convert.ToString(1);
            }
            else if (phonenumber2[2] != null && phonenumber2[3] == null)
            {
                phonenumber2[3] = Convert.ToString(1);
            }
            else if (phonenumber2[3] != null && phonenumber3[0] == null)
            {
                phonenumber3[0] = Convert.ToString(1);
            }
            else if (phonenumber3[0] != null && phonenumber3[1] == null)
            {
                phonenumber3[1] = Convert.ToString(1);
            }
            else if (phonenumber3[1] != null && phonenumber3[2] == null)
            {
                phonenumber3[2] = Convert.ToString(1);
            }
            else if (phonenumber3[2] != null && phonenumber3[3] == null)
            {
                phonenumber3[3] = Convert.ToString(1);
            }
            InsertHyphone();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void Key_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (phonenumber1[0] == null)
            {
                phonenumber1[0] = Convert.ToString(2);
            }
            else if (phonenumber1[0] != null && phonenumber1[1] == null)
            {
                phonenumber1[1] = Convert.ToString(2);
            }
            else if (phonenumber1[1] != null && phonenumber1[2] == null)
            {
                phonenumber1[2] = Convert.ToString(2);
            }
            else if (phonenumber1[2] != null && phonenumber2[0] == null)
            {
                phonenumber2[0] = Convert.ToString(2);
            }
            else if (phonenumber2[0] != null && phonenumber2[1] == null)
            {
                phonenumber2[1] = Convert.ToString(2);
            }
            else if (phonenumber2[1] != null && phonenumber2[2] == null)
            {
                phonenumber2[2] = Convert.ToString(2);
            }
            else if (phonenumber2[2] != null && phonenumber2[3] == null)
            {
                phonenumber2[3] = Convert.ToString(2);
            }
            else if (phonenumber2[3] != null && phonenumber3[0] == null)
            {
                phonenumber3[0] = Convert.ToString(2);
            }
            else if (phonenumber3[0] != null && phonenumber3[1] == null)
            {
                phonenumber3[1] = Convert.ToString(2);
            }
            else if (phonenumber3[1] != null && phonenumber3[2] == null)
            {
                phonenumber3[2] = Convert.ToString(2);
            }
            else if (phonenumber3[2] != null && phonenumber3[3] == null)
            {
                phonenumber3[3] = Convert.ToString(2);
            }
            InsertHyphone();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void Key_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (phonenumber1[0] == null)
            {
                phonenumber1[0] = Convert.ToString(3);
            }
            else if (phonenumber1[0] != null && phonenumber1[1] == null)
            {
                phonenumber1[1] = Convert.ToString(3);
            }
            else if (phonenumber1[1] != null && phonenumber1[2] == null)
            {
                phonenumber1[2] = Convert.ToString(3);
            }
            else if (phonenumber1[2] != null && phonenumber2[0] == null)
            {
                phonenumber2[0] = Convert.ToString(3);
            }
            else if (phonenumber2[0] != null && phonenumber2[1] == null)
            {
                phonenumber2[1] = Convert.ToString(3);
            }
            else if (phonenumber2[1] != null && phonenumber2[2] == null)
            {
                phonenumber2[2] = Convert.ToString(3);
            }
            else if (phonenumber2[2] != null && phonenumber2[3] == null)
            {
                phonenumber2[3] = Convert.ToString(3);
            }
            else if (phonenumber2[3] != null && phonenumber3[0] == null)
            {
                phonenumber3[0] = Convert.ToString(3);
            }
            else if (phonenumber3[0] != null && phonenumber3[1] == null)
            {
                phonenumber3[1] = Convert.ToString(3);
            }
            else if (phonenumber3[1] != null && phonenumber3[2] == null)
            {
                phonenumber3[2] = Convert.ToString(3);
            }
            else if (phonenumber3[2] != null && phonenumber3[3] == null)
            {
                phonenumber3[3] = Convert.ToString(3);
            }
            InsertHyphone();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void Key_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (phonenumber1[0] == null)
            {
                phonenumber1[0] = Convert.ToString(4);
            }
            else if (phonenumber1[0] != null && phonenumber1[1] == null)
            {
                phonenumber1[1] = Convert.ToString(4);
            }
            else if (phonenumber1[1] != null && phonenumber1[2] == null)
            {
                phonenumber1[2] = Convert.ToString(4);
            }
            else if (phonenumber1[2] != null && phonenumber2[0] == null)
            {
                phonenumber2[0] = Convert.ToString(4);
            }
            else if (phonenumber2[0] != null && phonenumber2[1] == null)
            {
                phonenumber2[1] = Convert.ToString(4);
            }
            else if (phonenumber2[1] != null && phonenumber2[2] == null)
            {
                phonenumber2[2] = Convert.ToString(4);
            }
            else if (phonenumber2[2] != null && phonenumber2[3] == null)
            {
                phonenumber2[3] = Convert.ToString(4);
            }
            else if (phonenumber2[3] != null && phonenumber3[0] == null)
            {
                phonenumber3[0] = Convert.ToString(4);
            }
            else if (phonenumber3[0] != null && phonenumber3[1] == null)
            {
                phonenumber3[1] = Convert.ToString(4);
            }
            else if (phonenumber3[1] != null && phonenumber3[2] == null)
            {
                phonenumber3[2] = Convert.ToString(4);
            }
            else if (phonenumber3[2] != null && phonenumber3[3] == null)
            {
                phonenumber3[3] = Convert.ToString(4);
            }
            InsertHyphone();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void Key_5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (phonenumber1[0] == null)
            {
                phonenumber1[0] = Convert.ToString(5);
            }
            else if (phonenumber1[0] != null && phonenumber1[1] == null)
            {
                phonenumber1[1] = Convert.ToString(5);
            }
            else if (phonenumber1[1] != null && phonenumber1[2] == null)
            {
                phonenumber1[2] = Convert.ToString(5);
            }
            else if (phonenumber1[2] != null && phonenumber2[0] == null)
            {
                phonenumber2[0] = Convert.ToString(5);
            }
            else if (phonenumber2[0] != null && phonenumber2[1] == null)
            {
                phonenumber2[1] = Convert.ToString(5);
            }
            else if (phonenumber2[1] != null && phonenumber2[2] == null)
            {
                phonenumber2[2] = Convert.ToString(5);
            }
            else if (phonenumber2[2] != null && phonenumber2[3] == null)
            {
                phonenumber2[3] = Convert.ToString(5);
            }
            else if (phonenumber2[3] != null && phonenumber3[0] == null)
            {
                phonenumber3[0] = Convert.ToString(5);
            }
            else if (phonenumber3[0] != null && phonenumber3[1] == null)
            {
                phonenumber3[1] = Convert.ToString(5);
            }
            else if (phonenumber3[1] != null && phonenumber3[2] == null)
            {
                phonenumber3[2] = Convert.ToString(5);
            }
            else if (phonenumber3[2] != null && phonenumber3[3] == null)
            {
                phonenumber3[3] = Convert.ToString(5);
            }
            InsertHyphone();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void Key_6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (phonenumber1[0] == null)
            {
                phonenumber1[0] = Convert.ToString(6);
            }
            else if (phonenumber1[0] != null && phonenumber1[1] == null)
            {
                phonenumber1[1] = Convert.ToString(6);
            }
            else if (phonenumber1[1] != null && phonenumber1[2] == null)
            {
                phonenumber1[2] = Convert.ToString(6);
            }
            else if (phonenumber1[2] != null && phonenumber2[0] == null)
            {
                phonenumber2[0] = Convert.ToString(6);
            }
            else if (phonenumber2[0] != null && phonenumber2[1] == null)
            {
                phonenumber2[1] = Convert.ToString(6);
            }
            else if (phonenumber2[1] != null && phonenumber2[2] == null)
            {
                phonenumber2[2] = Convert.ToString(6);
            }
            else if (phonenumber2[2] != null && phonenumber2[3] == null)
            {
                phonenumber2[3] = Convert.ToString(6);
            }
            else if (phonenumber2[3] != null && phonenumber3[0] == null)
            {
                phonenumber3[0] = Convert.ToString(6);
            }
            else if (phonenumber3[0] != null && phonenumber3[1] == null)
            {
                phonenumber3[1] = Convert.ToString(6);
            }
            else if (phonenumber3[1] != null && phonenumber3[2] == null)
            {
                phonenumber3[2] = Convert.ToString(6);
            }
            else if (phonenumber3[2] != null && phonenumber3[3] == null)
            {
                phonenumber3[3] = Convert.ToString(6);
            }
            InsertHyphone();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void Key_7_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (phonenumber1[0] == null)
            {
                phonenumber1[0] = Convert.ToString(7);
            }
            else if (phonenumber1[0] != null && phonenumber1[1] == null)
            {
                phonenumber1[1] = Convert.ToString(7);
            }
            else if (phonenumber1[1] != null && phonenumber1[2] == null)
            {
                phonenumber1[2] = Convert.ToString(7);
            }
            else if (phonenumber1[2] != null && phonenumber2[0] == null)
            {
                phonenumber2[0] = Convert.ToString(7);
            }
            else if (phonenumber2[0] != null && phonenumber2[1] == null)
            {
                phonenumber2[1] = Convert.ToString(7);
            }
            else if (phonenumber2[1] != null && phonenumber2[2] == null)
            {
                phonenumber2[2] = Convert.ToString(7);
            }
            else if (phonenumber2[2] != null && phonenumber2[3] == null)
            {
                phonenumber2[3] = Convert.ToString(7);
            }
            else if (phonenumber2[3] != null && phonenumber3[0] == null)
            {
                phonenumber3[0] = Convert.ToString(7);
            }
            else if (phonenumber3[0] != null && phonenumber3[1] == null)
            {
                phonenumber3[1] = Convert.ToString(7);
            }
            else if (phonenumber3[1] != null && phonenumber3[2] == null)
            {
                phonenumber3[2] = Convert.ToString(7);
            }
            else if (phonenumber3[2] != null && phonenumber3[3] == null)
            {
                phonenumber3[3] = Convert.ToString(7);
            }
            InsertHyphone();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void Key_8_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (phonenumber1[0] == null)
            {
                phonenumber1[0] = Convert.ToString(8);
            }
            else if (phonenumber1[0] != null && phonenumber1[1] == null)
            {
                phonenumber1[1] = Convert.ToString(8);
            }
            else if (phonenumber1[1] != null && phonenumber1[2] == null)
            {
                phonenumber1[2] = Convert.ToString(8);
            }
            else if (phonenumber1[2] != null && phonenumber2[0] == null)
            {
                phonenumber2[0] = Convert.ToString(8);
            }
            else if (phonenumber2[0] != null && phonenumber2[1] == null)
            {
                phonenumber2[1] = Convert.ToString(8);
            }
            else if (phonenumber2[1] != null && phonenumber2[2] == null)
            {
                phonenumber2[2] = Convert.ToString(8);
            }
            else if (phonenumber2[2] != null && phonenumber2[3] == null)
            {
                phonenumber2[3] = Convert.ToString(8);
            }
            else if (phonenumber2[3] != null && phonenumber3[0] == null)
            {
                phonenumber3[0] = Convert.ToString(8);
            }
            else if (phonenumber3[0] != null && phonenumber3[1] == null)
            {
                phonenumber3[1] = Convert.ToString(8);
            }
            else if (phonenumber3[1] != null && phonenumber3[2] == null)
            {
                phonenumber3[2] = Convert.ToString(8);
            }
            else if (phonenumber3[2] != null && phonenumber3[3] == null)
            {
                phonenumber3[3] = Convert.ToString(8);
            }
            InsertHyphone();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void Key_9_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (phonenumber1[0] == null)
            {
                phonenumber1[0] = Convert.ToString(9);
            }
            else if (phonenumber1[0] != null && phonenumber1[1] == null)
            {
                phonenumber1[1] = Convert.ToString(9);
            }
            else if (phonenumber1[1] != null && phonenumber1[2] == null)
            {
                phonenumber1[2] = Convert.ToString(9);
            }
            else if (phonenumber1[2] != null && phonenumber2[0] == null)
            {
                phonenumber2[0] = Convert.ToString(9);
            }
            else if (phonenumber2[0] != null && phonenumber2[1] == null)
            {
                phonenumber2[1] = Convert.ToString(9);
            }
            else if (phonenumber2[1] != null && phonenumber2[2] == null)
            {
                phonenumber2[2] = Convert.ToString(9);
            }
            else if (phonenumber2[2] != null && phonenumber2[3] == null)
            {
                phonenumber2[3] = Convert.ToString(9);
            }
            else if (phonenumber2[3] != null && phonenumber3[0] == null)
            {
                phonenumber3[0] = Convert.ToString(9);
            }
            else if (phonenumber3[0] != null && phonenumber3[1] == null)
            {
                phonenumber3[1] = Convert.ToString(9);
            }
            else if (phonenumber3[1] != null && phonenumber3[2] == null)
            {
                phonenumber3[2] = Convert.ToString(9);
            }
            else if (phonenumber3[2] != null && phonenumber3[3] == null)
            {
                phonenumber3[3] = Convert.ToString(9);
            }
            InsertHyphone();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void Key_0_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (phonenumber1[0] == null)
            {
                phonenumber1[0] = Convert.ToString(0);
            }
            else if (phonenumber1[0] != null && phonenumber1[1] == null)
            {
                phonenumber1[1] = Convert.ToString(0);
            }
            else if (phonenumber1[1] != null && phonenumber1[2] == null)
            {
                phonenumber1[2] = Convert.ToString(0);
            }
            else if (phonenumber1[2] != null && phonenumber2[0] == null)
            {
                phonenumber2[0] = Convert.ToString(0);
            }
            else if (phonenumber2[0] != null && phonenumber2[1] == null)
            {
                phonenumber2[1] = Convert.ToString(0);
            }
            else if (phonenumber2[1] != null && phonenumber2[2] == null)
            {
                phonenumber2[2] = Convert.ToString(0);
            }
            else if (phonenumber2[2] != null && phonenumber2[3] == null)
            {
                phonenumber2[3] = Convert.ToString(0);
            }
            else if (phonenumber2[3] != null && phonenumber3[0] == null)
            {
                phonenumber3[0] = Convert.ToString(0);
            }
            else if (phonenumber3[0] != null && phonenumber3[1] == null)
            {
                phonenumber3[1] = Convert.ToString(0);
            }
            else if (phonenumber3[1] != null && phonenumber3[2] == null)
            {
                phonenumber3[2] = Convert.ToString(0);
            }
            else if (phonenumber3[2] != null && phonenumber3[3] == null)
            {
                phonenumber3[3] = Convert.ToString(0);
            }
            InsertHyphone();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void Key_Del_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (phonenumber3[3] != null)
            {
                phonenumber3[3] = null;
            }
            else if (phonenumber3[3] == null && phonenumber3[2] != null)
            {
                phonenumber3[2] = null;
            }
            else if (phonenumber3[2] == null && phonenumber3[1] != null)
            {
                phonenumber3[1] = null;
            }
            else if (phonenumber3[1] == null && phonenumber3[0] != null)
            {
                phonenumber3[0] = null;
            }
            else if (phonenumber3[0] == null && phonenumber2[3] != null)
            {
                phonenumber2[3] = null;
            }
            else if (phonenumber2[3] == null && phonenumber2[2] != null)
            {
                phonenumber2[2] = null;
            }
            else if (phonenumber2[2] == null && phonenumber2[1] != null)
            {
                phonenumber2[1] = null;
            }
            else if (phonenumber2[1] == null && phonenumber2[0] != null)
            {
                phonenumber2[0] = null;
            }
            else if (phonenumber2[0] == null && phonenumber1[2] != null)
            {
                phonenumber1[2] = null;
            }
            else if (phonenumber1[2] == null && phonenumber1[1] != null)
            {
                phonenumber1[1] = null;
            }
            else if (phonenumber1[1] == null && phonenumber1[0] != null)
            {
                phonenumber1[0] = null;
            }
            DelinsertHyphone();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void InsertHyphone()
        {
            if (phonenumber1[2] == null)
            {
                Phonenumbertext.Text = phonenumber1[0] + phonenumber1[1] + phonenumber1[2];
            }
            else if (phonenumber1[2] != null && phonenumber2[3] == null)
            {
                Phonenumbertext.Text = phonenumber1[0] + phonenumber1[1] + phonenumber1[2] + "-" + phonenumber2[0] + phonenumber2[1] + phonenumber2[2] + phonenumber2[3];
            }
            else if (phonenumber2[3] != null && phonenumber3[3] == null)
            {
                Phonenumbertext.Text = phonenumber1[0] + phonenumber1[1] + phonenumber1[2] + "-" + phonenumber2[0] + phonenumber2[1] + phonenumber2[2] + phonenumber2[3] + "-" + phonenumber3[0] + phonenumber3[1] + phonenumber3[2] + phonenumber3[3];
            }
            if (phonenumber3[3] != null)
            {
                Phonenumbertext.Text = phonenumber1[0] + phonenumber1[1] + phonenumber1[2] + "-" + phonenumber2[0] + phonenumber2[1] + phonenumber2[2] + phonenumber2[3] + "-" + phonenumber3[0] + phonenumber3[1] + phonenumber3[2] + phonenumber3[3];
                Kakaobtn.Opacity = 1;
                Kakaobtn.IsEnabled = true;
                Key_1.IsEnabled = false;
                Key_2.IsEnabled = false;
                Key_3.IsEnabled = false;
                Key_4.IsEnabled = false;
                Key_5.IsEnabled = false;
                Key_6.IsEnabled = false;
                Key_7.IsEnabled = false;
                Key_8.IsEnabled = false;
                Key_9.IsEnabled = false;
                Key_0.IsEnabled = false;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void DelinsertHyphone()
        {
            if (phonenumber2[0] == null)
            {
                Phonenumbertext.Text = phonenumber1[0] + phonenumber1[1] + phonenumber1[2];
            }
            else if (phonenumber2[0] != null && phonenumber3[0] == null)
            {
                Phonenumbertext.Text = phonenumber1[0] + phonenumber1[1] + phonenumber1[2] + "-" + phonenumber2[0] + phonenumber2[1] + phonenumber2[2] + phonenumber2[3];
            }
            else if (phonenumber2[3] != null && phonenumber3[3] == null)
            {
                Phonenumbertext.Text = phonenumber1[0] + phonenumber1[1] + phonenumber1[2] + "-" + phonenumber2[0] + phonenumber2[1] + phonenumber2[2] + phonenumber2[3] + "-" + phonenumber3[0] + phonenumber3[1] + phonenumber3[2] + phonenumber3[3];
                if (phonenumber3[2] != null && phonenumber3[3] == null)
                {
                    Kakaobtn.IsEnabled = false;
                    Kakaobtn.Opacity = 0.25;
                    Key_1.IsEnabled = true;
                    Key_2.IsEnabled = true;
                    Key_3.IsEnabled = true;
                    Key_4.IsEnabled = true;
                    Key_5.IsEnabled = true;
                    Key_6.IsEnabled = true;
                    Key_7.IsEnabled = true;
                    Key_8.IsEnabled = true;
                    Key_9.IsEnabled = true;
                    Key_0.IsEnabled = true;
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        #endregion

        private void Kakaobtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Kakaobtn.IsEnabled = false;
                MainWindow.phonenumber = phonenumber1[0] + phonenumber1[1] + phonenumber1[2] + phonenumber2[0] + phonenumber2[1] + phonenumber2[2] + phonenumber2[3] + phonenumber3[0] + phonenumber3[1] + phonenumber3[2] + phonenumber3[3];
                Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 카카오톡 보내기 버튼 클릭, 보내는 번호 : " + MainWindow.phonenumber + "보내는 이미지 : " + ImgCompose.SendKakaoImgName);



                timer.Stop();
                Dispose();
                NavigationService.Navigate(new Uri("View/Final.xaml", UriKind.RelativeOrAbsolute));
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void NoKakaobtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        #region///타이머///

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                kduration--;
                Timer.Text = kduration.ToString();

                if (MainWindow.checkkakao.ToString() == "0" && MainWindow.qroption == 0)
                {
                    if (kduration == -1)
                    {
                        timer.Stop();
                        Dispose();
                        TakePic.photonum = 0;
                        MainWindow.checkretakenum = 0;
                        try
                        {
                            DirectoryInfo sns = new DirectoryInfo(MainWindow.SnsPath);
                            foreach (FileInfo file in sns.EnumerateFiles())
                            {
                                file.Delete();
                                Source.Log.log.Debug("Kakao 폴더 안 이미지 삭제 완료");
                            }
                            DirectoryInfo di = new DirectoryInfo(MainWindow.ResizePath);
                            foreach (FileInfo files in di.EnumerateFiles())
                            {
                                files.Delete();
                                Source.Log.log.Debug("Resize 폴더 안 이미지 삭제 완료");
                            }
                            DirectoryInfo photo = new DirectoryInfo(MainWindow.PhotoPath);
                            foreach (FileInfo files in photo.EnumerateFiles())
                            {
                                files.Delete();
                                Source.Log.log.Debug("Photo 폴더 안 이미지 삭제 완료");
                            }
                            DirectoryInfo print = new DirectoryInfo(MainWindow.Printpath);
                            foreach (FileInfo file in print.EnumerateFiles())
                            {
                                file.Delete();
                                Source.Log.log.Debug("Print 폴더 안 이미지 삭제 완료");
                            }
                        }
                        catch (Exception ex)
                        {
                            Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() -" + ex.Message);
                        }
                        NavigationService.Navigate(new Uri("View/Final.xaml", UriKind.RelativeOrAbsolute));
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();
                    }
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
            try
            {
                Backgroundimg.Source = null;
                Backgroundimg = null;
                Kakaobtn.Source = null;
                Kakaobtn = null;
                NoKakaobtn.Source = null;
                NoKakaobtn = null;
                Key_1.Source = null;
                Key_1 = null;
                Key_2.Source = null;
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
                Key_0.Source = null;
                Key_0 = null;
                Key_Del.Source = null;
                Key_Del = null;
                Phonenumbertext.Text = null;
                Phonenumbertext = null;
                QRCodegif.Source = null;
                QRCodegif = null;
                QRCode.Source = null;
                QRCode = null;
                timer.Tick -= new EventHandler(Timer_Tick);
                Timer.Text = null;
                Timer = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
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

        private void Cameradispose()
        {
            Main.MainCamera.Dispose();
            Main.MainCamera = null;
            Main.APIHandler.Dispose();
            Main.APIHandler = null;
            Main.SetImageAction = null;
        }
    }
}
