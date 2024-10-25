using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Windows.Threading;
using wpfTest;
using wpfTest.View;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Onecut.View
{
    /// <summary>
    /// SelectFrameDesign.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SelectFrameDesign : Page
    {
        int tsduration = Convert.ToInt32(MainWindow.count);
        public static int tempdesign = 0;
        DispatcherTimer timer = new DispatcherTimer();

        BitmapImage Nextbtnimgoff = new BitmapImage();

        BitmapImage Backgroundimg = new BitmapImage();

        BitmapImage designimg1 = new BitmapImage();
        BitmapImage designimg2 = new BitmapImage();
        BitmapImage designimg3 = new BitmapImage();
        BitmapImage designimg4 = new BitmapImage();
        BitmapImage designimg5 = new BitmapImage();
        BitmapImage designimg6 = new BitmapImage();
        BitmapImage designimg7 = new BitmapImage();
        BitmapImage designimg8 = new BitmapImage();

        BitmapImage designfront = new BitmapImage();


        public SelectFrameDesign()
        {
            wpfTest.Source.Log.log.Info("템플릿 디자인 선택 페이지 진입");
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                tempdesign = 0;

                if (MainWindow.checkretake == "1" || MainWindow.checkretakenum == 1)
                {
                    BackBtn.IsEnabled = false;
                    BackBtn.Opacity = 0;
                }

                switch (TempSelect.temp)
                {
                    case 1:
                        Backgroundimg = new BitmapImage();
                        Backgroundimg.BeginInit();
                        Backgroundimg.UriSource = new Uri(MainWindow.uipath + @"\DesignSelect1.png", UriKind.RelativeOrAbsolute);
                        Backgroundimg.CacheOption = BitmapCacheOption.OnLoad;
                        Backgroundimg.EndInit();

                        designimg1 = new BitmapImage();
                        designimg1.BeginInit();
                        designimg1.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_1.png", UriKind.RelativeOrAbsolute);
                        designimg1.CacheOption = BitmapCacheOption.OnLoad;
                        designimg1.EndInit();

                        designimg2 = new BitmapImage();
                        designimg2.BeginInit();
                        designimg2.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_2.png", UriKind.RelativeOrAbsolute);
                        designimg2.CacheOption = BitmapCacheOption.OnLoad;
                        designimg2.EndInit();

                        designimg3 = new BitmapImage();
                        designimg3.BeginInit();
                        designimg3.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_3.png", UriKind.RelativeOrAbsolute);
                        designimg3.CacheOption = BitmapCacheOption.OnLoad;
                        designimg3.EndInit();

                        designimg4 = new BitmapImage();
                        designimg4.BeginInit();
                        designimg4.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_4.png", UriKind.RelativeOrAbsolute);
                        designimg4.CacheOption = BitmapCacheOption.OnLoad;
                        designimg4.EndInit();

                        designimg5 = new BitmapImage();
                        designimg5.BeginInit();
                        designimg5.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_5.png", UriKind.RelativeOrAbsolute);
                        designimg5.CacheOption = BitmapCacheOption.OnLoad;
                        designimg5.EndInit();

                        designimg6 = new BitmapImage();
                        designimg6.BeginInit();
                        designimg6.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_6.png", UriKind.RelativeOrAbsolute);
                        designimg6.CacheOption = BitmapCacheOption.OnLoad;
                        designimg6.EndInit();

                        designimg7 = new BitmapImage();
                        designimg7.BeginInit();
                        designimg7.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_7.png", UriKind.RelativeOrAbsolute);
                        designimg7.CacheOption = BitmapCacheOption.OnLoad;
                        designimg7.EndInit();

                        designimg8 = new BitmapImage();
                        designimg8.BeginInit();
                        designimg8.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_8.png", UriKind.RelativeOrAbsolute);
                        designimg8.CacheOption = BitmapCacheOption.OnLoad;
                        designimg8.EndInit();

                        designfront = new BitmapImage();
                        designfront.BeginInit();
                        designfront.UriSource = new Uri(MainWindow.uipath + @"\designfront1.png", UriKind.RelativeOrAbsolute);
                        designfront.CacheOption = BitmapCacheOption.OnLoad;
                        designfront.EndInit();
                        break;
                    case 2:
                        Backgroundimg = new BitmapImage();
                        Backgroundimg.BeginInit();
                        Backgroundimg.UriSource = new Uri(MainWindow.uipath + @"\DesignSelect2.png", UriKind.RelativeOrAbsolute);
                        Backgroundimg.CacheOption = BitmapCacheOption.OnLoad;
                        Backgroundimg.EndInit();

                        designimg1 = new BitmapImage();
                        designimg1.BeginInit();
                        designimg1.UriSource = new Uri(MainWindow.TempPath + @"\Temp2_1.png", UriKind.RelativeOrAbsolute);
                        designimg1.CacheOption = BitmapCacheOption.OnLoad;
                        designimg1.EndInit();

                        designimg2 = new BitmapImage();
                        designimg2.BeginInit();
                        designimg2.UriSource = new Uri(MainWindow.TempPath + @"\Temp2_2.png", UriKind.RelativeOrAbsolute);
                        designimg2.CacheOption = BitmapCacheOption.OnLoad;
                        designimg2.EndInit();

                        designimg3 = new BitmapImage();
                        designimg3.BeginInit();
                        designimg3.UriSource = new Uri(MainWindow.TempPath + @"\Temp2_3.png", UriKind.RelativeOrAbsolute);
                        designimg3.CacheOption = BitmapCacheOption.OnLoad;
                        designimg3.EndInit();

                        designimg4 = new BitmapImage();
                        designimg4.BeginInit();
                        designimg4.UriSource = new Uri(MainWindow.TempPath + @"\Temp2_4.png", UriKind.RelativeOrAbsolute);
                        designimg4.CacheOption = BitmapCacheOption.OnLoad;
                        designimg4.EndInit();

                        designimg5 = new BitmapImage();
                        designimg5.BeginInit();
                        designimg5.UriSource = new Uri(MainWindow.TempPath + @"\Temp2_5.png", UriKind.RelativeOrAbsolute);
                        designimg5.CacheOption = BitmapCacheOption.OnLoad;
                        designimg5.EndInit();

                        designimg6 = new BitmapImage();
                        designimg6.BeginInit();
                        designimg6.UriSource = new Uri(MainWindow.TempPath + @"\Temp2_6.png", UriKind.RelativeOrAbsolute);
                        designimg6.CacheOption = BitmapCacheOption.OnLoad;
                        designimg6.EndInit();

                        designimg7 = new BitmapImage();
                        designimg7.BeginInit();
                        designimg7.UriSource = new Uri(MainWindow.TempPath + @"\Temp2_7.png", UriKind.RelativeOrAbsolute);
                        designimg7.CacheOption = BitmapCacheOption.OnLoad;
                        designimg7.EndInit();

                        designimg8 = new BitmapImage();
                        designimg8.BeginInit();
                        designimg8.UriSource = new Uri(MainWindow.TempPath + @"\Temp2_8.png", UriKind.RelativeOrAbsolute);
                        designimg8.CacheOption = BitmapCacheOption.OnLoad;
                        designimg8.EndInit();

                        designfront = new BitmapImage();
                        designfront.BeginInit();
                        designfront.UriSource = new Uri(MainWindow.uipath + @"\designfront2.png", UriKind.RelativeOrAbsolute);
                        designfront.CacheOption = BitmapCacheOption.OnLoad;
                        designfront.EndInit();

                        Design1front.Margin = new Thickness(218 - 5, 290 - 5, 1920 - 152 - 218 - 5, 1080 - 290 - 228 - 5);
                        Design2front.Margin = new Thickness(400 - 5, 290 - 5, 1920 - 152 - 400 - 5, 1080 - 290 - 228 - 5);
                        Design3front.Margin = new Thickness(582 - 5, 290 - 5, 1920 - 152 - 582 - 5, 1080 - 290 - 228 - 5);
                        Design4front.Margin = new Thickness(764 - 5, 290 - 5, 1920 - 152 - 764 - 5, 1080 - 290 - 228 - 5);
                        Design5front.Margin = new Thickness(218 - 5, 548 - 5, 1920 - 152 - 218 - 5, 1080 - 548 - 228 - 5);
                        Design6front.Margin = new Thickness(400 - 5, 548 - 5, 1920 - 152 - 400 - 5, 1080 - 548 - 228 - 5);
                        Design7front.Margin = new Thickness(582 - 5, 548 - 5, 1920 - 152 - 582 - 5, 1080 - 548 - 228 - 5);
                        Design8front.Margin = new Thickness(764 - 5, 548 - 5, 1920 - 152 - 764 - 5, 1080 - 548 - 228 - 5);

                        Design1front.Width = 162;
                        Design1front.Height = 238;
                        Design2front.Width = 162;
                        Design2front.Height = 238;
                        Design3front.Width = 162;
                        Design3front.Height = 238;
                        Design4front.Width = 162;
                        Design4front.Height = 238;
                        Design5front.Width = 162;
                        Design5front.Height = 238;
                        Design6front.Width = 162;
                        Design6front.Height = 238;
                        Design7front.Width = 162;
                        Design7front.Height = 238;
                        Design8front.Width = 162;
                        Design8front.Height = 238;


                        PriewViewIMG.Width = 450;
                        PriewViewIMG.Height = 675;
                        PriewViewIMG.Margin = new Thickness(1200, 200, 1920 - 1200 - 450, 1080 - 200 - 675);

                        firstpicborder.Margin = new Thickness(1200 - 3, 200 - 3, 1920 - 1200 - 450 - 3, 1080 - 200 - 675 - 3);
                        break;
                    case 3:
                        Backgroundimg = new BitmapImage();
                        Backgroundimg.BeginInit();
                        Backgroundimg.UriSource = new Uri(MainWindow.uipath + @"\DesignSelect3.png", UriKind.RelativeOrAbsolute);
                        Backgroundimg.CacheOption = BitmapCacheOption.OnLoad;
                        Backgroundimg.EndInit();

                        designimg1 = new BitmapImage();
                        designimg1.BeginInit();
                        designimg1.UriSource = new Uri(MainWindow.TempPath + @"\Temp3_1.png", UriKind.RelativeOrAbsolute);
                        designimg1.CacheOption = BitmapCacheOption.OnLoad;
                        designimg1.EndInit();

                        designimg2 = new BitmapImage();
                        designimg2.BeginInit();
                        designimg2.UriSource = new Uri(MainWindow.TempPath + @"\Temp3_2.png", UriKind.RelativeOrAbsolute);
                        designimg2.CacheOption = BitmapCacheOption.OnLoad;
                        designimg2.EndInit();

                        designimg3 = new BitmapImage();
                        designimg3.BeginInit();
                        designimg3.UriSource = new Uri(MainWindow.TempPath + @"\Temp3_3.png", UriKind.RelativeOrAbsolute);
                        designimg3.CacheOption = BitmapCacheOption.OnLoad;
                        designimg3.EndInit();

                        designimg4 = new BitmapImage();
                        designimg4.BeginInit();
                        designimg4.UriSource = new Uri(MainWindow.TempPath + @"\Temp3_4.png", UriKind.RelativeOrAbsolute);
                        designimg4.CacheOption = BitmapCacheOption.OnLoad;
                        designimg4.EndInit();

                        designimg5 = new BitmapImage();
                        designimg5.BeginInit();
                        designimg5.UriSource = new Uri(MainWindow.TempPath + @"\Temp3_5.png", UriKind.RelativeOrAbsolute);
                        designimg5.CacheOption = BitmapCacheOption.OnLoad;
                        designimg5.EndInit();

                        designimg6 = new BitmapImage();
                        designimg6.BeginInit();
                        designimg6.UriSource = new Uri(MainWindow.TempPath + @"\Temp3_6.png", UriKind.RelativeOrAbsolute);
                        designimg6.CacheOption = BitmapCacheOption.OnLoad;
                        designimg6.EndInit();

                        designimg7 = new BitmapImage();
                        designimg7.BeginInit();
                        designimg7.UriSource = new Uri(MainWindow.TempPath + @"\Temp3_7.png", UriKind.RelativeOrAbsolute);
                        designimg7.CacheOption = BitmapCacheOption.OnLoad;
                        designimg7.EndInit();

                        designimg8 = new BitmapImage();
                        designimg8.BeginInit();
                        designimg8.UriSource = new Uri(MainWindow.TempPath + @"\Temp3_8.png", UriKind.RelativeOrAbsolute);
                        designimg8.CacheOption = BitmapCacheOption.OnLoad;
                        designimg8.EndInit();

                        designfront = new BitmapImage();
                        designfront.BeginInit();
                        designfront.UriSource = new Uri(MainWindow.uipath + @"\designfront3.png", UriKind.RelativeOrAbsolute);
                        designfront.CacheOption = BitmapCacheOption.OnLoad;
                        designfront.EndInit();

                        Design1front.Margin = new Thickness(220 - 5, 290 - 5, 1920 - 152 - 220 - 5, 1080 - 290 - 228 - 5);
                        Design2front.Margin = new Thickness(402 - 5, 290 - 5, 1920 - 152 - 402 - 5, 1080 - 290 - 228 - 5);
                        Design3front.Margin = new Thickness(584 - 5, 290 - 5, 1920 - 152 - 584 - 5, 1080 - 290 - 228 - 5);
                        Design4front.Margin = new Thickness(766 - 5, 290 - 5, 1920 - 152 - 766 - 5, 1080 - 290 - 228 - 5);
                        Design5front.Margin = new Thickness(220 - 5, 558 - 5, 1920 - 152 - 220 - 5, 1080 - 558 - 228 - 5);
                        Design6front.Margin = new Thickness(400 - 5, 558 - 5, 1920 - 152 - 402 - 5, 1080 - 558 - 228 - 5);
                        Design7front.Margin = new Thickness(584 - 5, 558 - 5, 1920 - 152 - 584 - 5, 1080 - 558 - 228 - 5);
                        Design8front.Margin = new Thickness(766 - 5, 558 - 5, 1920 - 152 - 766 - 5, 1080 - 558 - 228 - 5);

                        Design1front.Width = 162;
                        Design1front.Height = 238;
                        Design2front.Width = 162;
                        Design2front.Height = 238;
                        Design3front.Width = 162;
                        Design3front.Height = 238;
                        Design4front.Width = 162;
                        Design4front.Height = 238;
                        Design5front.Width = 162;
                        Design5front.Height = 238;
                        Design6front.Width = 162;
                        Design6front.Height = 238;
                        Design7front.Width = 162;
                        Design7front.Height = 238;
                        Design8front.Width = 162;
                        Design8front.Height = 238;

                        PriewViewIMG.Width = 450;
                        PriewViewIMG.Height = 675;
                        PriewViewIMG.Margin = new Thickness(1200, 200, 1920 - 1200 - 450, 1080 - 200 - 675);
                        firstpicborder.Margin = new Thickness(1200 - 3, 200 - 3, 1920 - 1200 - 450 - 3, 1080 - 200 - 675 - 3);
                        break;
                }
                BackgroundImg.Source = Backgroundimg;

                Nextbtnimgoff.BeginInit();
                Nextbtnimgoff.UriSource = new Uri(MainWindow.uipath + @"\Next_off.png", UriKind.RelativeOrAbsolute);
                Nextbtnimgoff.CacheOption = BitmapCacheOption.OnLoad;
                Nextbtnimgoff.EndInit();

                Design1front.Source = designfront;
                Design2front.Source = designfront;
                Design3front.Source = designfront;
                Design4front.Source = designfront;
                Design5front.Source = designfront;
                Design6front.Source = designfront;
                Design7front.Source = designfront;
                Design8front.Source = designfront;

                NextImg.Source = Nextbtnimgoff;

                Timer.Text = tsduration.ToString();
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += new EventHandler(Timer_Tick);
                timer.Start();
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Design1front_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                wpfTest.Source.Log.log.Info("디자인 1 선택");
                tempdesign = 1;

                Design1front.Opacity = 1;
                Design2front.Opacity = 0;
                Design3front.Opacity = 0;
                Design4front.Opacity = 0;
                Design5front.Opacity = 0;
                Design6front.Opacity = 0;
                Design7front.Opacity = 0;
                Design8front.Opacity = 0;

                PriewViewIMG.Source = designimg1;

                NextImg.Source = MainWindow.nextbtn;

                NextImg.IsEnabled = true;
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Design2front_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                wpfTest.Source.Log.log.Info("디자인 2 선택");
                tempdesign = 2;

                Design1front.Opacity = 0;
                Design2front.Opacity = 1;
                Design3front.Opacity = 0;
                Design4front.Opacity = 0;
                Design5front.Opacity = 0;
                Design6front.Opacity = 0;
                Design7front.Opacity = 0;
                Design8front.Opacity = 0;

                PriewViewIMG.Source = designimg2;

                NextImg.Source = MainWindow.nextbtn;

                NextImg.IsEnabled = true;
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Design3front_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                wpfTest.Source.Log.log.Info("디자인 3 선택");
                tempdesign = 3;

                Design1front.Opacity = 0;
                Design2front.Opacity = 0;
                Design3front.Opacity = 1;
                Design4front.Opacity = 0;
                Design5front.Opacity = 0;
                Design6front.Opacity = 0;
                Design7front.Opacity = 0;
                Design8front.Opacity = 0;

                PriewViewIMG.Source = designimg3;

                NextImg.Source = MainWindow.nextbtn;

                NextImg.IsEnabled = true;
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Design4front_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                wpfTest.Source.Log.log.Info("디자인 4 선택");
                tempdesign = 4;

                Design1front.Opacity = 0;
                Design2front.Opacity = 0;
                Design3front.Opacity = 0;
                Design4front.Opacity = 1;
                Design5front.Opacity = 0;
                Design6front.Opacity = 0;
                Design7front.Opacity = 0;
                Design8front.Opacity = 0;

                PriewViewIMG.Source = designimg4;

                NextImg.Source = MainWindow.nextbtn;

                NextImg.IsEnabled = true;
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Design5front_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                wpfTest.Source.Log.log.Info("디자인 5 선택");
                tempdesign = 5;

                Design1front.Opacity = 0;
                Design2front.Opacity = 0;
                Design3front.Opacity = 0;
                Design4front.Opacity = 0;
                Design5front.Opacity = 1;
                Design6front.Opacity = 0;
                Design7front.Opacity = 0;
                Design8front.Opacity = 0;

                PriewViewIMG.Source = designimg5;

                NextImg.Source = MainWindow.nextbtn;

                NextImg.IsEnabled = true;
            }
            catch(Exception ex)
            {                 
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Design6front_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                wpfTest.Source.Log.log.Info("디자인 6 선택");
                tempdesign = 6;

                Design1front.Opacity = 0;
                Design2front.Opacity = 0;
                Design3front.Opacity = 0;
                Design4front.Opacity = 0;
                Design5front.Opacity = 0;
                Design6front.Opacity = 1;
                Design7front.Opacity = 0;
                Design8front.Opacity = 0;

                PriewViewIMG.Source = designimg6;

                NextImg.Source = MainWindow.nextbtn;

                NextImg.IsEnabled = true;
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Design7front_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                wpfTest.Source.Log.log.Info("디자인 7 선택");
                tempdesign = 7;

                Design1front.Opacity = 0;
                Design2front.Opacity = 0;
                Design3front.Opacity = 0;
                Design4front.Opacity = 0;
                Design5front.Opacity = 0;
                Design6front.Opacity = 0;
                Design7front.Opacity = 1;
                Design8front.Opacity = 0;

                PriewViewIMG.Source = designimg7;

                NextImg.Source = MainWindow.nextbtn;

                NextImg.IsEnabled = true;
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Design8front_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                wpfTest.Source.Log.log.Info("디자인 8 선택");
                tempdesign = 8;

                Design1front.Opacity = 0;
                Design2front.Opacity = 0;
                Design3front.Opacity = 0;
                Design4front.Opacity = 0;
                Design5front.Opacity = 0;
                Design6front.Opacity = 0;
                Design7front.Opacity = 0;
                Design8front.Opacity = 1;

                PriewViewIMG.Source = designimg8;

                NextImg.Source = MainWindow.nextbtn;

                NextImg.IsEnabled = true;
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
                    Design1front.IsEnabled = false;
                    Design2front.IsEnabled = false;
                    Design3front.IsEnabled = false;
                    Design4front.IsEnabled = false;
                    Design5front.IsEnabled = false;
                    Design1front.IsEnabled = false;
                    Design7front.IsEnabled = false;
                    Design8front.IsEnabled = false;
                }

                if (tsduration == -1)
                {
                    if (tempdesign == 0)
                    {
                        Random rand;
                        rand = new Random();
                        int randdomnum = rand.Next(1, 8);
                        tempdesign = randdomnum;
                    }

                    timer.Stop();
                    Dispose();
                    NavigationService.Navigate(new Uri("View/ImgCompose.xaml", UriKind.RelativeOrAbsolute));
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

        private void Dispose()
        {
            try
            {
                wpfTest.Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                Design1front.Source = null;
                Design1front = null;
                Design2front.Source = null;
                Design2front = null;
                Design3front.Source = null;
                Design3front = null;
                Design4front.Source = null;
                Design4front = null;
                Design5front.Source = null;
                Design5front = null;
                Design6front.Source = null;
                Design6front = null;
                Design7front.Source = null;
                Design7front = null;
                Design8front.Source = null;
                Design8front = null;
                BackgroundImg.Source = null;
                BackgroundImg = null;
                NextImg.Source = null;
                NextImg = null;
                BackBtn.Source = null;
                BackBtn = null;

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

        private void NextImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                wpfTest.Source.Log.log.Info("다음 버튼 클릭");

                if (tempdesign != 0)
                {
                    timer.Stop();
                    Dispose();
                    NavigationService.Navigate(new Uri("View/ImgCompose.xaml", UriKind.RelativeOrAbsolute));
                }
                else if (tempdesign == 0)
                { }
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void BackBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                wpfTest.Source.Log.log.Info("다음 버튼 클릭");

                timer.Stop();
                Dispose();
                NavigationService.Navigate(new Uri("View/TakePic.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                wpfTest.Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }
    }
}
