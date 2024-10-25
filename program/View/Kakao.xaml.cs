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
using System.Windows.Media.Animation;
using System.Xml.Serialization.Configuration;

namespace wpfTest.View
{
    /// <summary>
    /// Kakao.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Kakao : Page
    {
        #region///변수///

        string[] phonenumber1 = new string[3];
        string[] phonenumber2 = new string[4];
        string[] phonenumber3 = new string[4];
        int kduration = MainWindow.kakaotimer;
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer mediagramtimer = new DispatcherTimer();

        bool uploadqr = false;
        BitmapImage QRCodeimg = new BitmapImage();
        BitmapImage Successimg = new BitmapImage();
        int papercount;
        int pagecount;
        System.Drawing.Image qrcodeimg;

        public string correctedPrintername { get; private set; }

        bool checkprinted = false;

        #endregion

        #region///ini import///

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #endregion

        public Kakao()
        {
            Source.Log.log.Info("KaKaoPage진입");
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                checkprinted = false;
                MainWindow.checknokakaobtn = false;
                MainWindow.phonenumber = "";
                Backgroundimg.Source = MainWindow.kakaopage;

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

                if (MainWindow.inifoldername == "animalhospital")
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
                if (MainWindow.inifoldername == "hhh")
                {
                    Timer.Margin = new Thickness(1613, 130, 107, 850);
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

                if (MainWindow.inifoldername.Contains("mediagram"))
                {
                    Kakaobtn.Source = MainWindow.kakaobtn;
                    Kakaobtn.Width = MainWindow.kakaobtn.Width;
                    Kakaobtn.Height = MainWindow.kakaobtn.Height;
                    NoKakaobtn.Source = MainWindow.nokakaobtn;
                    NoKakaobtn.Width = MainWindow.nokakaobtn.Width;
                    NoKakaobtn.Height = MainWindow.nokakaobtn.Height;
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

                    Phonenumbertext.Foreground = System.Windows.Media.Brushes.Black;

                    Phonenumbertext.Width = 700;
                    Phonenumbertext.Height = 100;

                    Phonenumbertext.Margin = new Thickness(629, 351, 591, 629);

                    Key_1.Width = 100;
                    Key_1.Height = 100;
                    Key_2.Width = 100;
                    Key_2.Height = 100;
                    Key_3.Width = 100;
                    Key_3.Height = 100;
                    Key_4.Width = 100;
                    Key_4.Height = 100;
                    Key_5.Width = 100;
                    Key_5.Height = 100;
                    Key_6.Width = 100;
                    Key_6.Height = 100;
                    Key_7.Width = 100;
                    Key_7.Height = 100;
                    Key_8.Width = 100;
                    Key_8.Height = 100;
                    Key_9.Width = 100;
                    Key_9.Height = 100;
                    Key_0.Width = 100;
                    Key_0.Height = 100;
                    Key_Del.Width = 100;
                    Key_Del.Height = 100;

                    Kakaobtn.Width = 340;
                    Kakaobtn.Height = 210;
                    NoKakaobtn.Width = 370;
                    NoKakaobtn.Height = 100;

                    Key_1.Margin = new Thickness(497, 526, 1920 - 100 - 497, 1080 - 526 - 100);
                    Key_2.Margin = new Thickness(607, 526, 1920 - 100 - 607, 1080 - 526 - 100);
                    Key_3.Margin = new Thickness(717, 526, 1920 - 100 - 717, 1080 - 526 - 100);
                    Key_4.Margin = new Thickness(827, 526, 1920 - 100 - 827, 1080 - 526 - 100);
                    Key_5.Margin = new Thickness(937, 526, 1920 - 100 - 937, 1080 - 526 - 100);
                    Key_6.Margin = new Thickness(497, 636, 1920 - 100 - 497, 1080 - 636 - 100);
                    Key_7.Margin = new Thickness(607, 636, 1920 - 100 - 607, 1080 - 636 - 100);
                    Key_8.Margin = new Thickness(717, 636, 1920 - 100 - 717, 1080 - 636 - 100);
                    Key_9.Margin = new Thickness(827, 636, 1920 - 100 - 827, 1080 - 636 - 100);
                    Key_0.Margin = new Thickness(937, 636, 1920 - 100 - 937, 1080 - 636 - 100);
                    Key_Del.Margin = new Thickness(1053, 526, 1920 - 100 - 1053, 1080 - 526 - 100);
                    Kakaobtn.Margin = new Thickness(1187, 526, 1920 - 340 - 1187, 1080 - 526 - 210);
                    NoKakaobtn.Margin = new Thickness(979, 927, 1920 - 370 - 979, 1080 - 927 - 100);

                    Successimg = new BitmapImage();
                    Successimg.BeginInit();
                    Successimg.UriSource = new Uri(MainWindow.uipath + @"\SuccessKakao.png", UriKind.RelativeOrAbsolute);
                    Successimg.CacheOption = BitmapCacheOption.OnLoad;
                    Successimg.EndInit();

                    Success.Source = Successimg;
                }
                else
                {
                    if (MainWindow.checkkakao == "0" && MainWindow.qroption == 0 && MainWindow.checkvideo == "0")
                    {
                        Kakaobtn.Source = MainWindow.kakaobtn;
                        Kakaobtn.Width = MainWindow.kakaobtn.Width;
                        Kakaobtn.Height = MainWindow.kakaobtn.Height;
                        NoKakaobtn.Source = MainWindow.nokakaobtn;
                        NoKakaobtn.Width = MainWindow.nokakaobtn.Width;
                        NoKakaobtn.Height = MainWindow.nokakaobtn.Height;
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
                    else if (MainWindow.checkkakao == "0" && MainWindow.qroption == 0 && MainWindow.checkvideo == "1")
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
                    else if ((MainWindow.checkkakao.ToString() == "1" && MainWindow.qroption == 1) && MainWindow.checkvideo == "1")
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
                    else if (MainWindow.checkkakao.ToString() == "1" && MainWindow.qroption == 1 && MainWindow.checkvideo == "0")
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
            if(phonenumber1[2] == null)
            {
                Phonenumbertext.Text = phonenumber1[0] + phonenumber1[1] + phonenumber1[2];
            }
            else if(phonenumber1[2] != null && phonenumber2[3] == null)
            {
                Phonenumbertext.Text = phonenumber1[0] + phonenumber1[1] + phonenumber1[2] + "-" + phonenumber2[0] + phonenumber2[1] + phonenumber2[2] +phonenumber2[3];
            }
            else if (phonenumber2[3] != null && phonenumber3[3] == null)
            {
                Phonenumbertext.Text = phonenumber1[0] + phonenumber1[1] + phonenumber1[2] + "-" + phonenumber2[0] + phonenumber2[1] + phonenumber2[2] + phonenumber2[3] + "-" + phonenumber3[0] + phonenumber3[1] + phonenumber3[2] + phonenumber3[3];
            }
            if(phonenumber3[3] != null)
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

                MainWindow.Kakaothread = new System.Threading.Thread(() => SendKakao(MainWindow.phonenumber, ImgCompose.SendKakaoImgName));
                MainWindow.Kakaothread.Start();
                timer.Stop();
                Dispose();
                if (MainWindow.inifoldername == "dearpic1" || MainWindow.inifoldername == "dearpic4" || MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic2")
                {
                    if (MainWindow.coupon == "0")
                    {
                        NavigationService.Navigate(new Uri("View/Final.xaml", UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        NavigationService.Navigate(new Uri("View/coupon.xaml", UriKind.RelativeOrAbsolute));
                    }
                }
                else
                {
                    if (MainWindow.inifoldername.Contains("mediagram"))
                    {
                        Success.Margin = new Thickness(0, 0, 0, 0);
                        kduration = 3;


                        mediagramtimer.Interval = TimeSpan.FromSeconds(1);
                        mediagramtimer.Tick += new EventHandler(mediagramtimer_Tick);
                        mediagramtimer.Start();
                    }
                    else
                    {
                        NavigationService.Navigate(new Uri("View/Final.xaml", UriKind.RelativeOrAbsolute));
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

        private void SendMessage(string number) // 메세지 보내기
        {
            try
            {
                Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 보내는 번호 : " + number);
                string url = "";

                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                string sendData = "PHONE_NO=" + number;

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

                number = "";
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
                bool isConnected = MainWindow.CheckInternetConnection();
                if (isConnected)
                {
                    Source.Log.log.Info("인터넷 연결 되어있음");
                    string url = "";

                    WebRequest request = WebRequest.Create(url);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";

                    string sendData = "PHONE_NO=" + number;

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

                    number = "";
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                else
                {
                    Source.Log.log.Info("인터넷 연결 끊김");
                }
            }
        }

        private void NoKakaobtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                NoKakaobtn.IsEnabled = false;
                MainWindow.checknokakaobtn = true;
                bool isConnected = MainWindow.CheckInternetConnection();
                if (isConnected)
                {
                    Source.Log.log.Info("인터넷 연결");
                    if (MainWindow.paymentini.ToString() == "free" && MainWindow.checkfreeornot != "0")
                    {
                        SendTotalData();
                    }
                }
                else
                {
                    Source.Log.log.Info("인터넷 연결 끊김");
                }
                timer.Stop();
                Dispose();
                if (MainWindow.inifoldername == "dearpic1" || MainWindow.inifoldername == "dearpic4" || MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic2")
                {
                    if (MainWindow.coupon == "0")
                    {
                        NavigationService.Navigate(new Uri("View/Final.xaml", UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        NavigationService.Navigate(new Uri("View/coupon.xaml", UriKind.RelativeOrAbsolute));
                    }
                }
                else
                {
                    NavigationService.Navigate(new Uri("View/Final.xaml", UriKind.RelativeOrAbsolute));
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
                        MainWindow.checknokakaobtn = true;
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
                            DirectoryInfo gray = new DirectoryInfo(MainWindow.GrayFilterPath);
                            foreach (FileInfo file in gray.EnumerateFiles())
                            {
                                file.Delete();
                                Source.Log.log.Debug("gray 폴더 안 이미지 삭제 완료");
                            }
                        }
                        catch (Exception ex)
                        {
                            Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() -" + ex.Message);
                        }
                        if (MainWindow.inifoldername == "dearpic1" || MainWindow.inifoldername == "dearpic4" || MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic2")
                        {
                            if (MainWindow.coupon == "0")
                            {
                                NavigationService.Navigate(new Uri("View/Final.xaml", UriKind.RelativeOrAbsolute));
                            }
                            else
                            {
                                NavigationService.Navigate(new Uri("View/coupon.xaml", UriKind.RelativeOrAbsolute));
                            }
                        }
                        else
                        {
                            NavigationService.Navigate(new Uri("View/Final.xaml", UriKind.RelativeOrAbsolute));
                        }
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();
                    }
                }
                else if (MainWindow.checkkakao.ToString() == "1" && MainWindow.qroption == 1)
                {
                    if (MainWindow.inifoldername == "animalhospital")
                    {
                        if (kduration == -1)
                        {
                            MainWindow.checknokakaobtn = true;
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
                                DirectoryInfo gray = new DirectoryInfo(MainWindow.GrayFilterPath);
                                foreach (FileInfo file in gray.EnumerateFiles())
                                {
                                    file.Delete();
                                    Source.Log.log.Debug("gray 폴더 안 이미지 삭제 완료");
                                }
                            }
                            catch (Exception ex)
                            {
                                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() -" + ex.Message);
                            }
                            if (MainWindow.inifoldername == "dearpic1" || MainWindow.inifoldername == "dearpic4" || MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic2")
                            {
                                if (MainWindow.coupon == "0")
                                {
                                    NavigationService.Navigate(new Uri("View/Final.xaml", UriKind.RelativeOrAbsolute));
                                }
                                else
                                {
                                    NavigationService.Navigate(new Uri("View/coupon.xaml", UriKind.RelativeOrAbsolute));
                                }
                            }
                            else
                            {
                                NavigationService.Navigate(new Uri("View/Final.xaml", UriKind.RelativeOrAbsolute));
                            }
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            GC.Collect();
                        }
                    }
                    else if (MainWindow.inifoldername == "hhh")
                    {
                        if (kduration == -1)
                        {
                            MainWindow.checknokakaobtn = true;
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
                                DirectoryInfo gray = new DirectoryInfo(MainWindow.GrayFilterPath);
                                foreach (FileInfo file in gray.EnumerateFiles())
                                {
                                    file.Delete();
                                    Source.Log.log.Debug("gray 폴더 안 이미지 삭제 완료");
                                }
                            }
                            catch (Exception ex)
                            {
                                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() -" + ex.Message);
                            }
                            if (MainWindow.inifoldername == "dearpic1" || MainWindow.inifoldername == "dearpic4" || MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic2")
                            {
                                if (MainWindow.coupon == "0")
                                {
                                    NavigationService.Navigate(new Uri("View/Final.xaml", UriKind.RelativeOrAbsolute));
                                }
                                else
                                {
                                    NavigationService.Navigate(new Uri("View/coupon.xaml", UriKind.RelativeOrAbsolute));
                                }
                            }
                            else
                            {
                                NavigationService.Navigate(new Uri("View/Final.xaml", UriKind.RelativeOrAbsolute));
                            }
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            GC.Collect();
                        }
                    }
                    else if (MainWindow.inifoldername == "boryeongparm")
                    {
                        if (MainWindow.checkupload)
                        {
                            if (File.Exists(MainWindow.savekakaopath + @"\QRCode" + ImgCompose.SendKakaoImgName))
                            {
                                QRCodeimg = new BitmapImage();
                                QRCodeimg.BeginInit();
                                QRCodeimg.UriSource = new Uri(MainWindow.savekakaopath + @"\QRCode" + ImgCompose.SendKakaoImgName, UriKind.RelativeOrAbsolute);
                                QRCodeimg.CacheOption = BitmapCacheOption.OnLoad;
                                QRCodeimg.EndInit();
                                QRCode.Source = QRCodeimg;
                                QRCodegif.Opacity = 0;
                                uploadqr = true;
                            }
                        }

                        if (kduration == -1)
                        {
                            timer.Stop();
                            QRCodegif.Opacity = 0;
                            Dispose();
                            NavigationService.Navigate(new Uri("View/Final.xaml", UriKind.RelativeOrAbsolute));
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            GC.Collect();
                        }
                    }
                    else
                    {
                        if (MainWindow.checkupload)
                        {
                            if (File.Exists(MainWindow.savekakaopath + @"\QRCode" + ImgCompose.SendKakaoImgName))
                            {
                                QRCodeimg = new BitmapImage();
                                QRCodeimg.BeginInit();
                                QRCodeimg.UriSource = new Uri(MainWindow.savekakaopath + @"\QRCode" + ImgCompose.SendKakaoImgName, UriKind.RelativeOrAbsolute);
                                QRCodeimg.CacheOption = BitmapCacheOption.OnLoad;
                                QRCodeimg.EndInit();
                                QRCode.Source = QRCodeimg;
                                Source.Log.log.Info("Qrcode 이미지 업로드 완료");
                                QRCodegif.Opacity = 0;
                                uploadqr = true;

                                /*Bitmap qrcodebitmap = new Bitmap(MainWindow.savekakaopath + @"\QRCode" + ImgCompose.SendKakaoImgName);

                                for (int x = 0; x < qrcodebitmap.Width; x++)
                                {
                                    for (int y = 0; y < qrcodebitmap.Height; y++)
                                    {
                                        System.Drawing.Color pixelColor = qrcodebitmap.GetPixel(x, y);
                                        if (pixelColor.R == 255 && pixelColor.G == 255 && pixelColor.B == 255)
                                        {
                                            qrcodebitmap.SetPixel(x, y, System.Drawing.Color.Transparent);
                                        }
                                    }
                                }

                                qrcodeimg = (System.Drawing.Image)qrcodebitmap;*/

                                qrcodeimg = System.Drawing.Image.FromFile(MainWindow.savekakaopath + @"\QRCode" + ImgCompose.SendKakaoImgName);
                                if (!checkprinted)
                                {
                                    checkprinted = true;
                                    Printthread();
                                }

                                bool internetcheck = MainWindow.CheckInternetConnection();
                                if (internetcheck)
                                {
                                    Source.Log.log.Info("인터넷 연결이 되어있음.");
                                    //SendTotalData();
                                }
                                else
                                {
                                    Source.Log.log.Info("인터넷 연결이 끊겨있음");
                                }
                            }
                        }

                        if (kduration == -1)
                        {
                            timer.Stop();
                            if (!uploadqr)
                            {
                                Printthread2();
                            }
                            QRCodegif.Opacity = 0;
                            Dispose();
                            NavigationService.Navigate(new Uri("View/Final.xaml", UriKind.RelativeOrAbsolute));
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            GC.Collect();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void mediagramtimer_Tick(object sender, EventArgs e)
        {
            try
            {
                kduration--;
                
                if (kduration == 1)
                {
                    Success.IsEnabled = false;
                }
                if (kduration == 0)
                {
                    mediagramtimer.Stop();
                    mediagramtimer.Tick -= new EventHandler(mediagramtimer_Tick);
                    NavigationService.Navigate(new Uri("View/Final.xaml", UriKind.RelativeOrAbsolute));
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

        private void UploadIMG(string phonenumber,string filename)
        {
            try
            {
                //FTP 접속에 필요한 정보
                string addr = string.Empty;
                string user = string.Empty;
                string pwd = string.Empty;
                string port = string.Empty;

                addr = "";
                user = "";
                pwd = "";
                port = "";

                Source.FTPManager manager = new Source.FTPManager();
                bool result = manager.ConnectToServer(addr, port, user, pwd);
                string path = string.Empty;
                string fileName = string.Empty;

                string localPath = MainWindow.SnsPath; //업로드 파일이있는 위치
                path = MainWindow.foldername + "\\" + phonenumber; //업로드 할 파일 저장할 FTP 경로 지정 //폴더없으면 생성후 저장

                DirectoryInfo dirInfo = new DirectoryInfo(localPath);
                FileInfo[] infos = dirInfo.GetFiles();
                if (result == true)
                {
                    Source.Log.log.Info("FTP 접속 성공");
                    foreach (FileInfo file in infos)
                    {
                        if (file.Name == filename)
                        {
                            Source.Log.log.Info("이미지 검색 성공");
                            string localFilePath = file.FullName;
                            string remoteFileName = System.IO.Path.Combine(path);

                            if (manager.UpLoad(remoteFileName, localFilePath) == false)
                            {
                                Source.Log.log.Info("FTP 이미지 Upload 실패");
                                file.Delete();
                            }
                            else
                            {
                                Source.Log.log.Info("FTP 이미지 Upload 성공");
                                file.Delete();
                                if (MainWindow.inifoldername.ToString().Contains("animalhospital"))
                                {
                                    Source.SendMessage.Sendmessage(phonenumber, MainWindow.inifoldername, MainWindow.phpurl);
                                }
                                if (MainWindow.inifoldername == "hhh")
                                {
                                    Source.SendMessage.Sendmessage(phonenumber, MainWindow.inifoldername, MainWindow.phpurl);
                                }
                                else
                                {
                                    Source.SendMessage.Sendmessage(phonenumber, MainWindow.inifoldername, "");
                                }
                            }
                        }
                    }
                }
                else
                {
                    Source.Log.log.Info("FTP 접속 실패");
                    bool isConnected = MainWindow.CheckInternetConnection();
                    if (isConnected)
                    {
                        //FTP 접속에 필요한 정보
                        addr = string.Empty;
                        user = string.Empty;
                        pwd = string.Empty;
                        port = string.Empty;

                        addr = "";
                        user = "";
                        pwd = "";
                        port = "";

                        manager = new Source.FTPManager();
                        result = manager.ConnectToServer(addr, port, user, pwd);
                        path = string.Empty;
                        fileName = string.Empty;

                        localPath = MainWindow.SnsPath; //업로드 파일이있는 위치
                        path = MainWindow.foldername + "\\" + phonenumber; //업로드 할 파일 저장할 FTP 경로 지정 //폴더없으면 생성후 저장

                        dirInfo = new DirectoryInfo(localPath);
                        infos = dirInfo.GetFiles();
                        if (result == true)
                        {
                            Source.Log.log.Info("FTP 접속 성공");
                            foreach (FileInfo file in infos)
                            {
                                if (file.Name == filename)
                                {
                                    Source.Log.log.Info("이미지 검색 성공");
                                    string localFilePath = file.FullName;
                                    string remoteFileName = System.IO.Path.Combine(path);

                                    if (manager.UpLoad(remoteFileName, localFilePath) == false)
                                    {
                                        Source.Log.log.Info("FTP 이미지 Upload 실패");
                                        file.Delete();
                                    }
                                    else
                                    {
                                        Source.Log.log.Info("FTP 이미지 Upload 성공");
                                        file.Delete();
                                        if (MainWindow.inifoldername.ToString().Contains("animalhospital"))
                                        {
                                            Source.SendMessage.Sendmessage(phonenumber, MainWindow.inifoldername, MainWindow.htmlUrl);
                                        }
                                        if (MainWindow.inifoldername == "hhh")
                                        {
                                            Source.SendMessage.Sendmessage(phonenumber, MainWindow.inifoldername, MainWindow.phpurl);
                                        }
                                        else
                                        {
                                            Source.SendMessage.Sendmessage(phonenumber, MainWindow.inifoldername, "");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Source.Log.log.Info("인터넷 연결 끊겨있음");
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void SendKakao(string phonenumber, string imgname)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                bool isConnected = MainWindow.CheckInternetConnection();
                if (isConnected)
                {
                    Source.Log.log.Info("인터넷에 연결되어 있습니다.");
                    if (MainWindow.paymentini.ToString() == "free" && MainWindow.checkfreeornot != "0")
                    {
                        SendTotalData();
                    }
                    if (MainWindow.inifoldername == "animalhospital" || MainWindow.inifoldername == "hhh" || MainWindow.checkvideo == "1")
                    {
                        MainWindow.checkupload = true;
                    }
                    else
                    {
                        SendData(phonenumber, imgname);
                        UploadIMG(phonenumber, imgname);
                    }
                }
                else
                {
                    Source.Log.log.Info("인터넷에 연결되어 있지 않습니다.");
                }
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 쓰레드 종료지점");
            }
            catch (ThreadAbortException threadex)
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - " + threadex.Message);
                Source.Log.log.Info("SendKakao thread 종료");
            }
            catch (Exception ex)
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void SendData(string number, string filename)
        {
            try
            {
                string url = "";

                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                string sendData = "M_PATH=" + number + "&P_PATH=" + MainWindow.foldername.ToString() + "&FILE_NM=" + filename;
                Source.Log.log.Info("폴더 등록 정보 : " + sendData);

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
                bool isConnected = MainWindow.CheckInternetConnection();
                if (isConnected)
                {
                    string url = "";

                    WebRequest request = WebRequest.Create(url);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";

                    string sendData = "M_PATH=" + number + "&P_PATH=" + MainWindow.foldername.ToString() + "&FILE_NM=" + filename;
                    Source.Log.log.Info("폴더 등록 정보 : " + sendData);

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
                else
                {
                    Source.Log.log.Info("인터넷 연결 끊겨있음");
                }
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

                if (MainWindow.checkfreeornot == "1")
                {
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


        private void Cameradispose()
        {
            Main.MainCamera.Dispose();
            Main.MainCamera = null;
            Main.APIHandler.Dispose();
            Main.APIHandler = null;
            Main.SetImageAction = null;
        }

        private void Printthread()
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                string day = DateTime.Today.ToString("dd"); if (MainWindow.printedpage.ToString() == "0")
                {
                    pagecount = 0;
                    pagecount = pagecount + 1;
                    MainWindow.printedpage.Append(pagecount);
                    WritePrivateProfileString("PrintedPage", day, Convert.ToString(MainWindow.printedpage), MainWindow.bankbookinipath);

                }
                else
                {
                    if (MainWindow.day == day)
                    {
                        pagecount = Convert.ToInt32(MainWindow.printedpage.ToString());
                        pagecount = pagecount + 1;
                        MainWindow.printedpage.Clear();
                        MainWindow.printedpage.Append(pagecount);
                        WritePrivateProfileString("PrintedPage", day, Convert.ToString(MainWindow.printedpage), MainWindow.bankbookinipath);
                    }
                    else
                    {
                        MainWindow.printedpage.Clear();
                        pagecount = 1;
                        MainWindow.printedpage.Append(pagecount);
                        WritePrivateProfileString("PrintedPage", day, Convert.ToString(MainWindow.printedpage), MainWindow.bankbookinipath);
                    }
                }

                //temp1numberload();

                if (MainWindow.SorR == "R")
                {
                    switch (MainWindow.printerratio)
                    {
                        case 1:
                            using (MainWindow.d = Graphics.FromImage(MainWindow.canvus))
                            {
                                MainWindow.d.DrawImage(qrcodeimg, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                            break;
                        case 2:
                            using (MainWindow.g = Graphics.FromImage(MainWindow.bigcanvus2))
                            {
                                MainWindow.g.DrawImage(qrcodeimg, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                            break;
                        case 3:
                            using (MainWindow.r = Graphics.FromImage(MainWindow.bigcanvus))
                            {
                                MainWindow.r.DrawImage(qrcodeimg, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                            break;
                    }
                }

                int offSetX = Convert.ToInt32(MainWindow.offsetx);
                int offSetY = Convert.ToInt32(MainWindow.offsety);
                int factorX = Convert.ToInt32(MainWindow.factorx);
                int factorY = Convert.ToInt32(MainWindow.factory);

                switch (MainWindow.printerratio)
                {
                    case 1:
                        MainWindow.canvus.Save(MainWindow.Printpath + "\\Print" + ".PNG", ImageFormat.Png);

                        System.Drawing.Image saveimg2_1 = Bitmap.FromFile(MainWindow.Printpath + "\\Print" + ".PNG");

                        MainWindow.g.DrawImage(saveimg2_1, 0, 0);
                        MainWindow.g.DrawImage(saveimg2_1, 600, 0);
                        SetAutoPrinter();
                        if (MainWindow.pagenum == "1")
                        {
                            PrintBitmap(MainWindow.bigcanvus2, 1, isLandScape: true, offSetX, offSetY, factorX, factorY); //4컷 
                            papercount = Convert.ToInt32(MainWindow.papercount.ToString()) - 1;
                        }
                        else
                        {
                            PrintBitmap(MainWindow.bigcanvus2, PageSelect.PaperCount, isLandScape: true, offSetX, offSetY, factorX, factorY); //4컷 
                            papercount = Convert.ToInt32(MainWindow.papercount.ToString()) - PageSelect.PaperCount;
                        }
                        MainWindow.papercount.Clear();
                        MainWindow.papercount.Append(papercount);
                        WritePrivateProfileString("Setting", "PaperCount", papercount.ToString(), MainWindow.iniPath);
                        WritePrivateProfileString("RemainPaper", day, papercount.ToString(), MainWindow.bankbookinipath);
                        Source.Log.log.Info("지금까지 출력된 장수 : " + pagecount.ToString());
                        saveimg2_1.Dispose();
                        MainWindow.canvus.Dispose();
                        MainWindow.canvus = new Bitmap(600, 1800);
                        MainWindow.bigcanvus2.Dispose();
                        MainWindow.bigcanvus2 = new Bitmap(1200, 1800);
                        MainWindow.d = Graphics.FromImage(MainWindow.canvus);
                        MainWindow.g = Graphics.FromImage(MainWindow.bigcanvus2);
                        break;
                    case 2:
                        MainWindow.bigcanvus2.Save(MainWindow.Printpath + "\\Print" + ".PNG", ImageFormat.Png);
                        SetAutoPrinter();
                        if (MainWindow.pagenum == "1")
                        {
                            PrintBitmap(MainWindow.bigcanvus2, 1, isLandScape: true, offSetX, offSetY, factorX, factorY); //4컷 
                            papercount = Convert.ToInt32(MainWindow.papercount.ToString()) - 1;
                        }
                        else
                        {
                            PrintBitmap(MainWindow.bigcanvus2, PageSelect.PaperCount, isLandScape: true, offSetX, offSetY, factorX, factorY); //4컷 
                            papercount = Convert.ToInt32(MainWindow.papercount.ToString()) - PageSelect.PaperCount;
                        }
                        MainWindow.papercount.Clear();
                        MainWindow.papercount.Append(papercount);
                        WritePrivateProfileString("Setting", "PaperCount", papercount.ToString(), MainWindow.iniPath);
                        WritePrivateProfileString("RemainPaper", day, papercount.ToString(), MainWindow.bankbookinipath);
                        Source.Log.log.Info("지금까지 출력된 장수 : " + pagecount.ToString());
                        MainWindow.canvus.Dispose();
                        MainWindow.canvus = new Bitmap(600, 1800);
                        MainWindow.bigcanvus2.Dispose();
                        MainWindow.bigcanvus2 = new Bitmap(1200, 1800);
                        MainWindow.d = Graphics.FromImage(MainWindow.canvus);
                        MainWindow.g = Graphics.FromImage(MainWindow.bigcanvus2);
                        break;
                    case 3:
                        MainWindow.bigcanvus.Save(MainWindow.Printpath + "\\Print" + ".PNG", ImageFormat.Png);
                        SetAutoPrinter();
                        MainWindow.bigcanvus.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        if (MainWindow.pagenum == "1")
                        {
                            PrintBitmap(MainWindow.bigcanvus, 1, isLandScape: true, offSetX, offSetY, factorX, factorY); //4컷 
                            papercount = Convert.ToInt32(MainWindow.papercount.ToString()) - 1;
                        }
                        else
                        {
                            PrintBitmap(MainWindow.bigcanvus, PageSelect.PaperCount, isLandScape: true, offSetX, offSetY, factorX, factorY); //4컷 
                            papercount = Convert.ToInt32(MainWindow.papercount.ToString()) - PageSelect.PaperCount;
                        }
                        MainWindow.papercount.Clear();
                        MainWindow.papercount.Append(papercount);
                        WritePrivateProfileString("Setting", "PaperCount", papercount.ToString(), MainWindow.iniPath);
                        WritePrivateProfileString("RemainPaper", day, papercount.ToString(), MainWindow.bankbookinipath);
                        Source.Log.log.Info("지금까지 출력된 장수 : " + pagecount.ToString());
                        MainWindow.bigcanvus.Dispose();
                        MainWindow.bigcanvus = new Bitmap(1800, 1200);
                        MainWindow.r = Graphics.FromImage(MainWindow.bigcanvus);
                        break;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Printthread2()
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                string day = DateTime.Today.ToString("dd"); if (MainWindow.printedpage.ToString() == "0")
                {
                    pagecount = 0;
                    pagecount = pagecount + 1;
                    MainWindow.printedpage.Append(pagecount);
                    WritePrivateProfileString("PrintedPage", day, Convert.ToString(MainWindow.printedpage), MainWindow.bankbookinipath);

                }
                else
                {
                    if (MainWindow.day == day)
                    {
                        pagecount = Convert.ToInt32(MainWindow.printedpage.ToString());
                        pagecount = pagecount + 1;
                        MainWindow.printedpage.Clear();
                        MainWindow.printedpage.Append(pagecount);
                        WritePrivateProfileString("PrintedPage", day, Convert.ToString(MainWindow.printedpage), MainWindow.bankbookinipath);
                    }
                    else
                    {
                        MainWindow.printedpage.Clear();
                        pagecount = 1;
                        MainWindow.printedpage.Append(pagecount);
                        WritePrivateProfileString("PrintedPage", day, Convert.ToString(MainWindow.printedpage), MainWindow.bankbookinipath);
                    }
                }

                int offSetX = Convert.ToInt32(MainWindow.offsetx);
                int offSetY = Convert.ToInt32(MainWindow.offsety);
                int factorX = Convert.ToInt32(MainWindow.factorx);
                int factorY = Convert.ToInt32(MainWindow.factory);

                switch (MainWindow.printerratio)
                {
                    case 1:
                        MainWindow.canvus.Save(MainWindow.Printpath + "\\Print" + ".PNG", ImageFormat.Png);

                        System.Drawing.Image saveimg2_1 = Bitmap.FromFile(MainWindow.Printpath + "\\Print" + ".PNG");

                        MainWindow.g.DrawImage(saveimg2_1, 0, 0);
                        MainWindow.g.DrawImage(saveimg2_1, 600, 0);
                        SetAutoPrinter();
                        if (MainWindow.pagenum == "1")
                        {
                            PrintBitmap(MainWindow.bigcanvus2, 1, isLandScape: true, offSetX, offSetY, factorX, factorY); //4컷 
                            papercount = Convert.ToInt32(MainWindow.papercount.ToString()) - 1;
                        }
                        else
                        {
                            PrintBitmap(MainWindow.bigcanvus2, PageSelect.PaperCount, isLandScape: true, offSetX, offSetY, factorX, factorY); //4컷 
                            papercount = Convert.ToInt32(MainWindow.papercount.ToString()) - PageSelect.PaperCount;
                        }
                        MainWindow.papercount.Clear();
                        MainWindow.papercount.Append(papercount);
                        WritePrivateProfileString("Setting", "PaperCount", papercount.ToString(), MainWindow.iniPath);
                        WritePrivateProfileString("RemainPaper", day, papercount.ToString(), MainWindow.bankbookinipath);
                        Source.Log.log.Info("지금까지 출력된 장수 : " + pagecount.ToString());
                        saveimg2_1.Dispose();
                        MainWindow.canvus.Dispose();
                        MainWindow.canvus = new Bitmap(600, 1800);
                        MainWindow.bigcanvus2.Dispose();
                        MainWindow.bigcanvus2 = new Bitmap(1200, 1800);
                        MainWindow.d = Graphics.FromImage(MainWindow.canvus);
                        MainWindow.g = Graphics.FromImage(MainWindow.bigcanvus2);
                        break;
                    case 2:
                        MainWindow.bigcanvus2.Save(MainWindow.Printpath + "\\Print" + ".PNG", ImageFormat.Png);
                        SetAutoPrinter();
                        if (MainWindow.pagenum == "1")
                        {
                            PrintBitmap(MainWindow.bigcanvus2, 1, isLandScape: true, offSetX, offSetY, factorX, factorY); //4컷 
                            papercount = Convert.ToInt32(MainWindow.papercount.ToString()) - 1;
                        }
                        else
                        {
                            PrintBitmap(MainWindow.bigcanvus2, PageSelect.PaperCount, isLandScape: true, offSetX, offSetY, factorX, factorY); //4컷 
                            papercount = Convert.ToInt32(MainWindow.papercount.ToString()) - PageSelect.PaperCount;
                        }
                        MainWindow.papercount.Clear();
                        MainWindow.papercount.Append(papercount);
                        WritePrivateProfileString("Setting", "PaperCount", papercount.ToString(), MainWindow.iniPath);
                        WritePrivateProfileString("RemainPaper", day, papercount.ToString(), MainWindow.bankbookinipath);
                        Source.Log.log.Info("지금까지 출력된 장수 : " + pagecount.ToString());
                        MainWindow.canvus.Dispose();
                        MainWindow.canvus = new Bitmap(600, 1800);
                        MainWindow.bigcanvus2.Dispose();
                        MainWindow.bigcanvus2 = new Bitmap(1200, 1800);
                        MainWindow.d = Graphics.FromImage(MainWindow.canvus);
                        MainWindow.g = Graphics.FromImage(MainWindow.bigcanvus2);
                        break;
                    case 3:
                        MainWindow.bigcanvus.Save(MainWindow.Printpath + "\\Print" + ".PNG", ImageFormat.Png);
                        SetAutoPrinter();
                        MainWindow.bigcanvus.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        if (MainWindow.pagenum == "1")
                        {
                            PrintBitmap(MainWindow.bigcanvus, 1, isLandScape: true, offSetX, offSetY, factorX, factorY); //4컷 
                            papercount = Convert.ToInt32(MainWindow.papercount.ToString()) - 1;
                        }
                        else
                        {
                            PrintBitmap(MainWindow.bigcanvus, PageSelect.PaperCount, isLandScape: true, offSetX, offSetY, factorX, factorY); //4컷 
                            papercount = Convert.ToInt32(MainWindow.papercount.ToString()) - PageSelect.PaperCount;
                        }
                        MainWindow.papercount.Clear();
                        MainWindow.papercount.Append(papercount);
                        WritePrivateProfileString("Setting", "PaperCount", papercount.ToString(), MainWindow.iniPath);
                        WritePrivateProfileString("RemainPaper", day, papercount.ToString(), MainWindow.bankbookinipath);
                        Source.Log.log.Info("지금까지 출력된 장수 : " + pagecount.ToString());
                        MainWindow.bigcanvus.Dispose();
                        MainWindow.bigcanvus = new Bitmap(1800, 1200);
                        MainWindow.r = Graphics.FromImage(MainWindow.bigcanvus);
                        break;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void SetAutoPrinter()
        {
            PrinterSettings printerSettings = new PrinterSettings();
            if (MainWindow.SorR == "R")
            {
                correctedPrintername = printerSettings.PrinterName;
            }
            else
            {
                if(printerSettings.PrinterName.Contains("SINFONIA"))
                {
                    if (MainWindow.inifoldername == "dearpic1")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                printerSettings.PrinterName = "SINFONIA_Split";
                                correctedPrintername = printerSettings.PrinterName;
                                break;
                            case 2:
                                printerSettings.PrinterName = "SINFONIA";
                                correctedPrintername = printerSettings.PrinterName;
                                break;
                            case 3:
                                printerSettings.PrinterName = "SINFONIA";
                                correctedPrintername = printerSettings.PrinterName;
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "dearpic4" || MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic2")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                printerSettings.PrinterName = "SINFONIA";
                                correctedPrintername = printerSettings.PrinterName;
                                break;
                            case 2:
                                printerSettings.PrinterName = "SINFONIA";
                                correctedPrintername = printerSettings.PrinterName;
                                break;
                            case 3:
                                printerSettings.PrinterName = "SINFONIA";
                                correctedPrintername = printerSettings.PrinterName;
                                break;
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                printerSettings.PrinterName = "SINFONIA_Split";
                                correctedPrintername = printerSettings.PrinterName;
                                break;
                            case 2:
                                printerSettings.PrinterName = "SINFONIA";
                                correctedPrintername = printerSettings.PrinterName;
                                break;
                            case 3:
                                printerSettings.PrinterName = "SINFONIA";
                                correctedPrintername = printerSettings.PrinterName;
                                break;
                        }
                    }
                }
                else
                {
                    if (MainWindow.inifoldername == "dearpic1")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                printerSettings.PrinterName = "DS-RX1_Split";
                                correctedPrintername = printerSettings.PrinterName;
                                break;
                            case 2:
                                printerSettings.PrinterName = "DS-RX1";
                                correctedPrintername = printerSettings.PrinterName;
                                break;
                            case 3:
                                printerSettings.PrinterName = "DS-RX1";
                                correctedPrintername = printerSettings.PrinterName;
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "dearpic4" || MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic2")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                printerSettings.PrinterName = "DS-RX1";
                                correctedPrintername = printerSettings.PrinterName;
                                break;
                            case 2:
                                printerSettings.PrinterName = "DS-RX1";
                                correctedPrintername = printerSettings.PrinterName;
                                break;
                            case 3:
                                printerSettings.PrinterName = "DS-RX1";
                                correctedPrintername = printerSettings.PrinterName;
                                break;
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                printerSettings.PrinterName = "DS-RX1_Split";
                                correctedPrintername = printerSettings.PrinterName;
                                break;
                            case 2:
                                printerSettings.PrinterName = "DS-RX1";
                                correctedPrintername = printerSettings.PrinterName;
                                break;
                            case 3:
                                printerSettings.PrinterName = "DS-RX1";
                                correctedPrintername = printerSettings.PrinterName;
                                break;
                        }
                    }
                }
            }
        }

        private void PrintBitmap(Bitmap bitmap, int Copies = 1, bool isLandScape = false, int OffSetX = 0, int OffSetY = 0, int FactorX = 0, int FactorY = 0, bool isMessage = true)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 비트맵 인쇄");
            try
            {
                if (correctedPrintername != null)
                {
                    Source.Log.log.Debug("Print Copy : " + Copies);
                    PrintDocument printDocument = new PrintDocument();
                    printDocument.PrintController = new StandardPrintController();
                    printDocument.DefaultPageSettings.PrinterSettings.PrinterName = correctedPrintername;
                    if (bitmap.Width > bitmap.Height)
                    {
                        isLandScape = true;
                    }
                    else
                    {
                        isLandScape = false;
                    }
                    printDocument.DefaultPageSettings.Landscape = isLandScape;
                    //Margins margins = new Margins(8 + OffSetX, 0, 6 + OffSetY, 0);
                    Margins margins = new Margins(8 + OffSetX, 0, 7 + OffSetY, 0);
                    if (isLandScape)
                    {
                        margins = new Margins(7 + OffSetY, 0, 2 + OffSetX, 0);
                    }
                    printDocument.DefaultPageSettings.Margins = margins;
                    printDocument.OriginAtMargins = true;
                    printDocument.DefaultPageSettings.PrinterSettings.Copies = (short)Copies;
                    PrinterResolution printerResolution = new PrinterResolution();
                    printerResolution.Kind = PrinterResolutionKind.Custom;
                    printerResolution.X = 600;
                    printerResolution.Y = 600;
                    printDocument.DefaultPageSettings.PrinterResolution = printerResolution;
                    printDocument.PrintPage += delegate (object sender, PrintPageEventArgs args)
                    {
                        System.Drawing.Image image = new Bitmap(bitmap);
                        args.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        if (!isLandScape)
                        {
                            args.Graphics.DrawImage(image, new System.Drawing.Rectangle(0, 0, 400 + FactorX, 600 + FactorY), new System.Drawing.Rectangle(0, 0, 1200, 1800), GraphicsUnit.Pixel);
                        }
                        else
                        {
                            args.Graphics.DrawImage(image, new System.Drawing.Rectangle(0, 0, 603 + FactorY, 401 + FactorX), new System.Drawing.Rectangle(0, 0, 1800, 1200), GraphicsUnit.Pixel);
                        }
                        image.Dispose();
                    };
                    printDocument.Print();
                    bitmap.Dispose();
                    printDocument.Dispose();
                }
                else if (isMessage)
                {
                    MessageBox.Show("출력할 프린터가 없습니다.");
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

        private void Success_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                mediagramtimer.Stop();
                mediagramtimer.Tick -= new EventHandler(mediagramtimer_Tick);
                NavigationService.Navigate(new Uri("View/Final.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
                NavigationService.Navigate(new Uri("View/Final.xaml", UriKind.RelativeOrAbsolute));
            }
        }
    }
}
